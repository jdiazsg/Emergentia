namespace Emergentia;

internal class Program
{
    private static void Main()
    {
        var world = new World(width: 20, height: 10, agentCount: 20, agentChar: 'X');
    
        while (true)
        {
            world.Update();
            World.Draw(world);
            Thread.Sleep(100);
        }
    }
}
