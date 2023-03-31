using Newtonsoft.Json;
using Oefentoets1MVC.Models;

namespace Oefentoets1MVC.Utils;

public static class SessionExtensions
{
	public static void SetObject(this ISession session, string key, object value)
	{
		session.SetString(key, JsonConvert.SerializeObject(value));
	}

	public static void AddToLog(this ISession session, string message)
	{
		var whatHappend = GetObject<WhatHappened>(session, "log") ?? new WhatHappened();
		whatHappend.Add(message);
		SetObject(session, "log", whatHappend);
	}

	public static List<string> GetLog(this ISession session)
	{
		var log = GetObject<WhatHappened>(session, "log") ?? new WhatHappened();
		return log.Export();
	}

	public static T? GetObject<T>(this ISession session, string key)
	{
		var value = session.GetString(key);
		return value is not null ? JsonConvert.DeserializeObject<T>(value) : default(T);
	}
}