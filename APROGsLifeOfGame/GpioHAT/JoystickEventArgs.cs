using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GpioHAT
{
    public class JoystickEventArgs : EventArgs
    {
        public JoystickButtons Button { get; }
        public JoystickEventArgs(JoystickButtons buttons)
        {
            this.Button = buttons;
        }
    }
}