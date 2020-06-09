using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _3DGame
{
    public partial class Form1 : Form
    {
        private int MaxFPS = 70;
        private Timer timer = new Timer();
        private Stopwatch stopwatch;
        private SoundPlayer BackMusic;

        public Form1()
        {
            InitializeComponent();
            InitStaticComponents();
            ColdStartLoad();
            timer.Interval = 1000 / MaxFPS;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        private void ColdStartLoad()
        {
            ClientSize = new Size(320, 240);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Controller.MenuOn = true;
            Controller.SettingsOn = false;
            Controller.GameOn = false;
            Controller.GameMenuOn = false;
            stopwatch = new Stopwatch();
            Health.BackColor = Color.FromArgb(171, 0, 9);
            Health.Size = new Size((int)(Width * 0.2), (int)(Height * 0.2));
            Health.Font = new Font(Controller.PFC.Families[0], 24);
            Ammo.BackColor = Color.FromArgb(171, 0, 9);
            Ammo.Size = new Size((int)(Width * 0.2), (int)(Height * 0.2));
            Ammo.Font = new Font(Controller.PFC.Families[0], 24);
            BackMusic = new SoundPlayer(Properties.Resources.backmusicd);
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (!Controller.MenuOn && !Controller.GameMenuOn)
            {
                Controller.Acting();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Controller.MenuOn)
                Menu();
            else if (Controller.SettingsOn)
                Settings();
            else if (Controller.GameMenuOn)
                GameMenu();
            else
            {
                stopwatch.Restart();
                Core.ParallelScreenRendering(e);
                EnemyCast.Cast();
                //ScreenRender.DrawWeapon();
                ScreenRender.DrawBuffer(e);
                ScreenRender.DrawWeapon();
                stopwatch.Stop();
                FPS.Text = "FPS: " + (int)(1000 / stopwatch.Elapsed.TotalMilliseconds);
                Health.Text = Game._Player.Health.ToString();
                if (!(Game._Player.Weapon is null))
                    Ammo.Text = Game._Player.Weapon.Ammo.ToString();

            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                Controller.KeyPlayerPressed[Command.KeyUp] = true;
            if (e.KeyCode == Keys.S)
                Controller.KeyPlayerPressed[Command.KeyDown] = true;
            if (e.KeyCode == Keys.A)
                Controller.KeyPlayerPressed[Command.KeyLeft] = true;
            if (e.KeyCode == Keys.D)
                Controller.KeyPlayerPressed[Command.KeyRight] = true;
            if (e.KeyCode == Keys.Space)
                Controller.ShotCommand.Enqueue(Command.Shot);
            if (e.KeyCode == Keys.Escape)
                Controller.KeySystemPressed[Command.Esc] = true;

        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
                Controller.KeyPlayerPressed[Command.KeyUp] = false;
            if (e.KeyCode == Keys.S)
                Controller.KeyPlayerPressed[Command.KeyDown] = false;
            if (e.KeyCode == Keys.A)
                Controller.KeyPlayerPressed[Command.KeyLeft] = false;
            if (e.KeyCode == Keys.D)
                Controller.KeyPlayerPressed[Command.KeyRight] = false;
            if (e.KeyCode == Keys.Escape)
                Controller.KeySystemPressed[Command.Esc] = false;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitNonStaticComponents();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            return;
        }

        private void InitNonStaticComponents()
        {
            Core.InitCore(Width, Height);
        }

        private void InitStaticComponents()
        {
            Map.CreateMap(new Level_2());
            Game.InitGame();
            Controller.InitController();
        }

        public new void Menu()
        {
            FPS.Visible = false;
            Health.Visible = false;
            Ammo.Visible = false;
            var buttonColor = Color.FromArgb(171, 0, 9);
            BackColor = Color.FromArgb(236, 235, 233);

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var pictureBox = new PictureBox();
            pictureBox.Image = new Bitmap(Properties.Resources.logo);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Fill;

            var newGame = CreateButton(
                  "NEW GAME",
                  new Size(50, 25),
                  new Point(100, 100),
                  new Font(Controller.PFC.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  buttonColor);
            newGame.Dock = DockStyle.Fill;

            var settings = CreateButton(
                  "SETTINGS",
                  new Size(100, 50),
                  new Point(100, 100),
                  new Font(Controller.PFC.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  buttonColor);
            settings.Dock = DockStyle.Fill;

            var exit = CreateButton(
                "EXIT",
                new Size(100, 50),
                new Point(100, 25),
                new Font(Controller.PFC.Families[0], 14),
                Color.FromArgb(0, 0, 0),
                buttonColor);
            exit.Dock = DockStyle.Fill;

            table.Controls.Add(pictureBox, 0, 0);
            table.Controls.Add(newGame, 0, 1);
            table.Controls.Add(settings, 0, 2);
            table.Controls.Add(exit, 0, 3);

            table.Dock = DockStyle.Fill;

            Controls.Add(table);

            newGame.Click += NewGameOnClick;
            settings.Click += settingsOnClick;
            exit.Click += ExitOnClick;
        }

        private void settingsOnClick(object sender, EventArgs e)
        {
            Controller.MenuOn = false;
            Controller.SettingsOn = true;
            Controls.Clear();
            Focus();
            Invalidate();
        }

        private void ExitOnClick(object sender, EventArgs e)
        {
            Close();
        }

        private void NewGameOnClick(object sender, EventArgs e)
        {
            InitStaticComponents();
            Controller.MenuOn = false;
            Controller.GameOn = true;
            FPS.Visible = true;
            Health.Visible = true;
            Ammo.Visible = true;
            Controls.Clear();
            Controls.Add(FPS);
            Controls.Add(Health);
            Controls.Add(Ammo);
            //BackMusic.Play();
            Focus();
            Invalidate();
        }

        private void Settings()
        {
            var buttonColor = Color.FromArgb(171, 0, 9);
            BackColor = Color.FromArgb(236, 235, 233);

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var pictureBox = new PictureBox();
            pictureBox.Image = new Bitmap(Properties.Resources.logo);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Fill;

            var text = CreateLabel(
                  "Screen Resolution",
                  new Size(50, 20),
                  new Point(100, 100),
                  new Font(Controller.PFC.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  Color.Empty);
            text.Dock = DockStyle.Fill;

            var resolutionMedium = CreateButton(
                  "320x240 Medium (Recommended)",
                  new Size(50, 50),
                  new Point(100, 100),
                  new Font(Controller.PFC.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  buttonColor);
            resolutionMedium.Dock = DockStyle.Fill;

            var resolutionHigh = CreateButton(
                "400x300 High",
                new Size(50, 50),
                new Point(100, 25),
                new Font(Controller.PFC.Families[0], 14),
                Color.FromArgb(0, 0, 0),
                buttonColor);
            resolutionHigh.Dock = DockStyle.Fill;

            var resolutionExtra = CreateButton(
                "800x600 Extra (RTX only)",
                new Size(50, 50),
                new Point(100, 25),
                new Font(Controller.PFC.Families[0], 14),
                Color.FromArgb(0, 0, 0),
                buttonColor);
            resolutionExtra.Dock = DockStyle.Fill;

            table.Controls.Add(pictureBox, 0, 0);
            table.Controls.Add(text, 0, 1);
            table.Controls.Add(resolutionMedium, 0, 2);
            table.Controls.Add(resolutionHigh, 0, 3);
            table.Controls.Add(resolutionExtra, 0, 4);

            table.Dock = DockStyle.Fill;

            Controls.Add(table);

            resolutionMedium.Click += resolutionMediumOnClick;
            resolutionHigh.Click += resolutionHighOnClick;
            resolutionExtra.Click += resolutionExtraOnClick;
        }

        private void resolutionMediumOnClick(object sender, EventArgs e)
        {
            ClientSize = new Size(320, 240);
            Controller.SettingsOn = false;
            if (Controller.GameOn)
                Controller.GameMenuOn = true;
            else
                Controller.MenuOn = true;
            Controls.Clear();
            Focus();
            Invalidate();
        }

        private void resolutionHighOnClick(object sender, EventArgs e)
        {
            ClientSize = new Size(400, 300);
            Controller.SettingsOn = false;
            if (Controller.GameOn)
                Controller.GameMenuOn = true;
            else
                Controller.MenuOn = true;
            Controls.Clear();
            Focus();
            Invalidate();
        }

        private void resolutionExtraOnClick(object sender, EventArgs e)
        {
            ClientSize = new Size(800, 600);
            Controller.SettingsOn = false;
            if (Controller.GameOn)
                Controller.GameMenuOn = true;
            else
                Controller.MenuOn = true;
            Controls.Clear();
            Focus();
            Invalidate();
        }

        private Button CreateButton(string text, Size size, Point location, Font font, Color foreColor, Color backColor)
        {
            var button = new Button();
            button.Text = text;
            button.Location = location;
            button.Size = size;
            button.Font = font;
            button.ForeColor = foreColor;
            button.BackColor = backColor;
            return button;
        }

        private Label CreateLabel(string text, Size size, Point location, Font font, Color foreColor, Color backColor)
        {
            var label = new Label();
            label.Text = text;
            label.Location = location;
            label.Size = size;
            label.Font = font;
            label.ForeColor = foreColor;
            label.BackColor = backColor;
            return label;
        }

        private void GameMenu()
        {
            FPS.Visible = false;
            Health.Visible = false;
            Ammo.Visible = false;
            BackMusic.Stop();
            var pfc = new PrivateFontCollection();
            int fontLength = Properties.Resources.OCRAEXT.Length;
            byte[] fontdata = Properties.Resources.OCRAEXT;
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);
            pfc.AddMemoryFont(data, fontLength);

            var buttonColor = Color.FromArgb(171, 0, 9);
            BackColor = Color.FromArgb(236, 235, 233);

            var table = new TableLayoutPanel();
            table.RowStyles.Clear();
            table.ColumnStyles.Clear();
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var pictureBox = new PictureBox();
            pictureBox.Image = new Bitmap(Properties.Resources.logo);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Dock = DockStyle.Fill;

            var continueGame = CreateButton(
                  "CONTINUE",
                  new Size(50, 25),
                  new Point(100, 100),
                  new Font(pfc.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  buttonColor);
            continueGame.Dock = DockStyle.Fill;

            var settings = CreateButton(
                  "SETTINGS",
                  new Size(100, 50),
                  new Point(100, 100),
                  new Font(pfc.Families[0], 14),
                  Color.FromArgb(0, 0, 0),
                  buttonColor);
            settings.Dock = DockStyle.Fill;

            var exit = CreateButton(
                "EXIT",
                new Size(100, 50),
                new Point(100, 25),
                new Font(pfc.Families[0], 14),
                Color.FromArgb(0, 0, 0),
                buttonColor);
            exit.Dock = DockStyle.Fill;

            table.Controls.Add(pictureBox, 0, 0);
            table.Controls.Add(continueGame, 0, 1);
            table.Controls.Add(settings, 0, 2);
            table.Controls.Add(exit, 0, 3);

            table.Dock = DockStyle.Fill;

            Controls.Add(table);

            continueGame.Click += continueGameOnClick;
            settings.Click += settingsOnClick;
            exit.Click += ExitOnClick;
        }

        private void continueGameOnClick(object sender, EventArgs e)
        {
            Controller.GameMenuOn = false;
            Controls.Clear();
            Controls.Add(FPS);
            Controls.Add(Health);
            FPS.Visible = true;
            Health.Visible = true;
            Ammo.Visible = true;
            //BackMusic.Play();
            Focus();
            Invalidate();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "coVirus";
            DoubleBuffered = true;
        }
    }
}