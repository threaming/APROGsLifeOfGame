//#define USE_PI_CONTROLS
#define USE_COMPUTER_CONTROLS

using System;
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
    const int playgroundHeight = 40, playgroundWidth = 40;
    static bool gameExit = false;

#if USE_PI_CONTROLS
    static JoystickDirect joystick = (JoystickDirect)Raspberry.Instance.Joystick;
#endif

    static Cursor cursor = new Cursor(1, 2, playgroundWidth, playgroundHeight, ConsoleColor.Red);
    static Playground playground = new Playground(new bool[playgroundWidth, playgroundHeight], 1, 2, ConsoleColor.White);
    
    enum GameStates
	  {
      SIMULATION_STOP = 0,
      SIMULATION_START,
      SIMULATION_NEXT,
      SIMULATION_EXIT,
      EDITOR_ENTER,
      EDITOR_EXIT
	  }

#if USE_PI_CONTROLS
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
#endif
    static void Main(string[] args)
    {
      Util.WaitForDebugger();

      // [INITALIZE]
      GameStates gameState = GameStates.SIMULATION_STOP;


      // "Game" Objects
      Border border = new Border(0, 1, playgroundWidth+2, playgroundHeight+2, ConsoleColor.DarkGray);
      Text title = new Text("aprog's GAME OF LIFE", 0, 0, ConsoleColor.Yellow);
      Text info = new Text("generation: ",1,playgroundHeight + 3);

#if USE_PI_CONTROLS
      joystick.AttachEvent(PinEventTypes.Falling, InputHandler);
#endif

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

      // Windows has specific commands to automatically resize the window.
      // Here it's used to set the minimum size for all the content to fit.
      if(System.OperatingSystem.IsWindows())
      {
        Console.SetWindowSize(playgroundWidth + 10, playgroundHeight + 10);
      }

      title.draw();
      border.draw();
      bool startLoop = false;
      while (!gameExit)
      {
        playground.draw();
        cursor.draw();
        info.draw();

        if (startLoop)
        {
          playground.Next();
        }

#if USE_COMPUTER_CONTROLS
        if(Console.KeyAvailable)
        {
          switch(Console.ReadKey(true).Key)
          {
            case ConsoleKey.Enter:
              startLoop = !startLoop;
              break;
            case ConsoleKey.UpArrow:
              cursor.moveCursor(CursorDirection.UP);
              break;
            case ConsoleKey.DownArrow:
              cursor.moveCursor(CursorDirection.DOWN);
              break;
            case ConsoleKey.LeftArrow:
              cursor.moveCursor(CursorDirection.LEFT);
              break;
            case ConsoleKey.RightArrow:
              cursor.moveCursor(CursorDirection.RIGHT);
              break;
            case ConsoleKey.Spacebar:
              playground.ToggleDot(cursor.relx, cursor.rely);
              break;
            case ConsoleKey.Escape:
              gameExit = true;
              break;
            case ConsoleKey.C:
              playground.LoadPattern(PlaygroundPattern.CLEAR);
              break;
            case ConsoleKey.R:
              playground.LoadPattern(PlaygroundPattern.RANDOM);
              break;
          }
        }
#endif
        info.Value = $"Generation: {playground.Generation}";
      }
      playground.cleardraw();
      while (true) ;


      //GameOfLife game = new GameOfLife();
      //Console.WriteLine("o");
    }
  }
}