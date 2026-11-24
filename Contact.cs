/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Contact represents one person on a project's distribution
 * list, including their company, role, email, and the document types
 * they should receive. Each contact gets a unique ID number.
*/
public class Contact
{
    //ACCESS SPECIFIERS: s_nextId is private and static because it is
    //shared by all contacts and only this class should change it.
    //ContactId can be read anywhere but only set inside this class.
    private static int s_nextId;

    public int ContactId { get; private set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string ReceivesTypes { get; set; }

    //STATIC CONSTRUCTOR: runs once, before the first Contact is created,
    //to set the starting ID number
    static Contact()
    {
        s_nextId = 1;
    }

    //CONSTRUCTORS: the first assigns the next available ID automatically;
    //the second accepts an existing ID (for loading saved contacts from
    //the database next week) and keeps the counter ahead of it
    public Contact(string name, string company, string role, string email,
        string receivesTypes)
        : this(s_nextId, name, company, role, email, receivesTypes)
    {
    }

    public Contact(int contactId, string name, string company, string role,
        string email, string receivesTypes)
    {
        ContactId = contactId;
        Name = name;
        Company = company;
        Role = role;
        Email = email;
        ReceivesTypes = receivesTypes;
        if (contactId >= s_nextId)
        {
            s_nextId = contactId + 1;
        }
    }

    //Returns true if this contact should receive the given document type
    public bool Receives(string docType)
    {
        return ReceivesTypes.Contains(docType, StringComparison.OrdinalIgnoreCase);
    }

    public override string ToString()
    {
        return string.Format("{0} ({1}, {2}) - {3}", Name, Role, Company, Email);
    }
}
