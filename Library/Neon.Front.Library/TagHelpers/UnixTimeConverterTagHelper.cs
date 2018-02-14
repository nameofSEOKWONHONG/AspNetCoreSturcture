using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Threading.Tasks;

namespace Neon.Front.Library.TagHelpers
{
    public class UnixTimeConverterTagHelper : TagHelper
    {
        public string Formatter { get; set; } = "yyyy-MM-dd hh:mm:ss";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var childContent = (await output.GetChildContentAsync()).GetContent();

            var unixTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var currentTime = unixTime.AddSeconds(Convert.ToDouble(childContent));

            output.Content.SetContent(currentTime.ToString(Formatter));
        }
    }
}
