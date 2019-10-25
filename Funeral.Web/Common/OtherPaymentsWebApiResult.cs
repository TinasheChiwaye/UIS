using Funeral.Model;
using System;
//using Funeral.Model.Search;

namespace Funeral.Web.Common
{
    public class OtherPaymentsWebApiResult<SearchType, ResultType> where SearchType : Model.Search.OthrePaymentSearch
    {
        public static OtherPaymentsSearchResult<SearchType, ResultType> Error(Funeral.Model.OtherPaymentsSearchResult<SearchType, ResultType> searchResult, Exception ex)
        {
            searchResult.Message = ex.Message;
            searchResult.Error = ex.StackTrace;
            searchResult.StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.InternalServerError.ToString());
            return searchResult;
        }
    }

}