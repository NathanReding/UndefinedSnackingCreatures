using System.Collections.Generic;
using DTOs;

public class AbilityLibrary
{

    private static AbilityLibrary singleton;

    public static AbilityLibrary getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilityLibrary();
        }
        return singleton;
    }


    Dictionary<string, EffectTermSegment> allEffects;

    private AbilityLibrary(){
        allEffects = new Dictionary<string, EffectTermSegment>();
        // get all effects strings
        List<string> effects = new List<string>() {
            "",
            ""
        };
        foreach(string effect in effects){

        }
    }



}
