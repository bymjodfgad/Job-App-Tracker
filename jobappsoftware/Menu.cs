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
            Console.WriteLine("========== Job Application Tracker ==========");
            Console.WriteLine("1. Lägg till ny ansökan");
            Console.WriteLine("2. Visa alla ansökningar");
            Console.WriteLine("3. Uppdatera status");
            Console.WriteLine("4. Ta bort en ansökan");
            Console.WriteLine("0. Avsluta");
            Console.Write("Välj ett alternativ: ");
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
                    Console.WriteLine("Programmet avslutas...");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val.\n");
                    break;
            }
        }
    }
}
