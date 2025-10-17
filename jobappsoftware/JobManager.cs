﻿using System;
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
                Console.WriteLine("No.\n");
                return;
            }

            Console.WriteLine("All applications:");
            foreach (var app in Applications)
            {
                Console.WriteLine(app.GetSummary());
            }
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
