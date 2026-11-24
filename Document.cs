/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Document is the ABSTRACT BASE CLASS for every controlled
 * project document. It holds the data all documents share (number,
 * title, revision, status, date received, review comments) and the
 * review behavior from the IReviewable interface. Phase 3 makes the
 * constructors protected, adds a second constructor, and limits which
 * properties can be changed from outside the class. Drawing,
 * Specification, and Submittal inherit from this class.
*/

//ABSTRACTION: Document is abstract, so a plain "Document" can never be
//created. Only the specific document types can. It also implements the
//IReviewable interface, so every document type gets the same review
//actions without rewriting them.
public abstract class Document : IReviewable
{
    //ACCESS SPECIFIERS: DocNumber and DateReceived can be read anywhere
    //but only set when the document is created (private set). Revision
    //and Status can only change through the methods below, so outside
    //code can't skip the review process.
    public string DocNumber { get; private set; }
    public string Title { get; set; }
    public string Revision { get; private set; }
    public string Status { get; private set; }
    public DateTime DateReceived { get; private set; }
    public string ReviewComments { get; private set; }

    //ABSTRACTION: each derived class must supply its own type name and
    //review period. ReviewDays is protected because only Document and
    //its derived classes need it.
    public abstract string DocType { get; }
    protected abstract int ReviewDays { get; }

    //ABSTRACTION: each document type writes its own one-line review
    //summary (kept from Week 2), so it is abstract here. Making it
    //abstract in the base class means the compiler won't let a new
    //document type be added without one.
    public abstract string GetReviewSummary();

    //CONSTRUCTORS: protected because only derived classes can create a
    //Document. The first sets every value; the second is for a brand new
    //document and chains to the first with defaults (Rev 0, Received,
    //today's date).
    protected Document(string docNumber, string title, string revision,
        string status, DateTime dateReceived)
    {
        DocNumber = docNumber;
        Title = title;
        Revision = revision;
        Status = status;
        DateReceived = dateReceived;
        ReviewComments = "";
    }

    protected Document(string docNumber, string title)
        : this(docNumber, title, "0", "Received", DateTime.Today)
    {
    }

    //IReviewable methods
    public void Approve()
    {
        Status = "Approved";
        ReviewComments = "";
    }

    public void RequestRevision(string comments)
    {
        Status = "Revise";
        ReviewComments = comments;
    }

    public void Reject(string reason)
    {
        Status = "Rejected";
        ReviewComments = reason;
    }

    //POLYMORPHISM: virtual so a derived class can calculate its due date
    //differently (Submittal overrides this)
    public virtual DateTime GetReviewDueDate()
    {
        return DateReceived.AddDays(ReviewDays);
    }

    //A new revision restarts the review: status goes back to Received
    public void SubmitNewRevision(string newRevision, DateTime dateReceived)
    {
        Revision = newRevision;
        DateReceived = dateReceived;
        Status = "Received";
        ReviewComments = "";
    }

    //A document is still open while it is waiting on a review decision
    public bool IsOpen()
    {
        return Status == "Received" || Status == "In Review";
    }

    public bool IsOverdue()
    {
        return IsOpen() && GetReviewDueDate() < DateTime.Today;
    }

    //POLYMORPHISM: virtual so each document type can add its own fields
    public virtual string GetDetails()
    {
        string details = string.Format(
            "Document Number: {0}\nType: {1}\nTitle: {2}\nRevision: {3}\n"
                + "Status: {4}\nDate Received: {5:MM/dd/yyyy}\n"
                + "Review Due: {6:MM/dd/yyyy}{7}",
            DocNumber, DocType, Title, Revision, Status, DateReceived,
            GetReviewDueDate(), IsOverdue() ? "  ** OVERDUE **" : "");
        if (ReviewComments.Length > 0)
        {
            details += "\nReview Comments: " + ReviewComments;
        }
        return details;
    }

    public override string ToString()
    {
        return GetDetails();
    }
}
