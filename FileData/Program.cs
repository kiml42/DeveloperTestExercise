using System;
using System.Diagnostics;
using ThirdPartyTools;

namespace FileData
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var detail = new FileDataReader(new FileDetails()).GetDetail(args);
            Console.WriteLine(detail);
            Debug.WriteLine(detail);
        }
    }
}
