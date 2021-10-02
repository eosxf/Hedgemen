using System;

namespace Hgm.Utilities
{
	[Serializable]
	public struct ResourceName
	{
		/*public static ResourceName Empty => new ResourceName(EmptyNamespace, EmptyName);

		public static string EmptyNamespace => "any";

		public static string EmptyName => "null";

		public bool IsEmpty => Equals(Empty);
		
		private string ns;

		private string name;

		public string Namespace
		{
			get
			{
				ns ??= EmptyNamespace;
				return ns;
			}
			set => ns = value;
		}
		
		public string Name
		{
			get
			{
				name ??= EmptyName;
				return name;
			}
			set => name = value;
		}

		public ResourceName(string resourceNamespace, string name)
		{
			ns = resourceNamespace;
			this.name = name;
		}

		[JsonConstructor]
		public ResourceName(string resource)
		{
			if (resource == null)
			{
				ns = EmptyNamespace;
				name = EmptyName;
				return;
			}
			
			var names = resource.Split(':');

			if (names.Length == 2)
			{
				ns = names[0];
				name = names[1];
			}

			else
			{
				ns = EmptyNamespace;
				name = EmptyName;
			}
		}

		[JsonProperty("resource")]
		public string FullName => Namespace + ':' + Name;

		public override string ToString()
		{
			return FullName;
		}

		public ResourceName ToGeneric()
		{
			return new ResourceName(EmptyNamespace, Name);
		}

		public override bool Equals(object obj)
		{
			if(obj is ResourceName value)
			{
				return FullName.Equals(value.FullName);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public static implicit operator ResourceName(string str) => new ResourceName(str);

		public static implicit operator string(ResourceName resource) => resource.FullName;*/
	}
}