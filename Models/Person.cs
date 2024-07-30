using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Models
{
    internal class Person
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string password { get; set; }

        public Person(int id,string name ,string password) {
            this.Id= id;
            this.Name = name;
            this.password = password;
        }
    }
}
