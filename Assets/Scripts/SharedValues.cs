using System;
using System.Collections.Generic;
using DTOs;

namespace SharedScripts
{

    static class SharedValues
    {
        public static List<string> validCodexViews = new List<string> 
            { "Creatures", "Abilities", "Cards", "Zones", "Keywords"};
        public static List<Type> validTemplateClasses = new List<Type> 
            { typeof(CardTemplate), typeof(AbilityTemplate), typeof(CreatureTemplate), typeof(Region), typeof(Zone), typeof(CreatureType), typeof(KeyWord)};
    }
}
