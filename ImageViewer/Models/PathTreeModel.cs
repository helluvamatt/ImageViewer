using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ImageViewer.Models
{
    internal class PathTreeModel : TreeNode
    {
        public PathTreeModel(string text) : base()
        {
            Name = Text = text.TrimEnd(Path.PathSeparator, Path.AltDirectorySeparatorChar);
        }

        public bool IsEmpty => Nodes.Count < 1;

        public void AddPath(string path)
        {
            path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            string subpath = GetSubpath(path);
            if (!string.IsNullOrEmpty(subpath))
            {
                string nextNode = GetPathRoot(subpath);
                var subNode = Nodes.Cast<PathTreeModel>().FirstOrDefault(n => n.Name == nextNode);
                if (subNode == null)
                {
                    subNode = new PathTreeModel(nextNode);
                    Nodes.Add(subNode);
                }
                subNode.AddPath(subpath);
            }
        }

        public void RemovePath(string path)
        {
            path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            string subpath = GetSubpath(path);
            if (!string.IsNullOrEmpty(subpath))
            {
                string nextNode = GetPathRoot(subpath);
                var subNode = Nodes.Cast<PathTreeModel>().FirstOrDefault(n => n.Name == nextNode);
                if (subNode != null)
                {
                    subNode.RemovePath(subpath);
                    if (subNode.IsEmpty) Nodes.Remove(subNode);
                }
            }
        }

        private string GetSubpath(string path)
        {
            if (path.StartsWith(Text))
            {
                path = path.Substring(Text.Length);
                if (path.Length > 1)
                {
                    path = path.Substring(1);
                    return path;
                }
            }
            return null;
        }

        private string GetPathRoot(string path)
        {
            int index = path.IndexOf(Path.DirectorySeparatorChar);
            if (index > -1)
            {
                path = path.Substring(0, index);
            }
            return path;
        }
    }
}
