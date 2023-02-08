
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shell.Common
{
    internal class GetValidData
    {
        static bool TryParse<T>(string input, out T result) where T : struct
        {
            Type type = typeof(T);

            try
            {
                result = (T)Convert.ChangeType(input, type);
                return true;
            }
            catch (FormatException)
            {
                result = default(T);
                return false;
            }
        }

        internal static T GetValidNumber<T>(string? userInput) where T : struct 
        {
            T number;

            while (userInput == null || !TryParse(userInput, out number))
            {
                Console.WriteLine("Por favor ingrese un número válido");
            }

            return number;
        }
    }
}
