using PaystubJsonApp.Models;
using PaystubJsonApp.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace PaystubJsonApp.ViewModels
{
    public class AddViewModel: ViewModelBase
    {
        #region - Fields & Properties
        public event EventHandler<AddPaystubsEventArgs> AddNewPaystubsEvent;
        private bool _notChanged = false;
        private ObservableCollection<PaystubModel> _newPaystubs;

        private double _payRate;
        private int _payPeriod = AppSettings.Default.DefaultPayPeriod;

        private bool _datesInOrder;
        private decimal _gross;
        private decimal _net;
        private double _hours;
        private double _flatrateHours;
        private DateTime _startDate = DateTime.Now;
        private DateTime _endDate = DateTime.Now.AddDays(AppSettings.Default.DefaultPayPeriod);

        private DateTime _autoStartDate = DateTime.Now;
        #endregion

        #region - Constructors
        public AddViewModel( )
        {
            NewPaystubs = new ObservableCollection<PaystubModel>();
        }
        #endregion

        #region - Methods
        public void HandleDataEntryKeyDownEvent( object sender, KeyEventArgs e )
        {
            if (e.Key == Key.Enter)
            {
                HandleCreateNewPaystub(sender, e);
            }
        }

        public void HandleCreateNewPaystub( object sender, EventArgs e )
        {
            var tempStartDate = DatesInOrder ? AutoStartDate : StartDate;
            if (DatesInOrder)
            {
                EndDate = AddDaysToDate(StartDate);
            }

            NewPaystubs.Add(new PaystubModel
            {
                Gross = Gross,
                Net = Net,
                Hours = Hours,
                FlatrateHours = FlatrateHours,
                Rate = PayRate,
                StartDate = tempStartDate,
                EndDate = EndDate,
                Period = PayPeriod
            });

            ClearEntryValues();
            HasChanged = true;
        }

        public void HandlePaystubDelete( Guid id )
        {
            NewPaystubs.Remove(NewPaystubs.First(stub => stub.PaystubID == id));
            HasChanged = true;
        }

        public void HandleSave( )
        {
            AddNewPaystubsEvent?.Invoke(
                this,
                new AddPaystubsEventArgs(NewPaystubs.ToArray())
            );
        }

        public void ClearEntryValues( )
        {
            Gross = 0;
            Net = 0;
            Hours = 0;
            FlatrateHours = 0;
            StartDate = EndDate.AddDays(1);
            EndDate = AddDaysToDate(StartDate);
            if (DatesInOrder)
            {
                AutoStartDate = StartDate;
            }
            else
            {
                AutoStartDate = DateTime.Now;
                StartDate = DateTime.Now;
                EndDate = AddDaysToDate(DateTime.Now);
                PayPeriod = 0;
                PayRate = 0;
            }

        }

        private DateTime AddDaysToDate( DateTime startDate )
        {
            return startDate.AddDays(PayPeriod);
        }

        private DateTime AddDaysToDate( int days )
        {
            return StartDate.AddDays(days);
        }
        #endregion

        #region - Full Properties
        public ObservableCollection<PaystubModel> NewPaystubs
        {
            get { return _newPaystubs; }
            set
            {
                _newPaystubs = value;
                NotifyOfPropertyChange(nameof(NewPaystubs));
            }
        }

        public double PayRate
        {
            get { return _payRate; }
            set
            {
                _payRate = value;
                NotifyOfPropertyChange(nameof(PayRate));
            }
        }

        public int PayPeriod
        {
            get { return _payPeriod; }
            set
            {
                _payPeriod = value;
                NotifyOfPropertyChange(nameof(PayPeriod));
            }
        }

        public bool DatesInOrder
        {
            get { return _datesInOrder; }
            set
            {
                _datesInOrder = value;
                NotifyOfPropertyChange(nameof(DatesInOrder));
                NotifyOfPropertyChange(nameof(SingleDatesVisible));
                NotifyOfPropertyChange(nameof(AutoDatesVisible));
            }
        }

        public Visibility SingleDatesVisible
        {
            get
            {
                return DatesInOrder
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        public Visibility AutoDatesVisible
        {
            get
            {
                return
                    SingleDatesVisible == Visibility.Visible
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }
        }

        public decimal Gross
        {
            get { return _gross; }
            set
            {
                _gross = value;
                NotifyOfPropertyChange(nameof(Gross));
            }
        }

        public decimal Net
        {
            get { return _net; }
            set
            {
                _net = value;
                NotifyOfPropertyChange(nameof(Net));
            }
        }

        public double Hours
        {
            get { return _hours; }
            set
            {
                _hours = value;
                NotifyOfPropertyChange(nameof(Hours));
            }
        }

        public double FlatrateHours
        {
            get { return _flatrateHours; }
            set
            {
                _flatrateHours = value;
                NotifyOfPropertyChange(nameof(FlatrateHours));
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                NotifyOfPropertyChange(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                NotifyOfPropertyChange(nameof(EndDate));
            }
        }

        public DateTime AutoStartDate
        {
            get { return _autoStartDate; }
            set
            {
                _autoStartDate = value;
                NotifyOfPropertyChange(nameof(AutoStartDate));
            }
        }

        public bool HasChanged
        {
            get { return _notChanged; }
            set
            {
                _notChanged = value;
                NotifyOfPropertyChange(nameof(HasChanged));
            }
        }
        #endregion
    }
}
