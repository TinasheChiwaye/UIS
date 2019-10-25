using Funeral.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model.Search
{
    public class SearchResult<T, U> : BaseResult where U : BaseSearch
    {
        public object Data { get; set; }
        public SearchResult(T data, U search)
        {

        }
    }
}
