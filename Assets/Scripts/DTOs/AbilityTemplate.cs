using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class AbilityTemplate : DTODisplayable
    {
        public AbilityTemplate(
            string name,
            string description,
            List<CreatureType> types
        ) : base(name, description)
        {
            this.types = types;
        }
        public List<CreatureType> types;
    }
}