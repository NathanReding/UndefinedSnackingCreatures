using System.Collections.Generic;

namespace DTOs
{
    public class EffectTermSegment
    {
        public string name;
        public string nonSingularString;
        public AbilityTerm term;
        public Dictionary<string, EffectTermSegment> values;

        public EffectTermSegment(){
            values = new Dictionary<string, EffectTermSegment>();
        }
    }
}