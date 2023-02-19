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
        internal static void LogMinimoOMaximoInvalido(int? minimun, int? maximun)
        {
            Console.WriteLine($"El número debe ser{(minimun != null ? $" mayor a o igual a {minimun}" : "")}{(maximun != null ? $"{(minimun != null ? " y " : " ")}menor o igual a {maximun}" : "")}.");
        }

        internal static bool EsValidoRango(int? minimun, int? maximun, int? number)
        {
            return !((maximun != null && number > maximun) || (minimun != null && number < minimun));
        }
    }
}
