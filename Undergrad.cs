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

namespace StudentDB
{
    public enum YearRank
    {
        Freshman = 1,
        Sophomore = 2,
        Junior = 3,
        Senior = 4
    }

    internal class Undergrad : Student
    {
        public YearRank Rank { get; set; }
        public string DegreeMajor { get; set; }

        public Undergrad (string first, string last, double gpa,
                          string email, DateTime enrolled, 
                          YearRank rank, string degree)
            : base (new ContactInfo(first, last, email), gpa, enrolled)
        {
            Rank = rank;
            DegreeMajor = degree;
        }

        // C# lamda expression
        public override string ToString() => base.ToString() + $"      Year: {Rank}\n     Major: {DegreeMajor}";

        public override string ToStringForOutputFile()
        {
            string str = this.GetType() + "\n";
            str += base.ToStringForOutputFile();
            str += $"{Rank}\n";
            str += $"{DegreeMajor}";

            return str;
        }
    }
}
