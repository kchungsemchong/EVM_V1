using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace EVM
{
    public class Helper
    {
        public string ConvertToTitleCase(string name)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            name = textInfo.ToTitleCase(name);

            return name;
        }
    }
}