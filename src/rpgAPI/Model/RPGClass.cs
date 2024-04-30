using System.Text.Json.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace rpgAPI.Model
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RPGClass
    {
       Knight = 1,
       Mage = 2,
       Cleric = 3,
       Elf = 4 
    }
}
