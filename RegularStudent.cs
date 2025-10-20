using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG203_TIC
{
    public class RegularStudent : Student
    {
        // Simple attendance percentage field (0-100)
        public double AttendancePercentage { get; set; }

        public RegularStudent(string name, string course) : base(name, course)
        {
            AttendancePercentage = 100.0;
        }

        // Regular students: final grade is the simple average of marks
        public override double CalculateFinalGrade()
        {
            double final = 0;

            if (Marks.Count > 0)
            {
                double sum = 0;
                for (int i = 0; i < Marks.Count; i++)
                {
                    sum += Marks[i];
                }
                final = sum / Marks.Count;
            }

            CheckAndRaiseAlerts(final);
            return final;
        }

        public string GetStudentType()
        {
            return "Regular";
        }
    }
}
