namespace Emergentia;

public class World
{
    public int Width { get; }
    public int Height { get; }

    private readonly char[,] _grid;
    private readonly List<Agent> _agents = new();
    private readonly Random _rand = new();

    private readonly int _agentCount;
    private readonly char _agentChar;

    public World(int width, int height, int agentCount, char agentChar)
    {
        Width = width;
        Height = height;
        _agentCount = agentCount;
        _agentChar = agentChar;
        _grid = new char[width, height];
    }

    public void InitAgents()
    {
        for (int i = 0; i < _agentCount; i++)
        {
            int x, y;
            do
            {
                x = _rand.Next(Width);
                y = _rand.Next(Height);
            } while (_grid[x, y] != '\0');

            var agent = new Agent(x, y);
            _agents.Add(agent);
            _grid[x, y] = _agentChar;
        }
    }

    public void Update()
    {
        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
            _grid[x, y] = '\0';

        foreach (var agent in _agents)
        {
            agent.Update(_grid, Width, Height, _agents);
            _grid[agent.X, agent.Y] = _agentChar;
        }
    }

    public void Draw()
    {
        Console.Clear();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
                Console.Write(_grid[x, y] == '\0' ? '.' : _grid[x, y]);
            Console.WriteLine();
        }
    }
}
