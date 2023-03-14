//using Newtonsoft.Json;
using System.Collections;
using System.Text;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using static PathfinderToolkit.Models.Resources.Bestiary;
//using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PathfinderToolkit.Models;

public class Resources
{
    public class Dice
    {
        private static Random randomGenerator = new Random();

        /// <summary>
        /// Pass a die roll string in any standard d20-type format, including 
        /// parenthetical rolls, and it will output a string breaking down each 
        /// roll as well as summing the total. This works very nicely. The code was
        /// hacked from java code from Malar's RPG Dice Version 0.9 By Simon Cederqvist 
        /// (simon.cederqvist(INSERT AT HERE) gmail.com). 13.March.2007
        /// http://users.tkk.fi/~scederqv/Dice/
        /// His same license still applies to this code. But if you figure out how to
        /// make a million dollars off this code, then you're smarter than me and 
        /// you deserve to keep it.  On the other hand, share and share alike. 
        /// </summary>
        /// <param name="diceString">This is a standard d20 die roll string, parenthesis 
        /// are allowed. Example Input: (2d8+9)+(3d6+1)-10</param>
        /// <returns>A string breaking down and summing the roll. Example Output: 
        /// (2d8+9)+(3d6+1)-10 = 7+4+9+5+1+2+1-10 = 20+9-10 = 19</returns>
        public static string Roll(string diceString)
        {
            StringBuilder finalResultBuilder = new StringBuilder();
            string tempString = "";
            int intermediateTotal = 0;
            ArrayList sums = new ArrayList();
            ArrayList items = new ArrayList();
            ArrayList dice = new ArrayList();
            int totals = 0;
            bool collate = false;
            bool positive = true;
            string validChars = "1234567890d";
            char[] diceCharArray = diceString.ToLower().ToCharArray();

            for (int i = 0; i < diceString.Length; i++)
            {
                switch (diceCharArray[i])
                {
                    case '+':
                        {
                            if (tempString.Length < 1)
                            {
                                positive = true;
                                break;
                            }
                            dice = calcSubStringRoll(tempString);
                            for (int j = 0; j < dice.Count; j++)
                            {
                                if (!positive)
                                {
                                    items.Add(-1 * Convert.ToInt32(dice[j].ToString()));
                                    intermediateTotal += -1 * Convert.ToInt32(dice[j].ToString());
                                }
                                else
                                {
                                    items.Add(Convert.ToInt32(dice[j].ToString()));
                                    intermediateTotal += Convert.ToInt32(dice[j].ToString());
                                }
                            }
                            if (!collate)
                            {
                                sums.Add(intermediateTotal);
                                intermediateTotal = 0;
                            }
                            positive = true;
                            tempString = "";
                            break;
                        }
                    case '-':
                        {
                            if (tempString.Length < 1)
                            {
                                positive = false;
                                break;
                            }
                            dice = calcSubStringRoll(tempString);
                            for (int j = 0; j < dice.Count; j++)
                            {
                                if (!positive)
                                {
                                    items.Add(-1 * Convert.ToInt32(dice[j].ToString()));
                                    intermediateTotal += -1 * Convert.ToInt32(dice[j].ToString());
                                }
                                else
                                {
                                    items.Add(Convert.ToInt32(dice[j].ToString()));
                                    intermediateTotal += Convert.ToInt32(dice[j].ToString());
                                }
                            }
                            if (!collate)
                            {
                                sums.Add(intermediateTotal);
                                intermediateTotal = 0;
                            }
                            positive = false;
                            tempString = "";
                            break;
                        }
                    case '(': collate = true; break;
                    case ')': collate = false; break;
                    default:
                        {
                            if (validChars.Contains("" + diceCharArray[i]))
                                tempString += diceCharArray[i];
                            break;
                        }
                }
            }

            // And once more for the remaining text
            if (tempString.Length > 0)
            {
                dice = calcSubStringRoll(tempString);
                for (int j = 0; j < dice.Count; j++)
                {
                    if (!positive)
                    {
                        items.Add(-1 * Convert.ToInt32(dice[j].ToString()));
                        intermediateTotal += -1 * Convert.ToInt32(dice[j].ToString());
                    }
                    else
                    {
                        items.Add(Convert.ToInt32(dice[j].ToString()));
                        intermediateTotal += Convert.ToInt32(dice[j].ToString());
                    }
                }
                sums.Add(intermediateTotal);
                intermediateTotal = 0;
            }

            //// Print it all.
            finalResultBuilder.Append(diceString + " = ");
            if (items.Count == 1)
            {
                finalResultBuilder.Append(items.GetRange(0, 1).ToString() + "\n");
                return finalResultBuilder.ToString();
            }
            for (int i = 0; i < items.Count; i++)
            {
                if (Convert.ToInt32(items[i].ToString()) > 0 && i > 0)
                    finalResultBuilder.Append("+" + items[i].ToString());
                else
                    finalResultBuilder.Append(items[i].ToString());
            }
            if (sums.Count > 1 && items.Count > sums.Count)
            { // Don't print just one, or items again.
                finalResultBuilder.Append(" = ");
                for (int i = 0; i < sums.Count; i++)
                {
                    if (Convert.ToInt32(sums[i].ToString()) > 0 && i > 0)
                        finalResultBuilder.Append("+" + sums[i].ToString());
                    else
                        finalResultBuilder.Append(sums[i].ToString());
                }
            }
            for (int i = 0; i < sums.Count; i++)
                totals += Convert.ToInt32(sums[i].ToString());
            finalResultBuilder.Append(" = " + totals + "\n");

            return finalResultBuilder.ToString();
        }


