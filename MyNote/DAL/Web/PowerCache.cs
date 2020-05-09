using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;

namespace DAL.Web
{
	public static class PowerCache
	{
		private static readonly Cache SystemCache = PowerCache.InitCache();
		public static int Count
		{
			get
			{
				return PowerCache.SystemCache.Count;
			}
		}
		public static void Remove(string key)
		{
			PowerCache.SystemCache.Remove(key);
		}
		public static void Clear()
		{
			System.Collections.IDictionaryEnumerator enumerator = PowerCache.SystemCache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				PowerCache.Remove(enumerator.Key.ToString());
			}
		}
		public static void Clear(string pattern)
		{
			System.Collections.IDictionaryEnumerator enumerator = PowerCache.SystemCache.GetEnumerator();
			Regex regex = new Regex(pattern, RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
			while (enumerator.MoveNext())
			{
				string text = enumerator.Key.ToString();
				if (regex.IsMatch(text))
				{
					PowerCache.Remove(text);
				}
			}
		}
		public static T Get<T>(string key, Func<T> getValue, CacheDependency dependencies = null, int minutes = 2147483647, CacheItemRemovedCallback onRemoveCallback = null, bool isAbsoluteExpiration = true, CacheItemPriority priority = CacheItemPriority.Normal)
		{
			object obj = PowerCache.SystemCache.Get(key);
			if (obj == null)
			{
				obj = getValue();
				if (isAbsoluteExpiration)
				{
					PowerCache.SystemCache.Insert(key, obj, dependencies, System.DateTime.UtcNow.AddMinutes((double)minutes), Cache.NoSlidingExpiration, priority, onRemoveCallback);
				}
				else
				{
					PowerCache.SystemCache.Insert(key, obj, dependencies, Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes((double)minutes), priority, onRemoveCallback);
				}
			}
			return (T)obj;
		}
		public static void Set<T>(string key, Func<T> getValue, CacheDependency dependencies = null, int minutes = 2147483647, CacheItemRemovedCallback onRemoveCallback = null, bool isAbsoluteExpiration = true, CacheItemPriority priority = CacheItemPriority.Normal)
		{
			T t = getValue();
			if (isAbsoluteExpiration)
			{
				PowerCache.SystemCache.Insert(key, t, dependencies, System.DateTime.Now.AddMinutes((double)minutes), Cache.NoSlidingExpiration, priority, onRemoveCallback);
			}
			else
			{
				PowerCache.SystemCache.Insert(key, t, dependencies, Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes((double)minutes), priority, onRemoveCallback);
			}
		}
		private static Cache InitCache()
		{
			return (HttpContext.Current != null) ? HttpContext.Current.Cache : HttpRuntime.Cache;
		}
	}
}
