namespace Emergentia;

public class World
{
    private int Width { get; }
    private int Height { get; }

    private readonly Grid _grid;
    private readonly List<Agent> _agents = [];
    private readonly Random _rand = new();

    private readonly char _agentChar;

    public World(int width, int height, int agentCount, char agentChar)
    {
        Width = width;
        Height = height;
        _agentChar = agentChar;
        _grid = new Grid(width, height);
        
        InitAgents(agentCount);
    }

    private void InitAgents(int agentCount)
    {
        for (int i = 0; i < agentCount; i++)
        {
            int x, y;
            do
            {
                x = _rand.Next(Width);
                y = _rand.Next(Height);
            } while (_grid.IsOccupied(x, y));

            var agent = new Agent(x, y);
            _agents.Add(agent);
            _grid.SetOccupied(x, y, true);
        }
    }

    public void Update()
    {
        _grid.Clear();

        foreach (var agent in _agents)
        {
            agent.Update(_grid, Width, Height, _agents);
            _grid.SetOccupied(agent.X, agent.Y, true);
        }
    }

    public static void Draw(World world)
    {
        Console.Clear();
        for (int y = 0; y < world.Height; y++)
        {
            for (int x = 0; x < world.Width; x++)
                Console.Write(world._grid.IsOccupied(x, y) ? world._agentChar : '.');
            Console.WriteLine();
        }
    }
}
