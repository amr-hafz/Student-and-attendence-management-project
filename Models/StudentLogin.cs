using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Models
{
    internal class StudentLogin
    {

        public int StudentId { get; set; }
        public string StudentPassword { get; set;}
        public StudentLogin(int StudentId, string StudentPassword)
        {
            this.StudentId = StudentId; 
            this.StudentPassword = StudentPassword;
        }
    }
}
