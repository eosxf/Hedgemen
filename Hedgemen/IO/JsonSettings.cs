/*using System;
using Newtonsoft.Json;

namespace Hgm.Engine.IO
{
	public static class JsonSettings
	{
		public static Func<JsonSerializerSettings> ManifestSettings { get; private set; }

		static JsonSettings()
		{
			ManifestSettings = () => new JsonSerializerSettings
			{
				ReferenceLoopHandling = ReferenceLoopHandling.Error,
				MissingMemberHandling = MissingMemberHandling.Error,
				NullValueHandling = NullValueHandling.Include,
				DefaultValueHandling = DefaultValueHandling.Include,
				ObjectCreationHandling = ObjectCreationHandling.Replace,
				PreserveReferencesHandling = PreserveReferencesHandling.None,
				ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
				TypeNameHandling = TypeNameHandling.None,
				MetadataPropertyHandling = MetadataPropertyHandling.Default,
				Formatting = Formatting.Indented,

				ContractResolver = new HedgemenJsonContractResolver()
			};
		}
	}
}*/