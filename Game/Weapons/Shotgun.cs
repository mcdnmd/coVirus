using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame
{
    public class Shotgun : IWeapon, IEntity
    {
        public int Damage { get; set; }
        public int StartStrip { get; set; }
        public int EndStrip { get; set; }
        public int DamageDistance { get; set; }
        public int Ammo { get; set; }
        public int MaxAmmo { get; set; }
        public PointF Location { get; set; }
        public int Health { get; set; }
        public bool Alive { get; set; }
        private List<IEntity> Enemies;
        private Stopwatch wathc;

        public Shotgun(int x, int y)
        {
            Damage = 500;
            DamageDistance = 60;
            Ammo = 10;
            MaxAmmo = 50;
            Location = new PointF(x, y);
            Health = int.MinValue;
            Alive = false;
            var FOSProjection = (int) Core.ScreenWidth * 0.6;
            StartStrip = (int)((Core.ScreenWidth - FOSProjection) / 2);
            EndStrip = Core.ScreenWidth - StartStrip + 1;
            Enemies = new List<IEntity>();
            wathc = new Stopwatch();
            wathc.Start();
        }

        public void Shot()
        {
            if (Ammo <= 0)
                return;
            Ammo--;
            FindEnemyInSpreadField();
            foreach (var enemy in Enemies)
                GiveDamge(enemy);
        }

        private void FindEnemyInSpreadField()
        {
            Enemies = EnemyCast.CastedEnemies;
        }
        
        public void GiveDamge(IEntity enemy)
        {
            var distance = new PointF(enemy.Location.X - Game._Player.Location.X, enemy.Location.Y - Game._Player.Location.Y);
            //enemy.Health -= (int)(Damage / Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y));
            enemy.Health -= (int)(Damage * 1 / Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y));
        }

        public void Act()
        {
            return;
        }
    }
}
