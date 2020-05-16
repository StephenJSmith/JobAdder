using Core.Specifications;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test.Spcifications
{
  [TestClass]
  public class PageSpecParamsTests
  {
    [TestClass]
    public class PageNumber
    {
      [TestMethod]
      public void DefaultPageNumberIs1()
      {
        // Arrange
        var expected = 1;
        var sut = new PageSpecParams();

        // Act
        var actual = sut.PageNumber;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void PageNumberIsSetPageNumber()
      {
        // Arrange
        var testPageNumber = 3;
        var expected = testPageNumber;
        var sut = new PageSpecParams();
        sut.PageNumber = testPageNumber;

        // Act
        var actual = sut.PageNumber;

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }

    [TestClass]
    public class MaxPageSize
    {
      [TestMethod]
      public void HardCodedMaxPageSizeIs50()
      {
        // Arrange
        var expected = 50;
        var testPageSize = 200;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void ConfigurationMaxPageSizeOverridesHardcodedValue()
      {
        // Arrange
        var configMaxPageSize = 75;
        var configDefaultPageSize = 25;
        var expected = configMaxPageSize;
        var testPageSize = 200;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }

    [TestClass]
    public class DefaultPageSize
    {
      [TestMethod]
      public void HardCodedDefaultIs10()
      {
        // Arrange
        var expected = 10;
        var sut = new PageSpecParams();

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void ConfigurationDefaultPageSizeOverridesHardCodedValue()
      {
        // Arrange
        var configDefaultPageSize = 15;
        var configMaxPageSize = 40;
        var expected = configDefaultPageSize;
        var sut = new PageSpecParams();
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void SetPageSizeOverridesConfigurationDefaults()
      {
        // Arrange
        var testPageSize = 9;
        var configDefaultPageSize = 15;
        var configMaxPageSize = 40;
        var expected = testPageSize;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void SetPageSizeCanNotOverridesConfigurationMaxPageSize()
      {
        // Arrange
        var testPageSize = 45;
        var configDefaultPageSize = 15;
        var configMaxPageSize = 40;
        var expected = configMaxPageSize;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.PageSize;

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }

    [TestClass]
    public class Skip
    {
      [TestMethod]
      public void SkipForPage1IsZero()
      {
        // Arrange
        var expected = 0;
        var testPageNr = 1;
        var sut = new PageSpecParams();
        sut.PageNumber = testPageNr;

        // Act
        var actual = sut.Skip;

        // assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void SkipForPage2IsPageSize()
      {
        // Arrange
        var testPageNr = 2;
        var testPageSize = 15;
        var expected = testPageSize;
        var sut = new PageSpecParams();
        sut.PageNumber = testPageNr;
        sut.PageSize = testPageSize;

        // Act
        var actual = sut.Skip;

        // Asse
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void SkipForPageNIsPageSizeTimesNMinus1()
      {
        // Arrange
        var testPageNr = 5;
        var testPageSize = 15;
        var expected = (testPageNr - 1) * testPageSize;
        var sut = new PageSpecParams();
        sut.PageNumber = testPageNr;
        sut.PageSize = testPageSize;

        // Act
        var actual = sut.Skip;

        // Asse
        Assert.AreEqual(expected, actual);
      }
    }

    [TestClass]
    public class Take
    {
      [TestMethod]
      public void TakeValueIsConfiguredPageSize()
      {
        // Arrange
        var configMaxPageSize = 60;
        var configDefaultPageSize = 20;
        var expected = configDefaultPageSize;
        var sut = new PageSpecParams();
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.Take;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void TakeValueIsSetPageSize()
      {
        // Arrange
        var testPageSize = 15;
        var configMaxPageSize = 60;
        var configDefaultPageSize = 20;
        var expected = testPageSize;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.Take;

        // Assert
        Assert.AreEqual(expected, actual);
      }

      [TestMethod]
      public void TakeValueIsMaxPageSizeWhenSetPageSizeIsGreater()
      {
        // Arrange
        var testPageSize = 75;
        var configMaxPageSize = 60;
        var configDefaultPageSize = 20;
        var expected = configMaxPageSize;
        var sut = new PageSpecParams();
        sut.PageSize = testPageSize;
        sut.ApplyConfigurationDefaults(configMaxPageSize, configDefaultPageSize);

        // Act
        var actual = sut.Take;

        // Assert
        Assert.AreEqual(expected, actual);
      }
    }
  }
}