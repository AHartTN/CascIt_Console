using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CascIt_Console
{
    public static class Extensions
    {
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}
