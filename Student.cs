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
    internal class Student // : object
    {
        // definition of the data stored in a POCO student class object
        public ContactInfo Info { get; set; }
        public DateTime EnrollmentDate { get; set; }

        private double gradePtAvg;

        // replace the do nothning no-arg constructor
        // public Student()
        // {
        // }

        // overloading the constructor for student class
        // fully specified constructor for making student POCO objects
        public Student (ContactInfo info, double grades, DateTime enrolled)
        {
            Info = info;
            GradePtAvg = grades;
            EnrollmentDate = enrolled;
        }

        public double GradePtAvg
        {
            get
            {
                return gradePtAvg;
            }

            set
            {
                // only set the gpa if the passed in value is between 
                // "legal" defined gpa range 0 - 4 inclusive
                if (value >= 0 && value <= 4)
                {
                    gradePtAvg = value;
                }

                else
                {
                    gradePtAvg = 0.7;
                }
            }
        }

        // format a student object to a string for displaying student data
        // to the user in the user interface
        public override string ToString()
        {
            // create a temp string to hold the output
            string str = string.Empty;

            str += "\n\n---------- Student Record ----------\n";
            // build up the string with data from the object
            str += $"First Name: {Info.FirstName}\n";
            str += $" Last Name: {Info.LastName}\n";
            str += $"       GPA: {GradePtAvg}\n";
            str += $"     Email: {Info.EmailAddress}\n";
            str += $"  Enrolled: {EnrollmentDate}\n";

            // return the string
            return str;
        }

        // format a student object to a string for writing the data to the output file
        // virtual - this isn't the only ToStringForOuputFile method - lookout for another one in another class
        public virtual string ToStringForOutputFile()
        {
            // create a temp string to hold the output
            string str = string.Empty;

            // build up the string with data from the object
            str += $"{Info.FirstName}\n";
            str += $"{Info.LastName}\n";
            str += $"{GradePtAvg}\n";
            str += $"{Info.EmailAddress}\n";
            str += $"{EnrollmentDate}\n";

            // return the string
            return str;
        }
    }
}