using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class CreatureTemplate : DTODisplayable
    {
        // name
        // last modified time
        // author

        public CreatureTemplate() : base("", "")
        {
            this.abilities = new List<AbilityTemplate>();
            this.zones = new List<Zone>();
            this.types = new List<CreatureType>();
            this.keyWords = new List<KeyWord>();
        }

        public CreatureTemplate(
            string name,
            string description
        ) : base(name, description)
        {
            this.abilities = new List<AbilityTemplate>();
            this.zones = new List<Zone>();
            this.types = new List<CreatureType>();
            this.keyWords = new List<KeyWord>();
        }

        public CreatureTemplate(
            string name,
            string description,
            List<AbilityTemplate> abilities,
            List<Zone> zones,
            List<CreatureType> types,
            List<KeyWord> keyWords
        ) : base(name, description)
        {
            this.description = description;
            this.abilities = abilities;
            this.zones = zones;
            this.types = types;
            this.keyWords = keyWords;
        }

        public List<AbilityTemplate> abilities { get; set; }
        public List<Zone> zones { get; set; }
        public List<CreatureType> types { get; set; }
        public List<KeyWord> keyWords { get; set; }

    }
}
