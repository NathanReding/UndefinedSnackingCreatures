using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class AbilityTemplate : DTODisplayable
    {
        public AbilityTemplate(
            string name,
            string description,
            List<CreatureType> types,
            string code
        ) : base(name, description)
        {
            this.types = types;
            this.code = code;
        }
        public List<CreatureType> types;
        public string code;
    }
}