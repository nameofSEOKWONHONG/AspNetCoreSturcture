using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Neon.Front.Library.TagHelpers
{
    [HtmlTargetElement("tl")]
    public class TelLinkTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            string originText = (await output.GetChildContentAsync()).GetContent();

            string telString = $"{originText}";

            output.Attributes.Add("href", $"tel:{telString}");
            output.Attributes.Add("style", "text-decoration:underline");

            output.Content.SetContent(telString);
        }
    }
}
