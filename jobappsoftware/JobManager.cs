using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace jobappsoftware
{
    public class JobManager
    {
        public List<JobApplication> Applications { get; private set; }

        public JobManager()
        {
            Applications = new List<JobApplication>();
        }

        public void AddJob()
        {
            Console.Write("Företagsnamn: ");
            string company = Console.ReadLine();

            Console.Write("Jobbtitel: ");
            string title = Console.ReadLine();

            Console.Write("Löneanspråk (kr): ");
            int salary = int.Parse(Console.ReadLine());

            var job = new JobApplication(company, title, salary);
            Applications.Add(job);

            Console.WriteLine("Ansökan tillagd!\n");
        }

        public void ShowAll()
        {
            if (Applications.Count == 0)
            {
                Console.WriteLine("Inga ansökningar att visa.\n");
                return;
            }

            Console.WriteLine("Alla ansökningar:");
            foreach (var app in Applications)
            {
                Console.WriteLine(app.GetSummary());
            }
            Console.WriteLine();
        }

        public void UpdateStatus()
        {
            ShowAll();
            Console.Write("Ange index på ansökan att uppdatera (börjar från 0): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Applications.Count)
            {
                Console.WriteLine("Välj ny status:");
                foreach (var status in Enum.GetValues(typeof(ApplicationStatus)))
                    Console.WriteLine($"{(int)status} - {status}");

                Console.Write("Ditt val: ");
                int choice = int.Parse(Console.ReadLine());
                Applications[index].Status = (ApplicationStatus)choice;

                Console.WriteLine("Status uppdaterad!\n");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.\n");
            }
        }

        public void RemoveJob()
        {
            ShowAll();
            Console.Write("Ange index på ansökan att ta bort: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Applications.Count)
            {
                Applications.RemoveAt(index);
                Console.WriteLine("Ansökan borttagen!\n");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.\n");
            }
        }
    }
}
