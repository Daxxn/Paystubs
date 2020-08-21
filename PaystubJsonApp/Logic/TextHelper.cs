using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Logic
{
    public static class TextHelper
    {
        #region - Fields & Properties

        #endregion

        #region - Methods
        public static char DiscardLetters( char input )
        {
            return Char.IsDigit(input) ? input : '0';
        }
        #endregion

        #region - Full Properties

        #endregion
    }
}
