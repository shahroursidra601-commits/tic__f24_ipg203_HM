using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG203_TIC
{
    public delegate void GradeAlertHandler(Student sender, string message);

    public abstract class Student : IReportable
    {
        // Static counter for assigning IDs
        private static int _idCounter = 0;
        // Readonly ID assigned at construction
        private readonly int _id;
        private string _name = string.Empty;
        private List<double> _marks = new List<double>();
        private readonly string _course = string.Empty;

        // Additional classic fields
        private string _email = string.Empty;
        private DateTime? _dateOfBirth = null;



        // Static property 
        public static int TotalStudents
        {
            get { return _idCounter; }
        }
        // Encapsulated fields exposed via properties
        public int Id
        {
            get
            {
                return _id;
            }
            // No Set Because it's readonly after initialization
        }
        //  email property with basic validation
        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                {
                    Console.WriteLine("Invalid email format.");
                }
                else
                {
                    _email = value;
                }
            }
        }

        //  date of birth
        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                try
                {
                    if (!DataValidator.IsValidName(value))
                    {
                        Console.WriteLine("Invalid name.");
                    }
                    else
                        _name = value;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Name set failed: {0}", (ex.Message));
                }
            }
        }

        public List<double> Marks
        {
            get { return _marks; }
        }

        public string Course
        {
            get { return _course; }
        }


        protected Student(string name, string course)
        {
            if (!DataValidator.IsValidName(name))
            {
                Console.WriteLine("Invalid name" + name);
            }
            else
            {
                _idCounter++;
                _id = _idCounter;
                _name = name;
                _course = course;
                _marks = new List<double>();
            }
        }

        public void AddMark(double mark)
        {
            if (!DataValidator.IsValidMark(mark))
            {
                Console.WriteLine("Invalid mark, must be between 0 and 100.");
                return;
            }

            _marks.Add(mark);
        }
        // Abstract method to be implemented by subclasses (inheritance & polymorphism)
        public abstract double CalculateFinalGrade();

        // Calculate a simple GPA scaled to 4.0 based on the average mark
        public double GetGPA()
        {
            if (Marks == null || Marks.Count == 0) return 0.0;
            double sum = 0;
            for (int i = 0; i < Marks.Count; i++)
            {
                sum += Marks[i];
            }
            double avg = sum / Marks.Count; // 0 - 100
            // Scale 0-100 to 0.0-4.0 (classic simple scaling)
            return Math.Round((avg / 100.0) * 4.0, 2);
        }

        // Clear all marks for the student
        public void ClearMarks()
        {
            _marks.Clear();
        }

        // Number of marks currently recorded for the student
        public int MarkCount
        {
            get { return Marks == null ? 0 : Marks.Count; }
        }

        // Age computed from DateOfBirth (returns null if DateOfBirth not set)
        public int? Age
        {
            get
            {
                if (!DateOfBirth.HasValue) return null;
                var today = DateTime.Today;
                int age = today.Year - DateOfBirth.Value.Year;
                if (DateOfBirth.Value.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        // Classic helper: check if the student is an adult (>= 18)
        public bool IsAdult()
        {
            var a = Age;
            return a.HasValue && a.Value >= 18;
        }

        public string GetInfo()
        {
            return "ID: " + Id + ", Name: " + Name + ", Course: " + Course;
        }

        public event GradeAlertHandler? GradeBelowThreshold;

        protected void CheckAndRaiseAlerts(double finalGrade)
        {
            if (finalGrade < 50)
            {
                if (GradeBelowThreshold != null)
                {
                    GradeBelowThreshold(this, "Final grade " + finalGrade + " is below threshold for " + Name);
                }
            }
        }
    }
}



