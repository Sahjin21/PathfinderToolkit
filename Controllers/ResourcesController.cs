using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PathfinderToolkit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static PathfinderToolkit.Models.Resources;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static PathfinderToolkit.Models.Resources.Bestiary;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PathfinderToolkit.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult Ability()
        {
            string jsonFilePath = "wwwroot/Data/Json PF/abilities.json";
            string jsonString = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Ability>>>(jsonString);
            var abilityList = json["ability"].ToArray();

            ViewBag.Abilities = abilityList.Select(a => new SelectListItem { Text = a.name, Value = a.name }).ToList();

            Resources.Ability selectedAbility = null;
            if (Request.Method == "POST")
            {
                var selectedAbilityName = Request.Form["abilityDropdown"];
                selectedAbility = abilityList.FirstOrDefault(a => a.name == selectedAbilityName);
            }

            return View("~/Views/Home/Resources/Ability.cshtml", selectedAbility);
        }

        [HttpPost]
        public IActionResult Ability(string abilityDropdown)
        {
            string jsonFilePath = "wwwroot/Data/Json PF/abilities.json";
            string jsonString = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Ability>>>(jsonString);
            var abilityList = json["ability"].ToArray();

            ViewBag.Abilities = abilityList.Select(a => new SelectListItem { Text = a.name, Value = a.name }).ToList();

            var selectedAbility = abilityList.FirstOrDefault(a => a.name == abilityDropdown);

            return View("~/Views/Home/Resources/Ability.cshtml", selectedAbility);
        }

        public IActionResult DiceRoller()
        {
            return View("~/Views/Home/Resources/DiceRoller.cshtml");
        }

        public IActionResult Bestiary(string creatureDropdown)
        {
            try
            {
                // Load the JSON data from the file
                string jsonText = System.IO.File.ReadAllText("Resources/bestiary.json");

                // Deserialize the JSON into a Bestiary object
                var bestiary = JsonConvert.DeserializeObject<Bestiary>(jsonText);

                // Get the list of creatures from the Bestiary object
                var creatureList = Resources.Bestiary.Creature.ToList();

                // If a creature was selected, get its details and add them to the view bag
                if (!string.IsNullOrEmpty(creatureDropdown))
                {
                    var selectedCreature = creatureList.FirstOrDefault(c => c.Name == creatureDropdown);
                    if (selectedCreature != null)
                    {
                        ViewBag.SelectedCreature = selectedCreature;
                    }
                }

                // Add the creature list to the view bag
                ViewBag.CreatureList = new SelectList(creatureList, "Name", "Name", creatureDropdown);

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("~/Views/Home/Resources/Bestiary.cshtml");
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
