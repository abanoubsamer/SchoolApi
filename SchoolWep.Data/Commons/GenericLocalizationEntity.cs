using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Data.Commons
{
    public class GenericLocalizationEntity
    {
        public string Localize(string textAr, string textEN)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return textEN;
        
        
        }

       
    }
}
