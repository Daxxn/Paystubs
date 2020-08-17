using System;

namespace PaystubJsonApp.Models
{
    public class PaystubModel
    {
        #region - Fields & Properties
        public Guid PaystubID { get; }
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
            PaystubID = Guid.NewGuid();
            StartDate = DateTime.MinValue;
            EndDate = DateTime.MinValue;
        }
        #endregion

        #region - Methods

        #endregion

        #region - Full Properties
        public int PayPeriod
        {
            get
            {
                return StartDate == DateTime.MinValue || EndDate == DateTime.MinValue
                    ? TimeSpan.Zero.Days
                    : EndDate.Subtract(StartDate).Days;
            }
        }
        #endregion
    }
}