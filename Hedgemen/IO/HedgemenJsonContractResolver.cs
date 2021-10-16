/*using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hgm.Engine.IO
{
	public class HedgemenJsonContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			
			var hasJsonPropertyAttribute = property.HasMemberAttribute;
			
			property.ShouldSerialize = o => hasJsonPropertyAttribute;

			return property;
		}
	}
}*/