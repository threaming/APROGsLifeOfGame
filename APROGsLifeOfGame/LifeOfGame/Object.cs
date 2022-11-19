using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public abstract class Object
  {

    public ConsoleColor color { get; }
    public int x { get; set; }
    public int y { get; set; }
    protected int width;
    protected int height;
    public bool Visible { get; set; }

    protected Object(int x, int y, int width, int height, ConsoleColor color)
    {
      this.color = color;
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
    }

    public abstract void draw();
  }
}