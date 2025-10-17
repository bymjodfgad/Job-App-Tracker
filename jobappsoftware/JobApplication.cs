using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jobappsoftware
{


    public enum ApplicationStatus
    {
        Applied,
        Interview,
        Offer,
        Rejected
    }
    public class JobApplication
    {
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public ApplicationStatus Status { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        public int SalaryExpectation { get; set; }

        public JobApplication(string companyName, string positionTitle, int salaryExpectation)
        {
            this.CompanyName = companyName;
            this.PositionTitle = positionTitle;
            this.SalaryExpectation = salaryExpectation;
            this.Status = ApplicationStatus.Applied;
            this.ApplicationDate = DateTime.Now;
            this.ResponseDate = null;
        }

        public int GetDaysSinceApplied()
        {
            return (DateTime.Now - ApplicationDate).Days;
        }

        public string GetSummary()
        {
            return $"{CompanyName} - {PositionTitle} ({Status}) | Ansökt: {ApplicationDate.ToShortDateString()}";
        }

    }
}
