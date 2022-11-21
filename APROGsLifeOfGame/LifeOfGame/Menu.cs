using GpioHAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public class Menu
  {
    public Menu()
    {
      // Logo
      Util.WriteColored(
      "\n@@@       @@@  @@@@@@@@  @@@@@@@@   @@@@@@   @@@@@@@@   @@@@@@@@   @@@@@@   @@@@@@@@@@   @@@@@@@@" +
      "\n@@@       @@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@  @@@@@@@@@  @@@@@@@@  @@@@@@@@@@@  @@@@@@@@" +
      "\n@@!       @@!  @@!       @@!       @@!  @@@  @@!       !@@        @@!  @@@  @@! @@! @@!  @@!" +
      "\n!@!       !@!  !@!       !@!       !@!  @!@  !@!       !@!        !@!  @!@  !@! !@! !@!  !@!" +
      "\n@!!       !!@  @!!!:!    @!!!:!    @!@  !@!  @!!!:!    !@! @!@!@  @!@!@!@!  @!! !!@ @!@  @!!!:!" +
      "\n!!!       !!!  !!!!!:    !!!!!:    !@!  !!!  !!!!!:    !!! !!@!!  !!!@!!!!  !@!   ! !@!  !!!!!:" +
      "\n!!:       !!:  !!:       !!:       !!:  !!!  !!:       :!!   !!:  !!:  !!!  !!:     !!:  !!:" +
      "\n :!:      :!:  :!:       :!:       :!:  !:!  :!:       :!:   !::  :!:  !:!  :!:     :!:  :!: " +
      "\n :: ::::   ::   ::        :: ::::  ::::: ::   ::        ::: ::::  ::   :::  :::     ::    :: :::: " +
      "\n: :: : :  :     :        : :: ::    : :  :    :         :: :: :    :   : :   :      :    : :: ::", ConsoleColor.Red, false);
      // Credit
      Console.WriteLine("\n ___   _           _   ___   ____  _         _      ___   _          ___   ___  _____ ____");
      Console.WriteLine("| |_) \\ \\_/       | | / / \\ | |_  | |       \\ \\  / / / \\ | |\\ |     | |_) / / \\  | |   / /");
      Console.WriteLine("|_|_)  |_|      \\_|_| \\_\\_/ |_|__ |_|__      \\_\\/  \\_\\_/ |_| \\|     |_| \\ \\_\\_/  |_|  /_/_");
      Console.WriteLine("  _         __    _      ___   ___   ____   __    __       _      _   _      __ ");
      Console.WriteLine("_|_)       / /\\  | |\\ | | | \\ | |_) | |_   / /\\  ( (`     | |\\/| | | | |\\ | / /`");
      Console.WriteLine("(|__7     /_/--\\ |_| \\| |_|_/ |_| \\ |_|__ /_/--\\ _)_)     |_|  | |_| |_| \\| \\_\\_/");
    }

    public void ChooseGame()
    {
      Console.Write("\nTo play ");
      Util.WriteColored("Game of Life", ConsoleColor.Green, false);
      Console.WriteLine(" press ENTER...");
    }

    public void Escape()
    {
      Console.Write("\nTo exit ");
      Util.WriteColored("Life of Game", ConsoleColor.Red, false);
      Console.WriteLine(" press ESC...");
    }
  }
}