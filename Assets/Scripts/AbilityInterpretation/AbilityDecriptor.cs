using System.Collections.Generic;
using UnityEngine;
using SharedScripts;
using DTOs;
public class AbilityDecriptor
{

    private static AbilityDecriptor singleton;

    public static AbilityDecriptor getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilityDecriptor();
        }
        return singleton;
    }

    TermLibrary termLibrary;

    private AbilityDecriptor()
    {
        termLibrary = TermLibrary.getSingleton();
    }


    Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();


    //TODO use this method
    public EffectTermSegment decriptEffect(string effectString)
    {
        string modifiedEffectString =
            effectString.Replace("{", " { ")
        .Replace("}", " } ")
        .Replace("(", " ( ")
        .Replace(")", " ) ")
        .Replace(",", " , ");

        string[] splitString = modifiedEffectString.Split(' ');

        if (splitString.Length < 1) return null;
        if (!SharedValues.validCodexViews.Contains(splitString[0])) return null;

        Queue<string> queue = new Queue<string>(splitString);

        EffectTermSegment result = new EffectTermSegment();
        result.name = queue.Dequeue();
        result.term = termLibrary.getTerm(result.name);

        if (queue.Peek() != "{") result = decriptInnerEffects(result.name, queue);
        else
        {
            queue.Dequeue();
            do
            {
                string nextName = queue.Dequeue();
                if (termLibrary.isValidSubterm(result.name, nextName)) return null;
                result.values.Add(nextName, decriptInnerEffects(nextName, queue));
                if (queue.Peek() == ",") queue.Dequeue();
            }
            while (queue.Peek() != "}");
            queue.Dequeue();
        }
        return result;
    }

// Used for all parts of the effect that are inside from the head effect term
    private EffectTermSegment decriptInnerEffects(string symbolName, Queue<string> queue)
    {
        EffectTermSegment result = new EffectTermSegment();
        result.name = symbolName;
        result.term = termLibrary.getTerm(result.name);

        if (queue.Peek() == "," || queue.Peek() == "}") return result;
        // if it is a non single, add the rest of the queue until , or } into the string and return
        if (termLibrary.getTerm(symbolName).nonSingular) // TODO fix to be nonsingular and make code for declaritive
        {
            result.nonSingularString = getNonSingularString(queue);
            return result;
        }
        //if not a valid subterm, determin which non-declaritive perscriptive term it is

        // identify next symbol

        if (queue.Peek() != "{") result = identifySubsymbol(symbolName, queue.Peek(), queue);
        else
        {
            queue.Dequeue();
            do
            {
                string nextName = queue.Peek();
                result.values.Add(nextName, identifySubsymbol(symbolName, nextName, queue));
                if (queue.Peek() == ",") queue.Dequeue();
            }
            while (queue.Peek() != "}");
            queue.Dequeue();
        }
        return result;
    }

    private EffectTermSegment identifySubsymbol(string term, string subterm, Queue<string> queue){
        //if
        if(termLibrary.isValidSubterm(term, subterm)) {
            queue.Dequeue();
            return decriptInnerEffects(subterm, queue);
        }
        string actualSymbol = termLibrary.identifyPerscriptiveTerm(term, subterm);
        return decriptInnerEffects(actualSymbol, queue);
    }

    private string getNonSingularString(Queue<string> queue)
    {
        string result = "";
        int depth = 0;
        while (depth > 0 || (queue.Peek() != "," && queue.Peek() != "}"))
        {
            if (queue.Peek() != "{")
            {
                depth++;
            }
            else if (queue.Peek() != "}")
            {
                depth--;
            }
            result += " " + queue.Dequeue();
        }

        return result;
    }


    bool testIsValidAbility(string abilityString)
    {
        string modifiedAbilityString =
            abilityString.Replace("{", " { ")
                .Replace("}", " } ")
                .Replace(",", " , ");

        string[] splitString = modifiedAbilityString.Split(' ');

        if (splitString.Length < 1) return false;
        if (!SharedValues.validCodexViews.Contains(splitString[0])) return false;

        return validateSymbol(splitString[0], new Queue<string>(splitString));
    }

    bool validateSymbol(string symbolName, Queue<string> queue)
    {
        while (dictionary[symbolName].Contains(queue.Peek()))
        {
            string temp = queue.Dequeue();

        }
        if (queue.Peek() == "multiple")
        {

        }
        while (dictionary[symbolName].Contains(queue.Peek()))
        {

        }
        return true;
    }


}