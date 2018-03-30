using FileData;
using System;
using ThirdPartyTools;
using Xunit;

namespace FileDataTests
{
    public class FileDataReaderTests
    {
        private readonly FileDetails _fileData = new FileDetails();

        [Theory]
        [InlineData("-v")]
        [InlineData("--v")]
        [InlineData("/v")]
        [InlineData("--version")]
        public void GetDetailForValidVersionStrings(string detailTypeString)
        {
            var reader = new FileDataReader(_fileData);

            var result = reader.GetDetail(detailTypeString, "c:/test.txt");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var parts = result.Split('.');
            Assert.Equal(3, parts.Length);  //there are three parts
            Assert.All(parts, p =>
                Assert.True(int.TryParse(p, out int versionPart))   //each part can be parsed as an int.
            );
        }

        [Theory]
        [InlineData("-s")]
        [InlineData("--s")]
        [InlineData("/s")]
        [InlineData("--size")]
        public void GetDetailForValidSizeStrings(string detailTypeString)
        {
            var reader = new FileDataReader(_fileData);

            var result = reader.GetDetail(detailTypeString, "c:/test.txt");

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(int.TryParse(result, out int size));    //the size string can be parsed as an int
        }

        [Theory]
        [InlineData("blancmange")]
        [InlineData("s")]
        [InlineData("---s")]
        [InlineData(@"\s")]
        [InlineData("---size")]
        [InlineData("v")]
        [InlineData("---v")]
        [InlineData(@"\v")]
        [InlineData("---version")]
        public void GetDetailForInvalidStrings(string detailTypeString)
        {
            var reader = new FileDataReader(_fileData);

            var result = Assert.Throws<ArgumentException>(() => reader.GetDetail(detailTypeString, "c:/test.txt"));

            Assert.NotNull(result);
            Assert.Contains(detailTypeString, result.Message);
            Assert.Equal($"'{detailTypeString}' is not a valid detail type argument.{Environment.NewLine}" +
                $"Please use '-v' or '--version' as the first argument to read the file version or '-s' or '--size' as the first argument to read the file size.", result.Message);

        }
    }
}
