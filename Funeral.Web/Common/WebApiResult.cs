using Funeral.Model;
using System;
//using Funeral.Model.Search;

namespace Funeral.Web.Common
{
    public class WebApiResult<SearchType, ResultType> where SearchType : Model.Search.BaseSearch
    {
        public static SearchResult<SearchType, ResultType> Error(Funeral.Model.SearchResult<SearchType, ResultType> searchResult, Exception ex)
        {
            searchResult.Message = ex.Message;
            searchResult.Error = ex.StackTrace;
            searchResult.StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.InternalServerError.ToString());
            return searchResult;
        }
    }

}