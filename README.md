# PCache
 Simple C# caching system

# Usage
'''
PCache pCache = new PCache();
DateTime expirationTime = new DateTime(2023, 5, 31, 0, 0, 0); // set the expiration time to May 31st, 2023 at midnight
pCache.Load();
pCache.Cache("mykey", myData, false, expirationTime);
Data myData = pCache.Retreive("mykey");
myData.doSomething();
