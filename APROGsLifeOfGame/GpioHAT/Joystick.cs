using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GpioHAT
{
    public abstract class Joystick : IJoystick
    {
        public event EventHandler<JoystickEventArgs> JoystickChanged;
        abstract public JoystickButtons State { get; }  // implementation is dependent on Hardware
        private JoystickButtons oldState = JoystickButtons.UP | JoystickButtons.DOWN;
        
        // Event
        public void Update()
        {
            while (true)
            {
                JoystickButtons newState = State;

                if(oldState != newState)    // a change on the joystick
                {
                    // Event fire
                    JoystickChanged?.Invoke(this, new JoystickEventArgs(newState));
                    oldState = newState;
                }
                Thread.Sleep(100);  // primitiv debounce
            }
        }
    }
}