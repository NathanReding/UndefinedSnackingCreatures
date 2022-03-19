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
        Dictionary<string, EffectObject> effects = new Dictionary<string, EffectObject>();
        foreach (EffectObject tempEffect in ability.effects)
        {
            effects.Add(tempEffect.name, tempEffect);
        }

        foreach ((string, string) temp in reactions)
        {
            if (effects.Contains(temp.Item1))
            {
                effects[temp.Item1].modify(temp.Item2);
            }
            else
            {
                ability.effects.Add(new EffectObject(temp.Item1, temp.Item2));
            }
        }

    }


}
