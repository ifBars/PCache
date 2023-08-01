# PCache
 Simple C# caching system

# Usage

```csharp
DateTime expirationTime = new DateTime(2023, 5, 31, 0, 0, 0); // set the expiration time to May 31st, 2023 at midnight
pCache.Load(); // Load the cache from previously saved file
pCache.Cache("mykey", myData, false, expirationTime); // Cache the object
Data myData = pCache.Retrieve("mykey"); // Retrieve the object
myData.doSomething(); // Do something with cached object
