using System;
using Xunit;

namespace LH.CommandLine.UnitTests.OptionsParser
{
    public class WhenParsingCollections
    {
        /*
         * Collection with spaces cannot be positional!
         * -> Separator
         * -> asda,asd,asd,asda,sd,asdad
         * -> 1|2|3.5|3,5
         * -> 1,2,3,4,5
         * "asasd as adasd,asdasd sda asd a,asdasdas sd asda sd"
         * "asdasd", "asdasd", "asdasd", "asdasdasd", "asdasd" -> quotes are lost when dotnet parses args :/
         */

        [Fact]
        public void ShouldParseIEnumerableOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseArrayOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseReadOnlyCollectionOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseCollectionOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseReadOnlyListOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseListOfIntegers()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldParseCollectionWithCustomParser()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldPassWhenCollectionHasNoItems()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldThrowIfCollectionValidationFails()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ShouldNotIncludeNextNamedOptionIntoCollection()
        {
            throw new NotImplementedException();
        }
    }
}