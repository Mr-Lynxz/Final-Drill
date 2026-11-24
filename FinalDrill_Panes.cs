
using System;
using System.Collections.Generic;
using System.Linq;

// ------------------------------------
// STUDENT CLASS
// ------------------------------------
class Student
{
    public string StudentNumber { get; set; }
    public string Surname { get; set; }
    public string FirstName { get; set; }
    public string Occupation { get; set; }
    public string Gender { get; set; }
    public string CountryCode { get; set; }
    public string AreaCode { get; set; }
    public string PhoneNumber { get; set; }

    public Student(string studentNumber, string surname,
        string firstName, string occupation, string gender,
        string countryCode, string areaCode, string phoneNumber)
    {
        StudentNumber = studentNumber;
        Surname = surname;
        FirstName = firstName;
        Occupation = occupation;
        Gender = gender;
        CountryCode = countryCode;
        AreaCode = areaCode;
        PhoneNumber = phoneNumber;
    }

    public void DisplayInformation()
    {
        string pronoun = Gender == "M" ? "His" : "Her";

        Console.WriteLine(
            $"{FirstName} {Surname} is a {Occupation}. " +
            $"{pronoun} number is {CountryCode}-{AreaCode}-{PhoneNumber}"
        );
    }
}

// ------------------------------------
// ASEAN PHONEBOOK CLASS
// ------------------------------------
class ASEANPhonebook
{
    private List<Student> students = new List<Student>();

    private readonly Dictionary<string, string> countries =
        new Dictionary<string, string>()
    {
        { "60", "Malaysia" },
        { "62", "Indonesia" },
        { "63", "Philippines" },
        { "65", "Singapore" },
        { "66", "Thailand" }
    };

    // Check if country code is valid
    public bool IsValidCountryCode(string code)
    {
        return countries.ContainsKey(code);
    }

    // Find student using student number
    public Student FindStudent(string studentNumber)
    {
        return students.FirstOrDefault(
            s => s.StudentNumber == studentNumber
        );
    }

    // --------------------------------
    // STORE STUDENT
    // --------------------------------
    
public void AddStudent()
{
    string answer = "Y";

    do
    {
        Console.WriteLine("\n===== STORE TO ASEAN PHONEBOOK =====");

        Console.Write("Enter student number: ");
        string studentNumber = Console.ReadLine();

        if (FindStudent(studentNumber) != null)
        {
            Console.WriteLine("Student number already exists!");
            answer = "Y";
            continue;
        }

        Console.Write("Enter surname: ");
        string surname = Console.ReadLine();

        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter occupation: ");
        string occupation = Console.ReadLine();

        string gender;

        do
        {
            Console.Write("Enter gender (M/F): ");
            gender = Console.ReadLine().ToUpper();

            if (gender != "M" && gender != "F")
            {
                Console.WriteLine("Invalid gender. Enter M or F.");
            }

        } while (gender != "M" && gender != "F");

        string countryCode;

        do
        {
            Console.Write("Enter country code: ");
            countryCode = Console.ReadLine();

            if (!IsValidCountryCode(countryCode))
            {
                Console.WriteLine("Invalid country code!");
                Console.WriteLine("Available codes: 60, 62, 63, 65, 66");
            }

        } while (!IsValidCountryCode(countryCode));

        Console.Write("Enter area code: ");
        string areaCode = Console.ReadLine();

        Console.Write("Enter number: ");
        string phoneNumber = Console.ReadLine();

        Student newStudent = new Student(
            studentNumber,
            surname,
            firstName,
            occupation,
            gender,
            countryCode,
            areaCode,
            phoneNumber
        );

        students.Add(newStudent);

        Console.WriteLine("\nStudent successfully added!");

        Console.Write("Do you want to enter another entry [Y/N]? ");
        answer = Console.ReadLine().ToUpper();

    } while (answer == "Y");
}


