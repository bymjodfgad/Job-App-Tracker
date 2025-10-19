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
        private const string FilePath = "applications.json";
        public List<JobApplication> Applications { get; private set; }

        public JobManager()
        {
            Applications = LoadFromFile();
        }

        public void AddJob()
        {
            Console.Write("Company: ");
            string company = Console.ReadLine();

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Salary (kr): ");
            int salary = int.Parse(Console.ReadLine());

            var job = new JobApplication(company, title, salary);
            Applications.Add(job);

            SaveToFile();
            Console.WriteLine("Application added!\n");
        }

        public void ShowAll()
        {
            if (Applications.Count == 0)
            {
                Console.WriteLine("No applications.\n");
                return;
            }

            Console.WriteLine($"Number of applications: {Applications.Count}\n");

            Console.WriteLine("Applications:");
         
            foreach (var app in Applications)
            {


            switch(app.Status)
                {
                    case ApplicationStatus.Offer:
                        Console.ForegroundColor = ConsoleColor.Green; 
                        break;
                    case ApplicationStatus.Rejected:
                        Console.ForegroundColor= ConsoleColor.Red;
                        break;
                    case ApplicationStatus.Interview:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case ApplicationStatus.Applied:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }
                Console.WriteLine(app.GetSummary());
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        public void UpdateStatus()
        {
            ShowAll();
            Console.Write("Choose applcation to update (Starts from 0): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Applications.Count)
            {
                Console.WriteLine("Choose new status:");
                foreach (var status in Enum.GetValues(typeof(ApplicationStatus)))
                    Console.WriteLine($"{(int)status} - {status}");

                Console.Write("Your choice: ");
                int choice = int.Parse(Console.ReadLine());
                Applications[index].Status = (ApplicationStatus)choice;

                SaveToFile();
                Console.WriteLine("Status updated!\n");
            }
            else
            {
                Console.WriteLine("Invalid choice.\n");
            }
        }
        
        public void RemoveJob()
        {
            ShowAll();
            Console.Write("Choose applcation to remove (Starts from 0): ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < Applications.Count)
            {
                Applications.RemoveAt(index);

                SaveToFile();
                Console.WriteLine("Application removed!\n");
            }
            else
            {
                Console.WriteLine("Invalid choice.\n");
            }
        }

        public void FilterByStatus() //linq filter based on enum
        {
            Console.WriteLine("Select a status to filter by:");
            foreach (var status in Enum.GetValues(typeof(ApplicationStatus)))
            Console.WriteLine($"{(int)status} - {status}");

            Console.Write("Your choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                Enum.IsDefined(typeof(ApplicationStatus), choice))
            {
                var selectedStatus = (ApplicationStatus)choice;

                var filtered = Applications
                    .Where(a => a.Status == selectedStatus)
                    .ToList();

                Console.WriteLine($"\nApplications with status: {selectedStatus}\n");

                if (filtered.Count == 0)
                    Console.WriteLine("No applications found.\n");
                else
                    foreach (var app in filtered)
                        Console.WriteLine(app.GetSummary());
            }
            else
            {
                Console.WriteLine("Invalid choice.\n");
            }
        }

        public void SortByDate() //linq sorting based on date
        {
            var ordered = Applications.OrderBy(a => a.ApplicationDate).ToList();
            if (ordered.Count == 0)
            {
                Console.WriteLine("No applications to show.\n");
                return;
            }

            Console.WriteLine("Applications sorted by date:\n");
            foreach (var app in ordered)
            {
                Console.WriteLine(app.GetSummary());
            }
            Console.WriteLine();
        }

        //saving json
        private void SaveToFile()
        {
            string json = JsonSerializer.Serialize(Applications, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        //loading jsonz
        private List<JobApplication> LoadFromFile()
        {
            if (!File.Exists(FilePath))
                return new List<JobApplication>();

            try
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<JobApplication>>(json) ?? new List<JobApplication>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
                return new List<JobApplication>();
            }
        }
    }
}
