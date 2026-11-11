/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/11/2026
 * Assignment: SDC320 Course Project - Week 1 (Phase 1)
 *
 * Purpose: Specification represents a technical specification section.
 * It adds the specification section number and the division name.
*/

//INHERITANCE: Specification is derived from the Document base class
public class Specification : Document
{
    public string SectionNumber { get; set; }
    public string Division { get; set; }

    public override string DocType { get { return "Specification"; } }

    public Specification(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string sectionNumber,
        string division)
        : base(docNumber, title, revision, status, dateReceived)
    {
        SectionNumber = sectionNumber;
        Division = division;
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nSpec Section: {1}\nDivision: {2}",
            base.GetDetails(), SectionNumber, Division);
    }
}
