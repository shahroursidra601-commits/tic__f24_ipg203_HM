using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG203_TIC
{
    public class StudentGroup
    {
        private List<Student> _students = new List<Student>();

        public List<Student> Students
        {
            get { return _students; }
        }

        public void AddStudent(Student s)
        {
            _students.Add(s);
            s.GradeBelowThreshold += OnGradeAlert;
        }

        // Remove student by ID, returns true if removed
        public bool RemoveStudentById(int id)
        {
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].Id == id)
                {
                    _students[i].GradeBelowThreshold -= OnGradeAlert;
                    _students.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        // Remove students by exact name, returns how many were removed
        public int RemoveStudentByName(string name)
        {
            int removed = 0;
            for (int i = _students.Count - 1; i >= 0; i--)
            {
                if (_students[i].Name == name)
                {
                    _students[i].GradeBelowThreshold -= OnGradeAlert;
                    _students.RemoveAt(i);
                    removed++;
                }
            }
            return removed;
        }

        // Return a simple summary string for the group
        public string GetSummary()
        {
            return "Students: " + Count + ", Average GPA: " + GetAverageGPA().ToString("0.00");
        }

        private double GetAverageGPA()
        {
            if (_students.Count == 0) return 0.0;
            double sum = 0;
            for (int i = 0; i < _students.Count; i++) sum += _students[i].GetGPA();
            return sum / _students.Count;
        }

        // Find students by name (exact match) and return list
        public List<Student> FindStudentByName(string name)
        {
            var result = new List<Student>();
            for (int i = 0; i < _students.Count; i++)
            {
                if (_students[i].Name == name)
                {
                    result.Add(_students[i]);
                }
            }
            return result;
        }

        // Simple count property
        public int Count
        {
            get { return _students.Count; }
        }

        private void OnGradeAlert(Student sender, string message)
        {
            Console.WriteLine("EVENT: " + message);
        }

        public void ProcessAll()
        {
            for (int i = 0; i < _students.Count; i++)
            {
                Student s = _students[i];
                double finalGrade = s.CalculateFinalGrade();
                Console.WriteLine(s.GetInfo() + " | Final Grade: " + finalGrade);
            }
        }

        public static int TotalStudentsAcrossSystem
        {
            get { return Student.TotalStudents; }
        }
    }
}
