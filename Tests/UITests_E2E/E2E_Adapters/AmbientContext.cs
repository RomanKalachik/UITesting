using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace UITests_E2E.E2E_Adapters;

public static class AmbientContext
{
	private static readonly ConcurrentDictionary<string, AsyncLocal<object>> Contexts = new(StringComparer.Ordinal);

	public static void Set<T>(string key, [MaybeNull] T val)
	{
		AsyncLocal<object> keyctx = Contexts.AddOrUpdate(
			key,
			k => new AsyncLocal<object>(),
			(k, al) => al);
		keyctx.Value = val;
	}

	public static T Get<T>(string key)
	{
		return Contexts.TryGetValue(key, out AsyncLocal<object> keyctx)
			? (T)(keyctx!.Value ?? default(T)!)
			: default;
	}
}