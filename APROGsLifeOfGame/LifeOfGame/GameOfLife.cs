using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GpioHAT;

namespace LifeOfGame
{
    public class GameOfLife : GameEngine
    {
    public override int Game()
    {
      return 0;
    }

    public GameOfLife()
    {
      WriteInstructions();
    }

    protected override void WriteInstructions()
    {
      // Print Title :)
      PrintTitle();

      Util.WriteColored("[Instructions]", ConsoleColor.Black, ConsoleColor.Blue);
      Util.WriteColored(" - In Menu: ", ConsoleColor.Blue);
      Console.WriteLine("  (CENTER): Select current Menu\n" + 
                        "  (UP/DOWN): Navigate Menu");

      Util.WriteColored(" - In Game: ", ConsoleColor.Blue);
      Console.Write("  (CENTER): ");
      Util.WriteColored("Hold", ConsoleColor.Red, false);
      Console.WriteLine(" 1 second to switch between Editor & Simulate Editor mode");
      Util.WriteColored("     Click", ConsoleColor.Red, false);
      Console.WriteLine(" to toggle cell state");
      Console.WriteLine("  (UP/DOWN/LEFT/RIGHT): Navigate Grid");



    }

    private void PrintTitle()
    {
      Util.WriteColored("APROG's\n" +
      "    ______                              ____   __    _ ____\n" +
      "   / ____/___ _____ ___  ___     ____  / __/  / /   (_) __/__\n" +
      "  / / __/ __ `/ __ `__ \\/ _ \\   / __ \\/ /_   / /   / / /_/ _ \\\n" +
      " / /_/ / /_/ / / / / / /  __/  / /_/ / __/  / /___/ / __/  __/\n" +
      " \\____/\\__,_/_/ /_/ /_/\\___/   \\____/_/    /_____/_/_/  \\___/\n", ConsoleColor.Green);
    }

  }
}