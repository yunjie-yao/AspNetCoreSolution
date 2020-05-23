using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YangXuAPI.Services
{
    public class PropertyMapping<TSource,TDestination>
    {
        public Dictionary<string,PropertyMappingValue> MappingDictionary { get;private set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary
                                 ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
