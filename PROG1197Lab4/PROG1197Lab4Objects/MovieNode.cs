using System;
using System.Collections.Generic;

namespace PROG1197Lab4Objects
{
    public class MovieNode : IComparable<MovieNode>, IEquatable<MovieNode>, ICloneable
    {        
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }        
        public TimeSpan Runtime { get; set; }
        public string Director { get; set; }     
        public double Rating { get; set; }

        public MovieNode Right { get; set; }
        public MovieNode Left { get; set; }

        public List<MovieNode> Children
        {
            get
            {
                List<MovieNode> result = new List<MovieNode>();                
                if (Left != null)
                {
                    result.Add(Left);
                    result.AddRange(Left.Children);
                }
                if (Right != null)
                {
                    result.Add(Right);
                    result.AddRange(Right.Children);
                }
                return result;
            }
        }

        public int CompareTo(MovieNode other)
        {
            if (this.ReleaseDate.Year < other.ReleaseDate.Year)
                return -1;
            else if (this.ReleaseDate.Year > other.ReleaseDate.Year)
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

        public void Clear()
        {
            Right = null;
            Left = null;            
        }

        public override string ToString()
        {
            return Title;
        }

        public object Clone()
        {
            return new MovieNode
            {
                Title = this.Title,
                Director = this.Director,
                Rating = this.Rating,
                Runtime = this.Runtime,
                ReleaseDate = this.ReleaseDate
            };
        }
    }
}
