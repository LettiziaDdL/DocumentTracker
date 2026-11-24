/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/24/2026
 * Assignment: SDC320 Course Project - Week 3 (Phase 3)
 *
 * Purpose: IReviewable is the INTERFACE for anything that goes through
 * a review. Week 2 defined GetReviewSummary(); Week 3 adds the review
 * actions (approve, request a revision, reject) and the review due date.
 * It contains no properties or implementation.
*/
public interface IReviewable
{
    string GetReviewSummary();
    void Approve();
    void RequestRevision(string comments);
    void Reject(string reason);
    DateTime GetReviewDueDate();
}
