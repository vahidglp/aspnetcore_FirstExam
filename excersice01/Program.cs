using Dal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace excersice01
{
    class Program
    {
        static void Main(string[] args)
        {
            ExcersiceDBContext _Context = new ExcersiceDBContext();
            _Context.Database.EnsureCreated();
            Student st = new Student
            {
                FirstName = "وحید",
                LastName = "گلپایگانی",
            };
            _Context.Students.Add(st);
            _Context.SaveChanges();

            var dd = _Context.Students.First();
            dd.FirstName = "علی";
            dd.LastName = "علوی";
            SaveList(new List<string>{ "FirstName" }, dd);
            //_Context.Students.RemoveAll();
            Console.WriteLine("Hello World!");
        }

        public static void SaveList<T>(List<string> feilds, T entity) where T : class
        {
            ExcersiceDBContext _Context = new ExcersiceDBContext();
                             
            foreach (var item in feilds)
            {
                if (_Context.Entry(entity).Properties.Any(a => a.Metadata.Name == item))
                    _Context.Entry(entity).Properties.Single(a => a.Metadata.Name == item).IsModified = true;
            }

            _Context.SaveChanges();
        }
    }
}
