using System.Text;

namespace Shell.Common
{
    internal static class ConsoleExtensions
    {
        internal static string GetPassword()
        {
            ConsoleKeyInfo info = Console.ReadKey(true);
            StringBuilder password = new StringBuilder();

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password.Append(info.KeyChar);
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password.ToString()))
                    {
                        password.Remove(password.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                }
                info = Console.ReadKey(true);
            }

            Console.WriteLine();

            return password.ToString();
        }
    }
}
