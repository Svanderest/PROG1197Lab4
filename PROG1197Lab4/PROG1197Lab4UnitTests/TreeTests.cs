using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG1197Lab4Objects;
using System.Linq;

namespace PROG1197Lab4UnitTests
{
    [TestClass]
    public class TreeTests
    {
        [TestMethod]
        public void TestCount()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            int expected = 14;
            int actual = target.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod]
        public void TestDepth()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            int expected = 4;
            int actual = target.Depth;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RootTest()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            string expected = "Children of Men";
            string acutal = target.Root.Title;

            //Assert
            Assert.AreEqual(expected, acutal);
        }

        [TestMethod]
        public void RootAddTest()
        {
            //Arrange
            MovieTree target = MovieTree.Create();
            MovieNode node = new MovieNode
            {
                ReleaseDate = new DateTime(1982, 5, 4),
                Title = "Star Trek II: The Wrath of Khan",
                Director = "Nicholas Meyer",
                Runtime = TimeSpan.FromMinutes(113),
                Rating = 7.4
            };
            target.Add(node);

            //Act
            string expected = "The Matrix";
            string actual = target.Root.Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CountAddTest()
        {
            //Arrange
            MovieTree target = MovieTree.Create();
            MovieNode node = new MovieNode
            {
                ReleaseDate = new DateTime(1982, 5, 4),
                Title = "Star Trek II: The Wrath of Khan",
                Director = "Nicholas Meyer",
                Runtime = TimeSpan.FromMinutes(113),
                Rating = 7.4
            };
            target.Add(node);

            //Act
            int expected = 15;
            int actual = target.Count;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DisplayAddTest()
        {
            //Arrange
            MovieTree target = MovieTree.Create();
            MovieNode node = new MovieNode
            {
                ReleaseDate = new DateTime(1982, 5, 4),
                Title = "Star Trek II: The Wrath of Khan",
                Director = "Nicholas Meyer",
                Runtime = TimeSpan.FromMinutes(113),
                Rating = 7.4
            };
            target.Add(node);

            //Act
            int expected = 15;
            int actual = target.Display.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MinAddTest()
        {
            //Arrange
            MovieTree target = MovieTree.Create();
            MovieNode node = new MovieNode
            {
                ReleaseDate = new DateTime(1982, 5, 4),
                Title = "Star Trek II: The Wrath of Khan",
                Director = "Nicholas Meyer",
                Runtime = TimeSpan.FromMinutes(113),
                Rating = 7.4
            };
            target.Add(node);

            //Act
            string expected = "Star Wars";
            string actual = target.Root.Left.Left.Left.Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]        
        public void TestIndexEight()
        {
            //Arrange
            MovieTree tree = MovieTree.Create();
            MovieNode target = tree[8];

            //Act
            string expected = "Star Wars";
            string actual = target.Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIndexEleven()
        {
            //Arrange
            MovieTree tree = MovieTree.Create();
            MovieNode target = tree[11];

            //Act
            string expected = "The Matrix";
            string actual = target.Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]        
        public void TestIndexFifteen()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            MovieNode item = target[15];

            //Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public void TestIndexFullTree()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            int[] expected = new int[] { 109, 107, 113, 115, 106, 97, 144, 121, 117, 140, 136, 127, 148, 169 };
            bool condition = true;
            for (int i = 0; i < expected.Length; i++)
            {
                condition = condition && target[i + 1].Runtime == TimeSpan.FromMinutes(expected[i]);
                if (!condition)
                    throw new Exception($"{target[i + 1].Title} does not have a runtime of {expected[i]} minutes");
            }

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestLt()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            bool condition = true;
            for(int i = 1; i < target.Root.Children.Count + 1; i++)            
                condition = condition && target[i].Left == null || target[i].Left.CompareTo(target[i]) == -1;

            //Assert
            Assert.IsTrue(condition);
        }


        [TestMethod]
        public void TestGt()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            bool condition = true;
            for (int i = 1; i < target.Root.Children.Count + 1; i++)
                condition = condition && target[i].Right == null || target[i].Right.CompareTo(target[i]) == 1;

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestIndex()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            bool condition = true;
            for (int i = 1; i < Math.Pow(2, target.Depth); i++)
            {
                try
                {
                    condition = condition && target.IndexOf(target[i]) == i;
                }
                catch(NullReferenceException)
                {
                    condition = condition && target[i] == null;
                }
            }

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestParent()
        {
            //Arrange
            MovieTree target = MovieTree.Create();

            //Act
            bool condition = true;
            for (int i = 2; i < Math.Pow(2, target.Depth); i++)
                condition = condition &&
                    (
                        target[i / 2].Left == target[i] ||
                        target[i / 2].Right == target[i]
                    );

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestAddFullTree()
        {
            //Arrange
            MovieTree reference = MovieTree.Create();
            MovieTree taget = new MovieTree();

            //Act
            foreach (MovieNode n in reference.Display)
                taget.Add((MovieNode)n.Clone());
            bool condition = true;
            for (int i = 1; i <= taget.Count; i++)
            {
                try
                {
                    condition = condition && taget[i].Equals(reference[i]);
                }
                catch(NullReferenceException)
                {
                    condition = condition && taget[i] == null && reference[i] == null;
                }
            }

            //Assert
            Assert.IsTrue(condition);
        }
    }
}
