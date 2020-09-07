using PaystubJsonApp.Models.Paystubs;
using PaystubJsonApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Logic
{
    public static class FilterMethods
    {
        #region - Fields & Properties
        public static Dictionary<string, Func<PaystubCollection, Prop, PaystubModel>> PaystubFilters = 
            new Dictionary<string, Func<PaystubCollection, Prop, PaystubModel>>
        {
            { "Max", (collection, prop) => PaystubMaxFunc(collection, prop) },
            { "Min", (collection, prop) => PaystubMinFunc(collection, prop) }
        };

        public static Func<PaystubCollection, Prop, PaystubModel> PaystubMaxFunc = ( collection, prop ) =>
        {
            switch ( prop )
            {
                case Prop.Gross:
                    return collection.Paystubs.First(p => collection.Paystubs.Max(s => s.Gross) == p.Gross);
                case Prop.Net:
                    return collection.Paystubs.First(p => collection.Paystubs.Max(s => s.Net) == p.Net);
                case Prop.Hours:
                    return collection.Paystubs.First(p => collection.Paystubs.Max(s => s.Hours) == p.Hours);
                case Prop.Flat:
                    return collection.Paystubs.First(p => collection.Paystubs.Max(s => s.FlatrateHours) == p.FlatrateHours);
                default:
                    throw new ArgumentException("Somehow the prop doesnt exist...", prop.ToString());
            }
        };

        public static Func<PaystubCollection, Prop, PaystubModel> PaystubMinFunc = ( collection, prop ) =>
        {
            switch ( prop )
            {
                case Prop.Gross:
                    return collection.Paystubs.First(p => collection.Paystubs.Min(s => s.Gross) == p.Gross);
                case Prop.Net:
                    return collection.Paystubs.First(p => collection.Paystubs.Min(s => s.Net) == p.Net);
                case Prop.Hours:
                    return collection.Paystubs.First(p => collection.Paystubs.Min(s => s.Hours) == p.Hours);
                case Prop.Flat:
                    return collection.Paystubs.First(p => collection.Paystubs.Min(s => s.FlatrateHours) == p.FlatrateHours);
                default:
                    throw new ArgumentException("Somehow the prop doesnt exist...", prop.ToString());
            }
        };
        #endregion

        #region - Methods
        #endregion

        #region - Full Properties

        #endregion
    }
}
