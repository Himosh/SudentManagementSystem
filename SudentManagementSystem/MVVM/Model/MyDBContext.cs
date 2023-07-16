using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudentManagementSystem.MVVM.Model
{
    class MyDBContext:DbContext
    {
        public DbSet<Student> Students { get; set; }
        public readonly string FilePath = @"C:\Users\94760\Downloads\Himosh\SudentManagementSystem\SudentManagementSystem\MVVM\DataBase\StudentDB.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={FilePath}");
        }
    }
}
