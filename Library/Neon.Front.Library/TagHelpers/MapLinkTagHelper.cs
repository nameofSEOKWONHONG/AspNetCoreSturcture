using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Neon.Front.Library.TagHelpers
{
    [HtmlTargetElement("ml")]
    public class MapLinkTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            string originText = (await output.GetChildContentAsync()).GetContent();

            string mapString = $"{originText}";

            output.Attributes.Add("href", $@"http://local.daum.net/map/index.jsp?q={mapString}");
            output.Attributes.Add("style", "text-decoration:underline");
            output.Attributes.Add("target", "_blank");

            output.Content.SetContent(mapString);
        }
    }
}
