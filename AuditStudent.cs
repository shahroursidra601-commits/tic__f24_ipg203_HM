using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG203_TIC
{
    public class AuditStudent : Student
    {
        // Constructor - delegates initialization to base Student
        public AuditStudent(string name, string course) : base(name, course) { }

        // For audit students we take the top 2 marks (or fewer if not available) and average them
        public override double CalculateFinalGrade()
        {
            double final = 0;

            if (Marks.Count > 0)
            {
                // Sort descending and average the top two marks
                Marks.Sort();
                Marks.Reverse();

                int count = Marks.Count >= 2 ? 2 : Marks.Count;
                double sum = 0;

                for (int i = 0; i < count; i++)
                {
                    sum += Marks[i];
                }

                final = sum / count;
            }

            CheckAndRaiseAlerts(final);
            return final;
        }

        // Return the top N marks for this student 
        public List<double> GetTopMarks(int n)
        {
            var copy = new List<double>(Marks);
            copy.Sort();
            copy.Reverse();
            if (n <= 0 || copy.Count == 0) return new List<double>();
            if (n >= copy.Count) return copy;
            return copy.GetRange(0, n);
        }

        // Simple helper that identifies the student type
        public string GetStudentType()
        {
            return "Audit";
        }

        // Count how many marks are currently recorded
        public int TopMarksCount()
        {
            return Marks == null ? 0 : Marks.Count;
        }
    }
}