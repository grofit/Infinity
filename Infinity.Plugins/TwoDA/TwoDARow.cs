using System.Collections.Generic;
using netextender.extensions;

namespace Infinity.Plugins.TwoDA
{
    public class TwoDARow
    {
        /// <summary>
        /// The name of the row
        /// </summary>
        public string RowName { get; set; }

        /// <summary>
        /// The data contained within each cell
        /// </summary>
        public IList<string> RowData { get; set; }

        public TwoDARow() : this(string.Empty, new List<string>()){}

        public TwoDARow(string rowName, IList<string> rowData)
        {
            RowName = rowName;
            RowData = rowData;
        }
        
        public override bool Equals(object obj)
        {
            if(!(obj is TwoDARow))
            { return base.Equals(obj);}

            var castObj = obj as TwoDARow;
            return (RowName.Equals(castObj.RowName) &&
                    RowData.SameAs(castObj.RowData));
        }
    }
}