    // --------------------------------
    // EDIT STUDENT
    // --------------------------------
    public void EditStudent()
    {
        Console.WriteLine("\n===== EDIT ASEAN PHONEBOOK =====");

        Console.Write("Enter student number: ");
        string studentNumber = Console.ReadLine();

        Student student = FindStudent(studentNumber);

        if (student == null)
        {
            Console.WriteLine("Error: Student not found!");
            return;
        }

        int choice;

        do
        {
            Console.WriteLine($"\nHere is the existing information about {student.StudentNumber}:");
            student.DisplayInformation();
            Console.WriteLine($"Gender: {student.Gender}");

            Console.WriteLine("\nWhich of the following information do you wish to change?");
            Console.WriteLine("[1] Student number");
            Console.WriteLine("[2] Surname");
            Console.WriteLine("[3] Gender");
            Console.WriteLine("[4] Occupation");
            Console.WriteLine("[5] Country code");
            Console.WriteLine("[6] Area code");
            Console.WriteLine("[7] Phone number");
            Console.WriteLine("[8] None - Go back to main menu");

            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice!");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter new student number: ");
                    string newStudentNumber = Console.ReadLine();

                    if (FindStudent(newStudentNumber) != null)
                    {
                        Console.WriteLine("Student number already exists!");
                    }
                    else
                    {
                        student.StudentNumber = newStudentNumber;
                    }
                    break;

                case 2:
                    Console.Write("Enter new surname: ");
                    student.Surname = Console.ReadLine();
                    break;

                case 3:
                    string newGender;

                    do
                    {
                        Console.Write("Enter new gender (M/F): ");
                        newGender = Console.ReadLine().ToUpper();

                    } while (newGender != "M" && newGender != "F");

                    student.Gender = newGender;
                    break;

                case 4:
                    Console.Write("Enter new occupation: ");
                    student.Occupation = Console.ReadLine();
                    break;

                case 5:
                    string newCountryCode;

                    do
                    {
                        Console.Write("Enter new country code: ");
                        newCountryCode = Console.ReadLine();

                        if (!IsValidCountryCode(newCountryCode))
                        {
                            Console.WriteLine("Invalid country code!");
                        }

                    } while (!IsValidCountryCode(newCountryCode));

                    student.CountryCode = newCountryCode;
                    break;

                case 6:
                    Console.Write("Enter new area code: ");
                    student.AreaCode = Console.ReadLine();
                    break;

                case 7:
                    Console.Write("Enter new phone number: ");
                    student.PhoneNumber = Console.ReadLine();
                    break;

                case 8:
                    Console.WriteLine("Returning to main menu...");
                    break;

                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }

            if (choice >= 1 && choice <= 7)
            {
                Console.WriteLine("Information successfully updated!");
            }

        } while (choice != 8);
    }

    // --------------------------------
    // SEARCH STUDENT BY COUNTRY
    // --------------------------------
    public void SearchByCountry()
    {
        Console.WriteLine("\n===== SEARCH ASEAN PHONEBOOK =====");

        Console.WriteLine("From which country:");
        Console.WriteLine("[1] Philippines");
        Console.WriteLine("[2] Thailand");
        Console.WriteLine("[3] Singapore");
        Console.WriteLine("[4] Indonesia");
        Console.WriteLine("[5] Malaysia");
        Console.WriteLine("[6] ALL");
        Console.WriteLine("[0] No More");

        Dictionary<string, string> selectedCountries =
            new Dictionary<string, string>()
        {
            { "1", "63" },
            { "2", "66" },
            { "3", "65" },
            { "4", "62" },
            { "5", "60" }
        };

        List<string> chosenCodes = new List<string>();

        bool allCountries = false;

        while (true)
        {
            Console.Write("\nEnter choice: ");
            string choice = Console.ReadLine();

            if (choice == "0")
            {
                break;
            }
            else if (choice == "6")
            {
                allCountries = true;
                break;
            }
            else if (selectedCountries.ContainsKey(choice))
            {
                string code = selectedCountries[choice];

                if (!chosenCodes.Contains(code))
                {
                    chosenCodes.Add(code);
                }
                else
                {
                    Console.WriteLine("Country already selected!");
                }
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }

        List<Student> results;

        if (allCountries)
        {
            results = students.ToList();
        }
        else
        {
            results = students
                .Where(s => chosenCodes.Contains(s.CountryCode))
                .ToList();
        }

        // Arrange students alphabetically by surname
        results = results
            .OrderBy(s => s.Surname)
            .ToList();

        Console.WriteLine("\n===== SEARCH RESULTS =====");

        if (results.Count == 0)
        {
            Console.WriteLine("No students found.");
            return;
        }

        foreach (Student student in results)
        {
            string country = countries[student.CountryCode];

            Console.WriteLine($"\nCountry: {country}");

            string pronoun = student.Gender == "M" ? "His" : "Her";

            Console.WriteLine(
                $"{student.Surname}, {student.FirstName}, " +
                $"with student number {student.StudentNumber}, " +
                $"is a {student.Occupation}. " +
                $"{pronoun} phone number is " +
                $"{student.CountryCode}-{student.AreaCode}-{student.PhoneNumber}"
            );
        }
    }
}

// ------------------------------------
// MAIN PROGRAM
// ------------------------------------
class FinalDrill
{
    static void Main(string[] args)
    {
        ASEANPhonebook phonebook = new ASEANPhonebook();

        int choice;

        do
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("       ASEAN PHONEBOOK SYSTEM");
            Console.WriteLine("=================================");
            Console.WriteLine("[1] Store to ASEAN phonebook");
            Console.WriteLine("[2] Edit entry in ASEAN phonebook");
            Console.WriteLine("[3] Search ASEAN phonebook by country");
            Console.WriteLine("[4] Exit");

            Console.Write("\nEnter choice: ");

            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid option! Please try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    phonebook.AddStudent();
                    break;

                case 2:
                    phonebook.EditStudent();
                    break;

                case 3:
                    phonebook.SearchByCountry();
                    break;

                case 4:
                    Console.WriteLine("Thank you for using ASEAN Phonebook!");
                    break;

                default:
                    Console.WriteLine("Invalid option! Please try again.");
                    break;
            }

        } while (choice != 4);
    }
}
