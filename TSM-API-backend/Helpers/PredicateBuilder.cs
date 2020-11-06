using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public static class  PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }


        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>

                  (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);

        }



        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {

            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());

            return Expression.Lambda<Func<T, bool>>

                  (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);

        }

    }



    public class GenerateFilter
    {
        public static Expression<Func<TimesheetView, bool>> GenerateTimesheetFilter(TimesheetObj timesheetObj)
        {

            List<Expression<Func<TimesheetView, bool>>> navigationProperties = new List<Expression<Func<TimesheetView, bool>>>();

            var predicate = PredicateBuilder.True<TimesheetView>();

            if(timesheetObj.IdManager != -1)
            {
                navigationProperties.Add(p => p.IdManager == timesheetObj.IdManager);
            }

            if (timesheetObj.IdTeam != -1)
            {
                navigationProperties.Add(p => p.IdTeam == timesheetObj.IdTeam);
            }

            if (!timesheetObj.Date.Equals("nullDate"))
            {
                
                navigationProperties.Add(p => p.Date.Equals(DateTime.Parse(timesheetObj.Date)));

            }

            if (timesheetObj.IdProject != -1)
            {

                navigationProperties.Add(p => p.IdProject == timesheetObj.IdProject);

            }

            if (timesheetObj.IdUser !=-1)
            {

                navigationProperties.Add(p => p.IdUser == timesheetObj.IdUser);

            }




            foreach (var p in navigationProperties)
            {

                predicate = predicate.And(p);

            }



            return predicate;

        }







        //public List<CompetitionDTO> GetTopCompetitions(int numberOfComp, int pageNumber, string sortItem, string searchKey, string tag, CompetitionStatusEnum status, CompetitionCategoryEnum category, ObjectTypeEnum competitionType, bool hidden = false)

        //{

        //    try

        //    {



        //        string searchKeyLowerCase = searchKey.ToLower();

        //        List<CompetitionDTO> competitions = new List<CompetitionDTO>();

        //        var compToSkip = pageNumber * numberOfComp;



        //        var competitionFilter = GenerateCompetitionFilter(status, category, competitionType, searchKey, sortItem, tag, hidden);

        //        if (sortItem.Equals("Prizes") || sortItem.Equals("Created"))

        //        {

        //            sortItem = sortItem + " descending";

        //        }

        //        var compsFromDal = CompetitionRepository.GetList(competitionFilter, sortItem, compToSkip == 0 ? (int?)null : compToSkip, numberOfComp == 0 ? (int?)null : numberOfComp, p => p.CreatedBy, p => p.Discussions, p => p.Upvotes, p => p.GeneralInformations, p => p.UserCompetitions);



        //        var rez = _mapper.Map<List<CompetitionDTO>>(compsFromDal);



        //        _log.Info(string.Format("Got {0} competitions filtered by sortItem: {1}, searchKey: {2}, status: {3}, category: {4}, pageNo: {5}", numberOfComp, sortItem, searchKey, status, category, pageNumber));

        //        return rez;



        //    }

        //    catch (Exception ex)

        //    {

        //        _log.Error(string.Format("Tried to get {0} competitions filtered by sortItem: {1}, searchKey: {2}, status: {3}, category: {4}, pageNo: {5}, failed with error: {6}", numberOfComp, sortItem, searchKey, status, category, pageNumber, ex));

        //        throw ex;

        //    }
    }

   

    
}

