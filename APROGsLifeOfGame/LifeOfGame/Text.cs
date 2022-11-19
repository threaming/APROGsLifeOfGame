using GpioHAT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public class Text : Object
  {
    string _text;
    public string Value
    {
      get
      {
        return _text;
      }
      set
      {
        _text = value;
        this.width = _text.Length;
      }
    }

    public Text(string text, int x, int y, ConsoleColor color = ConsoleColor.White, bool visible = true) : base(x, y, text.Length, 1, color)
    {
      this._text = text;
      this.Visible = visible;
    }

    public override void draw()
    {
      if (Visible)
      {
        Console.SetCursorPosition(x, y);
        Util.WriteColored(_text, color);
      }
    }
  }
}