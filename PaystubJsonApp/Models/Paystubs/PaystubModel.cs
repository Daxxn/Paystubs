using System;

namespace PaystubJsonApp.Models.Paystubs
{
    public class PaystubModel : ModelBase
    {
        #region - Fields & Properties
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Gross { get; set; }
        public decimal Net { get; set; }
        public double Hours { get; set; }
        public double FlatrateHours { get; set; }
        public double Rate { get; set; }
        public int Period { get; set; }
        #endregion

        #region - Constructors
        public PaystubModel( )
        {
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
        }
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties
        public int PayPeriod => StartDate == DateTime.MinValue || EndDate == DateTime.MinValue
                    ? TimeSpan.Zero.Days
                    : EndDate.Subtract(StartDate).Days;
        #endregion
    }
}