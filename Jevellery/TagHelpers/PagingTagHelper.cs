using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace Jevellery.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper : TagHelper
    {
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("sort")]
        public string? Sort { get; set; }
        [HtmlAttributeName("categoryId")]
        public int? CategoryId { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }
        [HtmlAttributeName("filterMax")]
        public int FilterMax { get; set; }
        [HtmlAttributeName("filterMin")]
        public int FilterMin { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {

            output.TagName = "section";
            var sb = new StringBuilder();
            var sort = Sort != "default" && !string.IsNullOrEmpty(Sort) ? $"&sort={Sort}":"";
            var category = CategoryId.Value > 0 ? $"&categoryId={CategoryId}" : "";
            var filterMin = FilterMin > 0 ? $"&filterMin={FilterMin}" : "";
            var filterMax = FilterMax > 0 ? $"&filterMax={FilterMax}" : "";
            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");
                if (CurrentPage > 1)
                {
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link page\" href=/shop/index?page={CurrentPage - 1}");
                    sb.Append(sort+category+filterMin+filterMax);
                    sb.Append(">Prev</a></li>");
                }
                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");
                    sb.Append($"<a class=\"page-link page\" href=/shop/index?page={i}");
                    sb.Append(sort + category + filterMin + filterMax);
                    sb.Append($">{i}</a></li>");

                }
                if (CurrentPage != PageCount)
                {
                    sb.Append($"<li class=\"page-item\"><a class=\"page-link page\" href=/shop/index?page={CurrentPage + 1}");
                    sb.Append(sort + category + filterMin + filterMax);
                    sb.Append(">Next</a></li>");
                }
                sb.Append("</ul>");
            }
            output.Content.SetHtmlContent(sb.ToString());
        }

    }
}
