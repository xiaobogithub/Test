using System.Web.Mvc;

namespace Ethos.Utility
{
    public static class HtmlUtility
    {
        public static string TextBox(string name, string value)
        {
            TagBuilder tb = new TagBuilder("input");
            tb.MergeAttribute("type", "text");
            tb.GenerateId(name);
            tb.MergeAttribute("value", value);
            return tb.ToString(TagRenderMode.SelfClosing);
        }
    }
}
