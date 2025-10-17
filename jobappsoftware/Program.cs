namespace jobappsoftware
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var manager = new JobManager();
            var menu = new Menu(manager);
            menu.Run();
        }
    }   
}
