using System;
using System.Collections;
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
            using (FileStream fs = new FileStream(@"C:\\Users\\sebas\\source\repos\\PROG1197Lab4\\PROG1197Lab4\\PROG1197Lab4Objects\\MovieText.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                List<string> lines = new List<string>();
                while (!sr.EndOfStream)
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
                tree = new MovieTree(nodes);
            }
            return tree;
        }

        public MovieTree() { }

        public MovieTree(IEnumerable<MovieNode> nodes)
        {
            Root = Build(nodes);
        }

        public MovieNode Root { get; private set; }
        public int Count { get; private set; }

        public int Depth
        {
            get
            {
                if (Root == null)
                    return 0;
                return 1 + (int)Math.Floor(Math.Log(Count,2));
            }
        }

        public MovieNode this[int index]
        {
            get
            {
                if (index <= 0 || index >= Math.Pow(2, Depth))
                    throw new IndexOutOfRangeException();
                var current = Root;
                bool start = false;
                var bits = new BitArray(new int[] { index });
                for (int i = Depth - 1; i >= 0; i--)
                {
                    if (start)
                        current = bits[i] ? current.Right : current.Left;
                    start = start || bits[i];
                }
                return current;
            }
        }

        public int IndexOf(MovieNode item)
        {
            List<bool> bits = new List<bool> { true };
            for (MovieNode current = Root; item.CompareTo(current) != 0; current = item.CompareTo(current) < 0 ? current.Left : current.Right)
                bits.Add(item.CompareTo(current) == 1);
            bits.Reverse();
            int[] index = new int[1];
            new BitArray(bits.ToArray()).CopyTo(index, 0);
            return index[0];
        }

        public IEnumerable<MovieNode> Display
        {
            get
            {
                return new MovieNode[] { Root }.Union(Root.Children);
            }
        }

        public MovieNode Build(IEnumerable<MovieNode> nodes)
        {
            foreach (MovieNode N in nodes.Where(n => nodes.Count(mn => mn.Equals(n)) > 1))
                return Build(nodes.Where(n => !n.Equals(N)).Union(new MovieNode[] { N }));
            Count = Math.Max(Count, nodes.Count());
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
            var nodes = Display.Union(new List<MovieNode> { item });
            foreach (MovieNode n in nodes)
                n.Clear();
            Root = Build(nodes);
        }

        public MovieNode Find(MovieNode item)
        {
            var current = Root;
            while (current != null && item.CompareTo(current) != 0)
                current = item.CompareTo(current) < 0 ? current.Left : current.Right;
            return current;
        }
    }
}
