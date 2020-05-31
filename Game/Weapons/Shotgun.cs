using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame
{
    public class Shotgun : IWeapon, IEntity
    {
        public int Damage { get; set; }
        public int SpreadField { get; set; }
        public int DamageDistance { get; set; }
        public int Ammo { get; set; }
        public int MaxAmmo { get; set; }
        public PointF Location { get; set; }
        public int Health { get; set; }
        public bool Alive { get; set; }
        private double Norma;

        public Shotgun(int x, int y)
        {
            Damage = 9000;
            SpreadField = 40;
            DamageDistance = 60;
            Ammo = 10;
            MaxAmmo = 50;
            Location = new PointF(x, y);
            Health = int.MinValue;
            Alive = false;
            CalculateVectorNorma();
        }

        public void Shot()
        {
            if (Ammo <= 0)
                return;
            Ammo--;
            FindEnemyInSpreadField();

        }

        private void FindEnemyInSpreadField()
        {
            foreach(var enemy in Game.AliveActors)
                if (enemy is Zombi)
                    if (EnemyInSpreadField(enemy))
                        GiveDamge(enemy);
        }
        

        private bool EnemyInSpreadField(IEntity enemy)
        {
            return false;
        }

        private void CalculateVectorNorma()
        {
            return;
        }

        public void GiveDamge(IEntity enemy)
        {
            var distance = new PointF(enemy.Location.X - Game._Player.Location.X, enemy.Location.Y - Game._Player.Location.Y);
            enemy.Health -= (int)(Damage / Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y));
        }

        public void Act()
        {
            return;
        }
    }
}
