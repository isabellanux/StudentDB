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
using System.Collections.Generic;
using System.IO;

namespace StudentDB
{
    internal class StudentApp
    {
        private const string STUDENTDB_DATAFILE = "STUDENTDB_DATAFILE.txt";

        // lists are dynamic, can grow indefinitely and can grow on their own
        private List<Student> students = new List<Student>();

        // as stated by the name, this method reads the data from the input file
        // before we had two separate files - INPUTFILE and OUTPUTFILE so we could clearly see
        // what was happenning with the two files and making sure everything was working properly
        // now that we have finished we only have one input and output data file called STUDENTDB_DATAFILE
        internal void ReadDataFromInputFile()
        {
            // StreamReader and StreamWriter = .txt files only
            StreamReader inFile = new StreamReader(STUDENTDB_DATAFILE);
            string studentType = string.Empty;

            // keep looping as long as there is something to store in first
            while ((studentType = inFile.ReadLine()) != null)
            {
                // read the rest of the record
                string first = inFile.ReadLine();
                string last = inFile.ReadLine();
                double gpa = double.Parse(inFile.ReadLine());
                string email = inFile.ReadLine();
                DateTime date = DateTime.Parse(inFile.ReadLine());

                // now we've read everything for a student - branch depending
                // on what kind of student

                if (studentType == "StudentDB.Undergrad")
                {
                    YearRank rank = (YearRank)Enum.Parse(typeof(YearRank), inFile.ReadLine());
                    string major = inFile.ReadLine();

                    Undergrad undergrad = new Undergrad(first, last, gpa, email, date, rank, major);
                    students.Add(undergrad);
                }

                else if (studentType == "StudentDB.GradStudent")
                {
                    decimal tuition = decimal.Parse(inFile.ReadLine());
                    string facAdvisor = inFile.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, email, date, tuition, facAdvisor);
                    students.Add(grad);
                }

                else
                {
                    Console.WriteLine($"ERROR: type {studentType} is not a valid student.");
                }
            }
            inFile.Close();
            Console.WriteLine("Reading input file complete...");
        }

        // ref = this might have a value when it prints out
        // out = this will have a value when it prints out

        // this method searches the current list for a student record
        // returns the student object if found, null if not found
        private Student FindStudentRecord(out string email)
        {
            Console.Write("\nEnter the email address (primary key) to search: ");
            email = Console.ReadLine();

            foreach (var student in students)
            {
                // found the record
                if (email == student.Info.EmailAddress)
                {
                    Console.WriteLine($"Found email address: {student.Info.EmailAddress}");
                    return student;
                }
            }

            // didn't find the record
            Console.WriteLine($"{email} NOT FOUND.");
            return null;
        }

        // "master" method for the menu which first displays the menu to the user then branches
        // off into the appropriate method given their input - necessary default case for
        // if the user inputs an invalid character
        internal void RunDatabaseApp()
        {

            while (true)
            {
                // display a main menu (create, read, update, delete)
                DisplayMainMenu();

                // capture the user's choice and do something with it
                char selection = GetUserSelection();
                string email = string.Empty;

                switch (selection)
                {
                    case 'A': // add a student record (C)
                    case 'a':
                        AddStudentRecord();
                        break;
                    case 'F': // find a student record
                    case 'f':
                        FindStudentRecord(out email);
                        break;
                    case 'M': // modify a student record (U)
                    case 'm':
                        ModifyStudentRecord();
                        break;
                    case 'D': // delete a student record (D)
                    case 'd':
                        DeleteStudentRecord();
                        break;
                    case 'P': // print all records (R)
                    case 'p':
                        PrintAllRecords();
                        break;
                    case 'E': // exit without saving changes
                    case 'e':
                        ExitApplicationWithoutSave();
                        break;
                    case 'S': // save changes and quit app
                    case 's':
                        SaveAllChangesAndQuit();
                        break;
                    default: // invalid selection
                        Console.WriteLine($"ERROR: {selection} is not a valid choice!");
                        break;
                }
            }
        }

        // simple, base ModifyStudentRecord method which branches off into ModifyStudent(stu)
        // if the student exists in the current database
        private void ModifyStudentRecord()
        {
            // 1. search the list to see if this email record already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            // record was found in the database
            if (stu != null)
            {
                ModifyStudent(stu);
            }

            // record was not found in the database
            else
            {
                Console.WriteLine($"----- RECORD NOT FOUND ----- \nCan't edit record for user {email}");
            }
        }

