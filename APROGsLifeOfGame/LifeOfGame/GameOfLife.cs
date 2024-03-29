﻿using System;
using System.Collections.Generic;
using System.Device.Gpio.Drivers;
using System.Linq;
using System.Text;
using GpioHAT;

namespace LifeOfGame
{
    public class GameOfLife : GameEngine
    {
        const int playgroundHeight = 20, playgroundWidth = 20;
        static bool gameExit = false;

        static JoystickDirect joystick;   // only for Raspberry in use

        static Cursor cursor;
        static Playground playground;
        static bool startLoop = false;
        static System.Timers.Timer timer;

        enum GameStates
        {
            SIMULATION_STOP = 0,
            SIMULATION_START,
            SIMULATION_NEXT,
            SIMULATION_EXIT,
            EDITOR_ENTER,
            EDITOR_EXIT
        }

        //TODO implement dependent implementation
        static void JoystickHandler(object sender, JoystickEventArgs e)
        {
            // Button Pressed
            switch (e.Button)
            {
                case JoystickButtons.LEFT:  // move cursor to left
                    cursor.moveCursor(CursorDirection.LEFT);
                    break;
                case JoystickButtons.RIGHT: // move cursor to right
                    cursor.moveCursor(CursorDirection.RIGHT);
                    break;
                case (JoystickButtons.CENTER | JoystickButtons.DOWN):    // toggle active cell
                    playground.ToggleDot(cursor.relx, cursor.rely);
                    break;
                case JoystickButtons.UP:        // move cursor up
                    cursor.moveCursor(CursorDirection.UP);
                    break;
                case JoystickButtons.DOWN:      // move cursor up
                    cursor.moveCursor(CursorDirection.DOWN);
                    break;
                case (JoystickButtons.CENTER | JoystickButtons.UP):     // toggle simulation
                    startLoop = !startLoop;
                    if (startLoop)
                    {
                        timer.Start();
                    }
                    else
                    {
                        timer.Stop();
                    }
                    break;
                case (JoystickButtons.CENTER | JoystickButtons.LEFT):   // Escape game
                    gameExit = true;
                    Console.Clear();
                    joystick.JoystickChanged -= JoystickHandler;
                    break;
                case (JoystickButtons.CENTER | JoystickButtons.RIGHT):  // Randomized pattern
                    playground.LoadPattern(PlaygroundPattern.RANDOM);
                    break;
            }
        }

        public override int Game()
        {
            return 0;
        }

        /// <summary>
        /// Initialize a Game Of Life.
        /// </summary>
        /// <param name="platform">
        /// Descripes the device of the platform
        /// </param>
        public GameOfLife(Device platform = Device.Computer)
        {
            Platform = platform;
            if (Platform == Device.Raspberry)
            {
                joystick = (JoystickDirect)Raspberry.Instance.Joystick;
                joystick.JoystickChanged += JoystickHandler;
            }
            cursor = new Cursor(1, 2, playgroundWidth, playgroundHeight, ConsoleColor.Red);
            playground = new Playground(new bool[playgroundWidth, playgroundHeight], 1, 2, ConsoleColor.White);
        }
        private static Device Platform { get; set; }
        protected override void WriteInstructions()
        {
            // Print Title :)
            PrintTitle();
            Console.Clear();
            if (Platform == Device.Raspberry)
            {
                Util.WriteColored("[Instructions]", ConsoleColor.Black, ConsoleColor.Blue);
                Util.WriteColored(" - On Raspi-Joystick: ", ConsoleColor.DarkGreen);
                Console.WriteLine("  (CENTER) + (DOWN)    : Toggle active cell");
                Console.WriteLine("  (UP/DOWN/LEFT/RIGHT) : Navigate Grid");
                Console.WriteLine("  (CENTER) + (UP)      : Start/Stop simulation");
                Console.WriteLine("  (CENTER) + (RIGHT)   : Generate a random pattern");
                Console.Write("  (CENTER) + (LEFT)    : ");
                Util.WriteColored("Exit", ConsoleColor.Red, false);
                Console.WriteLine(" GameOfLife");
            }
            if (Platform == Device.Computer)
            {
                Util.WriteColored("[Instructions]", ConsoleColor.Black, ConsoleColor.Blue);
                Console.WriteLine("  (ENTER)              : Start/Stop simulation");
                Console.WriteLine("  (UP/DOWN/LEFT/RIGHT) : Navigate Grid");
                Console.WriteLine("  (SPACE)              : Toggle active cell");
                Console.WriteLine("  (R)                  : Generate a random pattern");
                Console.Write("  (ESC)                : ");
                Util.WriteColored("Exit", ConsoleColor.Red, false);
                Console.WriteLine(" GameOfLife");
            }
            Console.WriteLine("\nContinue with ENTER...");
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }

        private void PrintTitle()
        {
            Util.WriteColored("\n" +
            "    ______                              ____   __    _ ____\n" +
            "   / ____/___ _____ ___  ___     ____  / __/  / /   (_) __/__\n" +
            "  / / __/ __ `/ __ `__ \\/ _ \\   / __ \\/ /_   / /   / / /_/ _ \\\n" +
            " / /_/ / /_/ / / / / / /  __/  / /_/ / __/  / /___/ / __/  __/\n" +
            " \\____/\\__,_/_/ /_/ /_/\\___/   \\____/_/    /_____/_/_/  \\___/\n", ConsoleColor.Green);
        }


        /// <summary>
        /// With this method a round of *Game of Life* is initiated. You can play as long as you want.
        /// 
        /// Exit the round with *ESC*.
        /// </summary>
        public void PlayARound()
        {
            WriteInstructions();
            // [INITALIZE]
            GameStates gameState = GameStates.SIMULATION_STOP;
            gameExit = false;

            // "Game" Objects
            Border border = new Border(0, 1, playgroundWidth + 2, playgroundHeight + 2, ConsoleColor.DarkGray);
            Text title = new Text("aprog's GAME OF LIFE", 0, 0, ConsoleColor.Yellow);
            Text info = new Text("generation: ", 1, playgroundHeight + 3);


            // configure user control 
            // - joystick control & grid navigation

            // create grid
            // populate grid via templates or start with blank version

            Console.CursorVisible = false;
            Console.Clear();

            // Windows has specific commands to automatically resize the window.
            // Here it's used to set the minimum size for all the content to fit.
            int origWidth = 100;
            int origHeight = 100;
            if (System.OperatingSystem.IsWindows())
            {
                origWidth = Console.WindowWidth;
                origHeight = Console.WindowHeight;
                Console.SetWindowSize(playgroundWidth + 10, playgroundHeight + 10);
            }

            timer = new System.Timers.Timer();
            timer.Elapsed += playground.NextTimed;
            timer.Interval = 50;

            title.draw();
            border.draw();

            while (!gameExit)
            {
                playground.draw(cursor);
                //cursor.draw();
                info.draw();

                if ((Platform == Device.Computer || Platform == Device.Raspberry) && Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Enter:
                            startLoop = !startLoop;
                            if (startLoop)
                            {
                                timer.Start();
                            }
                            else
                            {
                                timer.Stop();
                            }
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
                            if (System.OperatingSystem.IsWindows()) { Console.SetWindowSize(origWidth, origHeight); }
                            Console.Clear();
                            break;
                        case ConsoleKey.C:
                            playground.LoadPattern(PlaygroundPattern.CLEAR);
                            break;
                        case ConsoleKey.R:
                            playground.LoadPattern(PlaygroundPattern.RANDOM);
                            break;
                    }
                }
                info.Value = $"Generation: {playground.Generation}";
            }
        }
    }
}