/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Project represents a single construction or energy project.
 * It holds the project number, name, and location, along with the
 * project's documents and its distribution list. Methods are provided
 * to add, find, and remove documents and to look up which contacts
 * should receive a given document.
*/

//COMPOSITION: a Project "has a" list of Document objects and "has a"
//list of Contact objects. The project can't do its job without them.
public class Project
{
    public string ProjectNumber { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public List<Document> Documents { get; }
    public List<Contact> DistributionList { get; }

    public Project(string projectNumber, string name, string location)
    {
        ProjectNumber = projectNumber;
        Name = name;
        Location = location;
        Documents = new List<Document>();
        DistributionList = new List<Contact>();
    }

    public void AddDocument(Document doc)
    {
        Documents.Add(doc);
    }

    public void AddContact(Contact contact)
    {
        DistributionList.Add(contact);
    }

    //Find a document by its number (not case sensitive). Returns null
    //if no document matches.
    public Document? FindDocument(string docNumber)
    {
        return Documents.Find(d =>
            d.DocNumber.Equals(docNumber, StringComparison.OrdinalIgnoreCase));
    }

    //Remove a document by number. Returns true if one was removed.
    public bool RemoveDocument(string docNumber)
    {
        Document? doc = FindDocument(docNumber);
        if (doc == null)
        {
            return false;
        }
        Documents.Remove(doc);
        return true;
    }

    //Everyone on the distribution list who receives this document's type
    public List<Contact> GetRecipients(Document doc)
    {
        return DistributionList.FindAll(c => c.Receives(doc.DocType));
    }
}
