using System.Collections.Generic;
using DTOs;
using UnityEngine;


namespace SharedScripts
{
    public class SharedCode
    {

        public static bool verifyTyping(System.Object obj, System.Type type, string errorMessage)
        {
            if (obj.GetType() != type)
            {
                Debug.LogError("Invalid type. Type: " + obj.GetType() + " " + errorMessage);
                return false;
            }
            return true;
        }

        public static Dictionary<string, AbilityTerm> toTermDictionary(List<AbilityTerm> termList)
        {
            Dictionary<string, AbilityTerm> result = new Dictionary<string, AbilityTerm>();
            foreach (AbilityTerm termObject in termList)
            {
                result.Add(termObject.name, termObject);
            }
            return result;
        }

        public static string getEffectDataType(string name)
        {
            switch (name)
            {
                case "gainShields":
                    return "int";
                case "applyKeyword":
                    return "string";
                case "dealDamage":
                    return "int";
            }
            /*
            gainShields
            gainAttack
            summon
            heal
            applyKeyword
            addValidAttackOppertunity
            dealDamage
            */
            return "";
        }


    }
}