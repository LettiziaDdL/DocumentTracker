/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: ConsoleUI handles all of the console input and output for
 * the application: the header, welcome message, menu, formatted tables,
 * messages, and getting validated values from the user.
*/

//ACCESS SPECIFIERS: the class is static because it holds no data of its
//own. Methods the rest of the program uses are public; the divider line
//and the Shorten helper are private because only this class needs them.
public static class ConsoleUI
{
    private const string Line =
        "==============================================================================";

    public static void DisplayHeader()
    {
        Console.WriteLine(Line);
        Console.WriteLine("  SDC320 Course Project - Week 3");
        Console.WriteLine("  Project Document Tracker: Abstraction, Constructors & Access Specifiers");
        Console.WriteLine("  Lettizia Diaz de Leon");
        Console.WriteLine(Line);
    }

    public static void DisplayWelcome(Project project)
    {
        Console.WriteLine("\nWelcome to the Project Document Tracker!");
        Console.WriteLine("Track drawings, specifications, and submittals, their revision and");
        Console.WriteLine("review status, when each review is due, and who should receive them.");
        Console.WriteLine("\nCurrent project: {0} - {1} ({2})",
            project.ProjectNumber, project.Name, project.Location);
        Console.WriteLine("\nType the number of a menu option and press Enter.");
        Console.WriteLine("Type Q at the main menu to quit.");
    }

    public static string DisplayMenu()
    {
        Console.WriteLine("\n--------------------- MAIN MENU ---------------------");
        Console.WriteLine("    1. View all documents");
        Console.WriteLine("    2. View document details");
        Console.WriteLine("    3. Add a document");
        Console.WriteLine("    4. Submit a new revision");
        Console.WriteLine("    5. Delete a document");
        Console.WriteLine("    6. View distribution list");
        Console.WriteLine("    7. View document review summaries");
        Console.WriteLine("    8. Review a document (approve, revise, reject)");
        Console.WriteLine("    9. View overdue documents");
        Console.WriteLine("   10. Add a contact");
        Console.WriteLine("   11. Delete a contact");
        Console.WriteLine("    Q. Quit");
        return GetString("Enter your choice: ").ToUpper();
    }

    public static void DisplayDocumentTable(List<Document> docs)
    {
        if (docs.Count == 0)
        {
            DisplayMessage("There are no documents to display.");
            return;
        }
        Console.WriteLine();
        Console.WriteLine("{0,-12} {1,-14} {2,-32} {3,-4} {4,-10} {5,-10}",
            "Doc #", "Type", "Title", "Rev", "Status", "Due");
        Console.WriteLine(new string('-', 87));
        foreach (Document d in docs)
        {
            Console.WriteLine("{0,-12} {1,-14} {2,-32} {3,-4} {4,-10} {5:MM/dd/yyyy}{6}",
                d.DocNumber, d.DocType, Shorten(d.Title, 32), d.Revision, d.Status,
                d.GetReviewDueDate(), d.IsOverdue() ? " *" : "");
        }
        Console.WriteLine("\n{0} document(s) listed. * = overdue", docs.Count);
    }

    //Displays summaries through the shared IReviewable interface (Week 2).
    //Each document type supplies its own review-summary implementation.
    public static void DisplayReviewSummaries(IEnumerable<IReviewable> items)
    {
        Console.WriteLine("\n---------------- DOCUMENT REVIEW SUMMARIES ----------------");
        foreach (IReviewable item in items)
        {
            Console.WriteLine("- " + item.GetReviewSummary());
        }
    }

    public static void DisplayContactTable(List<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            DisplayMessage("No one is on the distribution list.");
            return;
        }
        Console.WriteLine();
        Console.WriteLine("{0,-4} {1,-16} {2,-22} {3,-20} {4}",
            "ID", "Name", "Company", "Role", "Receives");
        Console.WriteLine(new string('-', 96));
        foreach (Contact c in contacts)
        {
            Console.WriteLine("{0,-4} {1,-16} {2,-22} {3,-20} {4}",
                c.ContactId, c.Name, c.Company, c.Role, c.ReceivesTypes);
        }
    }

    public static void DisplayMessage(string message)
    {
        Console.WriteLine("\n>> " + message);
    }

    //Keeps asking until the user types something
    public static string GetString(string prompt)
    {
        string? input;
        do
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (input == null)
            {
                return "Q"; //input stream closed
            }
            input = input.Trim();
            if (input.Length == 0)
            {
                Console.WriteLine("   A value is required. Please try again.");
            }
        } while (input.Length == 0);
        return input;
    }

    //Keeps asking until the user types a whole number
    public static int GetInt(string prompt)
    {
        while (true)
        {
            if (int.TryParse(GetString(prompt), out int result))
            {
                return result;
            }
            Console.WriteLine("   Please enter a whole number.");
        }
    }

    //Keeps asking until the user types a valid date
    public static DateTime GetDate(string prompt)
    {
        while (true)
        {
            if (DateTime.TryParse(GetString(prompt), out DateTime result))
            {
                return result;
            }
            Console.WriteLine("   Please enter a date like 11/23/2026.");
        }
    }

    //Keeps asking until the user picks one of the listed options
    public static string GetChoice(string prompt, string[] options)
    {
        while (true)
        {
            string input = GetString(prompt);
            foreach (string option in options)
            {
                if (option.Equals(input, StringComparison.OrdinalIgnoreCase))
                {
                    return option;
                }
            }
            Console.WriteLine("   Please enter one of: " + string.Join(", ", options));
        }
    }

    private static string Shorten(string text, int max)
    {
        return text.Length <= max ? text : text.Substring(0, max - 3) + "...";
    }
}
