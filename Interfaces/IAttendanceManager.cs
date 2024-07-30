using pro1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Interfaces
{
    internal interface IAttendanceManager
    {
        void AddStudent(Student student);
        void MarkAttendance(int studentId, DateTime date, bool isPresent);
        List<AttendanceRecord> GetAttendanceRecords();
        List<AttendanceRecord> FindAttendanceRecordsByStudentId(int studentId);
        List<AttendanceRecord> FindAttendanceRecordsByDate(DateTime date);
        void UpdateAttendance(int studentId, DateTime date, bool isPresent);
        void SaveToCsv(string filePath);
        void LoadFromCsv(string filePath);
        
        List<AttendanceRecord> SortAttendanceRecordsByDate();
        List<AttendanceRecord> SortAttendanceRecordsByStudentId();
        List<AttendanceRecord> SortAttendanceRecordsByPresence();
    }
}
