using Newtonsoft.Json;
using UnityEngine;

namespace MoonKart
{
	/// <summary>
	/// Static Class That Store data in player pref
	/// </summary>
	
	public static class PersistentStorage
	{
		// CONSTANTS

		private const int VALUE_TRUE  = 1;
		private const int VALUE_FALSE = 0;

		// PUBLIC METHODS

		public static bool GetBool(string key, bool defaultValue = false)
		{
			int defaultValueInt = defaultValue == true ? VALUE_TRUE : VALUE_FALSE;
			return PlayerPrefs.GetInt(key, defaultValueInt) == VALUE_TRUE;
		}

		public static float GetFloat(string key, float defaultValue = 0f)
		{
			return PlayerPrefs.GetFloat(key, defaultValue);
		}

		public static int GetInt(string key, int defaultValue = 0)
		{
			return PlayerPrefs.GetInt(key, defaultValue);
		}

		public static string GetString(string key, string defaultValue = null)
		{
			return PlayerPrefs.GetString(key, defaultValue);
		}
		public static bool IsSave(string key)
		{
			return PlayerPrefs.HasKey(key);
		}

		public static T GetObjectWithJsonUtility<T>(string key, T defaultValue = default)
		{
			var objectJson = GetString(key);

			if (objectJson.HasValue() == false)
				return defaultValue;

			return JsonUtility.FromJson<T>(objectJson);
		}

		public static T GetObjectWithNewtonsoftJson<T>(string key, T defaultValue = default)
		{
			var objectJson = GetString(key);

			if (objectJson.HasValue() == false)
				return defaultValue;

			return JsonConvert.DeserializeObject<T>(objectJson);
		}

		public static void SetBool(string key, bool value, bool saveImmediately = true)
		{
			PlayerPrefs.SetInt(key, value == true ? VALUE_TRUE : VALUE_FALSE);

			if (saveImmediately == true)
			{
				PlayerPrefs.Save();
			}
		}

		public static void SetInt(string key, int value, bool saveImmediately = true)
		{
			PlayerPrefs.SetInt(key, value);

			if (saveImmediately == true)
			{
				PlayerPrefs.Save();
			}
		}

		public static void SetFloat(string key, float value, bool saveImmediately = true)
		{
			PlayerPrefs.SetFloat(key, value);

			if (saveImmediately == true)
			{
				PlayerPrefs.Save();
			}
		}

		public static void SetString(string key, string value, bool saveImmediately = true)
		{
			PlayerPrefs.SetString(key, value);

			if (saveImmediately == true)
			{
				PlayerPrefs.Save();
			}
		}

		/// <summary>
		/// Set Object to String and Save to player pref
		/// </summary>
		/// <param name="key">Key to Store data</param>
		/// <param name="value"> Value id Data that Store in Playerpref </param>
		/// <param name="saveImmeditely"> save Immeditely</param>
		public static void SetObjectWithJsonUtility(string key, object value, bool saveImmeditely = true)
		{
			var objectJson = JsonUtility.ToJson(value);
			SetString(key, objectJson, saveImmeditely);
		}

		public static void SetObjectWithNewtonsoftJson(string key, object value, bool saveImmeditely = true)
		{
			var objectJson = JsonConvert.SerializeObject(value, Formatting.Indented);
			SetString(key, objectJson, saveImmeditely);
		}

		public static void Delete(string key, bool saveImmediately = true)
		{
			PlayerPrefs.DeleteKey(key);

			if (saveImmediately == true)
			{
				PlayerPrefs.Save();
			}
		}

		public static void Save()
		{
			PlayerPrefs.Save();
		}
	}
}
