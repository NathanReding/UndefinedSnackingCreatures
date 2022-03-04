using System.Collections.Generic;

public class AbilityLanguageValues{

    private static AbilityLanguageValues singleton;

    public Dictionary<string, AbilityLanguageValues> dictionary;

    static AbilityLanguageValues getAbilityLanguageValues(){
        if (singleton == null){
            singleton = new AbilityLanguageValues();
        }
        return singleton;
    }

    private AbilityLanguageValues(){
        dictionary = new Dictionary<string, AbilityLanguageValues>();
        // dictionary.Add(
        
        // );
    }
}