using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using static PathfinderToolkit.Models.Resources;
using static System.Reflection.Metadata.BlobBuilder;

namespace PathfinderToolkit.Models;

public class Resources
{
    public class Ability
    {
        public string name { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<object> entries { get; set; }
        public List<string> creature { get; set; }
        public Activity activity { get; set; }
        public string trigger { get; set; }
        public List<string> traits { get; set; }
        public Frequency frequency { get; set; }
        public string requirements { get; set; }
    }
    public class Root
    {
        public List<Ability> ability { get; set; }
        public List<Actions> action { get; set; }
    }
    public class Actions
    {
        public string name { get; set; }
        public Activity activity { get; set; }
        public string source { get; set; }
        public int page { get; set; }
        public List<string> traits { get; set; }
        public string requirements { get; set; }
        public List<object> entries { get; set; }
        public string trigger { get; set; }
        public Frequency frequency { get; set; }
        public string prerequisites { get; set; }
        public List<string> special { get; set; }
        public ActionType actionType { get; set; }
        public List<object> info { get; set; }
        public string cost { get; set; }
        public string add_hash { get; set; }
    }

    public class ActionType
    {
        public List<string> @class { get; set; }
        public List<string> archetype { get; set; }
        public bool? basic { get; set; }
        public Skill skill { get; set; }
        public List<string> ancestry { get; set; }
        public List<string> heritage { get; set; }
        public List<string> variantrule { get; set; }
        public List<string> versatileHeritage { get; set; }
        public List<string> subclass { get; set; }
        public bool? item { get; set; }
    }

    public class Activity
    {
        public int number { get; set; }
        public string unit { get; set; }
        public string entry { get; set; }
    }

    public class Frequency
    {
        public int freq { get; set; }
        public string unit { get; set; }
        public string special { get; set; }
        public int? interval { get; set; }
    }


    public class Skill
    {
        public List<string> untrained { get; set; }
        public List<string> trained { get; set; }
    }
    public class BestiaryViewModel
    {
        public List<Creature> Creatures { get; set; }
        public List<SelectListItem> BookDropdown { get; set; }
        public string SelectedBook { get; set; }
        public List<SelectListItem> Books { get; set; }
        public Creature SelectedCreature { get; set; }
        public string JsonFilePath { get; set; }
        public string jsonString { get; set; }
        public string[] bookFiles{ get; set; }
        public string SelectedCreatureName { get; set; }
        public List<SelectList> BookDropdownList { get; set; }
    }


    //TODO uncomment properties and verify functionality
    public class Creature
    {
        /*        public class Abilities
                {
                    public List<Bot> bot { get; set; }
                }*/
        //[JsonPropertyName("name")]
        public string name { get; set; }
        public int level { get; set; }
        //public List<string> traits { get; set; }
/*        public Perception perception { get; set; }
        public Languages languages { get; set; }*/
        //public Skills skills { get; set; }
        //public Speed speed { get; set; }
/*        public List<Attack> attacks { get; set; }*/
        //public Abilities abilities { get; set; }
/*        public Defenses defenses { get; set; }
        public List<Spellcasting> spellcasting { get; set; }
        public List<Sense> senses { get; set; }*/
/*        public class Speed
        {
            public int walk { get; set; }
            public int? climb { get; set; }
            public int? fly { get; set; }
            public int? swim { get; set; }
        }*/
        /*public class Bot
        {            
            public Activity activity { get; set; }
            public List<string> traits { get; set; }
            public List<object> entries { get; set; }
            public string name { get; set; }

            public class Speed
        {
            public int walk { get; set; }
            public int? climb { get; set; }
            public int? fly { get; set; }
            public int? swim { get; set; }
        }*/
/*            public string type { get; set; }
            public int? DC { get; set; }
            public string savingThrow { get; set; }
            public string maxDuration { get; set; }
            public List<Stage> stages { get; set; }
            public Generic generic { get; set; }
            public string entries_as_xyz { get; set; }
            public string requirements { get; set; }
            public string trigger { get; set; }
            public Frequency frequency { get; set; }*/
       // }

    }

