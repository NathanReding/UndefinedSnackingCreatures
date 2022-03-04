using DTOs;

public class AbilityPipeline
{

    private static AbilityPipeline singleton;

    public static AbilityPipeline getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilityPipeline();
        }
        return singleton;
    }



    private AbilityPipeline(){

    }

    public void resolveAbility(AbilityObject abilityO){
        // Modifications
        // Replacement
        // Absolutes
        // 
        // 
        // 
    }

}
