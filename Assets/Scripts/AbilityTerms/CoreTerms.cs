using System.Collections.Generic;
using DTOs;
using SharedScripts;

namespace AbilityTerms
{

    public class CoreTerms
    {
        public Dictionary<string, AbilityTerm> terms;
        public CoreTerms()
        {
            List<AbilityTerm> termList = new List<AbilityTerm>();
            termList.Add(new AbilityTerm());
            // TODO add in the terms




            terms = SharedCode.toTermDictionary(termList);
        }


    }
}