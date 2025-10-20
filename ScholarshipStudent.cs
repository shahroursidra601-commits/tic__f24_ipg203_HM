using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPG203_TIC
{
    public class ScholarshipStudent : Student
    {
        // Scholarship amount in dollars (simple field)
        public double ScholarshipAmount { get; private set; }

        public ScholarshipStudent(string name, string course) : base(name, course)
        {
            ScholarshipAmount = 0.0;
        }

        // Final grade: last mark is considered exam, weighted 60% exam, 40% other work
        public override double CalculateFinalGrade()
        {
            double final = 0;

            if (Marks.Count > 0)
            {
                double exam = Marks[Marks.Count - 1];
                double othersSum = 0;
                for (int i = 0; i < Marks.Count - 1; i++)
                {
                    othersSum += Marks[i];
                }

                double othersAvg = 0;
                if (Marks.Count > 1)
                {
                    othersAvg = othersSum / (Marks.Count - 1);
                }

                final = (exam * 0.6) + (othersAvg * 0.4);
            }

            CheckAndRaiseAlerts(final);
            return final;
        }

        // Determine scholarship eligibility and set amount (classic rules)
        public bool EvaluateScholarship(double threshold, double amount)
        {
            double final = CalculateFinalGrade();
            if (final >= threshold)
            {
                ScholarshipAmount = amount;
                return true;
            }
            ScholarshipAmount = 0.0;
            return false;
        }

        // Reset scholarship amount to zero (classic administrative action)
        public void RevokeScholarship()
        {
            ScholarshipAmount = 0.0;
        }

        // Convenience property indicating current eligibility (simple rule)
        public bool IsEligible
        {
            get { return ScholarshipAmount > 0.0; }
        }

        public string GetStudentType()
        {
            return "Scholarship";
        }
    }
}
