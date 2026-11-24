/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Specification represents a technical specification section.
 * It adds the spec section number and the division name.
 * Specifications have a 14-day review period.
*/

//INHERITANCE: Specification is derived from the abstract Document class
public class Specification : Document
{
    public string SectionNumber { get; set; }
    public string Division { get; set; }

    //Implements the abstract members from Document
    public override string DocType { get { return "Specification"; } }
    protected override int ReviewDays { get { return 14; } }

    //CONSTRUCTORS: full constructor and a constructor for a new spec
    public Specification(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string sectionNumber,
        string division)
        : base(docNumber, title, revision, status, dateReceived)
    {
        SectionNumber = sectionNumber;
        Division = division;
    }

    public Specification(string docNumber, string title, string sectionNumber,
        string division)
        : base(docNumber, title)
    {
        SectionNumber = sectionNumber;
        Division = division;
    }

    //Implements the abstract GetReviewSummary from Document (Week 2)
    public override string GetReviewSummary()
    {
        return string.Format("{0} | {1} | Section: {2} | Division: {3} | Status: {4}",
            DocNumber, Title, SectionNumber, Division, Status);
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nSpec Section: {1}\nDivision: {2}",
            base.GetDetails(), SectionNumber, Division);
    }
}
