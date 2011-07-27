using System.Collections.Generic;
using netextender.extensions;

namespace Infinity.Plugins.TwoDA
{
    public class TwoDAResource
    {
        /// <summary>
        /// The default value if no value exists
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// The column names contained within this resource
        /// </summary>
        public IList<string> Columns { get; set; }

        /// <summary>
        /// The rows contained within this resource
        /// </summary>
        public IList<TwoDARow> Rows { get; private set; }

        public TwoDAResource() : this(string.Empty, new List<string>(), new List<TwoDARow>()){}

        public TwoDAResource(string defaultValue, IList<string> columns, IList<TwoDARow> rows)
        {
            DefaultValue = defaultValue;
            Columns = columns;
            Rows = rows;
        }
        
        public override bool Equals(object obj)
        {
            if(!(obj is TwoDAResource))
            { return base.Equals(obj); }

            var castObj = obj as TwoDAResource;

            return Columns.SameAs(castObj.Columns) &&
                   DefaultValue.Equals(castObj.DefaultValue) &&
                   Rows.SameAs(castObj.Rows);
        }
    }
}