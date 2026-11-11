/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: ConsoleUI handles all of the console input and output
 * for the application: the header, welcome message, menu, formatted
 * tables, messages, and getting validated values from the user.
 * Keeping this in its own class makes it reusable for the rest of
 * the course project.
*/
public static class ConsoleUI
{
    private const string Line = "==========================================================================";

    public static void DisplayHeader()
    {
        Console.WriteLine(Line);
        Console.WriteLine("  SDC320 Course Project - Week 1");
        Console.WriteLine("  Project Document Tracker");
        Console.WriteLine("  Lettizia Diaz de Leon");
        Console.WriteLine(Line);
    }

    public static void DisplayWelcome(Project project)
    {
        Console.WriteLine("\nWelcome to the Project Document Tracker!");
        Console.WriteLine("Keep track of drawings, specifications, and submittals, their");
        Console.WriteLine("current revision and review status, and who should receive them.");
        Console.WriteLine("\nCurrent project: {0} - {1} ({2})",
            project.ProjectNumber, project.Name, project.Location);
        Console.WriteLine("\nTo use the tracker, type the number of a menu option and press");
        Console.WriteLine("Enter. Type Q at the main menu to quit.");
    }

    public static string DisplayMenu()
    {
        Console.WriteLine("\n------------------- MAIN MENU -------------------");
        Console.WriteLine("  1. View all documents");
        Console.WriteLine("  2. View document details");
        Console.WriteLine("  3. Add a document");
        Console.WriteLine("  4. Update a document's revision or status");
        Console.WriteLine("  5. Delete a document");
        Console.WriteLine("  6. View distribution list");
        Console.WriteLine("  Q. Quit");
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
        Console.WriteLine("{0,-12} {1,-14} {2,-32} {3,-4} {4,-12}",
            "Doc #", "Type", "Title", "Rev", "Status");
        Console.WriteLine(new string('-', 78));
        foreach (Document d in docs)
        {
            Console.WriteLine("{0,-12} {1,-14} {2,-32} {3,-4} {4,-12}",
                d.DocNumber, d.DocType, Shorten(d.Title, 32), d.Revision, d.Status);
        }
        Console.WriteLine("\n{0} document(s) listed.", docs.Count);
    }

    public static void DisplayContactTable(List<Contact> contacts)
    {
        if (contacts.Count == 0)
        {
            DisplayMessage("No one is on the distribution list.");
            return;
        }
        Console.WriteLine();
        Console.WriteLine("{0,-16} {1,-22} {2,-20} {3}",
            "Name", "Company", "Role", "Receives");
        Console.WriteLine(new string('-', 92));
        foreach (Contact c in contacts)
        {
            Console.WriteLine("{0,-16} {1,-22} {2,-20} {3}",
                c.Name, c.Company, c.Role, c.ReceivesTypes);
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

    //Keeps asking until the user types a valid date
    public static DateTime GetDate(string prompt)
    {
        while (true)
        {
            string input = GetString(prompt);
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            Console.WriteLine("   Please enter a date like 11/15/2026.");
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
