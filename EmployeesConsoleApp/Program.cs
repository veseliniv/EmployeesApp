namespace EmployeesConsoleApp
{
    using EmployeesApp.Helpers;
    using EmployeesApp.Models;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.IO;
    public class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            List<EmployeeModel> employeesModels = new List<EmployeeModel>();
            List<MostWorkedEmploeeysModel> mostWorkedEmploeeys = new List<MostWorkedEmploeeysModel>();

            Console.Write(@"Open file(Y\N):");
            string openFileDialogKeyPressed = Console.ReadLine();
            
            if(openFileDialogKeyPressed == "Y")
            {
                try
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text Files(*.txt)|*.txt";
                    if (openFileDialog.ShowDialog() == true)
                    {
                        StreamReader reader = new StreamReader(openFileDialog.FileName);

                        using (reader)
                        {
                            string line = reader.ReadLine();

                            while (line != null)
                            {
                                employeesModels.Add(HelperUtil.ConvertTextToEmployeeModel(line));

                                line = reader.ReadLine();
                            }
                        }
                    }

                    mostWorkedEmploeeys.AddRange(HelperUtil.GetMostWorkedEmploeeys(employeesModels));

                    foreach (MostWorkedEmploeeysModel mostWorkedEmploeey in mostWorkedEmploeeys)
                    {
                        Console.WriteLine("First Employee ID -> {0}\nSecond Employee ID -> {1}\nProject ID -> {2}\nDays Worked -> {3}\n",
                                                         mostWorkedEmploeey.FirstEmployeeID.ToString(), mostWorkedEmploeey.SecondEmployeeID.ToString(),
                                                         mostWorkedEmploeey.ProjectID.ToString(), mostWorkedEmploeey.DaysWorked.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
