using DTOs;
using SharedScripts;

public class TargetSelection
{

    // TargetSpecObject
    // 
    private static TargetSelection singleton;

    public static TargetSelection getSingleton()
    {
        if (singleton == null)
        {
            singleton = new TargetSelection();
        }
        return singleton;
    }

    private TargetSelection()
    {

    }

    public void specifyCardTargets(System.Object obj)
    {
        if (SharedCode.verifyTyping(obj, typeof(CardObject), "specifyTargets")) return;
        CardObject card = obj as CardObject;

        // target of the card
        // other targets it has
    }
}