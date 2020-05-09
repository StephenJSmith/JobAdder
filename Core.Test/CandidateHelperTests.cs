using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Core.Helpers;
using Core.Entities;

namespace Core.Test
{
  [TestClass]
  public class CandidateHelperTests
  {
    [TestClass]
    public class GetCandidateSkills
    {
      [TestMethod]
      public void ReturnsCandidateIdOnAllSkills()
      {
        // Arrange
        var testId = 20;
        var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

        // Assert
        Assert.AreEqual(5, actual.Count());
        Assert.IsTrue(actual.All(s => s.CandidateId == testId));
      }

      [TestMethod]
      public void ReturnListFromCsvWithNoSpaces()
      {
        // Arrange
        var testId = 20;
        var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

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
        var testId = 20;
        var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

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
        var testId = 20;
        var testSkillTags = "fundraising, swift, creativity, reliable, cooking";
        var testWeightings = new List<int> { 5, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

        // Assert
        Assert.AreEqual(5, actual.Count());
        Assert.AreEqual(testWeightings[0], actual[0].Weighting);
        Assert.AreEqual(testWeightings[1], actual[1].Weighting);
        Assert.AreEqual(testWeightings[2], actual[2].Weighting);
        Assert.AreEqual(0, actual[3].Weighting);
        Assert.AreEqual(0, actual[4].Weighting);
      }

      [TestMethod]
      public void ReturnListWithDuplicatedSkillsRemoved()
      {
        // Arrange
        var testId = 20;
        var testSkillTags = "reliable, reliable, ms-office, xcode, detail";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

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
        var testId = 20;
        var testSkillTags = "reliable, reliable, ms-office, xcode, detail";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = CandidateHelper.GetCandidateSkills(testId, testSkillTags, testWeightings);

        // Assert
        Assert.AreEqual(4, actual.Count());
        Assert.AreEqual(testWeightings[0], actual[0].Weighting);
        Assert.AreEqual(testWeightings[1], actual[1].Weighting);
        Assert.AreEqual(testWeightings[2], actual[2].Weighting);
        Assert.AreEqual(testWeightings[3], actual[3].Weighting);
      }
    }

    [TestClass]
    public class GetCandidatesSkillsCsv
    {
      [TestMethod]
      public void ReturnsCsvOfSkillsNames()
      {
        // Arrange
        var expected = "illustrator, oration, kotlin";
        var testId = 23;
        var testSkills = new List<CandidateSkill> {
                    new CandidateSkill {
                        CandidateId = testId,
                        Name = "illustrator",
                        Weighting = 5
                    },
                    new CandidateSkill {
                        CandidateId = testId,
                        Name = "oration",
                        Weighting = 4
                    },
                    new CandidateSkill {
                        CandidateId = testId,
                        Name = "kotlin",
                        Weighting = 3
                    }
                };

        // Act
        var actual = CandidateHelper.GetCandidatesSkillsCsv(testSkills);

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }
  }
}
