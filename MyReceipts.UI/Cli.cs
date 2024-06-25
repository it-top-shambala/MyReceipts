using System.Text;

namespace Application;


public class Cli
{
    
    public static void PrintLine(string message, ConsoleColor colorText = ConsoleColor.Green, ConsoleColor colorBackground = ConsoleColor.Black)
    {
        Console.ForegroundColor = colorText;
        Console.BackgroundColor = colorBackground;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void PrintError(string message)
    {
        PrintLine(message, ConsoleColor.Red);
    }
    
    public static string ListPrint(List<string> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in list)
        {
            sb.Append(item).Append(", ");
        }
        if (sb.Length > 2)
        {
            sb.Length -= 2; // Remove the last comma and space
        }
        return sb.ToString();
    }
}