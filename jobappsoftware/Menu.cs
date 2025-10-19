using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jobappsoftware
{
    public class Menu
    {
        private readonly JobManager _manager;
        private bool _running = true;

        public Menu(JobManager manager)
        {
            _manager = manager;
        }

        public void Run()
        {
            while (_running)
            {
                ShowMenu();
                HandleInput();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        JOB APPLICATION TRACKER        ");
            Console.WriteLine("══════════════════════════════════════\n");
            Console.WriteLine("1. Add new application");
            Console.WriteLine("2. Show all applications");
            Console.WriteLine("3. Update status");
            Console.WriteLine("4. Remove an application");
            Console.WriteLine("0. Exit\n");
            Console.Write("--> Select an option: ");
        }

        private void HandleInput()
        {
            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    _manager.AddJob();
                    break;
                case "2":
                    _manager.ShowAll();
                    break;
                case "3":
                    _manager.UpdateStatus();
                    break;
                case "4":
                    _manager.RemoveJob();
                    break;
                case "0":
                    _running = false;
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice.\n");
                    break;
            }
        }
    }
}