        /// <summary>
        /// Rolls the specified number of die each with the specified number of
        /// sides and returns the numeric result as a string. I had to introduce a 
        /// call to Thread.Sleep() so that the random num gen would seed differently on 
        /// each iteration. 
        /// </summary>
        /// <param name="numberOfDice">The number of die to roll.</param>
        /// <param name="numberOfSides">The number of faces on each dice rolled.</param>
        /// <param name="rollMod"></param>
        /// <returns>A string containing the result of the roll.</returns>
        public static string Roll(int numberOfDice, int numberOfSides, int rollMod)
        {
            // don't allow a Number of Dice less than or equal to zero
            if (numberOfDice <= 0)
                throw new ApplicationException("Number of die must be greater than zero.");

            // don't allow a Number of Sides less than or equal to zero
            if (numberOfSides <= 0)
                throw new ApplicationException("Number of sides must be greater than zero.");

            //// Create the string builder class used to build the string
            //// we return with the result of the die rolls.
            //// See: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemtextstringbuilderclasstopic.asp
            //StringBuilder result = new StringBuilder();

            // Declare the integer in which we will keep the total of the rolls
            int total = 0;

            // repeat once for each number of dice
            for (int i = 0; i < numberOfDice; i++)
            {
                // Create the random class used to generate random numbers.
                // See: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfSystemRandomClassTopic.asp

                // Get a pseudo-random result for this roll
                int roll = randomGenerator.Next(1, numberOfSides);

                // Add the result of this roll to the total
                total += roll;

                //// Add the result of this roll to the string builder
                //result.AppendFormat("Dice {0:00}:\t{1}\n", i + 1, roll);
            }

            return (total + rollMod).ToString();

            //// Add a line to the result to seperate the rolls from the total
            //result.Append("\t\t--\n");

            //// Add the total to the result
            //result.AppendFormat("TOTAL:\t\t{0}\n", total);

            //// Now that we've finished building the result, get the string
            //// that we've been building and return it.
            //return result.ToString();            

        }

        /// <summary>
        /// This function merely breaks down the *basic* die roll string
        /// into the requsite integers. It is used by the above Roll(string) 
        /// method. 
        /// </summary>
        /// <param name="s">A simple die roll string, such as 3d6. Nothing more.</param>
        /// <returns>Returns an ArrayList of int's containing the various die
        /// rolls as passed in as a parameter.</returns>
        public static ArrayList calcSubStringRoll(string s)
        {
            int x, d;
            ArrayList dice = new ArrayList();
            if (s.Contains("d"))
            {
                x = Convert.ToInt32(s.Split('d')[0]);
                d = Convert.ToInt32(s.Split('d')[1]);

                // I loop here so that each roll is added to the ArrayList, and 
                // therefore works properly with the code I hacked from java above. 
                for (int i = 0; i < x; i++)
                    dice.Add(Roll(1, d, 0));
            }
            else
                dice.Add(Convert.ToInt32(s));

            return dice;
        }
    }


    public class Ability
    {
        public string name { get; set; }
        public OtherSources otherSources { get; set; }
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

    public class Activity
    {
        public int number { get; set; }
        public string unit { get; set; }
    }

    public class Frequency
    {
        public int freq { get; set; }
        public string unit { get; set; }
    }

    public class OtherSources
    {
        public List<string> Reprinted { get; set; }
    }

    public class Root
    {
        public List<Ability> ability { get; set; }
    }

    public class Test
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("page")]
        public int? Page { get; set; }
        [JsonPropertyName("level")]
        public int? Level { get; set; }
    }

    public class Creature
    {
        public class BestiaryViewModel
        {
            public Creature AgeOfAshesCreature { get; set; }
            public Creature AbominationVaultsCreature { get; set; }
        }
        public class Abilities
        {
            public List<Bot> bot { get; set; }
        }
        public string name { get; set; }
        public int level { get; set; }
        public List<string> traits { get; set; }
        public Perception perception { get; set; }
        public Languages languages { get; set; }
         public Skills skills { get; set; }
        public Speed speed { get; set; }
        public List<Attack> attacks { get; set; }
        public Abilities abilities { get; set; }
        public Defenses defenses { get; set; }
        public List<Spellcasting> spellcasting { get; set; }
        public List<Sense> senses { get; set; }

        public class Bot
        {            
            public Activity activity { get; set; }
            public List<string> traits { get; set; }
            public List<object> entries { get; set; }
            public string name { get; set; }
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
        }
    }
    public class Bestiary
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

    }

}


