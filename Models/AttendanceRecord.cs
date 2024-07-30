using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Models
{
    internal class AttendanceRecord
    {
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public bool Ispresent { get; set; }


        public AttendanceRecord(int studentId, DateTime date, bool ispresent)
        {
            StudentId = studentId;
            Date = date;
            Ispresent = ispresent;
        }
    }
}