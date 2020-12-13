using System.Collections.Generic;
using UnityEngine;
using System;
public class JsonHelper
{
	public static List<T> ListFromJson<T>(string json)
	{
		var newJson = "{ \"list\": " + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
		return wrapper.list;
	}

	[Serializable]
	class Wrapper<T>
	{
		public List<T> list;
	}
}