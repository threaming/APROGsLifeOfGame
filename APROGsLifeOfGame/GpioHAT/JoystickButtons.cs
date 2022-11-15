using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpioHAT
{
  // allows for multiple output 
  [Flags]
  public enum JoystickButtons : int
  {
    NONE = 0,
    LEFT = 1,
    RIGHT= 2,
    UP = 4,
    DOWN = 8,
    CENTER = 16
  }
}