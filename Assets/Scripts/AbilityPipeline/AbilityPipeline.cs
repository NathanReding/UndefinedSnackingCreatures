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
        reactionObserver.getModReactions(ability);

    }

}
