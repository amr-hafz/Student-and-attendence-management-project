using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using pro1.Services;
using pro1.Models;
namespace Pro1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // لا اله الا الله ..||..محمد رسول الله--
            // صل علي سيدنا محمد
            var studentAuthService = new StudentAuthenticationService();
            var attendanceManager = new AttendanceManager();

            bool isAuthenticated = false;
            const int maxRetries = 3;
            int attempt = 0;

            while (!isAuthenticated && attempt < maxRetries)
            {

                Console.WriteLine("Enter Student ID:");
                if (!int.TryParse(Console.ReadLine(), out int studentId))//set id in studentId
                {
                    Console.WriteLine("Invalid Student ID.");
                    attempt++;
                    continue;
                }

                Console.WriteLine("Enter password:");
                var password = Console.ReadLine();

                if (studentAuthService.Authenticate(studentId, password))
                {
                    isAuthenticated = true;
                    Console.WriteLine($"Welcome, Student {studentId}");
                }
                else
                {
                    Console.WriteLine("Invalid Student ID or password.");
                    attempt++;
                    Console.WriteLine($"Attempts remaining: {maxRetries - attempt}");
                }

                if (attempt >= maxRetries)
                {
                    Console.WriteLine("Too many failed attempts. Exiting...");
                    return;
                }
            }

            string studentFilePath = "students.csv";
            string attendanceFilePath = "attendance.csv";

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("\t 1. Add Student");
                Console.WriteLine("\t 2. Show Students");
                Console.WriteLine("\t 3. Mark Attendance");
                Console.WriteLine("\t 4. Show All Attendance");
                Console.WriteLine("\t 5. Update Attendance");
                Console.WriteLine("\t 6. attendance records by date");
                Console.WriteLine("\t 7. attendance records by student ID");
                Console.WriteLine("\t 8. attendance records by presence status");
                Console.WriteLine("\t 9. Find attendance records by student ID");
                Console.WriteLine("\t 10. Find attendance records by date");
                Console.WriteLine("\t 11. Get Student");
                Console.WriteLine("\t 12. Exit");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        // Add Student
                        Console.WriteLine("Enter new Student ID:");
                        if (!int.TryParse(Console.ReadLine(), out int newStudentId))
                        {
                            Console.WriteLine("Invalid Student ID.");
                            continue;
                        }

                        if (attendanceManager.IsStudentIdExists(newStudentId))
                        {
                            Console.WriteLine("Student ID already exists.");
                            continue;
                        }

                        Console.WriteLine("Enter new Student Name:");
                        var newStudentName = Console.ReadLine();

                        Console.WriteLine("Enter new Student Password:");
                        var newStudentPassword = Console.ReadLine();

                        attendanceManager.AddStudent(new Student(newStudentId, newStudentName, newStudentPassword));
                        attendanceManager.SaveStudentsToCsv(studentFilePath);
                        Console.WriteLine("New student added and saved to CSV.");
                        break;
                    case "2":
                        attendanceManager.LoadStudentsFromCsv(studentFilePath);
                        var StudentsRecords = attendanceManager.GetStudentRecords();
                        Console.WriteLine("\nAll Attendance Records:\n");
                        foreach (var record in StudentsRecords)
                        {
                            Console.WriteLine($"StudentId: {record.Id}, Name: {record.Name}, Password: {record.password}");
                        }
                        break;

                    case "3":

                        attendanceManager.loadFromCsv(attendanceFilePath);
                        Console.WriteLine("Enter new Student ID:");
                        if (!int.TryParse(Console.ReadLine(), out int IdOfStudent))
                        {
                            Console.WriteLine("Invalid Student ID.");
                            continue;
                        }

                        if (!attendanceManager.IsStudentIdExists(IdOfStudent))
                        {
                            Console.WriteLine("Student ID Not exists.");
                            continue;
                        }

                        Console.WriteLine("Date : ");
                        int day, month, year;
                        Console.Write($"Day => ");
                        day = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Month => ");
                        month = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Year => ");
                        year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("IsAbsent : y/n");
                        string Attendance = (Console.ReadLine()).ToLower();
                        bool IsAbsent = Attendance == "y" ? true : false;
                        attendanceManager.markAttendencce(IdOfStudent, new DateTime(year == null ? 0 : year, month == null ? 0 : month, day == null ? 0 : day), IsAbsent);
                        // Save attendance to CSV
                        attendanceFilePath = "attendance.csv";
                        attendanceManager.SaveToCsv(attendanceFilePath);
                        Console.WriteLine("Attendance saved to CSV.");
                        break;

                    case "4":
                        attendanceManager.loadFromCsv(attendanceFilePath);
                        var AttendanceRecords = attendanceManager.GetAttendanceRecords();
                        Console.WriteLine("\nAll Attendance Records:\n");
                        foreach (var record in AttendanceRecords)
                        {
                            Console.WriteLine($"StudentId: {record.StudentId}, Date: {record.Date}, IsPresent: {record.Ispresent}");
                        }
                        break;
                    case "5":
                        // Load attendance records from CSV
                        attendanceManager.loadFromCsv(attendanceFilePath);
                        Console.WriteLine("Enter new Student ID:");
                        if (!int.TryParse(Console.ReadLine(), out int IdOfUpdatedStudent))
                        {
                            Console.WriteLine("Invalid Student ID.");
                            continue;
                        }

                        if (!attendanceManager.IsStudentIdExists(IdOfUpdatedStudent))
                        {
                            Console.WriteLine("Student ID Not exists.");
                            continue;
                        }

                        Console.WriteLine("Date : ");
                        Console.Write($"Day => ");
                        day = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Month => ");
                        month = Convert.ToInt32(Console.ReadLine());
                        Console.Write($"Year => ");
                        year = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine("IsAbsent : y/n");
                        string UpdatedAttendance = (Console.ReadLine()).ToLower();
                        bool UpdatedIsAbsent = UpdatedAttendance == "y" ? true : false;
                        // Update attendance record
                        attendanceManager.UpdateAttendance(IdOfUpdatedStudent, new DateTime(year == null ? 0 : year, month == null ? 0 : month, day == null ? 0 : day), UpdatedIsAbsent);
                        Console.WriteLine("\nUpdated attendance record for StudentId 2:");
                        var updatedOldRecord = attendanceManager.FindAttendanceRecordsByStudentId(2).FirstOrDefault(r => r.Date.Date == DateTime.Now.Date);
                        if (updatedOldRecord != null)
                        {
                            Console.WriteLine($"Date: {updatedOldRecord.Date}, IsPresent: {updatedOldRecord.Ispresent}");
                        }
                        break;
                    case "6":

                        attendanceManager.loadFromCsv(attendanceFilePath);

                        var AttendancesortedByDate = attendanceManager.SortAttendanceRecordsByDate();
                        Console.WriteLine("\nSorted Attendance Records by Date:\n");
                        foreach (var record in AttendancesortedByDate)
                        {
                            Console.WriteLine($"Date: {record.Date}, StudentId: {record.StudentId}, IsPresent: {record.Ispresent}");
                        }
                        break;
                    case "7":
                        attendanceManager.loadFromCsv(attendanceFilePath);

                        var AttendancesortedByStudentId = attendanceManager.SortAttendanceRecordsByStudentId();
                        Console.WriteLine("\nSorted Attendance Records by Student ID:\n");
                        foreach (var record in AttendancesortedByStudentId)
                        {
                            Console.WriteLine($"StudentId: {record.StudentId}, Date: {record.Date}, IsPresent: {record.Ispresent}");
                        }
                        break;
                    case "8":
                        attendanceManager.loadFromCsv(attendanceFilePath);

                        var AttendancesortedByPresence = attendanceManager.SortAttendanceRecordsByPresence();
                        Console.WriteLine("\nSorted Attendance Records by Presence:\n");
                        foreach (var record in AttendancesortedByPresence)
                        {
                            Console.WriteLine($"IsPresent: {record.Ispresent}, StudentId: {record.StudentId}, Date: {record.Date}");
                        }
                        break;

                    case "9":
                        Console.Write("Student Id : ");
                        var id = Convert.ToInt32(Console.ReadLine());
                        var studentDataRecords = attendanceManager.FindAttendanceRecordsByStudentId(id);
                        Console.WriteLine($"\nAttendance records for StudentId {id}:");
                        foreach (var record in studentDataRecords)
                        {
                            Console.WriteLine($"Date: {record.Date}, IsPresent: {record.Ispresent}");
                        }
                        break;

                    case "10":
                        var DateRecords = attendanceManager.FindAttendanceRecordsByDate(DateTime.Now);
                        Console.WriteLine($"\nAttendance records for {DateTime.Now.ToShortDateString()}:");
                        foreach (var record in DateRecords)
                        {
                            Console.WriteLine($"StudentId: {record.StudentId}, IsPresent: {record.Ispresent}");
                        }
                        break;
                    case "11":
                        
                        ViewAllStudents(attendanceManager);
                        break;
                    case "12":
                        // Exit
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;

                }
            }

            attendanceManager.loadFromCsv(attendanceFilePath);
            attendanceManager.AddStudent(new Student(1, "Alice", "1234"));
            attendanceManager.AddStudent(new Student(2, "Bob", "1234"));
            attendanceManager.AddStudent(new Student(3, "Charlie", "1234"));

            attendanceManager.markAttendencce(1, DateTime.Now, true);
            attendanceManager.markAttendencce(2, DateTime.Now, false);
            attendanceManager.markAttendencce(3, DateTime.Now, true);

            attendanceManager.markAttendencce(1, new DateTime(2024, 7, 20), true);
            attendanceManager.markAttendencce(2, new DateTime(2024, 7, 20), false);
            attendanceManager.markAttendencce(3, new DateTime(2024, 7, 20), true);


            attendanceManager.SaveStudentsToCsv(studentFilePath);
            Console.WriteLine("Students saved to CSV.");

            
            string filePath = "attendance.csv";
            attendanceManager.SaveToCsv(filePath);
            Console.WriteLine("Attendance saved to CSV.");



            attendanceManager.loadFromCsv(filePath);
            Console.WriteLine("Attendance loaded from CSV.");

            // Display attendance records
            var records = attendanceManager.GetAttendanceRecords();
            foreach (var record in records)
            {
                Console.WriteLine($"StudentId: {record.StudentId}, Date: {record.Date}, IsPresent: {record.Ispresent}");
            }

            var studentRecords = attendanceManager.FindAttendanceRecordsByStudentId(1);
            Console.WriteLine("\nAttendance records for StudentId 1:");
            foreach (var record in studentRecords)
            {
                Console.WriteLine($"Date: {record.Date}, IsPresent: {record.Ispresent}");
            }



        }
        private static void ViewAllStudents(AttendanceManager attendanceManager)
        {
            Console.WriteLine("\nSelect a sorting option:");
            Console.WriteLine("1. Sort by ID");
            Console.WriteLine("2. Sort by Name");

            var sortChoice = Console.ReadLine();
            List<Student> sortedStudents = new List<Student>();

            switch (sortChoice)
            {
                case "1":
                    sortedStudents = attendanceManager.SortStudentsById();
                    break;
                case "2":
                    sortedStudents = attendanceManager.SortStudentsByName();
                    break;

                default:
                    Console.WriteLine("Invalid choice. Defaulting to sorting by ID.");
                    sortedStudents = attendanceManager.SortStudentsById();
                    break;
            }

            Console.WriteLine("Students:");
            foreach (var student in sortedStudents)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Password: {student.password}");
            }

        }
    } }
    