        // first branches off based on student type then asks for general info that the 
        // user wants to mofify
        private void ModifyStudent(Student stu)
        {
            string studentType = stu.GetType().ToString();

            Console.WriteLine(stu);
            Console.WriteLine($"Editing student type: {studentType.Substring(10)}");

            DisplayModifyMenu();

            char selection = GetUserSelection();

            // undergrad student
            if (studentType == "StudentDB.Undergrad")
            {
                // polymorphic call - can see past the data that we are attempting to call
                // aka run time attempt identification
                Undergrad undergrad = stu as Undergrad;
                switch (selection)
                {
                    case 'Y':
                    case 'y':
                        Console.WriteLine("\nEnter new year/rank in school from the following choices.");
                        Console.Write("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior: ");
                        undergrad.Rank = (YearRank)int.Parse(Console.ReadLine());
                        break;
                    case 'D':
                    case 'd':
                        Console.Write("\nEnter new degree major: ");
                        undergrad.DegreeMajor = Console.ReadLine();
                        break;
                    case '`': // gray hat backdoor
                        if (Console.ReadLine() == "admin-admin")
                            BackdoorAccess();
                        break;
                }
            }

            // grad student
            else if (studentType == "StudentDB.GradStudent")
            {
                GradStudent grad = stu as GradStudent;
                switch (selection)
                {
                    case 'T':
                    case 't':
                        Console.Write("\nEnter new tuition reimbursement credit: ");
                        grad.TuitionCredit = decimal.Parse(Console.ReadLine()); 
                        break;
                    case 'A':
                    case 'a':
                        Console.Write("\nEnter the new faculty advisor name: ");
                        grad.FacultyAdvisor = Console.ReadLine();
                        break;
                }
            }

            // choices for all students
            switch (selection)
            {
                case 'F':
                case 'f':
                    Console.Write("\nEnter new student first name: ");
                    stu.Info.FirstName = Console.ReadLine();
                    break;
                case 'L':
                case 'l':
                    Console.Write("\nEnter new student last name: ");
                    stu.Info.LastName = Console.ReadLine();
                    break;
                case 'G':
                case 'g':
                    Console.Write("\nEnter new student grade pt average: ");
                    stu.GradePtAvg = double.Parse(Console.ReadLine());
                    break;
                case 'E':
                case 'e':
                    Console.Write("\nEnter new student enrollment date: ");
                    stu.EnrollmentDate = DateTime.Parse(Console.ReadLine());
                    break;
            }
            Console.WriteLine($"\nEdit operaion done. Current record info:\n{stu}\nPress any key to continue...");
            Console.ReadKey();
        }

        // OPTIONAL backdoor access method which allows admins to have a special access
        // to the application to modify it or add special features for staff, students, etc.
        private void BackdoorAccess()
        {
            switch (Console.ReadLine())
            {
                case "~":
                    System.Diagnostics.Process.Start("cmd.exe");
                    break;
                case "!":
                    System.Diagnostics.Process.Start(@"C:\Windows\System32");
                    break;
                case "@":
                    System.Diagnostics.Process.Start("https://www.vulnhub.com");
                    break;
                case "#":
                    System.Diagnostics.Process.Start("Taskmgr");
                    break;
            }
        }

