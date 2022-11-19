using GpioHAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public class Border : Object
  {
    ConsoleColor color;

    public Border(int x, int y, int width, int height, ConsoleColor color, bool visible = true) : base(x, y, width, height)
    {
      this.color = color;
      this.Visible = visible;
    }

    public override void draw()
    {
      if (Visible)
      {
        Console.SetCursorPosition(x, y);
        string str = "";
        Util.WriteColored("+" + String.Concat(Enumerable.Repeat("-", width - 2)) + "+\n", color);
        
        for (int y = 0; y < height - 2; y++)
        {
          Console.SetCursorPosition(this.x, y+this.y+1);
          Util.WriteColored("|", color);
          Console.SetCursorPosition(this.x + this.width - 1, y + this.y + 1);
          Util.WriteColored("|", color);
        }
        Console.SetCursorPosition(this.x, this.y + this.height - 1);
        str += ("+" + String.Concat(Enumerable.Repeat("-", width - 2)) + "+");
        Util.WriteColored(str, color);
      }
    }
  }
}