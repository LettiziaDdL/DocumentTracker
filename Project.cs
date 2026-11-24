/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Project represents a single construction or energy project.
 * It holds the project number, name, and location, along with the
 * project's documents and its distribution list, and provides methods
 * to add, find, and remove them.
*/

//COMPOSITION: a Project "has a" list of Document objects and "has a"
//list of Contact objects.
public class Project
{
    //ACCESS SPECIFIERS: the lists are private fields. Outside code can
    //read them through the get-only properties, but must use the Add and
    //Remove methods below to change them.
    private readonly List<Document> m_documents;
    private readonly List<Contact> m_contacts;

    public string ProjectNumber { get; private set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<Document> Documents { get { return m_documents; } }
    public List<Contact> DistributionList { get { return m_contacts; } }

    public Project(string projectNumber, string name, string location)
    {
        ProjectNumber = projectNumber;
        Name = name;
        Location = location;
        m_documents = new List<Document>();
        m_contacts = new List<Contact>();
    }

    public void AddDocument(Document doc)
    {
        m_documents.Add(doc);
    }

    //Find a document by number (not case sensitive); null if not found
    public Document? FindDocument(string docNumber)
    {
        return m_documents.Find(d => SameNumber(d.DocNumber, docNumber));
    }

    public bool RemoveDocument(string docNumber)
    {
        Document? doc = FindDocument(docNumber);
        return doc != null && m_documents.Remove(doc);
    }

    public void AddContact(Contact contact)
    {
        m_contacts.Add(contact);
    }

    public Contact? FindContact(int contactId)
    {
        return m_contacts.Find(c => c.ContactId == contactId);
    }

    public bool RemoveContact(int contactId)
    {
        Contact? contact = FindContact(contactId);
        return contact != null && m_contacts.Remove(contact);
    }

    //Everyone on the distribution list who receives this document's type
    public List<Contact> GetRecipients(Document doc)
    {
        return m_contacts.FindAll(c => c.Receives(doc.DocType));
    }

    //POLYMORPHISM: IsOverdue calls GetReviewDueDate, which runs the right
    //version for each document type
    public List<Document> GetOverdueDocuments()
    {
        return m_documents.FindAll(d => d.IsOverdue());
    }

    //ACCESS SPECIFIERS: private helper, only used inside this class
    private static bool SameNumber(string a, string b)
    {
        return a.Equals(b, StringComparison.OrdinalIgnoreCase);
    }
}
