using Inventory.Localization;
using Microsoft.Extensions.Caching.Distributed;

namespace Inventory.Extension
{
    public static class LocalizationExtension
    {
        private static readonly IDistributedCache? _distributedCache;

        //public  LocalizationExtension(IDistributedCache distributedCache)
        //{
        //    _distributedCache = distributedCache;
        //}
        public static string GetLocalization(string key, string language)
        {
            Localizer localizer = new Localizer(_distributedCache);
            localizer.GetValue(key, language);
            return localizer.GetValue(key, language);
        }

    }
}
