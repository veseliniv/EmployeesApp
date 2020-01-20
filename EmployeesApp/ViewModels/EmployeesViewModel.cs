namespace EmployeesApp.ViewModels
{
    using EmployeesApp.Commands;
    using EmployeesApp.Helpers;
    using EmployeesApp.Models;
    using Microsoft.Win32;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;

    public class EmployeesViewModel 
    {
        private ObservableCollection<EmployeeModel> employeeViewModels;
        private ObservableCollection<MostWorkedEmploeeysModel> mostWorkedEmploeeysModels;

        private ICommand openFileCommand;

        public EmployeesViewModel()
        {
            employeeViewModels = new ObservableCollection<EmployeeModel>();
            mostWorkedEmploeeysModels = new ObservableCollection<MostWorkedEmploeeysModel>();
        }

        public ObservableCollection<EmployeeModel> Employees 
        { 
            get
            {
                return employeeViewModels;
            }
            set
            {
                employeeViewModels = value;
            }
        }

        public ObservableCollection<MostWorkedEmploeeysModel> MostWorkedEmploeeys
        {
            get
            {
                return mostWorkedEmploeeysModels;
            }
            set
            {
                mostWorkedEmploeeysModels = value;
            }
        }


        public ICommand OpenFile
        {
            get
            {
                if (this.openFileCommand == null)
                {
                    this.openFileCommand = new RelayCommand(this.HandleOpenFileCommand);
                }

                return this.openFileCommand;
            }
        }

        private void HandleOpenFileCommand(object obj)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text Files(*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == true)
                {

                    employeeViewModels.Clear();
                    mostWorkedEmploeeysModels.Clear();

                    StreamReader reader = new StreamReader(openFileDialog.FileName);

                    using(reader)
                    {
                        string line = reader.ReadLine();

                        while(line != null)
                        {
                            Employees.Add(HelperUtil.ConvertTextToEmployeeModel(line));

                            line = reader.ReadLine();
                        }
                    }
                }

                foreach (MostWorkedEmploeeysModel mostWorkedEmploeey in HelperUtil.GetMostWorkedEmploeeys(Employees.ToList()))
                {
                    MostWorkedEmploeeys.Add(mostWorkedEmploeey);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
