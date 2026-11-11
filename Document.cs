/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Document is the BASE CLASS for every controlled project
 * document in the Project Document Tracker. It holds the information
 * all documents share: document number, title, revision, review
 * status, and the date it was received. The Drawing, Specification,
 * and Submittal classes inherit from this class.
*/
public abstract class Document
{
    public string DocNumber { get; set; }
    public string Title { get; set; }
    public string Revision { get; set; }
    public string Status { get; set; }
    public DateTime DateReceived { get; set; }

    //Each derived class reports its own document type
    public abstract string DocType { get; }

    public Document(string docNumber, string title, string revision,
        string status, DateTime dateReceived)
    {
        DocNumber = docNumber;
        Title = title;
        Revision = revision;
        Status = status;
        DateReceived = dateReceived;
    }

    //Shared details for every document. Derived classes override this
    //method and add their own fields to the end.
    public virtual string GetDetails()
    {
        return string.Format(
            "Document Number: {0}\nType: {1}\nTitle: {2}\nRevision: {3}\n"
                + "Status: {4}\nDate Received: {5:MM/dd/yyyy}",
            DocNumber, DocType, Title, Revision, Status, DateReceived);
    }

    public override string ToString()
    {
        return GetDetails();
    }
}
