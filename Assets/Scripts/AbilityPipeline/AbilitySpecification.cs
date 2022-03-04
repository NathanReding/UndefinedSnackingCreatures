using DTOs;

public class AbilitySpecification // human
{

    private static AbilitySpecification singleton;

    public static AbilitySpecification getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilitySpecification();
        }
        return singleton;
    }


TargetSelesction targeting;
    private AbilitySpecification()
    {
        targeting = TargetSelesction.getSingleton();
    }


    //operate card
    // 
    public operateCard(CardObject card){
        targeting.specifyCardTargets(card);

    }
}