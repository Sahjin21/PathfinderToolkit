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
using Microsoft.AspNetCore.Mvc.ViewEngines;
using static PathfinderToolkit.Models.Resources.Creature;

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

        public IActionResult Bestiary(string ageOfAshesDropdown, string abominationVaultsDropdown)
        {
            try
            {
                string ageOfAshesFilePath = "wwwroot/Data/Json PF/bestiary/AgeOfAshes.json";
                string ageOfAshesString = System.IO.File.ReadAllText(ageOfAshesFilePath);
                var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Creature>>>(ageOfAshesString);
                var ageOfAshesList = json["creature"].ToArray();
                ViewBag.ageOfAshesCreature = ageOfAshesList.Select(a => new SelectListItem { Text = a.name + "(" + a.level + ")", Value = a.name}).ToList();
                var ageOfAshesCreature = ageOfAshesList.FirstOrDefault(a => a.name == ageOfAshesDropdown);

                string abominationVaultsFilePath = "wwwroot/Data/Json PF/bestiary/AbominationVaults.json";
                string abominationVaultsString = System.IO.File.ReadAllText(ageOfAshesFilePath);
                var abominationVaultsJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Creature>>>(abominationVaultsString);
                var abominationVaultsList = json["creature"].ToArray();
                ViewBag.ageOfAshesCreature = abominationVaultsList.Select(a => new SelectListItem { Text = a.name + "(" + a.level + ")", Value = a.name }).ToList();
                var abominationVaultsCreature = abominationVaultsList.FirstOrDefault(a => a.name == abominationVaultsDropdown);

                // create an instance of BestiaryViewModel
                var viewModel = new BestiaryViewModel();

                // populate its properties with the necessary data
                viewModel.AgeOfAshesCreature = ageOfAshesCreature;
                viewModel.AbominationVaultsCreature = abominationVaultsCreature;
                return View("~/Views/Home/Resources/Bestiary.cshtml", viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
<<<<<<< HEAD
                return View("~/Views/Shared/Error.cshtml"); 
=======
                return View("~/Views/Home/Resources/Bestiary.cshtml");
>>>>>>> 144ae53cb85ecc4cff02989a550f7109258c4a20
            }
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
