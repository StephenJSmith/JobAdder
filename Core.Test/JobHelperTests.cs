using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test
{
  [TestClass]
  public class JobHelperTests
  {
    [TestClass]
    public class GetJobSkills
    {
      [TestMethod]
      public void ReturnJobIdOnAllSkills()
      {
        // Arrange
        var testId = 15;
        var testSkills = "detail, ms-office, word, outlook, data-entry, communication";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(6, actual.Count);
        Assert.IsTrue(actual.All(s => s.JobId == testId));
      }

      [TestMethod]
      public void ReturnListFromCsvWithNoSpaces()
      {
        // Arrange
        var testId = 15;
        var testSkills = "mobile, java, swift, objective-c, iOS, xcode, fastlane, aws, kotlin, hockey-app";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(10, actual.Count());
        Assert.AreEqual("mobile", actual[0].Name);
        Assert.AreEqual("java", actual[1].Name);
        Assert.AreEqual("swift", actual[2].Name);
        Assert.AreEqual("objective-c", actual[3].Name);
        Assert.AreEqual("iOS", actual[4].Name);
        Assert.AreEqual("xcode", actual[5].Name);
        Assert.AreEqual("fastlane", actual[6].Name);
        Assert.AreEqual("aws", actual[7].Name);
        Assert.AreEqual("kotlin", actual[8].Name);
        Assert.AreEqual("hockey-app", actual[9].Name);
      }

      [TestMethod]
      public void ReturnListWithCorrectWeightings()
      {
        // Arrange
        var testId = 15;
        var testSkills = "mobile, java, swift, objective-c, iOS, xcode, fastlane, aws, kotlin, hockey-app";
        var testWeightings = new List<int> { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(10, actual.Count());
        Assert.AreEqual(testWeightings[0], actual[0].Weighting);
        Assert.AreEqual(testWeightings[1], actual[1].Weighting);
        Assert.AreEqual(testWeightings[2], actual[2].Weighting);
        Assert.AreEqual(testWeightings[3], actual[3].Weighting);
        Assert.AreEqual(testWeightings[4], actual[4].Weighting);
        Assert.AreEqual(testWeightings[5], actual[5].Weighting);
        Assert.AreEqual(testWeightings[6], actual[6].Weighting);
        Assert.AreEqual(testWeightings[7], actual[7].Weighting);
        Assert.AreEqual(testWeightings[8], actual[8].Weighting);
        Assert.AreEqual(testWeightings[9], actual[9].Weighting);
      }

      [TestMethod]
      public void ReturnsListWithZeroWeightingsWhereMoreSkillsThanWeightings()
      {
        // Arrange
        var testId = 15;
        var testSkills = "mobile, java, swift, objective-c, iOS, xcode, fastlane, aws, kotlin, hockey-app";
        var testWeightings = new List<int> { 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(10, actual.Count());
        Assert.AreEqual(testWeightings[0], actual[0].Weighting);
        Assert.AreEqual(testWeightings[1], actual[1].Weighting);
        Assert.AreEqual(testWeightings[2], actual[2].Weighting);
        Assert.AreEqual(testWeightings[3], actual[3].Weighting);
        Assert.AreEqual(testWeightings[4], actual[4].Weighting);
        Assert.AreEqual(testWeightings[5], actual[5].Weighting);
        Assert.AreEqual(testWeightings[6], actual[6].Weighting);
        Assert.AreEqual(0, actual[7].Weighting);
        Assert.AreEqual(0, actual[8].Weighting);
        Assert.AreEqual(0, actual[9].Weighting);
      }

      [TestMethod]
      public void ReturnsListWithDuplicatedSkillsRemoved()
      {
        // Arrange
        var testId = 15;
        var testSkills = "administration, outlook, spreadsheets, outlook, ordering";
        var testWeightings = new List<int> { 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(4, actual.Count());
        Assert.AreEqual("administration", actual[0].Name);
        Assert.AreEqual("outlook", actual[1].Name);
        Assert.AreEqual("spreadsheets", actual[2].Name);
        Assert.AreEqual("ordering", actual[3].Name);
      }

      [TestMethod]
      public void ReturnsListWithCorrectWeightingWhereDuplicatedSkillsRemoved()
      {
        // Arrange
        var testId = 15;
        var testSkills = "administration, outlook, spreadsheets, outlook, ordering";
        var testWeightings = new List<int> { 7, 6, 5, 4, 3, 2, 1 };

        // Act
        var actual = JobHelper.GetJobSkills(testId, testSkills, testWeightings);

        // Assert
        Assert.AreEqual(4, actual.Count());
        Assert.AreEqual(testWeightings[0], actual[0].Weighting);
        Assert.AreEqual(testWeightings[1], actual[1].Weighting);
        Assert.AreEqual(testWeightings[2], actual[2].Weighting);
        Assert.AreEqual(testWeightings[3], actual[3].Weighting);
      }
    }

    [TestClass]
    public class GetJobSkillsCsv
    {
      [TestMethod]
      public void ReturnsExpectedCsvOfSkillsNames()
      {
        // Arrange
        var expected = "reliable, data-entry, consultation";
        var testId = 42;
        var testSkills = new List<JobSkill> {
                    new JobSkill {
                        JobId = testId,
                        Name = "reliable",
                        Weighting = 5
                    },
                    new JobSkill {
                        JobId = testId,
                        Name = "data-entry",
                        Weighting = 4
                    },
                    new JobSkill {
                        JobId = testId,
                        Name = "consultation",
                        Weighting = 3
                    }
                };

        // Act
        var actual = JobHelper.GetJobSkillsCsv(testSkills);

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }
  }
}