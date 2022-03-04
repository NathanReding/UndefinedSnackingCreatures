//TermLibrary
using System.Collections.Generic;
using DTOs;
using AbilityTerms;
using UnityEngine;

public class TermLibrary
{

    private static TermLibrary singleton;

    public static TermLibrary getSingleton()
    {
        if (singleton == null)
        {
            singleton = new TermLibrary();
        }
        return singleton;
    }


    Dictionary<string, AbilityTerm> allTerms;
    List<string> uniqueTerms = new List<string>(){
        "number", "condition", "in", "where", "given",
        "name", "string", "passiveCondition", "null"
    }; // override



    private TermLibrary()
    {
        // Assemble library
        addAllFrom(new GeneralTerms().terms);
        addAllFrom(new CoreTerms().terms);
        addAllFrom(new ConditionalTerm().headTerms);

    }

    private void addAllFrom(Dictionary<string, AbilityTerm> terms)
    {
        foreach (string name in terms.Keys)
        {
            allTerms.Add(name, terms[name]);
        }
    }

    public bool isValidSubterm(string term, string subterm)
    {
        //TODO look through library
        return true;
    }

    public string identifyPerscriptiveTerm(string term, string subterm)
    {
        if(subterm == "(") return "condition";
        // string result = "";
        List<string> perscriptiveOptions = new List<string>();
        foreach(string tempSubterm in allTerms[term].subterms){
            // if(!allTerms[tempSubterm].isDeclaritive) perscriptiveOptions.Add(tempSubterm); // TODO return here and allow for multiple perscriptives to follow
            if(!allTerms[tempSubterm].isDeclaritive) return tempSubterm; // assume it is always the first
        }
        Debug.LogError("no perscriptive terms found for " + term);
        return "effect";
    }

    public AbilityTerm getTerm(string term)
    {
        return allTerms[term];
    }

}
