# PCache
 Simple C# caching system

# Usage
 - DateTime expirationTime = new DateTime(2023, 5, 31, 0, 0, 0); // set the expiration time to May 31st, 2023 at midnight
 - Cache("mykey", myData, false, expirationTime);
