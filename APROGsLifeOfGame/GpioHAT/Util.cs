using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpioHAT
{
  public class Util
  {
    public static void WriteColored(string text, ConsoleColor foreground, bool newLine = true)
    {
      ConsoleColor colorFg = Console.ForegroundColor;
      Console.ForegroundColor = foreground;
      Console.Write(text + ((newLine) ? "\n" : ""));
      Console.ForegroundColor = colorFg;
    }

    public static void WriteColored(string text, ConsoleColor foreground, ConsoleColor background, bool newLine = true)
    {
      ConsoleColor colorFg = Console.ForegroundColor;
      ConsoleColor colorBg = Console.BackgroundColor;
      Console.ForegroundColor = foreground;
      Console.BackgroundColor = background;
      Console.Write(text + ((newLine) ? "\n" : ""));
      Console.ForegroundColor = colorFg;
      Console.BackgroundColor = colorBg;

    }

    public static bool WaitForDebugger()
    {
      string[] args = Environment.GetCommandLineArgs();
      if (args.Contains("--debug"))
      {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Waiting for Debugger or press <Enter> to continue");
        Console.ForegroundColor = color;

        while (true)
        {
          if (Console.KeyAvailable)
          {
            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
              color = Console.ForegroundColor;
              Console.ForegroundColor = ConsoleColor.Red;
              Console.WriteLine("<Enter> pressed, continuing without debugger!");
              Console.ForegroundColor = color;
              break;
            }
          }
          if (System.Diagnostics.Debugger.IsAttached)
          {
            color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Debugger connected!");
            Console.ForegroundColor = color;
            return true;
          }
        }
      }
      return false;
    }
  }
}