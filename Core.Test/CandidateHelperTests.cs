using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Core.Helpers;

namespace Core.Test
{
    [TestClass]
    public class CandidateHelperTests
    {
        [TestClass]
        public class GetCandidateSkills {
            [TestMethod]
            public void ReturnListFromCsvWithNoSpaces() {
                // Arrange
                var sut = new CandidateHelper();
                var testId = 20;
                var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
                var testWeightings = new List<int> {10, 9, 8, 7, 6, 5, 4, 3, 2, 1};
                
                // Act
                var actual = sut.GetCandidateSkills(testId, testSkillTags, testWeightings);

                // Assert
                Assert.AreEqual(5, actual.Count());
                Assert.AreEqual("fundraising", actual[0].Name);
                Assert.AreEqual("swift", actual[1].Name);
                Assert.AreEqual("creativity", actual[2].Name);
                Assert.AreEqual("reliable", actual[3].Name);
                Assert.AreEqual("cooking", actual[4].Name);
            }

            [TestMethod]
            public void ReturnListWithCorrectWeightings()
            {
                // Arrange
                var sut = new CandidateHelper();
                var testId = 20;
                var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
                var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

                // Act
                var actual = sut.GetCandidateSkills(testId, testSkillTags, testWeightings);

                // Assert
                Assert.AreEqual(5, actual.Count());
                Assert.AreEqual(testWeightings[0], actual[0].Weighting);
                Assert.AreEqual(testWeightings[1], actual[1].Weighting);
                Assert.AreEqual(testWeightings[2], actual[2].Weighting);
                Assert.AreEqual(testWeightings[3], actual[3].Weighting);
                Assert.AreEqual(testWeightings[4], actual[4].Weighting);
            }

            [TestMethod]
            public void ReturnListWithZeroWeightingsWhenMoreSkillsThanWeighting()
            {
                // Arrange
                var sut = new CandidateHelper();
                var testId = 20;
                var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
                var testWeightings = new List<int> { 5, 2, 1 };

                // Act
                var actual = sut.GetCandidateSkills(testId, testSkillTags, testWeightings);

                // Assert
                Assert.AreEqual(5, actual.Count());
                Assert.AreEqual(testWeightings[0], actual[0].Weighting);
                Assert.AreEqual(testWeightings[1], actual[1].Weighting);
                Assert.AreEqual(testWeightings[2], actual[2].Weighting);
                Assert.AreEqual(0, actual[3].Weighting);
                Assert.AreEqual(0, actual[4].Weighting);
            }

            [TestMethod]
            public void ReturnListDuplicatedSkillsRemoved()
            {
                // Arrange
                var sut = new CandidateHelper();
                var testId = 20;
                var testSkillTags = "reliable, reliable, ms-office, xcode, detail";
                var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

                // Act
                var actual = sut.GetCandidateSkills(testId, testSkillTags, testWeightings);

                // Assert
                Assert.AreEqual(4, actual.Count());
                Assert.AreEqual("reliable", actual[0].Name);
                Assert.AreEqual("ms-office", actual[1].Name);
                Assert.AreEqual("xcode", actual[2].Name);
                Assert.AreEqual("detail", actual[3].Name);
            }

            [TestMethod]
            public void ReturnListWithCorrectWeightingsWhenDuplicatedSkillsRemoved()
            {
                // Arrange
                var sut = new CandidateHelper();
                var testId = 20;
                var testSkillTags = "reliable, reliable, ms-office, xcode, detail";
                var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

                // Act
                var actual = sut.GetCandidateSkills(testId, testSkillTags, testWeightings);

                // Assert
                Assert.AreEqual(4, actual.Count());
                Assert.AreEqual(testWeightings[0], actual[0].Weighting);
                Assert.AreEqual(testWeightings[1], actual[1].Weighting);
                Assert.AreEqual(testWeightings[2], actual[2].Weighting);
                Assert.AreEqual(testWeightings[3], actual[3].Weighting);
            }
        }
    }
}
