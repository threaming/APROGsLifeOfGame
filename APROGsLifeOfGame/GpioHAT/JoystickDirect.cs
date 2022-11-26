using System;
using System.Collections.Generic;
using System.Device.Gpio;
using System.Linq;
using System.Text;
using System.Threading;

namespace GpioHAT
{
  public class JoystickDirect : Joystick
  {
    private GpioController gpioControl;

    //private static int[] PinArray = { 6, 5, 19, 13, 26 };
    private Dictionary<JoystickButtons, int> pins = new Dictionary<JoystickButtons, int>() {
      { JoystickButtons.LEFT, 6 },
      { JoystickButtons.RIGHT, 5 },
      { JoystickButtons.UP, 19 },
      { JoystickButtons.DOWN, 13 },
      { JoystickButtons.CENTER, 26 }
    };

    public override JoystickButtons State
    {
      get {
        JoystickButtons state = JoystickButtons.NONE;
        foreach (JoystickButtons button in pins.Keys)
        { 
          state |= (!(bool)gpioControl.Read(pins[button])) ? button : JoystickButtons.NONE;
        }
        return state;
      }
    }

    public JoystickDirect(GpioController gpioControl)
    {
      this.gpioControl = gpioControl;

      // open all the required pins
      foreach (int pin in pins.Values)
      {
        this.gpioControl.OpenPin(pin, PinMode.InputPullUp);
      }

            // Open thread for input handling
            Thread tJoy = new Thread(Update);
            tJoy.IsBackground = true;
            tJoy.Start();
    }

    public void AttachEvent(PinEventTypes eventType, PinChangeEventHandler callback)
    {
      foreach (int pinNumber in pins.Values)
      {
        this.gpioControl.RegisterCallbackForPinValueChangedEvent(pinNumber, eventType, callback);
      }
    }
  }
}