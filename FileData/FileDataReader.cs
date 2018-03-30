using System;
using System.Collections.Generic;
using System.Linq;
using ThirdPartyTools;

namespace FileData
{
    public class FileDataReader
    {
        FileDetails _fileDetails;
        public FileDataReader(FileDetails fileDetails)
        {
            _fileDetails = fileDetails;
        }

        private static readonly string[] _versionArguments = new string[]
        {
            @"–v",@"--v",@"/v",@"--version"
        };

        private static readonly string[] _sizeArguments = new string[]
        {
            @"–s",@"--s",@"/s",@"--size"
        };

        public string GetDetail(string detailTypeString, string filePath)
        {
            var detailType = ParseDetailTypeString(detailTypeString);
            switch (detailType)
            {
                case DetailType.version:
                    return _fileDetails.Version(filePath);
                case DetailType.size:
                    return _fileDetails.Size(filePath).ToString();
                default:
                    throw GetInvalidDetailTypeException(detailTypeString);
            }
        }

        private static DetailType ParseDetailTypeString(string detailTypeString)
        {
            if (_versionArguments.Contains(detailTypeString))
            {
                return DetailType.version;
            }
            if (_versionArguments.Contains(detailTypeString))
            {
                return DetailType.size;
            }
            throw GetInvalidDetailTypeException(detailTypeString);
        }

        private static ArgumentException GetInvalidDetailTypeException(string detailTypeString)
        {
            return new ArgumentException($"'{detailTypeString}' is not a valid detail type argument.{Environment.NewLine}" +
                $"Please use '{_versionArguments[0]}' or '{_versionArguments[3]}' as the first argument to read the file version or '{_sizeArguments[0]}' or '{_sizeArguments[3]}' as the first argument to read the file size.");
        }
    }
}
