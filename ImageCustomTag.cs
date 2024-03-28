using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AdminPanel
{
    [HtmlTargetElement("cuet")]
    public class ImageCustomTag : TagHelper
    {
        public string ImageLink { get; set; }

        public string AlternateImageLink { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.StartTagOnly;

            output.Attributes.SetAttribute("alt", AlternateImageLink);
            output.Attributes.SetAttribute("src", ImageLink);
        }
    }
}

