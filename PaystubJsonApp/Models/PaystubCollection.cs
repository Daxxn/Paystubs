using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PaystubJsonApp.Models
{
    public class PaystubCollection
    {
        #region - Fields & Properties
        public ObservableCollection<PaystubModel> Paystubs { get; set; }
        public TimeSpan PayDurration { get; set; }
        #endregion

        #region - Constructors
        public PaystubCollection( )
        {
            Paystubs = new ObservableCollection<PaystubModel>();
        }

        public PaystubCollection( int days )
        {
            PayDurration = new TimeSpan(days, 0, 0, 0);
            Paystubs = new ObservableCollection<PaystubModel>();
        }
        #endregion

        #region - Methods
        public decimal GetAverage( string prop )
        {
            if (Paystubs is null || Paystubs.Count == 0)
            {
                return 0;
            }

            if (prop == "gross")
            {
                return Paystubs.Average(stub => stub.Gross);
            }
            else if (prop == "net")
            {
                return Paystubs.Average(stub => stub.Net);
            }
            else if (prop == "hours")
            {
                return (decimal)Paystubs.Average(stub => stub.Hours);
            }
            else if (prop == "flatrate")
            {
                return (decimal)Paystubs.Average(stub => stub.FlatrateHours);
            }
            else
            {
                throw new ArgumentException("Property given doesnt exist.");
            }
        }

        public override string ToString( )
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Paystubs:");
            builder.AppendLine($"Average Gross: {GetAverage("gross")}");
            builder.AppendLine($"Average Net:  {GetAverage("net")}");
            builder.AppendLine($"Average Hours:  {GetAverage("hours")}");
            builder.AppendLine($"Average Flat Rate hours:  {GetAverage("flatrate")}");
            return builder.ToString();
        }
        #endregion

        #region - Full Properties
        public decimal AverageGross
        {
            get
            {
                return Paystubs is null || Paystubs.Count == 0 ? 0 : Paystubs.Average(stub => stub.Gross);
            }
        }
        #endregion
    }
}
