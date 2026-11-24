/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: Drawing represents a project drawing, such as a site plan
 * or a single-line diagram. It adds the engineering discipline and
 * the sheet number. Drawings have a 10-day review period.
*/

//INHERITANCE: Drawing is derived from the abstract Document class
public class Drawing : Document
{
    public string Discipline { get; set; }
    public string SheetNumber { get; set; }

    //Implements the abstract members from Document
    public override string DocType { get { return "Drawing"; } }
    protected override int ReviewDays { get { return 10; } }

    //CONSTRUCTORS: the first sets every value (used when loading existing
    //documents); the second creates a brand new drawing and lets the
    //Document constructor fill in the defaults.
    public Drawing(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string discipline,
        string sheetNumber)
        : base(docNumber, title, revision, status, dateReceived)
    {
        Discipline = discipline;
        SheetNumber = sheetNumber;
    }

    public Drawing(string docNumber, string title, string discipline,
        string sheetNumber)
        : base(docNumber, title)
    {
        Discipline = discipline;
        SheetNumber = sheetNumber;
    }

    //Implements the abstract GetReviewSummary from Document (Week 2)
    public override string GetReviewSummary()
    {
        return string.Format("{0} | {1} | Discipline: {2} | Sheet: {3} | Status: {4}",
            DocNumber, Title, Discipline, SheetNumber, Status);
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nDiscipline: {1}\nSheet Number: {2}",
            base.GetDetails(), Discipline, SheetNumber);
    }
}
