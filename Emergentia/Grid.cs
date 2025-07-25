namespace Emergentia;

public class Grid
{
    private readonly bool[,] _cells;
    public int Width { get; }
    public int Height { get; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        _cells = new bool[width, height];
    }

    public bool IsOccupied(int x, int y) => _cells[x, y];

    public void SetOccupied(int x, int y, bool value) => _cells[x, y] = value;

    public void Clear()
    {
        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
            _cells[x, y] = false;
    }
}
