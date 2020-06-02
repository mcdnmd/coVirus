using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _3DGame
{
    public static class Controller
    {
        public static Queue<Keys> PressedKeys;
        public static Dictionary<Command, bool> KeyPlayerPressed;
        public static Dictionary<Command, bool> KeySystemPressed;
        public static Queue<Command> ShotCommand;
        public static bool MenuOn;
        public static bool SettingsOn;
        public static bool GameOn;
        public static bool GameMenuOn;
        public static PrivateFontCollection PFC;

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
            ShotCommand = new Queue<Command>();
            PFC = new PrivateFontCollection();
            int fontLength = Properties.Resources.OCRAEXT.Length;
            byte[] fontdata = Properties.Resources.OCRAEXT;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            PFC.AddMemoryFont(data, fontLength);
        }

        public static void Acting()
        {
            HandleKeyPressed();
            Game.PlayRound();
        }

        private static void HandleKeyPressed()
        {
            while (ShotCommand.Count != 0)
                Game._Player.Commands.Enqueue(ShotCommand.Dequeue());
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
    }
}
