using System.Text.Json;

namespace PathfinderToolkit.Models
{
    public class JsonReader
    {
        public static List<Resources.Creature> GetCreaturesFromJson(string fileName)
        {
            var path = Path.Combine("wwwroot/Data/Json PF/bestiary/", fileName);
            var json = System.IO.File.ReadAllText(path);
            var data = JsonSerializer.Deserialize<Dictionary<string, Resources.Creature>>(json);

            return data.Values.ToList();
        }
    }
}
