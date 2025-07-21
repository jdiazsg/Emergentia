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
        // Clear grid
        for (int x = 0; x < Width; x++)
        for (int y = 0; y < Height; y++)
            Grid[x, y] = '\0';

        foreach (var agent in Agents)
        {
            agent.Update(Grid, Width, Height, Agents);
            Grid[agent.X, agent.Y] = AgentChar;
        }
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