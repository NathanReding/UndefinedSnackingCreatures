using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class Encyclopedia
    {
        // name
        // last modified time
        // author

        public Encyclopedia()
        {
            this.creatures = new List<CreatureTemplate>();
            this.abilities = new List<AbilityTemplate>();
            this.cards = new List<CardTemplate>();
            this.zones = new List<Zone>();
            this.types = new List<CreatureType>();
            this.keyWords = new List<KeyWord>();
        }

        public Encyclopedia(
            List<CreatureTemplate> creatures,
            List<AbilityTemplate> abilities,
            List<CardTemplate> cards,
            List<Zone> zones,
            List<CreatureType> types,
            List<KeyWord> keyWords
        )
        {
            this.creatures = creatures;
            this.abilities = abilities;
            this.cards = cards;
            this.zones = zones;
            this.types = types;
            this.keyWords = keyWords;
        }

        public List<CreatureTemplate> creatures {get; set;}
        public List<AbilityTemplate> abilities {get; set;}
        public List<CardTemplate> cards {get; set;}
        public List<Zone> zones {get; set;}
        public List<CreatureType> types {get; set;}
        public List<KeyWord> keyWords {get; set;}

    }
}
