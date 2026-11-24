/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Submittal represents a contractor submittal, such as product
 * data or shop drawings, sent in for review. It adds the contractor and
 * the contract date the review response is due. Submittals use that
 * contract date as their review due date.
*/

//INHERITANCE: Submittal is derived from the abstract Document class
public class Submittal : Document
{
    public string Contractor { get; set; }
    public DateTime ResponseDue { get; set; }

    //Implements the abstract members from Document. ReviewDays is used
    //only as the default when a new submittal has no due date given.
    public override string DocType { get { return "Submittal"; } }
    protected override int ReviewDays { get { return 7; } }

    //CONSTRUCTORS: full constructor and a constructor for a new
    //submittal, which sets the response due date to the standard 7 days
    public Submittal(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string contractor,
        DateTime responseDue)
        : base(docNumber, title, revision, status, dateReceived)
    {
        Contractor = contractor;
        ResponseDue = responseDue;
    }

    public Submittal(string docNumber, string title, string contractor)
        : base(docNumber, title)
    {
        Contractor = contractor;
        ResponseDue = DateReceived.AddDays(ReviewDays);
    }

    //POLYMORPHISM: submittals are due on their contract response date,
    //not a calculated date, so this overrides the Document version
    public override DateTime GetReviewDueDate()
    {
        return ResponseDue;
    }

    //Implements the abstract GetReviewSummary from Document (Week 2)
    public override string GetReviewSummary()
    {
        return string.Format("{0} | {1} | Contractor: {2} | Response Due: {3:MM/dd/yyyy} | Status: {4}",
            DocNumber, Title, Contractor, ResponseDue, Status);
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nContractor: {1}\nResponse Due: {2:MM/dd/yyyy}",
            base.GetDetails(), Contractor, ResponseDue);
    }
}
