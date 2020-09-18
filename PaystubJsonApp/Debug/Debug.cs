using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PaystubJsonApp.Debug
{
    public class Debug
    {
        #region - Fields & Properties
        private static Debug _instance;
        private Dictionary<string, Action> ArgumentActions { get; set; }
        public bool Active { get; private set; }
        private bool ConsoleActive { get; set; }
        private bool DefaultActive { get; set; }
        public bool DefaultValues { get; private set; }
        private bool ExternalLogActive { get; set; }
        private string CurrentLogFolderName { get; set; }
        private string CurrentExecName { get; set; }
        private string LogFolderPath { get; set; }
        private string FullLogPath { get; set; }
        private Stack<LogModel> Logs { get; set; }
        #endregion

        #region - Constructors
        private Debug( ) => ArgumentActions = new Dictionary<string, Action>
            {
                { "debug",  () => Active = true },
                { "console", () => ConsoleActive = true },
                { "ext-log", InitializeLogger },
                { "default", InitializeDefault },
                { "default-values", () => DefaultValues = true }
            };
        #endregion

        #region - Methods
        public void OnStartup( string[] args )
        {
            if ( args != null )
            {
                foreach ( string arg in args )
                {
                    try
                    {
                        ArgumentActions[ arg ].Invoke();
                    }
                    catch ( Exception )
                    {
                        throw;
                    }
                }
            }
        }

        public void OnExit( int exitCode )
        {
            Post($"Application Closing - ExitCode: {exitCode}");
            if ( ExternalLogActive )
            {
                try
                {
                    using ( StreamWriter writer = new StreamWriter(FullLogPath) )
                    {
                        if ( Logs?.Count > 0 )
                        {
                            while ( Logs.Count > 0 )
                            {
                                writer.WriteLine(Logs.Pop());
                            }
                        }
                        writer.Flush();
                    }
                    Post("Log file save successful.");
                }
                catch ( Exception e )
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    Post("Closinging Logger.");
                }
            }
        }

        public void InitializeLogger( )
        {
            if ( !Active )
            { return; }
            ExternalLogActive = true;
            DateTime now = DateTime.Now;
            CurrentLogFolderName = $"log--{now:MM-dd-yy}";
            CurrentExecName = $"Instance--{Application.ResourceAssembly.GetHashCode()}-{DateTime.Now:hh-mm-ss.ffffff tt k}.log";
            LogFolderPath = Path.Combine(DebugSettings.Default.LogPath, CurrentLogFolderName);
            if ( !Directory.Exists(LogFolderPath) )
            {
                try
                {
                    Directory.CreateDirectory(LogFolderPath);
                }
                catch ( Exception e )
                {
                    Post("Error", e.Message,
                        new string[]
                        {
                        DebugSettings.Default.LogPath,
                        CurrentLogFolderName,
                        CurrentExecName,
                        LogFolderPath,
                        FullLogPath
                        }
                    );
                }
            }
            FullLogPath = Path.Combine(LogFolderPath, CurrentExecName);
            Logs = new Stack<LogModel>(DebugSettings.Default.MaxLogCount);

        }

        public void InitializeDefault( )
        {
            if ( !Active )
            { return; }
            DefaultActive = true;
            System.Diagnostics.Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            System.Diagnostics.Debug.AutoFlush = true;
        }

        public void PostEvent( object sender, EventArgs args ) => Post(new LogModel(LogType.Event, $"{sender.GetType()} - {args.GetType().Name}", null));

        public void Post( LogModel log )
        {
            if ( ExternalLogActive )
            {
                Logs.Push(log);
            }
            if ( ConsoleActive )
            {
                Console.WriteLine(log.Message);
                if ( DefaultActive )
                {
                    System.Diagnostics.Debug.WriteLine(log.Message);
                }
            }
        }

        public void Post( string logType, string message, IEnumerable<string> data ) => Post(new LogModel(( LogType )Enum.Parse(typeof(LogType), logType), message, data));

        public void Post( string logType, string message ) => Post(new LogModel(( LogType )Enum.Parse(typeof(LogType), logType), message, null));

        public void Post( string message ) => Post(new LogModel(LogType.Message, message, null));
        #endregion

        #region - Full Properties
        public static Debug Instance
        {
            get
            {
                if ( _instance is null )
                {
                    _instance = new Debug();
                }
                return _instance;
            }
        }
        #endregion
    }
}
