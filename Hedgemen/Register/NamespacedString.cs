using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Hgm.Register
{
    public struct NamespacedString
    {
        private string nameSpace;
        private string name;

        [JsonConstructor]
        public NamespacedString(string fullyQualifiedString)
        {
            string[] fullyQualifiedStringSplit = fullyQualifiedString.Split(':');
            
            if(fullyQualifiedStringSplit.Length != 2)
                throw new ArgumentException($"String {fullyQualifiedString} is not a valid namespaced string!");

            nameSpace = fullyQualifiedStringSplit[0];
            name = fullyQualifiedStringSplit[1];
        }

        public NamespacedString(string nameSpace, string name)
        {
            this.nameSpace = nameSpace;
            this.name = name;
        }

        [JsonIgnore]
        public string Namespace => nameSpace;

        [JsonIgnore]
        public string Name => name;

        [JsonPropertyName("name")]
        public string FullName => nameSpace + ':' + name;
    }
}