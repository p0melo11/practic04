namespace practic04;

class Program
{
    public static void Main()
    {
        var emitter = new EventEmitter();
        var dev = new Developer(emitter);
        var projectManger = new ProjectManager(emitter);
        var client = new Client(emitter);

        client.GenerateWork();

        Console.ReadLine();
    }
}
