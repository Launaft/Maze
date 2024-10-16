public class MazeCell
{
    public int X;
    public int Y;
    public int Weight;

    public bool North = true;
    public bool South = true;
    public bool East = true;
    public bool West = true;

    public bool Visited = false;
}