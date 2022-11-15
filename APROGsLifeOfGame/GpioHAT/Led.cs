using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpioHAT
{
  public abstract class Led : ILed
  {
    public Led(LedColors color)
    {
      Color = color;
    }

    public abstract bool Enable { get; set; }
    public LedColors Color { get; } // this is an implementation

    public void Toggle()
    {
      Enable = !Enable;
    }

    public override string ToString()
    {
      return $"LED {Color} is {Enable}";
    }
  }
}