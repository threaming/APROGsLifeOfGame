using System;
using System.Device.Gpio;

namespace GpioHAT
{
  public interface ILed
  {
    public LedColors Color { get; }

    public bool Enable { get; set; }

    public void Toggle();
  }
}