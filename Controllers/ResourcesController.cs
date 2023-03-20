using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PathfinderToolkit.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using static PathfinderToolkit.Models.Resources;
using static PathfinderToolkit.Models.JsonReader;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static PathfinderToolkit.Models.Resources.Bestiary;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static PathfinderToolkit.Models.Resources.Creature;
using static PathfinderToolkit.Models.Resources.Creature.BestiaryViewModel;
using Newtonsoft.Json.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System;



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
        [HttpGet]
       public IActionResult Bestiary()
        {
            var model = new BestiaryViewModel();

            // Populate book dropdown
            var bookFiles = Directory.GetFiles("wwwroot/Data/Json PF/bestiary/", "*.json");

            model.BookDropdown = new List<SelectListItem>();
            foreach (var bookFile in bookFiles)
            {
                var bookName = Path.GetFileNameWithoutExtension(bookFile);
                model.BookDropdown.Add(new SelectListItem { Value = bookName, Text = bookName });
            }

            return View("~/Views/Home/Resources/Bestiary.cshtml", model);
        }


        [HttpPost]
        public IActionResult Bestiary(BestiaryViewModel model, string creatureDropdown)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.SelectedBook = Request.Form["SelectedBook"];
                    var creaturesJson = System.IO.File.ReadAllText($"wwwroot/Data/Json PF/bestiary/{model.SelectedBook}.json");
                    model.Creatures = JsonConvert.DeserializeObject<List<Resources.Creature>>(creaturesJson);
                    var creatureList = model.Creatures.ToArray();
                    ViewBag.Creatures = creatureList.Select(a => new SelectListItem { Text = a.name, Value = a.name }).ToList();
                    var selectedCreature = creatureList.FirstOrDefault(a => a.name == creatureDropdown);
                }
                return View("~/Views/Home/Resources/Bestiary.cshtml", model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("~/Views/Shared/Error.cshtml", model);
            }
        }




        public IActionResult Index()
        {
            return View();
        }
    }
}
