using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Interfaces
{
    internal interface IStudentAuthenticationService
    {
        bool Authenticate(int studentId, string password);
    }
}
