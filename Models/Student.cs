using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Models
{
    internal class Student : Person
    {

        public Student(int id, string name,string password): base(id, name, password) { }

        public override string ToString()
        {
            return $"{Id},{Name},{password}";
        }
        public static Student FromCsv(string csv)
        {
            var values = csv.Split(',');
            if(values.Length < 3 ) {
                throw new FormatException("there is empty field ,please enter all data");
            }
            return new Student(int.Parse(values[0]), values[1], values[2]);
        }

    }
}
