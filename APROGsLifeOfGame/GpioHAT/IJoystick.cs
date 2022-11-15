using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Gpio;
using System.Linq;
using System.Text;

namespace GpioHAT
{
  public interface IJoystick
  {
    public JoystickButtons State {get;}
  }
}