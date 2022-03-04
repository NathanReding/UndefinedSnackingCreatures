using System.Collections.Generic;
using DTOs;
using SharedScripts;

namespace AbilityTerms
{

    public class ConditionalTerm
    {
        public Dictionary<string, AbilityTerm> terms; // TODO remove is not needed in conditional logic
        public Dictionary<string, AbilityTerm> headTerms;

        public ConditionalTerm()
        {
            headTerms.Add("condition", new AbilityTerm("condition", false, false, "null"));

            List<AbilityTerm> termList = new List<AbilityTerm>();
            termList.Add(new AbilityTerm());
            // TODO add in the terms




            terms = SharedCode.toTermDictionary(termList);
        }
    }
}