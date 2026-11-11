/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Submittal represents a contractor submittal, such as product
 * data or shop drawings, sent in for review. It adds the contractor
 * who submitted it and the date the review response is due.
*/

//INHERITANCE: Submittal is derived from the Document base class
public class Submittal : Document
{
    public string Contractor { get; set; }
    public DateTime ResponseDue { get; set; }

    public override string DocType { get { return "Submittal"; } }

    public Submittal(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string contractor,
        DateTime responseDue)
        : base(docNumber, title, revision, status, dateReceived)
    {
        Contractor = contractor;
        ResponseDue = responseDue;
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nContractor: {1}\nResponse Due: {2:MM/dd/yyyy}",
            base.GetDetails(), Contractor, ResponseDue);
    }
}
