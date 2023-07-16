using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using SudentManagementSystem.MVVM.Model;
using SudentManagementSystem.MVVM.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;



namespace SudentManagementSystem.MVVM.ViewModel
{
 public partial class EditStudentVM: ObservableObject
    {

        [ObservableProperty]
        public string firstName;
        [ObservableProperty]
        public string lastName;
        [ObservableProperty]
        public DateTime doB;
        [ObservableProperty]
        public int telNO;
        [ObservableProperty]
        public string department;
        [ObservableProperty]
        public double gPA;
        [ObservableProperty]
        public BitmapImage iMG;
        [ObservableProperty]
        public Model.Student std;

        [RelayCommand]
        public void Backbtn()
        {

            Application.Current.Windows.OfType<EditStudent>().FirstOrDefault().Close();

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

        
        [RelayCommand]
        public void Edit()
        {

            if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
            {
                MessageBox.Show("Fill All Cells", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (GPA < 0 || GPA >= 4)
            {
                MessageBox.Show("GPA value between 0 to 4.00 ", "Error");
                return;
            }

            var _context = new MyDBContext();
            Std = _context.Students.Find(Std.StdID);
            Std.FirstName = FirstName;
            Std.LastName = LastName;
            Std.DateOfBirth = DoB;
            Std.TelNo = TelNO;
            Std.Dept = Department;
            Std.Gpa = GPA;
            Std.Img = ConvertBitmapImageToByteArray(IMG);

                _context.SaveChanges();
            

          
            MessageBox.Show($"Student added successfully Student ID : {Std.StdID}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Windows.OfType<EditStudent>().FirstOrDefault()?.Close();
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

       public EditStudentVM(Model.Student student)
        {
            Std = student;
            FirstName= student.FirstName;
            LastName= student.LastName;
            DoB= student.DateOfBirth;
           TelNO= student.TelNo;
            Department = student.Dept;
            GPA= student.Gpa;
            IMG=ConvertByteArrayToBitmapImage(student.Img);
        }
        public BitmapImage ConvertByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                memoryStream.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }
            bitmapImage.Freeze(); 
            return bitmapImage;
        }

        public EditStudentVM()
        {
            doB = DateTime.Now;
        }


  


    }
}
