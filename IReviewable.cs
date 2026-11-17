/*******************************************************************
 * Name: Lettizia Diaz de Leon
 * Date: 11/16/2026
 * Assignment: SDC320 Course Project - Week 2 (Phase 2)
 *
 * Purpose: IReviewable defines the review summary that a controlled
 * project document can provide. Each document type supplies its own
 * implementation so the application can work with different reviewable
 * objects through one interface.
 *******************************************************************/
public interface IReviewable
{
    string GetReviewSummary();
}
