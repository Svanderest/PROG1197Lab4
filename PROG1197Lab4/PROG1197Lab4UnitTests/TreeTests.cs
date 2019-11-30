using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG1197Lab4Objects;

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
            int expected = 13;
            int actual = target.Root.Children.Count;

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
            string notExpected = "The Matrix";
            string actual = target.Root.Title;

            //Assert
            Assert.AreEqual(notExpected, actual);
        }
    }
}
