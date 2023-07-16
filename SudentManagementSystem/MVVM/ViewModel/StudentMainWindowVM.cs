using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SudentManagementSystem.MVVM.Model;
using SudentManagementSystem.MVVM.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SudentManagementSystem.MVVM.ViewModel
{
    public partial class StudentMainWindowVM : ObservableObject
    {

        [ObservableProperty]
        public ObservableCollection<Model.Student> students;
        [ObservableProperty]
        public Model.Student std;

        [RelayCommand]
        public void Close()
        {
            Application.Current.Shutdown();
        }

        [RelayCommand]
        public void OpenAddStudent() 
        {
            
            var addwindow = new AddStudent();
            addwindow.Show();
            LoadData();
        }

        [RelayCommand]
        public void EditAddStudent()
        {
            if (Std != null)
            {
                var stdvm = new EditStudentVM(Std);
                var editwindow = new EditStudent(stdvm);
                editwindow.Show();
                LoadData();
            }
            LoadData();
        }

        


        [RelayCommand]
        public void Delete()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure to delete this student details.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var db = new MyDBContext())
                {
                    db.Students.Remove(std);
                    db.SaveChanges();
                }
                LoadData();
            }
            std = null;
        }

        [RelayCommand]
        public void reload()
        {
            LoadData();
        }

        public void LoadData()
        {
            using (var db = new MyDBContext())
            {
                Students = new ObservableCollection<Model.Student>(db.Students.ToList());
            }
        }

        public StudentMainWindowVM()
        {
            LoadData();
        }

    }
}
