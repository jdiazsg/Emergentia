namespace Emergentia;

internal class Program
{
    private static void Main()
    {
        var world = new World(width: 20, height: 10, agentCount: 20, agentChar: 'X');
        world.InitAgents();

        while (true)
        {
            world.Update();
            world.Draw();
            Thread.Sleep(100);
        }
    }
}
