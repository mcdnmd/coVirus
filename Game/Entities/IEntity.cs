using System.Drawing;

public interface IEntity
{
    PointF Location { get; set; }
    int Health { get; set; }
    bool Alive { get; set; }

    void Act();
    //void MakeMovement(int dx, int dy);
}

