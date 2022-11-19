using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GpioHAT;

namespace LifeOfGame
{
  public class Playground : Object
  {

    ConsoleColor color;
    public bool[,] data { get; protected set; }
    public int Generation { get; protected set; }

    public Playground(bool[,] data, int x, int y, ConsoleColor color, bool visible = true) : base(x, y, data.GetLength(0), data.GetLength(1))
    {
      this.Generation = 0;
      this.data = data;
      PopulateRandom(this.data);
      this.color = color;
      this.Visible = visible;
    }


    public void next()
    {
      Generation++;
      bool[,] next = new bool[this.width, this.height];
      for (int y = 0; y < this.height; y++)
      {
        for (int x = 0; x < this.width; x++)
        {
          int count = CountNeighbours(x, y);
          bool state = data[x, y];

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
      data = next;
    }

    static void PopulateRandom(bool[,] array)
    {
      Random rand = new Random();
      for (int y = 0; y < array.GetLength(1); y++)
      {
        for (int x = 0; x < array.GetLength(0); x++)
        {
          array[x, y] = (rand.Next(2) == 1);
        }
      }
    }

    private int CountNeighbours(int x, int y)
    {
      int sum = 0;
      //
      for (int i = -1; i < 2; i++)
      {
        for (int j = -1; j < 2; j++)
        {
          if (i == 0 && j == 0) continue;

          int col = (x + i + this.width) % this.width;
          int row = (y + j + this.height) % this.height;

          sum += (data[col, row]) ? 1 : 0;
        }
      }
      return sum;
    }

    public void cleardraw()
    {
      string str = "";
      Console.SetCursorPosition(x, y);
      for (int y = 0; y < height; y++)
      {
        str = "";
        for (int x = 0; x < width; x++)
        {
          str += "x";
        }

        Console.Write(str);
        Console.CursorTop += 1;
        Console.CursorLeft = this.x;
      }
    }

    // [DRAWING FUNCTION]
    public override void draw()
    {
      string str;
      Console.SetCursorPosition(x, y);
      for (int y = 0; y < height; y++)
      {
        str = "";
        for (int x = 0; x < width; x++)
        {
          str += ((data[x, y]) ? "ä" : " ");
        }

        Console.Write(str);
        Console.CursorTop += 1;
        Console.CursorLeft = this.x;
      }
    }
  }
}