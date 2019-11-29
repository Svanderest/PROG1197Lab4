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
    }
}
