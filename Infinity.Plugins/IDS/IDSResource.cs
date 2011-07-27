using System.Collections.Generic;
using netextender.extensions;

namespace Infinity.Plugins.IDS
{
    public class IDSResource
    {
        /// <summary>
        /// The default value to be used incase no value exists for the mapping
        /// </summary>
        public int DefaultValue { get; set; }

        /// <summary>
        /// The mappings contained within the IDS resource
        /// </summary>
        public IDictionary<int, string> Mappings { get; private set; }

        public IDSResource() : this(0, new Dictionary<int, string>()) {}

        public IDSResource(int defaultValue, IDictionary<int, string> mappings)
        {
            DefaultValue = defaultValue;
            Mappings = mappings;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IDSResource))
            { return base.Equals(obj); }

            var castObj = obj as IDSResource;

            return DefaultValue.Equals(castObj.DefaultValue) &&
                   Mappings.SameAs(castObj.Mappings);
        }
    }
}
