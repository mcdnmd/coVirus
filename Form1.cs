using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DGame
{
    public partial class Form1 : Form
    {
        private int MaxFPS = 30;
        private Timer timer = new Timer();
        private Stopwatch stopWatch = new Stopwatch();
        private long oldTime = 0;

        public Form1()
        {
            InitializeComponent();
            InitStaticComponents();
            InitNonStaticComponents();
            timer.Interval = 1000 / MaxFPS;
            timer.Tick += new EventHandler(TimerTick);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs args)
        {
            Controller.Acting();
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            stopWatch.Start();
            ScreenRender.Clear(e);
            ScreenRender.Raycasting(e);
            ScreenRender.DrawBuffer();
            //Game._Player.Draw(e.Graphics);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            label1.Text = String.Format("{0:00}",
            1000 / (ts.Milliseconds - oldTime));
            oldTime = ts.Milliseconds;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            Controller.PressedKeys.Enqueue(e.KeyCode);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InitNonStaticComponents();
        }

        private void InitNonStaticComponents()
        {
            ScreenRender.InitScreenRender(Width, Height);
        }

        private void InitStaticComponents()
        {
            Map.CreateMap(new Level_1());
            Game.InitGame();
            Controller.InitController();
        }
    }
}
