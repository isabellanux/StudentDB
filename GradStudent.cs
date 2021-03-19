//////////////////////////////////////////////////////////////////////////////////////////////////////////
// Date                  Developer                  Description
// 2021-02-10            bellatn                    --created StudentDB and began working alongside video 10a
// 2021-02-10            bellatn                    --continued working alongside video 10b
// 2021-02-11            bellatn                    --created switch loop for the main menu and begin adding design elements
// 2021-02-18            bellatn                    --touched up switch loop in RunDatabaseApp (StudentApp class)
// 2021-02-18            bellatn                    --created GradStudent and Undergrad classes and added corresponding file info
// 2021-02-23            bellatn                    --deleted TestMain method to avoid any furthur complications
// 2021-02-23            bellatn                    --touched up case e and case f menu option methods
// 2021-02-23            bellatn                    --created 4 methods for CRUD operations (create, read, update and delete)
// 2021-02-24            bellatn                    --added gray hat backdoor access
// 2021-02-24            bellatn                    --changed INPUTFILE to STUDENTDB_DATAFILE and delelted OUTPUTFILE
// 2021-02-24            bellatn                    --touched up code + added comments + change history
// 2021-02-25            bellatn                    --added final details + submitted assignment

using System;
using System.Text;

namespace StudentDB
{
    internal class GradStudent : Student
    {
        public decimal TuitionCredit { get; set; }
        public string FacultyAdvisor { get; set; }

        public GradStudent(string first, string last, double gpa, 
                           string email, DateTime enrollment, 
                           decimal credit, string advisor) 
            : base (new ContactInfo(first, last, email), gpa, enrollment) // base = ref to parent element - receives variables and passes back to parent
        {
            TuitionCredit = credit;
            FacultyAdvisor = advisor;
        }

        // an example of a C# lamda expression, => reads goes to (returns)
        public override string ToString() => base.ToString() + $"    Credit: {TuitionCredit:C}\n       Fac: {FacultyAdvisor}";

        public override string ToStringForOutputFile()
        {
            StringBuilder str = new StringBuilder(this.GetType() + "\n");
            str.Append(base.ToStringForOutputFile());

            str.Append($"{TuitionCredit}\n");
            str.Append($"{FacultyAdvisor}"); // no control line feed because called from WriteLine

            return str.ToString();
        }
    }
}
