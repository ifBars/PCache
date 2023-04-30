using System.Runtime.Serialization.Formatters.Binary;

public class PCache
{
    private static Dictionary<string, CacheEntry> cache = new Dictionary<string, CacheEntry>();

    private static string GetCacheFilePath(string key)
    {
        var fileName = $"{key}.cache";
        var cacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApp", "Cache");
        Directory.CreateDirectory(cacheDirectory);
        return Path.Combine(cacheDirectory, fileName);
    }

    private static string GetKeyFromCacheFilePath(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        return fileName;
    }

    public static void Load()
    {
        var cacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyApp", "Cache");
        Directory.CreateDirectory(cacheDirectory);
        var cacheFiles = Directory.GetFiles(cacheDirectory, "*.cache");

        foreach (var file in cacheFiles)
        {
            using (var stream = new FileStream(file, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var entry = (CacheEntry)formatter.Deserialize(stream);

                if (entry.ExpirationTime > DateTime.Now)
                {
                    cache[GetKeyFromCacheFilePath(file)] = entry;
                }
                else
                {
                    File.Delete(file);
                }
            }
        }
    }

    public static void Cache(string key, object data, bool permanent, DateTime expirationTime)
    {
        var entry = new CacheEntry
        {
            Data = data,
            TimeCached = DateTime.Now,
            Permanent = permanent,
            ExpirationTime = expirationTime
        };

        if (!permanent)
        {
            cache[key] = entry;
        }
        else
        {
            var filePath = GetCacheFilePath(key);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, entry);
            }
        }
    }

    public static object Retrieve(string key)
    {
        if (cache.ContainsKey(key))
        {
            var entry = cache[key];
            if (entry.ExpirationTime > DateTime.Now)
            {
                return entry.Data;
            }
            else
            {
                cache.Remove(key);
            }
        }
        else
        {
            Load();
        }

        return null;
    }
}
