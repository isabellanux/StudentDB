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

namespace StudentDB
{
    // groups three "global" string variables into one global variable to avoid repitition
    public class ContactInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        private string emailAddress;

        public ContactInfo (string first, string last, string email)
        {
            FirstName = first;
            LastName = last;
            EmailAddress = email;
        }

        public string EmailAddress
        {
            get
            {
                return emailAddress;
            }

            set
            {
                // passed in email must have at least 3 characters and one must be "@"
                if (value.Contains("@") && value.Length > 3)
                {
                    emailAddress = value;
                }

                else
                {
                    emailAddress = "ERROR: Invalid email.";
                }
            }
        }

        // lamda expression for user friendly printing of the contact info
        public override string ToString() => $"{FirstName}\n{LastName}\n{EmailAddress}\n";

        public string ToStringLegal() => $"{LastName}, {FirstName}\n{EmailAddress}\n";

    }
}