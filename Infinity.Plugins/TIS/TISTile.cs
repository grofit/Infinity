using System.Collections.Generic;
using netextender.extensions;

namespace Infinity.Plugins.TIS
{
    public class TISTile
    {
        /// <summary>
        /// Dimensions of the tile, value is same for x/y
        /// </summary>
        public int Dimensions { get; private set; }

        /// <summary>
        /// The RGBA colour palette used for this tile
        /// </summary>
        public IList<TISColour> Palette { get; private set; }

        /// <summary>
        /// The pixels used within this tile, which reference the palette
        /// </summary>
        public IList<byte> Pixels { get; private set; }

        public TISTile() : this(new List<TISColour>(), new List<byte>(), 0)
        {}

        public TISTile(IList<TISColour> palette, IList<byte> pixels, int dimensions)
        {
            Palette = palette;
            Pixels = pixels;
            Dimensions = dimensions;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TISTile))
            { return base.Equals(obj); }

            var castObj = obj as TISTile;
            return (castObj.Dimensions.Equals(Dimensions) &&
                    castObj.Palette.SameAs(Palette) &&
                    castObj.Pixels.SameAs(Pixels));
        }
    }
}
