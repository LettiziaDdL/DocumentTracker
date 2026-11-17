/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/16/2026
 * Assignment: SDC320 Course Project - Week 2 (Phase 2)
 *
 * Purpose: Drawing represents a project drawing, such as a site plan
 * or a single-line diagram. It adds the engineering discipline and
 * the sheet number.
*/

//INHERITANCE: Drawing is derived from the Document base class
public class Drawing : Document, IReviewable
{
    public string Discipline { get; set; }
    public string SheetNumber { get; set; }

    public override string DocType { get { return "Drawing"; } }

    public Drawing(string docNumber, string title, string revision,
        string status, DateTime dateReceived, string discipline,
        string sheetNumber)
        : base(docNumber, title, revision, status, dateReceived)
    {
        Discipline = discipline;
        SheetNumber = sheetNumber;
    }

    public override string GetDetails()
    {
        return string.Format("{0}\nDiscipline: {1}\nSheet Number: {2}",
            base.GetDetails(), Discipline, SheetNumber);
    }

    // INTERFACE IMPLEMENTATION: drawing review summaries include discipline and sheet.
    public string GetReviewSummary()
    {
        return string.Format("{0} | {1} | Discipline: {2} | Sheet: {3} | Status: {4}",
            DocNumber, Title, Discipline, SheetNumber, Status);
    }

}
