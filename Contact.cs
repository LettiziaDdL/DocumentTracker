/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Contact represents one person on a project's distribution
 * list, including their company, role, email, and the document types
 * they should receive.
*/
public class Contact
{
    public string Name { get; set; }
    public string Company { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
    public string ReceivesTypes { get; set; }

    public Contact(string name, string company, string role, string email,
        string receivesTypes)
    {
        Name = name;
        Company = company;
        Role = role;
        Email = email;
        ReceivesTypes = receivesTypes;
    }

    //Returns true if this contact should receive the given document type
    public bool Receives(string docType)
    {
        return ReceivesTypes.Contains(docType);
    }

    public override string ToString()
    {
        return string.Format("{0} ({1}, {2}) - {3}", Name, Role, Company, Email);
    }
}
