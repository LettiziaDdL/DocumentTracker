/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Main application class for the Project Document Tracker.
 * It creates a sample project with realistic documents and contacts,
 * shows the header and welcome message, then runs the main menu loop
 * until the user chooses to quit.
*/
public class DocumentTrackerApp
{
    private static readonly string[] Statuses =
        { "Received", "In Review", "Approved", "Revise", "Rejected" };

    static void Main(string[] args)
    {
        Project project = CreateSampleProject();

        ConsoleUI.DisplayHeader();
        ConsoleUI.DisplayWelcome(project);

        bool running = true;
        while (running)
        {
            string choice = ConsoleUI.DisplayMenu();
            switch (choice)
            {
                case "1":
                    ConsoleUI.DisplayDocumentTable(project.Documents);
                    break;
                case "2":
                    ViewDetails(project);
                    break;
                case "3":
                    AddDocument(project);
                    break;
                case "4":
                    UpdateDocument(project);
                    break;
                case "5":
                    DeleteDocument(project);
                    break;
                case "6":
                    ConsoleUI.DisplayContactTable(project.DistributionList);
                    break;
                case "Q":
                    running = false;
                    break;
                default:
                    ConsoleUI.DisplayMessage("That isn't a menu option. Please enter 1-6 or Q.");
                    break;
            }
        }
        Console.WriteLine("\nThank you for using the Project Document Tracker. Goodbye!\n");
    }

    //Builds the sample project. Each Drawing, Specification, and
    //Submittal is stored in the project's list of Document objects.
    private static Project CreateSampleProject()
    {
        Project project = new Project("SF-2026-01", "Sunfield Solar Farm Phase 1",
            "Pecos County, TX");

        project.AddDocument(new Drawing("E-101", "Overall Single-Line Diagram", "2",
            "Approved", new DateTime(2026, 10, 6), "Electrical", "E-101"));
        project.AddDocument(new Drawing("C-201", "Site Grading and Drainage Plan", "1",
            "In Review", new DateTime(2026, 10, 20), "Civil", "C-201"));
        project.AddDocument(new Specification("SPEC-260519", "Low-Voltage Conductors and Cables",
            "0", "Approved", new DateTime(2026, 9, 28), "26 05 19", "26 - Electrical"));
        project.AddDocument(new Specification("SPEC-483100", "Photovoltaic Collectors",
            "1", "Received", new DateTime(2026, 11, 2), "48 31 00", "48 - Electrical Power Generation"));
        project.AddDocument(new Submittal("SUB-014", "Inverter Product Data", "0",
            "In Review", new DateTime(2026, 11, 3), "Lone Star Electrical",
            new DateTime(2026, 11, 17)));
        project.AddDocument(new Submittal("SUB-015", "Racking System Shop Drawings", "1",
            "Revise", new DateTime(2026, 11, 5), "Desert Steel Structures",
            new DateTime(2026, 11, 19)));

        project.AddContact(new Contact("Maria Gonzalez", "Sunfield Energy", "Project Manager",
            "mgonzalez@sunfield.example", "Drawing, Specification, Submittal"));
        project.AddContact(new Contact("David Chen", "Bright Path Eng.", "Electrical Engineer",
            "dchen@brightpath.example", "Drawing, Specification"));
        project.AddContact(new Contact("Sarah Patel", "Lone Star Electrical", "Superintendent",
            "spatel@lonestar.example", "Drawing, Submittal"));
        project.AddContact(new Contact("James Wright", "Sunfield Energy", "QA/QC Manager",
            "jwright@sunfield.example", "Specification, Submittal"));

        return project;
    }

    private static void ViewDetails(Project project)
    {
        string number = ConsoleUI.GetString("Enter the document number: ");
        Document? doc = project.FindDocument(number);
        if (doc == null)
        {
            ConsoleUI.DisplayMessage("No document found with number " + number + ".");
            return;
        }
        //GetDetails runs the version for the document's actual type
        Console.WriteLine();
        Console.WriteLine(doc.GetDetails());
        Console.WriteLine("\nDistribution:");
        foreach (Contact c in project.GetRecipients(doc))
        {
            Console.WriteLine("  " + c);
        }
    }

    private static void AddDocument(Project project)
    {
        string type = ConsoleUI.GetChoice(
            "Document type (Drawing, Specification, Submittal): ",
            new string[] { "Drawing", "Specification", "Submittal" });
        string number = ConsoleUI.GetString("Document number: ");
        if (project.FindDocument(number) != null)
        {
            ConsoleUI.DisplayMessage("A document with that number already exists.");
            return;
        }
        string title = ConsoleUI.GetString("Title: ");
        string revision = ConsoleUI.GetString("Revision: ");
        string status = ConsoleUI.GetChoice("Status (" + string.Join(", ", Statuses) + "): ",
            Statuses);
        DateTime received = ConsoleUI.GetDate("Date received (MM/DD/YYYY): ");

        Document doc;
        if (type == "Drawing")
        {
            string discipline = ConsoleUI.GetString("Discipline: ");
            string sheet = ConsoleUI.GetString("Sheet number: ");
            doc = new Drawing(number, title, revision, status, received, discipline, sheet);
        }
        else if (type == "Specification")
        {
            string section = ConsoleUI.GetString("Spec section number: ");
            string division = ConsoleUI.GetString("Division: ");
            doc = new Specification(number, title, revision, status, received, section, division);
        }
        else
        {
            string contractor = ConsoleUI.GetString("Contractor: ");
            DateTime due = ConsoleUI.GetDate("Response due (MM/DD/YYYY): ");
            doc = new Submittal(number, title, revision, status, received, contractor, due);
        }

        project.AddDocument(doc);
        ConsoleUI.DisplayMessage(type + " " + number + " was added.");
    }

    private static void UpdateDocument(Project project)
    {
        string number = ConsoleUI.GetString("Enter the document number to update: ");
        Document? doc = project.FindDocument(number);
        if (doc == null)
        {
            ConsoleUI.DisplayMessage("No document found with number " + number + ".");
            return;
        }
        Console.WriteLine("Current revision: {0}   Current status: {1}", doc.Revision, doc.Status);
        doc.Revision = ConsoleUI.GetString("New revision: ");
        doc.Status = ConsoleUI.GetChoice("New status (" + string.Join(", ", Statuses) + "): ",
            Statuses);
        ConsoleUI.DisplayMessage(doc.DocNumber + " is now Rev " + doc.Revision
            + ", " + doc.Status + ".");
    }

    private static void DeleteDocument(Project project)
    {
        string number = ConsoleUI.GetString("Enter the document number to delete: ");
        Document? doc = project.FindDocument(number);
        if (doc == null)
        {
            ConsoleUI.DisplayMessage("No document found with number " + number + ".");
            return;
        }
        string confirm = ConsoleUI.GetChoice("Delete " + doc.DocNumber + " - " + doc.Title
            + "? (Y/N): ", new string[] { "Y", "N" });
        if (confirm == "Y")
        {
            project.RemoveDocument(number);
            ConsoleUI.DisplayMessage(number + " was deleted.");
        }
        else
        {
            ConsoleUI.DisplayMessage("Delete canceled.");
        }
    }
}
