using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using SudentManagementSystem.MVVM.Model;
using SudentManagementSystem.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SudentManagementSystem.MVVM.ViewModel
{

    public partial class AddStudentVM: ObservableObject
    {
        [ObservableProperty]
        public string firstName;
        [ObservableProperty] 
        public string lastName;
        [ObservableProperty]
        public DateTime doB;
        [ObservableProperty]
        public DateOnly doBDate;
        [ObservableProperty]
        public int telNO;
        [ObservableProperty]
        public string department;
        [ObservableProperty]
        public double gPA;
        [ObservableProperty]
        public BitmapImage iMG;

        [RelayCommand]
        public void Backbtn()
        {

         Application.Current.Windows.OfType<AddStudent>().FirstOrDefault().Close();

        }




        [RelayCommand]
        private void UploadImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";
            if (dialog.ShowDialog() == true)
            {

                string _imageFilePath = dialog.FileName;
                BitmapImage image = new BitmapImage(new Uri(_imageFilePath));
                IMG = image;
            }
        }

        /// <summary>
        /// Saves a new student to the database.
        /// </summary>
        [RelayCommand]
        public void Save()
        {
            
            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Fill All Cells", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            
            if (GPA < 0 || GPA >=4)
            {
                MessageBox.Show("GPA value between 0 to 4.00 ", "Error");
                return;
            }



          
            Model.Student student = new Model.Student();
            student.FirstName = FirstName;
            student.LastName = LastName;
            student.DateOfBirth = DoB;
            student.TelNo = TelNO;
            student.Dept = Department;
            student.Gpa = GPA;
            student.Img = ConvertBitmapImageToByteArray(IMG);
            using (var _context = new MyDBContext())
            {
                _context.Students.Add(student);
                _context.SaveChanges();
            }

           
            MessageBox.Show($"Student added successfully Student ID : {student.StdID}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Windows.OfType<AddStudent>().FirstOrDefault()?.Close();
        }

        public byte[] ConvertBitmapImageToByteArray(BitmapImage bitmapImage)
        {
            byte[] data;
            BitmapEncoder encoder = new PngBitmapEncoder(); 
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }

       

        public AddStudentVM()
        {
            doB = DateTime.Now;
        }
    }
}


