using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IWeapon
{
    int Damage { get; set; }
    int StartStrip { get; set; }
    int EndStrip { get; set; }
    int DamageDistance { get; set; }
    int Ammo { get; set; }
    int MaxAmmo { get; set; }

    void Shot();
}

