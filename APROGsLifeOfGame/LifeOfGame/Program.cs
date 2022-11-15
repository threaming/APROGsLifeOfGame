using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using GpioHAT;
using MenuSpace;



/* APROG's
 *    ______                              ____   __    _ ____   
 *   / ____/___ _____ ___  ___     ____  / __/  / /   (_) __/__ 
 *  / / __/ __ `/ __ `__ \/ _ \   / __ \/ /_   / /   / / /_/ _ \
 * / /_/ / /_/ / / / / / /  __/  / /_/ / __/  / /___/ / __/  __/
 * \____/\__,_/_/ /_/ /_/\___/   \____/_/    /_____/_/_/  \___/ 
 *
 * [Rules of the Game of Life]
 * In the Game of Life each grid cell can have either one of two states: dead or alive.
 * The Game of Life is controlled by four simple rules which are applied to each grid cell
 * in the simulation domain:
 * 
 * - A live cell dies if it has fewer than two live neighbors.
 * - A live cell with two or three live neighbors lives on to the next generation.
 * - A live cell with more than three live neighbors dies.
 * - A dead cell will be brought back to live if it has exactly three live neighbors.
 *
 * -> Source: https://beltoforion.de/en/game_of_life/
 * -> Other sources: https://www.youtube.com/watch?v=FWSR_7kZuYg
 */


namespace LifeOfGame
{
  internal class Program
  {
    const int height = 50, width = 80;
    
    static void Main(string[] args)
    {
      // Add a menu
      Menu menu = new Menu("Top", null);
      menu.AddSub("Second", null);
      menu.AddSub("Third", null);

      // configure user control 
      // - joystick control & grid navigation

      // create grid
      // populate grid via templates or start with blank version
      bool[,] grid = Create2DArray(width, height);
      bool[,] next = Create2DArray(width, height);

      //populate first generation
      PopulateRandom(grid);
      int generation = 0;
      Console.CursorVisible = false;
      Console.WriteLine("aprog's GAME OF LIFE");

      Util.WriteColored("Press <ENTER> to start!", ConsoleColor.Yellow);
      while (true)
      {
        if (Console.KeyAvailable)
        {
          if (Console.ReadKey().Key == ConsoleKey.Enter) break;
        }
      }
      while (true)
      {
        Console.SetCursorPosition(0, 1);
        PrintArray(grid);
        Console.WriteLine($"Generation: {generation}");

        Thread.Sleep(20);
        NextGeneration(grid, next);
        generation++;
        Array.Copy(next, grid, next.Length);


        if (Console.KeyAvailable)
        {
          if (Console.ReadKey().Key == ConsoleKey.Escape)
          {
            break;
          }
        }
      }
      Console.WriteLine("Simulation stopped");

      // draw grid

      // calculate next generation
      // - calculate each cells neighbor sum and compare with rules

      // - decide on killing or creating the respective Cell


      //GameOfLife game = new GameOfLife();
      //Console.WriteLine("o");
    }

    static bool[,] NextGeneration(bool[,] current, bool[,] next)
    {
      for (int y = 0; y < current.GetLength(1); y++)
      {
        for (int x = 0; x < current.GetLength(0); x++)
        {
          int count = CountNeighbours(current, x, y);
          bool state = current[x, y];

          // 1) Any live cell with two or three live
          // neighbours survives.
          if ((state && count == 2) ||
              (state && count == 3))
            next[x, y] = true;
          // 2) Any dead cell with three live neighbours
          // becomes a live cell.
          else if (!state && count == 3)
            next[x, y] = true;
          // 3) All other live cells die in the next generation.
          // Similarly, all other dead cells stay dead.
          else
            next[x, y] = false;
        }
      }
      return next;
    }

    static int CountNeighbours(bool[,] array, int x, int y)
    {
      int sum = 0;
      int width = array.GetLength(0);
      int height = array.GetLength(1);
      //
      for (int i = -1; i < 2; i++)
      {
        for (int j = -1; j < 2; j++)
        {
          if (i == 0 && j == 0) continue;

          int col = (x + i + width) % width;
          int row = (y + j + height) % height;

          sum += (array[col, row]) ? 1 : 0;
        }
      }
      return sum;
    }

    static bool[,] PopulateRandom(bool[,] array)
    {
      Random rand = new Random();
      for (int y = 0; y < array.GetLength(1); y++)
      {
        for (int x = 0; x < array.GetLength(0); x++)
        {
          array[x, y] = (rand.Next(2) == 1);
        }
      }
      return array;
    }

    static void PrintArray(bool[,] array)
    {
      string str = "";
      str += ("+" + String.Concat(Enumerable.Repeat("-", array.GetLength(0))) + "+\n");
      for (int y = 0; y < array.GetLength(1); y++)
      {
        str += ("|");
        for (int x = 0; x < array.GetLength(0); x++)
        {
          str += ((array[x, y]) ? "ä" : " ");
        }
        str += ("|\n");
      }
      str += ("+" + String.Concat(Enumerable.Repeat("-", array.GetLength(0))) + "+");
      Console.WriteLine(str);
    }

    static bool[,] Create2DArray(int width, int height)
    {
      return new bool[width, height];
    }
  }
}