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

