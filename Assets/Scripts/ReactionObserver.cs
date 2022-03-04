using DTOs;

public class ReactionObserver
{
    private static ReactionObserver singleton;

    public static ReactionObserver getSingleton()
    {
        if (singleton == null)
        {
            singleton = new ReactionObserver();
        }
        return singleton;
    }



    private ReactionObserver()
    {

    }

    public List<> getModReactions(AbilityObject ability){
        
    }
}