    //This will be deleted after identifying any properties I may want in the Creature class
    /*public class Bestiary
    {
        public class Abilities
        {
            public List<Mid> mid { get; set; }
            public List<Bot> bot { get; set; }
            public List<Top> top { get; set; }
        }

 
        public class Ac
        {
            public int std { get; set; }

            [JsonProperty("with shield raised")]
            public int? withshieldraised { get; set; }

            [JsonProperty("while withdrawn into its shell")]
            public int? whilewithdrawnintoitsshell { get; set; }

            [JsonProperty("with mage armor")]
            public int? withmagearmor { get; set; }
        }

        public class Acrobatics
        {
            public int std { get; set; }
        }

        public class Activity
        {
            public int number { get; set; }
            public string unit { get; set; }
            public string entry { get; set; }
        }

        public class Arcana
        {
            public int std { get; set; }
        }

        public class Athletics
        {
            public int std { get; set; }

            [JsonProperty("to Climb in true or spider form")]
            public int? toClimbintrueorspiderform { get; set; }

            [JsonProperty("to Climb")]
            public int? toClimb { get; set; }
        }

        public class Attack
        {
            public string range { get; set; }
            public string name { get; set; }
            public int attack { get; set; }
            public List<string> traits { get; set; }
            public List<string> effects { get; set; }
            public string damage { get; set; }
            public List<string> types { get; set; }
        }

        public class Bot
        {
            public Activity activity { get; set; }
            public List<string> traits { get; set; }
            public List<object> entries { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public int? DC { get; set; }
            public string savingThrow { get; set; }
            public string maxDuration { get; set; }
            public List<Stage> stages { get; set; }
            public Generic generic { get; set; }
            public string entries_as_xyz { get; set; }
            public string requirements { get; set; }
            public string trigger { get; set; }
            public Frequency frequency { get; set; }
        }


        public class Creature
        {
            public string name { get; set; }
            public bool isNpc { get; set; }
            public string source { get; set; }
            public int page { get; set; }
            public int level { get; set; }
            public List<string> traits { get; set; }
            public Perception perception { get; set; }
            public Languages languages { get; set; }
            public Skills skills { get; set; }
            //public AbilityMods abilityMods { get; set; }
            public List<string> items { get; set; }
            public Speed speed { get; set; }
            public List<Attack> attacks { get; set; }
            public Abilities abilities { get; set; }
            public Defenses defenses { get; set; }
            public List<Spellcasting> spellcasting { get; set; }
            public List<Sense> senses { get; set; }
            public List<Ritual> rituals { get; set; }

            internal static object ToArray()
            {
                throw new NotImplementedException();
            }

            internal static object ToList()
            {
                throw new NotImplementedException();
            }
        }

        public class Deception
        {
            public int std { get; set; }
        }

        public class Defenses
        {
            public Ac ac { get; set; }
            public SavingThrows savingThrows { get; set; }
            public List<Hp> hp { get; set; }
            public List<string> immunities { get; set; }
            public List<Resistance> resistances { get; set; }
            public List<Weakness> weaknesses { get; set; }
        }

        public class Diplomacy
        {
            public int std { get; set; }
        }

        public class Fort
        {
            public int std { get; set; }
        }

        public class Frequency
        {
            public string unit { get; set; }
            public int freq { get; set; }
        }

        public class Generic
        {
            public string tag { get; set; }
            public string add_hash { get; set; }
        }

        public class Hp
        {
            public int hp { get; set; }
            public List<string> abilities { get; set; }
        }

        public class Intimidation
        {
            public int std { get; set; }
        }

        public class Languages
        {
            public List<string> languages { get; set; }
            public List<string> abilities { get; set; }
        }

        public class Mid
        {
            public Activity activity { get; set; }
            public List<string> entries { get; set; }
            public string name { get; set; }
            public Generic generic { get; set; }
            public List<string> traits { get; set; }
            public string trigger { get; set; }
        }


        public class Nature
        {
            public int std { get; set; }
        }

        public class Occultism
        {
            public int std { get; set; }
        }

        public class Perception
        {
            public int std { get; set; }
        }

        public class Performance
        {
            public int std { get; set; }
        }

        public class Ref
        {
            public int std { get; set; }
        }

        public class Religion
        {
            public int std { get; set; }
        }

        public class Resistance
        {
            public string name { get; set; }
            public int? amount { get; set; }
        }

        public class Ritual
        {
            public string tradition { get; set; }
            public int DC { get; set; }
            public List<Ritual> rituals { get; set; }
            public string name { get; set; }
        }

        public class Root
        {
            public List<Creature> creature { get; set; }
        }

        public class SavingThrows
        {
            public Fort fort { get; set; }
            public Ref @ref { get; set; }
            public Will will { get; set; }
        }

        public class Sense
        {
            public string name { get; set; }
            public string type { get; set; }
            public int? range { get; set; }
        }

        public class Skills
        {
            public Athletics athletics { get; set; }
            public Intimidation intimidation { get; set; }
            public Religion religion { get; set; }
            public Society society { get; set; }
            public Deception deception { get; set; }
            public Diplomacy diplomacy { get; set; }
            public Nature nature { get; set; }
            public Survival survival { get; set; }
            public Acrobatics acrobatics { get; set; }
            public Stealth stealth { get; set; }
            public Thievery thievery { get; set; }

            public Occultism occultism { get; set; }
            public Arcana arcana { get; set; }

        }

        public class Society
        {
            public int std { get; set; }
        }

        public class Speed
        {
            public int walk { get; set; }
            public int? climb { get; set; }
            public int? fly { get; set; }
            public int? swim { get; set; }
        }
        public class Spellcasting
        {
            public string name { get; set; }
            public string type { get; set; }
            public string tradition { get; set; }
            public int DC { get; set; }
            public int attack { get; set; }
            public object entry { get; set; }
            public int? fp { get; set; }
        }

        public class Stage
        {
            public int stage { get; set; }
            public string entry { get; set; }
            public string duration { get; set; }
        }

        public class Stealth
        {
            public int std { get; set; }

            [JsonProperty("in forests")]
            public int? inforests { get; set; }
        }

        public class Survival
        {
            public int std { get; set; }
        }

        public class Thievery
        {
            public int std { get; set; }
        }

        public class Top
        {
            public List<string> entries { get; set; }
            public string name { get; set; }
            public List<string> traits { get; set; }
            public Activity activity { get; set; }
            public string trigger { get; set; }
        }

        public class Weakness
        {
            public int amount { get; set; }
            public string name { get; set; }
        }
        public class Will
        {
            public int std { get; set; }
        }

    }*/
    
}


