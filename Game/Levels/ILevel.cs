using System.Collections.Generic;

public interface ILevel
{
    int[] IntMap { get; set; }
    Dictionary<char, int> ByteEntityRelation { get; set; }
    int Width { get; set; }
    int Height { get; set; }
}
