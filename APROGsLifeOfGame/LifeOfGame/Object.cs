using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public abstract class Object
  {
    protected int x;
    protected int y;
    protected int width;
    protected int height;
    protected bool Visible { get; set; }

    protected Object(int x, int y, int width, int height)
    {
      this.x = x;
      this.y = y;
      this.width = width;
      this.height = height;
    }

    protected abstract void render();
  }
}