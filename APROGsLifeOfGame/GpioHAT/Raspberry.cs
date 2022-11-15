using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;

namespace GpioHAT
{
  public class Raspberry
  {
    // Instance Handling to only allow one instance of 'Raspberry'
    private static Raspberry instance = new Raspberry();

    public static Raspberry Instance { get { return instance; } }

    public IJoystick Joystick { get; }
    private Dictionary<LedColors, ILed> leds = new Dictionary<LedColors, ILed>();

    private GpioController GpioCtrl { get; }
    
    private Raspberry()
    {

      bool direct = true;
      GpioCtrl = new GpioController();
      if (direct)
      {
        leds.Add(LedColors.RED, new LedDirect(GpioCtrl, LedColors.RED));
        leds.Add(LedColors.YELLOW, new LedDirect(GpioCtrl, LedColors.YELLOW));
        leds.Add(LedColors.GREEN, new LedDirect(GpioCtrl, LedColors.GREEN));
        Joystick = new JoystickDirect(GpioCtrl);
      }
    }

    public ILed this[LedColors color]
    {
      get { return leds[color]; }
    }

    public ILed getLED(LedColors color)
    {
      return leds[color];
    }
  }
}