using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DGame
{
    public static class Controller
    {
        public static Queue<Keys> PressedKeys;

        public static void InitController()
        {
            PressedKeys = new Queue<Keys>();
        }

        public static void Acting()
        {
            HandlePressedKeys();
            Game.PlayRound();
        }

        private static void HandlePressedKeys()
        {
            if (PressedKeys.Count > 0)
            {
                switch (PressedKeys.Dequeue())
                {
                    case Keys.W:
                        Game._Player.Commands.Enqueue(Command.KeyUp);
                        break;
                    case Keys.S:
                        Game._Player.Commands.Enqueue(Command.KeyDown);
                        break;
                    case Keys.A:
                        Game._Player.Commands.Enqueue(Command.KeyLeft);
                        break;
                    case Keys.D:
                        Game._Player.Commands.Enqueue(Command.KeyRight);
                        break;
                        // ESC
                }
            }
        }
    }
}
