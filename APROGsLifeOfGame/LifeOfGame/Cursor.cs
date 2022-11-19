using GpioHAT;
using System;

namespace LifeOfGame
{
  public class Cursor : Object
  {
    public int relx { get; protected set; } // relative to boundries
    public int rely { get; protected set; } // relative to boundries
    int boxWidth;
    int boxHeight;

    public Cursor(int x, int y, int boxWidth, int boxHeight, ConsoleColor color) : base(x, y, 1, 1, color)
    {
      this.Visible = true;
      this.boxWidth = boxWidth;
      this.boxHeight = boxHeight;

      relx = (boxWidth / 2);
      rely = (boxHeight / 2);
    }

    public void moveCursor(CursorDirection direction)
    {
      switch(direction)
      {
        case CursorDirection.LEFT:
          relx = (relx - 1 + boxWidth) % boxWidth;
          break;
        case CursorDirection.RIGHT:
          relx = (relx + 1 + boxWidth) % boxWidth;
          break;
        case CursorDirection.UP:
          rely = (rely - 1 + boxHeight) % boxHeight;
          break;
        case CursorDirection.DOWN:
          rely = (rely + 1 + boxHeight) % boxHeight;
          break;
      }
    }

    public override void draw()
    {
      if (Visible)
      {
        Console.SetCursorPosition(x + relx, y + rely);
        Util.WriteColored("ä", color);
      }
    }
  }
}