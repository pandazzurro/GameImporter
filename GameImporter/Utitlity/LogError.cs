using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameImporter.Utitlity
{
    public static class LogError
    {
        public static void AddError(Exception ex)
        {
            Error.Add(ex.Message);
        }

        public static void AddError(string personalError)
        {
            Error.Add(personalError);
        }

        public static List<string> GetError()
        {
            return Error;
        }

        private static List<string> Error = new List<string>();
    }
}
