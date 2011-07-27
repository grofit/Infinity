using System.Collections.Generic;
using netextender.extensions;

namespace Infinity.Plugins.BIF
{
    public class BIFResource
    {
        /// <summary>
        /// The list of file entries contained within the Bif file
        /// </summary>
        public IList<BIFFileEntry> FileEntries { get; private set; }

        /// <summary>
        /// The list of tileset entries contained within the Bif file
        /// </summary>
        public IList<BIFTilesetEntry> TilesetEntries { get; private set; }

        public BIFResource() : this(new List<BIFFileEntry>(), new List<BIFTilesetEntry>()){}

        public BIFResource(IList<BIFFileEntry> fileEntries, IList<BIFTilesetEntry> tileEntries)
        {
            FileEntries = fileEntries;
            TilesetEntries = tileEntries;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BIFResource))
            { return base.Equals(obj); }

            var castObj = obj as BIFResource;
            return FileEntries.SameAs(castObj.FileEntries) &&
                   TilesetEntries.SameAs(castObj.TilesetEntries);
        }
    }
}
