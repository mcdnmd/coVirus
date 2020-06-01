using System.Collections.Generic;
using System.Windows.Forms;

namespace _3DGame
{
    public static class Controller
    {
        public static Queue<Keys> PressedKeys;
        public static Dictionary<Command, bool> KeyPlayerPressed;
        public static Dictionary<Command, bool> KeySystemPressed;
        public static Queue<string> MouseMovements;
        public static bool MenuOn;
        public static bool SettingsOn;
        public static bool GameOn;
        public static bool GameMenuOn;

        public static void InitController()
        {
            PressedKeys = new Queue<Keys>();
            KeyPlayerPressed = new Dictionary<Command, bool>();
            KeyPlayerPressed[Command.KeyUp] = false;
            KeyPlayerPressed[Command.KeyDown] = false;
            KeyPlayerPressed[Command.KeyRight] = false;
            KeyPlayerPressed[Command.KeyLeft] = false;
            KeySystemPressed = new Dictionary<Command, bool>();
            KeySystemPressed[Command.Esc] = false;
            MouseMovements = new Queue<string>();
        }

        public static void Acting()
        {
            HandleKeyPressed();
            Game.PlayRound();
        }

        private static void HandleKeyPressed()
        {
            foreach (var command in KeyPlayerPressed)
                if (command.Value)
                    Game._Player.Commands.Enqueue(command.Key);
            foreach (var syscomand in KeySystemPressed)
                if (syscomand.Value)
                    switch (syscomand.Key)
                    {
                        case Command.Esc:
                            if (GameOn)
                                GameMenuOn = true;
                            break;
                    }
        }

        private static void HandlePressedKeys()
        {
            if (PressedKeys.Count > 0)
            {
                while(PressedKeys.Count > 0)
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
                        case Keys.Escape:
                            MenuOn = true;
                            break;
                            // ESC

                    }
            }
        }
    }
}
