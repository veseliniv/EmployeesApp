namespace EmployeesApp.Helpers
{
    using EmployeesApp.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class HelperUtil
    {
        public static List<MostWorkedEmploeeysModel> GetMostWorkedEmploeeys(List<EmployeeModel> employees)
        {
            List<MostWorkedEmploeeysModel> mostWorkedEmploeeys = new List<MostWorkedEmploeeysModel>();
            int daysWorked = 0;

            for (int i = 0; i < employees.Count; i++)
            {
                EmployeeModel currentEmployee = employees[i];

                for (int j = i + 1; j < employees.Count; j++)
                {
                    if(currentEmployee.ProjectID == employees[j].ProjectID)
                    {
                        if(daysWorked <= CalcDaysTogetherForPair(currentEmployee.DateFrom, employees[j].DateFrom, currentEmployee.DateTo, employees[j].DateTo))
                        {
                            daysWorked = CalcDaysTogetherForPair(currentEmployee.DateFrom, employees[j].DateFrom, currentEmployee.DateTo, employees[j].DateTo);
                            mostWorkedEmploeeys.Add(new MostWorkedEmploeeysModel()
                            {
                                FirstEmployeeID = currentEmployee.EmployeeID,
                                SecondEmployeeID = employees[j].EmployeeID,
                                ProjectID = currentEmployee.ProjectID,
                                DaysWorked = daysWorked
                            });
                        }
                    }
                }
            }

            return mostWorkedEmploeeys;
        }

        public static EmployeeModel ConvertTextToEmployeeModel(string line)
        {
            string[] employee = line.Split(',').Select(x => x.Trim()).ToArray();

            EmployeeModel employeeModel = new EmployeeModel()
            {
                EmployeeID = int.Parse(employee[0]),
                ProjectID = int.Parse(employee[1]),
                DateFrom = (employee[2] == "NULL") ? DateTime.Now : DateTime.Parse(employee[2]),
                DateTo = (employee[3] == "NULL") ? DateTime.Now : DateTime.Parse(employee[3])
            };

            return employeeModel;
        }

        private static int CalcDaysTogetherForPair(DateTime firstEmployeeDateFrom, DateTime secondEmployeeDateFrom, 
                                                   DateTime firstEmployeeDateTo, DateTime secondEmployeeDateTo)
        {
            int daysTogether = 0;

            if (firstEmployeeDateFrom <= secondEmployeeDateFrom && firstEmployeeDateTo <= secondEmployeeDateTo)
            {
                daysTogether = CalcDaysDiff(secondEmployeeDateFrom, firstEmployeeDateTo);
            }
            else if (firstEmployeeDateFrom >= secondEmployeeDateFrom && firstEmployeeDateTo >= secondEmployeeDateTo)
            {
                daysTogether = CalcDaysDiff(firstEmployeeDateFrom, secondEmployeeDateTo);
            }
            else if (firstEmployeeDateFrom >= secondEmployeeDateFrom && firstEmployeeDateTo <= secondEmployeeDateTo)
            {
                daysTogether = CalcDaysDiff(firstEmployeeDateFrom, firstEmployeeDateTo);
            }
            else if (firstEmployeeDateFrom <= secondEmployeeDateFrom && firstEmployeeDateTo >= secondEmployeeDateTo)
            {
                daysTogether = CalcDaysDiff(secondEmployeeDateFrom, secondEmployeeDateTo);
            }
            return daysTogether;
        }

        private static int CalcDaysDiff(DateTime start, DateTime end)
        {
            return (int)(end - start).TotalDays;
        }
    }
}
