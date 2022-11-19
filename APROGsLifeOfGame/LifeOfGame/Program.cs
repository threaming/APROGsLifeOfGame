
using System;
using System.Linq;
using System.Threading;
using GpioHAT;
using MenuSpace;
using System.Device.Gpio;



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
    const int height = 20, width = 20;
    static bool gameExit = false;

    static JoystickDirect joystick = (JoystickDirect)Raspberry.Instance.Joystick;
    static Cursor cursor = new Cursor(1, 2, width, height, ConsoleColor.Red);
    static Playground playground = new Playground(new bool[width, height], 1, 2, ConsoleColor.White);
    
    enum GameStates
	  {
      SIMULATION_STOP = 0,
      SIMULATION_START,
      SIMULATION_NEXT,
      SIMULATION_EXIT,
      EDITOR_ENTER,
      EDITOR_EXIT
	  }

    static void InputHandler(object sender, PinValueChangedEventArgs eventArgs)
    {
      // Button Pressed
      switch(joystick.State)
      {
        case JoystickButtons.LEFT:
          cursor.moveCursor(CursorDirection.LEFT);
          break;
        case JoystickButtons.RIGHT:
          cursor.moveCursor(CursorDirection.RIGHT);
          break;
        case JoystickButtons.CENTER:
          playground.ToggleDot(cursor.relx, cursor.rely);
          break;
        case JoystickButtons.UP:
          cursor.moveCursor(CursorDirection.UP);
          break;
        case JoystickButtons.DOWN:
          cursor.moveCursor(CursorDirection.DOWN);
          break;
      }
    }

    static void Main(string[] args)
    {
      Util.WaitForDebugger();

      // [INITALIZE]
      GameStates gameState = GameStates.SIMULATION_STOP;


      // "Game" Objects
      Border border = new Border(0, 1, width+2, height+2, ConsoleColor.DarkGray);
      Text title = new Text("aprog's GAME OF LIFE", 0, 0, ConsoleColor.Yellow);
      Text info = new Text("generation: ",1,height + 3);
      

      joystick.AttachEvent(PinEventTypes.Falling, InputHandler);


      // Add a menu
      Menu menu = new Menu("Top", null);
      menu.AddSub("Second", null);
      menu.AddSub("Third", null);

      // configure user control 
      // - joystick control & grid navigation

      // create grid
      // populate grid via templates or start with blank version



      Console.CursorVisible = false;
      Console.Clear();
      if(System.OperatingSystem.IsWindows())
      {
        Console.SetWindowSize(width + 10, height + 10);
      }

      title.draw();
      border.draw();
      bool startLoop = false;
      while (!gameExit)
      {
        playground.draw();
        cursor.draw();
        info.draw();
        Thread.Sleep(100);

        if (startLoop)
        {
          playground.Next();
        }

        if(Console.KeyAvailable)
        {
          if(Console.ReadKey(true).Key == ConsoleKey.Enter)
          {
            startLoop = !startLoop;
          }
        }
        info.Value = $"Generation: {playground.Generation}";
      }
      playground.cleardraw();


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
      (int curx, int cury) = Console.GetCursorPosition();
      for (int y = 0; y < array.GetLength(1); y++)
      {
        string str = "";
        for (int x = 0; x < array.GetLength(0); x++)
        {
          str += ((array[x, y]) ? "ä" : " ");
        }
        Console.Write(str);
        Console.CursorTop += 1;
        Console.CursorLeft = curx;

      }
    }

    static void PrintBorder(bool[,] array)
    {
      (int curx, int cury) = Console.GetCursorPosition();
      string str = "";
      str += ("+" + String.Concat(Enumerable.Repeat("-", array.GetLength(0))) + "+\n");
      for (int y = 0; y < array.GetLength(1); y++)
      {
        str += ("|" + String.Concat(Enumerable.Repeat(" ", array.GetLength(0))) + "|\n");
      }
      str += ("+" + String.Concat(Enumerable.Repeat("-", array.GetLength(0))) + "+");
      Util.WriteColored(str, ConsoleColor.DarkGray);
      Console.SetCursorPosition(curx, cury);
    }

    static bool[,] Create2DArray(int width, int height)
    {
      return new bool[width, height];
    }
  }
}