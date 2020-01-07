using Funeral.Model.Base;
using Funeral.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class SearchResult<SearchType, ResultType> : BaseResult where SearchType : BaseSearch
    {
        public SearchResult(SearchType searchData, List<ResultType> dataResult, Predicate<ResultType> filterPredicate)
        {
            this.SearchParameters = searchData;
            int startIndex = (searchData.PageNum - 1) * searchData.PageSize;

            if (!string.IsNullOrEmpty(searchData.SarchText))
            {
                dataResult = dataResult.Where(resultData => filterPredicate(resultData)).ToList();
            }
            this.TotalCount = dataResult.Count();
            this.Result = (dataResult as List<ResultType>);

            if (!string.IsNullOrEmpty(searchData.SortBy) && !string.IsNullOrEmpty(searchData.SortOrder) && (Result as List<ResultType>).Count > 0)
            {
                var propertyInfo = (this.Result as List<ResultType>).First().GetType().GetProperty(searchData.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (searchData.SortOrder == "asc")
                    this.Result = (this.Result as List<ResultType>).OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
                else
                    this.Result = (this.Result as List<ResultType>).OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();
            }

            this.Result = (this.Result as List<ResultType>).Skip(startIndex).Take(searchData.PageSize).ToList();
            this.FilteredCount = ((List<ResultType>)Result).Count();
            this.Message = "Request successfully executed.";
            this.Error = null;
            this.StatusCode = (int)Enum.Parse(typeof(System.Net.HttpStatusCode), System.Net.HttpStatusCode.OK.ToString());
        }

        public SearchType SearchParameters { get; private set; }
        public object Result { get; private set; }
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
    }
    
}
