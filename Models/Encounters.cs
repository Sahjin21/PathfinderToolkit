using Microsoft.AspNetCore.Mvc.Rendering;

namespace PathfinderToolkit.Models
{
    public class Encounters
    {
        public int EnemiesCount { get; set; }
        public List<SelectListItem> NPCs { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
    }
}
