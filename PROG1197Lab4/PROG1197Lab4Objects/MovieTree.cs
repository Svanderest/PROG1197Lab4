using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;

namespace PROG1197Lab4Objects
{
    public class MovieTree
    {
        private static MovieTree tree;

        public static MovieTree Create()
        {
            tree = new MovieTree();
            using (FileStream fs = new FileStream(@"C:\\Users\\sebas\\source\repos\\PROG1197Lab4\\PROG1197Lab4\\PROG1197Lab4Objects\\MovieText.txt", FileMode.Open))
            using(StreamReader sr = new StreamReader(fs))
            {
                List<string> lines = new List<string>();
                while(!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                var nodes = lines.Select(l => l.Split(';')).Select(line =>
                new MovieNode
                {
                    Title = line[0],
                    ReleaseDate = Convert.ToDateTime(line[1]),
                    Runtime = TimeSpan.FromMinutes(Convert.ToInt32(line[2])),
                    Director = line[3],
                    Rating = Convert.ToDouble(line[4])
                });                
                tree.Root = tree.Build(nodes);
            }
           return tree;
        }

        public MovieNode Root { get; private set; }

        public MovieNode Build(IEnumerable<MovieNode> nodes)
        {
            foreach (MovieNode N in nodes.Where(n => nodes.Count(mn => mn.Equals(n)) > 1))
                return Build(nodes.Where(n => !n.Equals(N)).Union(new MovieNode[] { N }));
            nodes = nodes.OrderBy(n => n);
            try
            {
                var current = nodes.ElementAt(nodes.Count() / 2);
                if (current.CompareTo(nodes.Max()) != 0)
                    current.Right = Build(nodes.Where(n => n.CompareTo(current) == 1));
                if (current.CompareTo(nodes.Min()) != 0)
                    current.Left = Build(nodes.Where(n => n.CompareTo(current) == -1));
                return current;
            }
            catch
            {
                return null; 
            }
        }

        public void Add(MovieNode item)
        {
            if (Root == null)
                Root = item;
            else if (Root.Children.Contains(item) || Root.CompareTo(item) == 0)
                return;
            else if (!Root.Inbalanced)
            {
                var current = Root;
                while (current.CompareTo(item) != 0)
                {
                    int i = item.CompareTo(current);
                    if (i == -1 && current.Left == null)
                        current.Left = item;
                    if (i == 1 && current.Right == null)
                        current.Right = item;
                    current = i == 1 ? current.Right : current.Left;
                }
            }
            else
            {
                var current = Root;
                while (current.CompareTo(item) != 0)
                {
                    var next = item.CompareTo(current) == -1 ? current.Left : current.Right;
                    if (next == null || !next.Inbalanced)
                    {
                        float i = current.CompareTo(Root);
                        MovieNode previous = null;
                        if (i != 0)
                        {
                            i = current.CompareTo(current.Parent.Left) - 0.5f;
                            previous = current.Parent;
                        }
                        var rebuiltTree = Build(current.Children.Union(new List<MovieNode> { current, item }));
                        if (i == 0)
                            Root = rebuiltTree;
                        else if (i < 0)
                            previous.Left = rebuiltTree;
                        else
                            previous.Right = rebuiltTree;
                        break;
                    }
                }
            }            
        }
    }
}
