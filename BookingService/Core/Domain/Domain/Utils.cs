using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (email == "") return false;
            return true;
        }
    }
}
