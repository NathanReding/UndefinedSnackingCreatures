using DTOs;
using SharedScripts;

public class AbilityPipeline
{

    ReactionObserver reactionObserver;

    private static AbilityPipeline singleton;

    public static AbilityPipeline getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilityPipeline();
        }
        return singleton;
    }



    private AbilityPipeline()
    {
        reactionObserver = ReactionObserver.getSingleton();
    }

    public void resolveAbility(System.Object obj)
    {
        if (SharedCode.verifyTyping(obj, typeof(AbilityObject), "")) return;
        AbilityObject ability = obj as AbilityObject;
        // Conditional Effects
        // Modifications
        applyModifications(ability);
        // Replacement
        // Absolutes
        // 
        // 
        // 
    }

    private void applyModifications(AbilityObject ability)
    {
        List<(string, string)> reactions = reactionObserver.getModReactions(ability);
        Dictionary<string, EffectObject>
        foreach (EffectObject tempEffect in ability.effects){

        }


        foreach ((string, string) temp in reactions){
            applyModification(temp, ability);
        }

    }




    private void applyModification((string, string) modification, AbilityObject ability){

    }

}
