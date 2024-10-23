public class MazeCell
{
    public int X;
    public int Y;
    public int Distance;

    public bool North = true;
    public bool South = true;
    public bool East = true;
    public bool West = true;

    public bool Visited = false;

    public bool NorthE = false;
    public bool SouthE = false;
    public bool EastE = false;
    public bool WestE = false;
}