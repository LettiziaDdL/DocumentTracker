/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Main application class for the Project Document Tracker.
 * It creates a sample project with realistic documents and contacts,
 * shows the header and welcome message, then runs the main menu loop
 * until the user chooses to quit. Each menu option has its own private
 * method.
*/
public class DocumentTrackerApp
{
    //ACCESS SPECIFIERS: everything except Main is private because only
    //this class uses it
    private static readonly string[] DocTypes = { "Drawing", "Specification", "Submittal" };

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
                case "1": ConsoleUI.DisplayDocumentTable(project.Documents); break;
                case "2": ViewDetails(project); break;
                case "3": AddDocument(project); break;
                case "4": SubmitNewRevision(project); break;
                case "5": DeleteDocument(project); break;
                case "6": ConsoleUI.DisplayContactTable(project.DistributionList); break;
                case "7":
                    //POLYMORPHISM: each document is accessed through IReviewable
                    ConsoleUI.DisplayReviewSummaries(project.Documents);
                    break;
                case "8": ReviewDocument(project); break;
                case "9": ViewOverdue(project); break;
                case "10": AddContact(project); break;
                case "11": DeleteContact(project); break;
                case "Q": running = false; break;
                default:
                    ConsoleUI.DisplayMessage("That isn't a menu option. Please enter 1-11 or Q.");
                    break;
            }
        }
        Console.WriteLine("\nThank you for using the Project Document Tracker. Goodbye!\n");
    }

    //Builds the sample project. Dates are set relative to today so some
    //documents are always overdue when the program runs. The full
    //constructors are used here because these documents already exist.
    private static Project CreateSampleProject()
    {
        DateTime today = DateTime.Today;
        Project project = new Project("SF-2026-01", "Sunfield Solar Farm Phase 1",
            "Pecos County, TX");

        project.AddDocument(new Drawing("E-101", "Overall Single-Line Diagram", "2",
            "Approved", today.AddDays(-30), "Electrical", "E-101"));
        project.AddDocument(new Drawing("C-201", "Site Grading and Drainage Plan", "1",
            "In Review", today.AddDays(-14), "Civil", "C-201"));
        project.AddDocument(new Specification("SPEC-260519", "Low-Voltage Conductors and Cables",
            "0", "Approved", today.AddDays(-45), "26 05 19", "26 - Electrical"));
        project.AddDocument(new Specification("SPEC-483100", "Photovoltaic Collectors",
            "1", "Received", today.AddDays(-3), "48 31 00", "48 - Electrical Power Generation"));
        project.AddDocument(new Submittal("SUB-014", "Inverter Product Data", "0",
            "In Review", today.AddDays(-10), "Lone Star Electrical", today.AddDays(-2)));
        project.AddDocument(new Submittal("SUB-015", "Racking System Shop Drawings", "1",
            "Revise", today.AddDays(-4), "Desert Steel Structures", today.AddDays(5)));

        //These contacts use the constructor that assigns the next ID
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

    //Shared helper: asks for a document number and returns the match,
    //or shows a message and returns null
    private static Document? AskForDocument(Project project, string prompt)
    {
        string number = ConsoleUI.GetString(prompt);
        Document? doc = project.FindDocument(number);
        if (doc == null)
        {
            ConsoleUI.DisplayMessage("No document found with number " + number + ".");
        }
        return doc;
    }

    private static void ViewDetails(Project project)
    {
        Document? doc = AskForDocument(project, "Enter the document number: ");
        if (doc == null) return;

        //POLYMORPHISM: GetDetails runs the version for the document's type
        Console.WriteLine();
        Console.WriteLine(doc.GetDetails());
        Console.WriteLine("\nDistribution:");
        foreach (Contact c in project.GetRecipients(doc))
        {
            Console.WriteLine("  " + c);
        }
    }

    //New documents use the shorter constructors, which start every
    //document at Rev 0, Received, dated today
    private static void AddDocument(Project project)
    {
        string type = ConsoleUI.GetChoice("Document type (Drawing, Specification, Submittal): ",
            DocTypes);
        string number = ConsoleUI.GetString("Document number: ");
        if (project.FindDocument(number) != null)
        {
            ConsoleUI.DisplayMessage("A document with that number already exists.");
            return;
        }
        string title = ConsoleUI.GetString("Title: ");

        Document doc;
        if (type == "Drawing")
        {
            doc = new Drawing(number, title, ConsoleUI.GetString("Discipline: "),
                ConsoleUI.GetString("Sheet number: "));
        }
        else if (type == "Specification")
        {
            doc = new Specification(number, title, ConsoleUI.GetString("Spec section number: "),
                ConsoleUI.GetString("Division: "));
        }
        else
        {
            doc = new Submittal(number, title, ConsoleUI.GetString("Contractor: "));
        }

        project.AddDocument(doc);
        ConsoleUI.DisplayMessage(string.Format("{0} {1} was added as Rev 0. Review due {2:MM/dd/yyyy}.",
            type, number, doc.GetReviewDueDate()));
    }

    private static void SubmitNewRevision(Project project)
    {
        Document? doc = AskForDocument(project, "Enter the document number: ");
        if (doc == null) return;

        Console.WriteLine("Current revision: {0}   Status: {1}", doc.Revision, doc.Status);
        string revision = ConsoleUI.GetString("New revision: ");
        DateTime received = ConsoleUI.GetDate("Date received (MM/DD/YYYY): ");
        doc.SubmitNewRevision(revision, received);
        ConsoleUI.DisplayMessage(string.Format("{0} is now Rev {1}, Received. Review due {2:MM/dd/yyyy}.",
            doc.DocNumber, doc.Revision, doc.GetReviewDueDate()));
    }

    //Uses the IReviewable methods through an IReviewable reference
    private static void ReviewDocument(Project project)
    {
        Document? doc = AskForDocument(project, "Enter the document number to review: ");
        if (doc == null) return;

        IReviewable item = doc;
        Console.WriteLine("{0} - {1} is currently {2}.", doc.DocNumber, doc.Title, doc.Status);
        string action = ConsoleUI.GetChoice("Decision (Approve, Revise, Reject): ",
            new string[] { "Approve", "Revise", "Reject" });
        if (action == "Approve")
        {
            item.Approve();
        }
        else if (action == "Revise")
        {
            item.RequestRevision(ConsoleUI.GetString("Revision comments: "));
        }
        else
        {
            item.Reject(ConsoleUI.GetString("Reason for rejection: "));
        }
        ConsoleUI.DisplayMessage(doc.DocNumber + " is now " + doc.Status + ".");
    }

    private static void DeleteDocument(Project project)
    {
        Document? doc = AskForDocument(project, "Enter the document number to delete: ");
        if (doc == null) return;

        string confirm = ConsoleUI.GetChoice("Delete " + doc.DocNumber + " - " + doc.Title
            + "? (Y/N): ", new string[] { "Y", "N" });
        if (confirm == "Y" && project.RemoveDocument(doc.DocNumber))
        {
            ConsoleUI.DisplayMessage(doc.DocNumber + " was deleted.");
        }
        else
        {
            ConsoleUI.DisplayMessage("Delete canceled.");
        }
    }

    private static void ViewOverdue(Project project)
    {
        Console.WriteLine("\nDocuments past their review due date:");
        ConsoleUI.DisplayDocumentTable(project.GetOverdueDocuments());
    }

    private static void AddContact(Project project)
    {
        string name = ConsoleUI.GetString("Name: ");
        string company = ConsoleUI.GetString("Company: ");
        string role = ConsoleUI.GetString("Role: ");
        string email = ConsoleUI.GetString("Email: ");
        string receives = ConsoleUI.GetString("Document types received (e.g., Drawing, Submittal): ");

        //Uses the constructor that assigns the next ID automatically
        Contact contact = new Contact(name, company, role, email, receives);
        project.AddContact(contact);
        ConsoleUI.DisplayMessage(name + " was added with ID " + contact.ContactId + ".");
    }

    private static void DeleteContact(Project project)
    {
        int id = ConsoleUI.GetInt("Enter the contact ID to delete: ");
        Contact? contact = project.FindContact(id);
        if (contact == null)
        {
            ConsoleUI.DisplayMessage("No contact found with ID " + id + ".");
            return;
        }
        string confirm = ConsoleUI.GetChoice("Delete " + contact.Name + "? (Y/N): ",
            new string[] { "Y", "N" });
        if (confirm == "Y" && project.RemoveContact(id))
        {
            ConsoleUI.DisplayMessage(contact.Name + " was removed from the distribution list.");
        }
        else
        {
            ConsoleUI.DisplayMessage("Delete canceled.");
        }
    }
}
