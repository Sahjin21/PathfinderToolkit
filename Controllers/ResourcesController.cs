using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PathfinderToolkit.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using static PathfinderToolkit.Models.Resources;
using static PathfinderToolkit.Models.JsonReader;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static PathfinderToolkit.Models.Resources.Creature;
using static PathfinderToolkit.Models.Resources.BestiaryViewModel;
using Newtonsoft.Json.Linq;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using System;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.Text;

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
            //Readpath abilityDropdown and deserialize selected json file
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

        private readonly string bestiaryDirectoryPath = "wwwroot/Data/Json PF/bestiary/";

        [HttpGet]
        public IActionResult Bestiary()
        {
            var model = new BestiaryViewModel();
            // Populate book dropdown
            model.bookFiles = Directory.GetFiles(bestiaryDirectoryPath, "*.json");
            model.BookDropdown = new List<SelectListItem>();

            foreach (var bookFile in model.bookFiles)
            {
                var bookName = Path.GetFileNameWithoutExtension(bookFile);
                model.BookDropdown.Add(new SelectListItem { Value = bookName, Text = bookName });
            }
            // Store the dropdown list in the model for future use
            model.BookDropdown = model.BookDropdown.Select(x => new SelectListItem { Value = x.Value, Text = x.Text }).ToList();

            return View("~/Views/Home/Resources/Bestiary.cshtml", model);
        }

        [HttpPost]
        public IActionResult Bestiary(BestiaryViewModel model)
        {
            // Set the selected book in the model
            model.SelectedBook = Request.Form["SelectedBook"];
            if (!string.IsNullOrEmpty(model.SelectedBook))
            {
                try
                {
                    //Readpath bookDropdown and deserialize selected json file
                    string jsonFilePath = Path.Combine(bestiaryDirectoryPath, $"{model.SelectedBook}.json");
                    model.jsonString = System.IO.File.ReadAllText(jsonFilePath);
                    jsonFilePath = jsonFilePath.Replace("\\", "/");
                    var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Creature>>>(model.jsonString);
                    model.Creatures = json["creature"];
                    model.JsonFilePath = jsonFilePath;
                    var creatureList = json["creature"].ToArray();
                    ViewBag.Creatures = creatureList.Select(a => new SelectListItem { Text = a.name, Value = a.name }).ToList();
                    ViewBag.JsonFilePath = jsonFilePath;
                    model.JsonFilePath = jsonFilePath;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("~/Views/Shared/Error.cshtml", model);
                }
            }

            // Update book dropdown if necessary
            if (model.BookDropdown == null)
            {
                var bookFiles = Directory.GetFiles(bestiaryDirectoryPath, "*.json");
                model.BookDropdown = new List<SelectListItem>();

                foreach (var bookFile in bookFiles)
                {
                    var bookName = Path.GetFileNameWithoutExtension(bookFile);
                    model.BookDropdown.Add(new SelectListItem { Value = bookName, Text = bookName });
                }
            }
            return View("~/Views/Home/Resources/Bestiary.cshtml", model);
        }

        [HttpPost]
        public IActionResult SelectCreature(BestiaryViewModel model, string SelectedCreature)
        {
            try
                {
                //Readpath creatureDropdown and deserialize selected json file
                string jsonString = System.IO.File.ReadAllText(model.JsonFilePath, Encoding.UTF8);
                jsonString = jsonString.Replace("\r", ""); // Remove carriage return characters
                var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Creature>>>(jsonString);

                //Verifies json data is valid.
                if (json.ContainsKey("creature"))
                {
                    System.Diagnostics.Debug.WriteLine("creature is not null");
                }
                if (json == null)
                {
                    System.Diagnostics.Debug.WriteLine("json is null");
                }
                else if (!json.ContainsKey("creature"))
                {
                    System.Diagnostics.Debug.WriteLine("creature key not found in json");
                }
                else
                {
                    var creatureList = json["creature"].ToArray();
                    ViewBag.Creature = creatureList.Select(a => new SelectListItem { Text = a.name, Value = a.name }).ToList();
                   // var selectedCreature = creatureList.FirstOrDefault(a => a.name == model.SelectedCreature?.name) ?? creatureList.FirstOrDefault();
                
                }
                model.SelectedCreatureName = Request.Form["SelectedCreatureName"];
                model.Creatures = json["creature"];
                var selectedCreature = json["creature"].FirstOrDefault(a => a.name == model.SelectedCreatureName);

                if (selectedCreature == null)
                {
                    System.Diagnostics.Debug.WriteLine("SelectedCreature not found");
                }
                else
                {
                    model.SelectedCreature = selectedCreature;
                }              
            }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return View("~/Views/Shared/Error.cshtml", model);
                }
            // Update book dropdown if necessary
            if (model.BookDropdown == null)
            {
                var bookFiles = Directory.GetFiles(bestiaryDirectoryPath, "*.json");
                model.BookDropdown = new List<SelectListItem>();

                foreach (var bookFile in bookFiles)
                {
                    var bookName = Path.GetFileNameWithoutExtension(bookFile);
                    model.BookDropdown.Add(new SelectListItem { Value = bookName, Text = bookName });
                }
            }
            /*System.Diagnostics.Debug.WriteLine("JsonFilePath: " + model.JsonFilePath);
            System.Diagnostics.Debug.WriteLine("SelectedCreature name: " + model.SelectedCreatureName);
            System.Diagnostics.Debug.WriteLine("Selected Book: " + model.SelectedBook);*/
            return View("~/Views/Home/Resources/Bestiary.cshtml", model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
