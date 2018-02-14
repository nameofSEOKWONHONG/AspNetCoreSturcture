using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Neon.Front.Library.TagHelpers
{
    [HtmlTargetElement("url")]
    public class UrlLinkTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            string originText = (await output.GetChildContentAsync()).GetContent();

            string homepageString = $"{originText}";

            output.Attributes.Add("href", $@"http://{homepageString}");
            output.Attributes.Add("style", "text-decoration:underline");
            output.Attributes.Add("target", "_blank");

            output.Content.SetContent(homepageString);
        }
    }
}
