using System.IO;
using Infinity.Plugins.BIF;

namespace Infinity.Tools.Helpers
{
    internal static class BifFileHelper
    {
        public static void ExtractResources(BIFPlugin plugin, string bifFile, string extractionDirectory)
        {
            using (var filestream = new FileStream(bifFile, FileMode.Open))
            {
                var bifResource = plugin.Import(filestream);
                var resourceCount = 0;
                foreach(var fileEntry in bifResource.FileEntries)
                {
                    byte[] fileBytes = GetFileBytes(filestream, fileEntry.Offset, fileEntry.Size);
                    string extractedFilename = GenerateResourceFilename(extractionDirectory, resourceCount, fileEntry.Type);

                    WriteFile(fileBytes, extractedFilename);
                    resourceCount++;
                }

                foreach(var tileEntry in bifResource.TilesetEntries)
                {
                    byte[] fileBytes = GetFileBytes(filestream, tileEntry.Offset, tileEntry.Size);
                    var extractedFilename = GenerateResourceFilename(extractionDirectory, resourceCount, tileEntry.Type);

                    WriteFile(fileBytes, extractedFilename);
                    resourceCount++;
                }
            }
        }

        private static byte[] GetFileBytes(FileStream filestream, int fileOffset, int fileSize)
        {
            filestream.Seek(fileOffset, SeekOrigin.Begin);
            var fileBytes = new byte[fileSize];
            filestream.Read(fileBytes, 0, fileBytes.Length);
            return fileBytes;
        }

        private static void WriteFile(byte[] fileBytes, string extractedFilename)
        {
            using(var extractedFile = new FileStream(extractedFilename, FileMode.Create))
            { extractedFile.Write(fileBytes, 0, fileBytes.Length); }
        }

        private static string GenerateResourceFilename(string extractionDirectory, int resourceCount, int resourceType)
        {
            return string.Format("{0}/{1}_{2}.{3}",
                        extractionDirectory, "extractedResource", resourceCount,
                        ResourceTypesHelper.GetResourceStringFromType(resourceType));
        }
    }
}