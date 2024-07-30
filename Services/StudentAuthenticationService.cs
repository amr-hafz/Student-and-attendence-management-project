using pro1.Interfaces;
using pro1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Services
{
    internal class StudentAuthenticationService : IStudentAuthenticationService
    {
        private List<StudentLogin> _studentLogins;

        public StudentAuthenticationService()
        {
            
            _studentLogins = new List<StudentLogin>
            {
                new StudentLogin(1, "1234"),
                new StudentLogin(2, "pass1"),
                new StudentLogin(3, "pass2")
            };
        }
        public bool Authenticate(int studentId, string password)
        {
            var studentLogin = _studentLogins.FirstOrDefault(s => s.StudentId == studentId && s.StudentPassword == password);
            return studentLogin != null;
        }
    }

}