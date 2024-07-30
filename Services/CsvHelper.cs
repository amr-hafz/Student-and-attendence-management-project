using pro1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pro1.Services
{
    internal class CsvHelper
    {
        public static void SaveAttendanceRecordsToCsv(List<AttendanceRecord> records, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine("StudentId,Date,IsPresent");
                foreach (var record in records)
                {
                    writer.WriteLine($"{record.StudentId},{record.Date},{record.Ispresent}");
                }
            }
        }
        public static List<AttendanceRecord> LoadAttendanceRecordsFromCsv(string filePath)
        {
            var records = new List<AttendanceRecord>();
            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); // Skip header line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    records.Add(new AttendanceRecord(
                        int.Parse(values[0]),
                        DateTime.Parse(values[1]),
                        bool.Parse(values[2])
                    ));
                }
            }
            return records;
        }
    }
}
