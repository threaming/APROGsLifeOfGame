using System;
using System.Collections.Generic;
using System.Device.Gpio;

namespace GpioHAT
{
  public class LedDirect : Led
  {
    private static int[] PinArray = new int[]{ 16, 20, 21 };


    private GpioController gpioControl;

    public LedDirect(GpioController gpioControl, LedColors color) : base(color)
    {
      this.gpioControl = gpioControl;

      gpioControl.OpenPin(PinArray[(int)color], PinMode.Output);

    }

    public override bool Enable
    {
      set
      {
        gpioControl.Write(PinArray[(int)Color], value);
      }
      get
      {
        return (bool)gpioControl.Read(PinArray[(int)Color]);
      }
    }
  }
}