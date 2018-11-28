using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AdvancedTopicsDemo.Controllers
{
    public class CacheDemoController : Controller
    {
        private readonly IMemoryCache cache;

        public CacheDemoController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public IActionResult Index()
        {
            DateTime cacheEntry;
            if (!this.cache.TryGetValue("NowCache", out cacheEntry))
            {
                cacheEntry = DateTime.UtcNow;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(10));

                cache.Set("NowCache", cacheEntry, cacheEntryOptions);
            }
                
            return View(cacheEntry);
        }
    }
}