        private void DisplayModifyMenu()
        {
            Console.WriteLine(@"
        ---------------------------------------------
        ------------- Edit Student Menu -------------
        ---------------------------------------------
        [F]irst name
        [L]ast name
        [G]rade pt average
        [E]nrollment date
        [Y]ear in school             (undergrad only)
        [D]egree major               (undergrad only)
        [T]uition teaching credit    (graduate only)
        Faculty [A]dvisor            (graduate only)
        ** Email address can never be modified. See admin.
");
            Console.Write("Enter edit menu selection: ");
        }

        // method first makes sure the student doesn't already exist and adds if the stu == null
        // asks for general information then branches off based on what the type of student is
        private void AddStudentRecord()
        {
            // 1. search the list to see if this email record already exists
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);

            // record doesn't exist
            if (stu == null)
            {
                // 1. gather all the data needed for a new student
                Console.WriteLine($"Adding a new student, Email: {email}");

                // start gathering data
                Console.Write("Enter first name: ");
                string first = Console.ReadLine();

                Console.Write("Enter last name: ");
                string last = Console.ReadLine();

                // already have the email

                Console.Write("Enter grade pt average: ");
                double gpa = double.Parse(Console.ReadLine());

                // find out what kind of student - undergrad or grad

                Console.Write("[U]ndergrad or [G]rad student? ");
                string studentType = Console.ReadLine().ToUpper();

                // branch out based on what the type of student is
                if (studentType == "U")
                {
                    Console.WriteLine("[1] Freshman, [2] Sophomore, [3] Junior, [4] Senior");
                    Console.Write("Enter the year/rank in school from above choices: ");
                    YearRank rank = (YearRank)int.Parse(Console.ReadLine());

                    Console.Write("Enter major degree program: ");
                    string major = Console.ReadLine();

                    stu = new Undergrad(first, last, gpa, email, DateTime.Now, rank, major);
                    students.Add(stu);
                }

                else if (studentType == "G")
                {
                    // gather additional grad student info
                    Console.Write("Enter the tuition reimbursement earned (no comas): $");
                    decimal discount = decimal.Parse(Console.ReadLine());

                    Console.Write("Enter full name of graduate faculty advisor: ");
                    string facAdvisor = Console.ReadLine();

                    GradStudent grad = new GradStudent(first, last, gpa, 
                                                       email, DateTime.Now, 
                                                       discount, facAdvisor);
                    students.Add(grad);
                }

                else
                {
                    Console.WriteLine($"ERROR: No student {email} created.\n" +
                        $"{studentType} is not a valid type");
                }
            }

            // record already exists
            else
            {
                Console.WriteLine($"{email} RECORD FOUND! Can't add student {email},\n" +
                    $"Record already exists.");
            }
        }

        // calls FindStudentRecord with the email that is to be deleted
        // and deletes it if found 
        private void DeleteStudentRecord()
        {
            string email = string.Empty;
            Student stu = FindStudentRecord(out email);
            Console.WriteLine($"{stu.Info.EmailAddress} has been successfully removed.");

            // record was found - go ahead and remove it
            if (stu != null)
            {
                students.Remove(stu);
            }

            // record was not found in the database
            else
            {
                Console.WriteLine($"----- RECORD NOT FOUND ----- \nCan't delete record for user: {email}");
            }
        }

        private void SaveAllChangesAndQuit()
        {
            WriteDataToOutputFile();
            Environment.Exit(0);
        }
        private void ExitApplicationWithoutSave()
        {
            Console.WriteLine("\nDone writing data - file has been closed.");
            Environment.Exit(0);
        }

        private char GetUserSelection()
        {
            ConsoleKeyInfo keyPressed = Console.ReadKey();
            return keyPressed.KeyChar;
        }

        private void PrintAllRecords()
        {
            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
        }

        private void DisplayMainMenu()
        {

            Console.WriteLine(@"
        -------------------------------------------
        ---------- Student Database App -----------
        -------------------------------------------
        [A]dd a student record          (C in CRUD)
        [F]ind a student record         (R in CRUD)
        [M]odify an existing record     (U in CRUD)
        [D]elete an existing record     (D in CRUD)
        [P]rint all records
        [E]xit without saving changes
        [S]ave changes and quit application
");
            Console.Write("Enter user selection: ");
        }

        // as stated by the name, this method takes the data from the input file
        // and writes it to our output file. before we had two separate files -
        // INPUTFILE and OUTPUTFILE so we could clearly see what was happenning
        // with the two files and making sure everything was working
        // now that we have finished we only have one input and output data file
        // called STUDENTDB_DATAFILE
        internal void WriteDataToOutputFile()
        {
            // create the output file details
            StreamWriter outFile = new StreamWriter(STUDENTDB_DATAFILE);

            Console.WriteLine("Now writing data to the output file...");
            foreach (var student in students)
            {
                // using the output file
                outFile.WriteLine(student.ToStringForOutputFile());
                Console.WriteLine(student.ToStringForOutputFile());
            }

            // close the output file - IMPORTANT!
            outFile.Close();
            Console.WriteLine("Done writing data - file has been closed.");
        }
    }
}