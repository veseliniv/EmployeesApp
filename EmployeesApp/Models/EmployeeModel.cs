namespace EmployeesApp.Models
{
    using System;

    public class EmployeeModel
    {
        public int EmployeeID { get; set; }

        public int ProjectID { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
