﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PathfinderToolkit.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static PathfinderToolkit.Models.Resources;
using Microsoft.AspNetCore.Http;

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
        public IActionResult Index()
        {
            /*string jsonFilePath = "wwwroot/Data/Json PF/test.json";
            string jsonString = System.IO.File.ReadAllText(jsonFilePath);

            var json = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, List<Resources.Test>>>(jsonString);
            var testList = json["test"].ToArray();
            return View(testList);    */ 
            return View();
        }
    }
}