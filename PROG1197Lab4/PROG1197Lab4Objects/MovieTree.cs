using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace PROG1197Lab4Objects
{
    public class MovieTree
    {
        private static MovieTree tree;

        public static MovieTree Create()
        {
            tree = new MovieTree();
            return tree;
        }

        public MovieNode Root { get; private set; }

        public MovieNode Build(IEnumerable<MovieNode> nodes)
        {
            nodes = nodes.OrderBy(n => n);
            var current = nodes.ElementAt(nodes.Count() / 2);
            if (current != nodes.Max())
                current.Right = Build(nodes.Where(n => n.CompareTo(current) == 1));
            if (current != nodes.Min())
                current.Left = Build(nodes.Where(n => n.CompareTo(current) == -1));
            return current;
        }

        public void Add(MovieNode item)
        {
            if (Root == null)
                Root = item;
            else if (!Root.Inbalanced)
            {
                var current = Root;
                while(current.CompareTo(item) != 0)
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
                while(true)
                {
                    var next = item.CompareTo(current) == -1 ? current.Left : current.Right;
                    if(next == null || !next.Inbalanced)
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
