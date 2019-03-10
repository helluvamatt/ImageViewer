using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ImageViewer.Controls
{
    internal class LinkLabelEx : LinkLabel
    {
        private readonly static Regex _LinkRegex = new Regex(@"(?:\[(?<text>.+?)\])?\((?<url>.+?)\)", RegexOptions.Compiled);

        public override string Text
        {
            get => base.Text;
            set
            {
                Links.Clear();
                var links = new List<Link>();
                int offset = 0;
                Match match = _LinkRegex.Match(value, offset);
                while (match.Success)
                {
                    var text = match.Groups["text"].Success ? match.Groups["text"].Value : match.Groups["url"].Value;
                    var url = match.Groups["url"].Value;
                    if (Uri.TryCreate(url, UriKind.Absolute, out Uri result))
                    {
                        value = value.Remove(match.Index, match.Length);
                        value = value.Insert(match.Index, text);
                        links.Add(new Link(match.Index, text.Length, url));
                        offset = match.Index + text.Length;
                    }
                    match = _LinkRegex.Match(value, offset);
                }
                base.Text = value;
                foreach (var link in links) Links.Add(link);
            }
        }
    }
}
