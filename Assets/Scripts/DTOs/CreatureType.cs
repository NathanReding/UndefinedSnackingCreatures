using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class CreatureType : DTODisplayable
    {
        public CreatureType(
            string name,
            string description,
            List<string> weaknesses,
            List<string> strengths,
            bool selfResistant
        ) : base(name, description)
        {
            this.weaknesses = weaknesses;
            this.strengths = strengths;
            this.selfResistant = selfResistant;
        }
        public List<string> weaknesses;
        public List<string> strengths;
        public bool selfResistant;
    }
}