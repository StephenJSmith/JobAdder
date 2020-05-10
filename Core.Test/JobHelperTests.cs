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
        var expected = "reliable (5), data-entry (4), consultation (3)";
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

    [TestClass]
    public class GetJobCandidateMatchedSkills {
      [TestMethod]
      public void NoMatchesReturnsEmptyList() {
        // Arrange
        var jobId = 13;
        var jobSkills = new List<JobSkill>{
          new JobSkill{JobId = jobId, Name = "architecture", Weighting = 10},
          new JobSkill{JobId = jobId, Name = "communication", Weighting = 8},
          new JobSkill{JobId = jobId, Name = "detail", Weighting = 5}
        };

        var candidateId = 113;
        var candidateSkills = new List<CandidateSkill>{
          new CandidateSkill{CandidateId = candidateId, Name = "plumbing", Weighting = 8},
          new CandidateSkill{CandidateId = candidateId, Name = "carpentry", Weighting = 5},
          new CandidateSkill{CandidateId = candidateId, Name = "bricklaying", Weighting = 3}
        };

        // Act
        var actual = JobHelper.GetJobCandidateMatchedSkills(jobSkills, candidateSkills);

        // Assert
        Assert.IsFalse(actual.Any());
      }

      [TestMethod]
      public void SingleNameMatchInReturnedList() {
        // Arrange
        var matchingSkill = "detail";
        var jobId = 13;
        var jobSkills = new List<JobSkill>{
          new JobSkill{JobId = jobId, Name = "architecture", Weighting = 10},
          new JobSkill{JobId = jobId, Name = "communication", Weighting = 8},
          new JobSkill{JobId = jobId, Name = matchingSkill, Weighting = 5}
        };

        var candidateId = 113;
        var candidateSkills = new List<CandidateSkill>{
          new CandidateSkill{CandidateId = candidateId, Name = "plumbing", Weighting = 8},
          new CandidateSkill{CandidateId = candidateId, Name = matchingSkill, Weighting = 5},
          new CandidateSkill{CandidateId = candidateId, Name = "bricklaying", Weighting = 3}
        };

        // Act
        var actual = JobHelper.GetJobCandidateMatchedSkills(jobSkills, candidateSkills);

        // Assert
        Assert.AreEqual(1, actual.Count);
        Assert.AreEqual(matchingSkill, actual.First().Name);
        Assert.AreEqual(5, actual.First().JobWeighting);
        Assert.AreEqual(5, actual.First().CandidateWeighting);
      }

      [TestMethod]
      public void MultipleNameMatchesInReturnedList() {
        // Arrange
        var matchingSkill1 = "communication";
        var matchingSkill2 = "detail";
        var jobId = 13;
        var jobSkills = new List<JobSkill>{
          new JobSkill{JobId = jobId, Name = "architecture", Weighting = 10},
          new JobSkill{JobId = jobId, Name = matchingSkill1, Weighting = 8},
          new JobSkill{JobId = jobId, Name = matchingSkill2, Weighting = 5}
        };

        var candidateId = 113;
        var candidateSkills = new List<CandidateSkill>{
          new CandidateSkill{CandidateId = candidateId, Name = "plumbing", Weighting = 8},
          new CandidateSkill{CandidateId = candidateId, Name = matchingSkill2, Weighting = 5},
          new CandidateSkill{CandidateId = candidateId, Name = "bricklaying", Weighting = 3,},
          new CandidateSkill{CandidateId = candidateId, Name = matchingSkill1, Weighting = 1,}
        };

        // Act
        var actual = JobHelper.GetJobCandidateMatchedSkills(jobSkills, candidateSkills);

        // Assert
        Assert.AreEqual(2, actual.Count);
        var skill1 = actual.First(s => s.Name == matchingSkill1);
        Assert.AreEqual(8, skill1.JobWeighting);
        Assert.AreEqual(1, skill1.CandidateWeighting);

        var skill2 = actual.First(s => s.Name == matchingSkill2);
        Assert.AreEqual(5, skill2.JobWeighting);
        Assert.AreEqual(5, skill2.CandidateWeighting);
      }
    }
  }
}