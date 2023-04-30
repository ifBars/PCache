using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class CacheEntry
    {

        // Define a class to represent a cache entry
            public object? Data { get; set; }
            public DateTime TimeCached { get; set; }
            public DateTime ExpirationTime { get; set; }
            public bool Permanent { get; set; }

    }
