using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Neon.Front.Library.TagHelpers
{
    [HtmlTargetElement("el")]
    public class EmailLinkTagHelper : TagHelper
    {
        const string DOMAIN = "dotnetkorea.com";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            string originText = (await output.GetChildContentAsync()).GetContent();

            string emailString = $"{originText}@{DOMAIN}";

            output.Attributes.Add("href", $"mailto:{emailString}");
            output.Attributes.Add("style", "text-decoration:underline");

            output.Content.SetContent(emailString);
        }
    }
}
