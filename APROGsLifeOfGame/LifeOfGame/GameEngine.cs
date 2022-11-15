using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LifeOfGame
{
  public abstract class GameEngine
  {

    // Game Setup & Execution
    public void GameSequence()
    {
      string name = null;
      int score = 0;
      // Provide Instructions
      WriteInstructions();
    }
    public abstract int Game(); //

    protected abstract void WriteInstructions();
  }
}