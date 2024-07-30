using pro1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Services
{
    internal class AttendanceManager
    {
        private List<Student> _students;
        private List<AttendanceRecord> _attendanceRecords;

        public AttendanceManager() {
            _students = new List<Student>();
            _attendanceRecords = new List<AttendanceRecord>();
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }
        public void markAttendencce(int id,DateTime date,bool ispresent )
        {
            _attendanceRecords.Add(new AttendanceRecord(id, date, ispresent));
        }

        public List<AttendanceRecord> GetAttendanceRecords()
        {
            return _attendanceRecords;
        }
        public List<Student> GetStudentRecords()
        {
            return _students;
        }
        public List<AttendanceRecord> FindAttendanceRecordsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _attendanceRecords.Where(record => record.Date.Date >= startDate.Date && record.Date.Date <= endDate.Date).ToList();
        }
        public List<AttendanceRecord> FindAttendanceRecordsByStudentId(int studentId)
        {
            return _attendanceRecords.Where(record => record.StudentId== studentId).ToList();
        }
        public List<AttendanceRecord> FindAttendanceRecordsByDate(DateTime date)
        {
            return _attendanceRecords.Where(record => record.Date.Date == date.Date).ToList();
        }
        public void UpdateAttendance(int studentId, DateTime date, bool isPresent)
        {
            var record = _attendanceRecords
                .FirstOrDefault(r => r.StudentId == studentId && r.Date.Date == date.Date);
            if (record != null)
            {
                record.Ispresent = isPresent;
            }
        }
        public void SaveToCsv(string filePath)
        {
            CsvHelper.SaveAttendanceRecordsToCsv(_attendanceRecords, filePath);

        }
        public void loadFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                return;
            _attendanceRecords.Clear();
            using(var reader= new StreamReader(filePath))
            {

                bool firline = true;
                string line;
                while((line=reader.ReadLine())!=null)
                {
                    if(firline)
                    {
                        firline = false;
                        continue;
                    }
                    var values =line.Split(',');

                    if (values.Length < 3)
                    {
                        Console.WriteLine($"Skipping invalid attendance line: {line}");
                        continue;
                    }
                    try
                    {
                        _attendanceRecords.Add(new AttendanceRecord(
                            int.Parse(values[0]),
                            DateTime.Parse(values[1]),
                            bool.Parse(values[2])
                        ));
                    }
                    catch (FormatException ex) {
                        Console.WriteLine($"Skipping invalid attendance line: {line}. Error: {ex.Message}");
                    }



                }
                


            }

        }

        public bool IsStudentIdExists(int studentId)
        {
            return _students.Any(s => s.Id == studentId);
        }
        public void SaveStudentsToCsv(string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var student in _students)
                {
                    writer.WriteLine(student.ToString());
                }
            }
        }
        public void LoadStudentsFromCsv(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            _students.Clear();
            using (var reader = new StreamReader(filePath))
            {
                bool isHeader = true;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false; 
                        continue;
                    }

                    try
                    {
                        _students.Add(Student.FromCsv(line));
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Skipping invalid student line: {line}. Error: {ex.Message}");
                    }
                }
            }
        }
        public List<AttendanceRecord> SortAttendanceRecordsByDate()
        {
            return _attendanceRecords.OrderBy(record => record.Date).ToList();
        }
        public List<AttendanceRecord> SortAttendanceRecordsByStudentId()
        {
            return _attendanceRecords.OrderBy(record => record.StudentId).ToList();
        }
        public List<AttendanceRecord> SortAttendanceRecordsByPresence()
        {
            return _attendanceRecords.OrderBy(record => record.Ispresent).ToList();
        }
        public List<Student> SortStudentsById()
        {
            return _students.OrderBy(student => student.Id).ToList();
        }
        public List<Student> SortStudentsByName()
        {
            return _students.OrderBy(student => student.Name).ToList();
        }
    }
}
