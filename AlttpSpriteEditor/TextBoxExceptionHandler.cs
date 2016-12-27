using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlttpSpriteEditor
{
    public class TextBoxExceptionHandler
    {
        public static int? CheckNumericRange(string s, int max)
        {
            int? s2 = 0;
            try
            {
                s2 = Convert.ToInt32(s);
            }
            catch (FormatException)
            {
                s = "";
                s2 = null;
            }

            if (s2 > max)
            {
                s2 = max;
            }
            return s2;
        }
    }
}
