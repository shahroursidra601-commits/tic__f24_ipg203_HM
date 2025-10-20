using IPG203_TIC;

namespace MyIPG203_F24_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {

                Console.WriteLine("=== Student Assessment System (demo of fields/properties/methods) ===");

                StudentGroup group = new StudentGroup();

                // Regular student: set email and date of birth, add marks
                RegularStudent s1 = new RegularStudent("Ali", "Math");
                s1.Email = "ali@example.com";
                s1.DateOfBirth = new DateTime(2004, 5, 12);
                s1.AddMark(40);
                s1.AddMark(55);

                // Scholarship student: demonstrate EvaluateScholarship and IsEligible
                ScholarshipStudent s2 = new ScholarshipStudent("Sara", "Math");
                s2.Email = "sara@student.edu";
                s2.DateOfBirth = new DateTime(2003, 3, 2);
                s2.AddMark(80);
                s2.AddMark(90);
                s2.AddMark(85);
                s2.AddMark(70);

                // Audit student: add several marks and show top marks
                AuditStudent s3 = new AuditStudent("Khaled", "Math");
                s3.DateOfBirth = new DateTime(2005, 8, 20);
                s3.AddMark(45);
                s3.AddMark(48);
                s3.AddMark(78);
                s3.AddMark(88);
                s3.AddMark(60);
                s3.AddMark(90);

                // Directly attach an event handler to demonstrate events
                s1.GradeBelowThreshold += (sender, msg) => Console.WriteLine("[ALERT] " + msg);

                // Add to group (StudentGroup also wires events internally)
                group.AddStudent(s1);
                group.AddStudent(s2);
                group.AddStudent(s3);

                // Use properties and helper methods to print details
                Console.WriteLine("\n-- Student Details --");
                foreach (var st in group.Students)
                {
                    string info = st.GetInfo();
                    string marksSummary = st.MarkCount + " marks";
                    string gpa = st.GetGPA().ToString();
                    string age = st.DateOfBirth.HasValue ? st.Age?.ToString() ?? "-" : "unknown";
                    string adult = st.IsAdult() ? "Adult" : "Minor";
                    Console.WriteLine(info + " | " + marksSummary + " | GPA: " + gpa + " | Age: " + age + " (" + adult + ")");
                }

                // Process all to compute and show final grades (and trigger any alerts)
                Console.WriteLine("\n-- Processing final grades --");
                group.ProcessAll();

                // Scholarship evaluation example
                Console.WriteLine("\n-- Scholarship evaluation (Sara) --");
                bool awarded = s2.EvaluateScholarship(75, 1000.0);
                Console.WriteLine("Sara eligible: " + awarded + " | Amount: " + s2.ScholarshipAmount);

                // Revoke scholarship and show IsEligible
                s2.RevokeScholarship();
                Console.WriteLine("After revoke, Sara eligible: " + s2.IsEligible + " | Amount: " + s2.ScholarshipAmount);

                // Audit student top marks
                Console.WriteLine("\n-- Audit student top marks (Khaled) --");
                Console.WriteLine("Top marks count: " + s3.TopMarksCount());
                var top3 = s3.GetTopMarks(3);
                Console.WriteLine("Top 3 marks: " + string.Join(", ", top3));

                // Demonstrate group helpers: summary and remove by name
                Console.WriteLine("\nGroup summary: " + group.GetSummary());
                int removed = group.RemoveStudentByName("Ali");
                Console.WriteLine("Removed by name 'Ali': " + removed + " | New summary: " + group.GetSummary());

                Console.WriteLine("Total Students in System: " + StudentGroup.TotalStudentsAcrossSystem);

                Console.WriteLine("=== Program End ===");
            }
        }
    }
