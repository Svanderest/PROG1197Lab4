using Microsoft.VisualStudio.TestTools.UnitTesting;
using PROG1197Lab4Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG1197Lab4UnitTests
{
    [TestClass]
    public class NodeTests
    {
        private IEnumerable<MovieNode> ReadNodes()
        {
            List<string> lines = new List<string>();
            using (FileStream fs = new FileStream(@"C:\\Users\\sebas\\source\repos\\PROG1197Lab4\\PROG1197Lab4\\PROG1197Lab4Objects\\MovieText.txt", FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }                
            }
            return lines.Select(l => l.Split(';')).Select(line =>
                new MovieNode
                {
                    Title = line[0],
                    ReleaseDate = Convert.ToDateTime(line[1]),
                    Runtime = TimeSpan.FromMinutes(Convert.ToInt32(line[2])),
                    Director = line[3],
                    Rating = Convert.ToDouble(line[4])
                });
        }

        [TestMethod]
        public void TestBuild()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            int expected = 15;
            int actual = target.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestLtComparison()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            int expected = -1;
            int actual = target.ElementAt(1).CompareTo(target.ElementAt(0));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestGtComparison()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            int expected = 1;
            int actual = target.ElementAt(0).CompareTo(target.ElementAt(1));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEqComparison()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            int expected = 0;
            int actual = target.ElementAt(4).CompareTo(target.ElementAt(13));

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestEq()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            bool condition = target.ElementAt(4).Equals(target.ElementAt(13));

            //Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void TestMax()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            string expected = "The Martian";
            string actual = target.Max().Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMin()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            string expected = "Star Wars";
            string actual = target.Min().Title;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SortLtTest()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes().OrderBy(n => n);

            //Act            
            bool ordered = true;
            for (int i = 0; i < target.Count() - 1; i++)
                ordered = ordered && target.ElementAt(i).CompareTo(target.ElementAt(i + 1)) <= 0;

            //Assert
            Assert.IsTrue(ordered);
        }

        [TestMethod]
        public void SortGtTest()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes().OrderBy(n => n);

            //Act
            bool ordered = true;
            for (int i = 1; i < target.Count(); i++)
                ordered = ordered && target.ElementAt(i).CompareTo(target.ElementAt(i - 1)) >= 0;

            //Assert
            Assert.IsTrue(ordered);
        }

        [TestMethod]
        public void DistinctTest()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes().Distinct<MovieNode>();

            //Act
            int expected = 14;
            int actual = target.Count();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CloneTest()
        {
            //Arrange
            IEnumerable<MovieNode> target = ReadNodes();

            //Act
            bool condition = true;
            foreach (MovieNode n in target)
                condition = condition && n.Equals((MovieNode)n.Clone());

            //Assert
            Assert.IsTrue(condition);
        }
    }
}
