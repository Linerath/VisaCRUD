using System;
using System.Globalization;

namespace VisaCRUD.Extensions
{
    public static class DecimalExtensions
    {
        public static String ToStr(this decimal d)
        {
            if (d == 0)
                return "0";

            NumberFormatInfo format = new NumberFormatInfo
            {
                NumberDecimalDigits = 2,
                NumberDecimalSeparator = "."
            };

            return d.ToString(format);
        }
    }
}