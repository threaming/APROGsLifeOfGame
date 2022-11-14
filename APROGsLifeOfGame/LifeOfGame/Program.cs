using System;



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
 * -> Source (https://beltoforion.de/en/game_of_life/)
 */


namespace LifeOfGame
{
  internal class Program
  {
    static void Main(string[] args)
    {

      Console.WriteLine("Hello, World!");
    }
  }
}