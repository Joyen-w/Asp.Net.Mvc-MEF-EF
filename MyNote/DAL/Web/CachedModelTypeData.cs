using DAL.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace DAL.Web
{
    public static class CachedModelTypeData
    {
        private const string ModelTypeDataCacheKey = "Power::ModelTypeData::";
        internal static ModelTypeData GetModelTypeData(System.Type type)
        {
            return PowerCache.Get<ModelTypeData>(string.Concat(new object[]
            {
                "Power::ModelTypeData::",
                type.Name,
                "::",
                type.GUID
            }), () => ModelTypeDataProvider.ReflectClass(type), null, 2147483647, null, true, CacheItemPriority.Normal);
        }
    }
}
