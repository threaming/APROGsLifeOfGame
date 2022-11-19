using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GpioHAT;

namespace LifeOfGame
{
  public class Playground : Object
  {

    public bool[,] data { get; protected set; }
    public int Generation { get; protected set; }

    public Playground(bool[,] data, int x, int y, ConsoleColor color, bool visible = true) : base(x, y, data.GetLength(0), data.GetLength(1), color)
    {
      this.Generation = 0;
      this.data = data;
      ClearPattern(this.data);
      this.Visible = visible;
    }

    public void LoadPattern(PlaygroundPattern pattern)
    {
      switch(pattern)
      {
        case PlaygroundPattern.RANDOM:
          PopulateRandom(this.data);
          break;
        case PlaygroundPattern.CLEAR:
          ClearPattern(this.data);
          break;
      }
    }

    public bool GetDot(int x, int y)
    {
      return this.data[x,y];
    }

    public void ToggleDot(int x, int y)
    {
      this.data[x, y] = !this.data[x, y];
    }

    public void Next()
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

    private static void PopulateRandom(bool[,] array)
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

    private static void ClearPattern(bool[,] array)
    {
      Random rand = new Random();
      for (int y = 0; y < array.GetLength(1); y++)
      {
        for (int x = 0; x < array.GetLength(0); x++)
        {
          array[x, y] = false;
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
      string str;
      Console.SetCursorPosition(1, y);
      for (int y = 0; y < this.height; y++)
      {
        str = new string('x', this.width);

        Util.WriteColored(str, this.color);
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

        Util.WriteColored(str, this.color);
        Console.CursorLeft = this.x;
      }
    }
  }
}