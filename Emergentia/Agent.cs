namespace Emergentia;

public class Agent
{
    public int X;
    public int Y;

    private static readonly Random Rand = new();

    public Agent(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Update(Grid grid, int width, int height, List<Agent> allAgents)
    {
        (X, Y) = ComputeMove(grid, width, height, allAgents);
    }

    public (int, int) ComputeMove(Grid grid, int width, int height, List<Agent> allAgents)
    {
        int neighbors = CountNeighbors(allAgents);

        int newX = X;
        int newY = Y;

        if (neighbors == 0)
        {
            // Move randomly
            GetRandomMove(out newX, out newY, width, height);
        }
        else if (neighbors >= 3)
        {
            // Select the least crowded neighbouring cell
            GetLeastCrowdedMove(out newX, out newY, width, height, allAgents);
        }
        // else: stay put
        
        // Move if new space is free
        if (grid.IsOccupied(newX, newY))
        {
            newX = X;
            newY = Y;
        }

        return (newX, newY);
    }

    private int CountNeighbors(List<Agent> allAgents)
    {
        return allAgents.Count(a =>
            a != this &&
            Math.Abs(a.X - X) <= 1 &&
            Math.Abs(a.Y - Y) <= 1);
    }

    private void GetRandomMove(out int newX, out int newY, int width, int height)
    {
        int dx = Rand.Next(-1, 2);
        int dy = Rand.Next(-1, 2);
        newX = Math.Clamp(X + dx, 0, width - 1);
        newY = Math.Clamp(Y + dy, 0, height - 1);
    }

    private void GetLeastCrowdedMove(out int newX, out int newY, int width, int height, List<Agent> allAgents)
    {
        int currentNeighbors = CountNeighbors(allAgents);
        int bestX = X, bestY = Y, minNeighbors = currentNeighbors;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int testX = Math.Clamp(X + dx, 0, width - 1);
                int testY = Math.Clamp(Y + dy, 0, height - 1);
                if (dx == 0 && dy == 0) continue;

                int count = allAgents.Count(a =>
                    a != this &&
                    Math.Abs(a.X - testX) <= 1 &&
                    Math.Abs(a.Y - testY) <= 1);

                if (count < minNeighbors)
                {
                    minNeighbors = count;
                    bestX = testX;
                    bestY = testY;
                }
            }
        }

        newX = bestX;
        newY = bestY;
    }
}