using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Jevellery.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        //[HtmlAttributeName("page-size")]
        //public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("sort")]
        public string? Sort { get; set; }
        [HtmlAttributeName("categoryId")]
        public int? CategoryId { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
  
            output.TagName = "section";
            var sb = new StringBuilder();
            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");
                if (CurrentPage > 1)
                {
                    
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link page\" href=/shop/index?sort={Sort}&page={CurrentPage - 1}&categoryId={CategoryId}>Prev</a></li>");
                }
                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");

                    sb.AppendFormat("<a class='page-link page' href='/shop/index?sort={0}&page={1}&categoryId={3}'>{2}</a>",
                        Sort, i, i,CategoryId);
                    sb.Append("</li>");

                }
                if (CurrentPage != PageCount)
                {
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link page\" href=/shop/index?sort={Sort}&page={CurrentPage + 1}&categoryId={CategoryId}>Next</a></li>");
                }
                sb.Append("</ul>");
            }
            output.Content.SetHtmlContent(sb.ToString());
        }

    }
}
