using System;
using Xunit;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using CheckLinksConsole;

namespace CheckLinksTests
{
    public class Tests
    {
        [Fact]
        public void WithoutHttpAtStartOfLink_NoLinks()
        {
            var links = LinkChecker.GetLinks("<a href=\"google.com\" />");
            Assert.Equal(0,links.Count());
        }

        [Fact]
        public void WithHttpAtStartOfLink_LinkParses()
        {
        // Arrange
        var links = LinkChecker.GetLinks("<a href=\"https://google.com\" />");
        // Act
        
        // Assert
        Assert.Equal(1, links.Count());
        }
    }
}
