using System;
using System.Collections.Generic;

namespace PROG1197Lab4Objects
{
    public class MovieNode : IComparable<MovieNode>, IEquatable<MovieNode>
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public TimeSpan Runtime { get; set; }
        public string Director { get; set; }
        public float Rating { get; set; }

        private MovieNode right;
        private MovieNode left;

        public MovieNode Parent { get; private set; }
        public MovieNode Right
        {
            get { return right; }
            set
            {
                value.Parent = this;
                right = value;
            }
        }

        public MovieNode Left
        {
            get { return left; }
            set
            {
                value.Parent = this;
                left = value;
            }
        }

        public List<MovieNode> Children
        {
            get
            {
                List<MovieNode> result = new List<MovieNode>();
                if (right != null)
                {
                    result.Add(right);
                    result.AddRange(right.Children);
                }
                if (left != null)
                {
                    result.Add(left);
                    result.AddRange(left.Children);
                }
                return result;
            }
        }

        public bool Inbalanced
        {
            get
            {
                return (left == null ^ right == null) || (right?.Inbalanced ?? false) || (left?.Inbalanced ?? false);
            }
        }

        public int CompareTo(MovieNode other)
        {
            if (this.ReleaseDate < other.ReleaseDate)
                return -1;
            else if (this.ReleaseDate > other.ReleaseDate)
                return 1;
            else
                return this.Title.CompareTo(other.Title);
        }

        public bool Equals(MovieNode other)
        {
            return this.Title == other.Title
                && this.ReleaseDate == other.ReleaseDate
                && this.Runtime == other.Runtime
                && this.Director == other.Director
                && this.Rating == other.Rating;
        }
    }
}
