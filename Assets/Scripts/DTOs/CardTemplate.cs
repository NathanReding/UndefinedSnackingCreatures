using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class CardTemplate : DTODisplayable
    {
        public CardTemplate(
            string name,
            string description,
            List<CreatureType> types,
            List<Zone> zones
        ) : base(name, description)
        {
            this.types = types;
            this.zones = zones;
        }
        public List<CreatureType> types;
        public List<Zone> zones;
    }
}