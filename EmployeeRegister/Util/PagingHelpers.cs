using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EmployeeRegister.Util
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages => (TotalItems + ItemsPerPage - 1) / ItemsPerPage;
    }

    public class DropDownListInfo
    {
        public class DropDownListItem
        {
            public string DisplayName { get; set; }
            public string ModelName { get; set; }
        }

        public List<DropDownListItem> Item = new List<DropDownListItem>();
        public string Title { get; set; }

    }

    public static class PagingHelpers
    {
        public static MvcHtmlString DropDownList(this HtmlHelper html, List<string> orderList, Func<int,string> pageUrl)
        {
            /*
             * Bootstrap 3 dropdown list
             */
            //
            //      <div class="dropdown div-inline">
            //          <button class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">Sort order
            //             <span class="caret"></span>
            //          </button>
            //          <ul class="dropdown-menu" aria-labelledby="dropdownMenu1">
            //            <li><a href = "@Url.Action("IndexWithPagination", new { page = ViewBag.pagingInfo.CurrentPage, sortOrder = "FirstName" })">First Name</a></li>
            //            <li><a href = "@Url.Action("IndexWithPagination", new { page = ViewBag.pagingInfo.CurrentPage, sortOrder = "LastName" })">Last Name</a></li>
            //            <li><a href = "@Url.Action("IndexWithPagination", new { page = ViewBag.pagingInfo.CurrentPage, sortOrder = "Id" })">Id</a></li>
            //          </ul>
            //      </div>
            //  
            var baseUrl = new TagBuilder("a");
            // Append sortOrder to pageUrl... or the property assosiciated 
            baseUrl.MergeAttribute("href", pageUrl(1));

            return MvcHtmlString.Create("");
        }


        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            //TODO add screen-reader code for complete implementation

            /* Bootstrap paginator example HTML
              <ul class="pagination">
                <li class="disabled"><a href="#">&laquo;</a></li>
                <li class="active"><a href="#">1</a></li>
                <li><a href="#">2</a></li>
                <li><a href="#">3</a></li>
                <li><a href="#">4</a></li>
                <li><a href="#">5</a></li>
                <li><a href="#">&raquo;</a></li>
              </ul>
            */

            StringBuilder result = new StringBuilder();

            var firstPageLink = new TagBuilder("a");
            firstPageLink.MergeAttribute("href", pageUrl(1));
            firstPageLink.InnerHtml = "&laquo;";

            var listTag = new TagBuilder("li");
            listTag.InnerHtml = firstPageLink.ToString();
            result.Append(listTag);

            var firstPaginationPage = (pagingInfo.CurrentPage - 2 > 0) ?
                                       pagingInfo.CurrentPage - 2 : 1;
            var lastPaginationPage = (pagingInfo.CurrentPage + 2 <= pagingInfo.TotalPages) ?
                                      pagingInfo.CurrentPage + 2 : pagingInfo.TotalPages;
            for (var i = firstPaginationPage; i <= lastPaginationPage; i++)
            {
                if (i == pagingInfo.CurrentPage)
                {
                    var currentListTag = new TagBuilder("li");
                    currentListTag.AddCssClass("active");
                    var span = new TagBuilder("span");
                    span.InnerHtml = i.ToString();
                    currentListTag.InnerHtml = span.ToString();
                    result.Append(currentListTag.ToString());
                }
                else
                {
                    var aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href", pageUrl(i));
                    aTag.InnerHtml = i.ToString();
                    listTag.InnerHtml = aTag.ToString();
                    result.Append(listTag);
                }
            }

            var lastPageLink = new TagBuilder("a");
            lastPageLink.MergeAttribute("href", pageUrl(pagingInfo.TotalPages));
            lastPageLink.InnerHtml = "&raquo;";

            listTag.InnerHtml = lastPageLink.ToString();
            result.Append(listTag);
            //            result.Append(lastPageLink.ToString());

            return MvcHtmlString.Create(result.ToString());
        }
    }
}