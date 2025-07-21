namespace Emergentia;

internal class Program
{
    private const int Width = 50;
    private const int Height = 25;
    private const int AgentCount = 30;
    private const char AgentChar = 'X';
    private static readonly char[,] Grid = new char[Width, Height];
    private static readonly List<Agent> Agents = [];
    private static readonly Random Rand = new();

    private static void Main()
    {
        InitAgents();
        while (true)
        {
            UpdateAgents();
            DrawGrid();
            Thread.Sleep(300);
        }
    }

    private static void InitAgents()
    {
        for (int i = 0; i < AgentCount; i++)
        {
            int x, y;
            do
            {
                x = Rand.Next(Width);
                y = Rand.Next(Height);
            } while (Grid[x, y] != '\0');

            var agent = new Agent(x, y);
            Agents.Add(agent);
            Grid[x, y] = AgentChar;
        }
    }

    private static void UpdateAgents()
    {
        var tempGrid = new char[Width, Height];
        var snapshot = Agents.Select(a => new Agent(a.X, a.Y)).ToList();
        var newPositions = new (int X, int Y)[Agents.Count];

        for (int i = 0; i < Agents.Count; i++)
        {
            var agent = Agents[i];
            var (nx, ny) = agent.ComputeMove(Grid, Width, Height, snapshot);

            if (tempGrid[nx, ny] == '\0')
            {
                newPositions[i] = (nx, ny);
                tempGrid[nx, ny] = AgentChar;
            }
            else
            {
                newPositions[i] = (agent.X, agent.Y);
                tempGrid[agent.X, agent.Y] = AgentChar;
            }
        }

        for (int i = 0; i < Agents.Count; i++)
        {
            Agents[i].X = newPositions[i].X;
            Agents[i].Y = newPositions[i].Y;
        }

        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
            Grid[x, y] = tempGrid[x, y];
    }

    private static void DrawGrid()
    {
        Console.Clear();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
                Console.Write(Grid[x, y] == '\0' ? '.' : Grid[x, y]);
            Console.WriteLine();
        }
    }
}