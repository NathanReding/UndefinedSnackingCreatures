using System.Collections.Generic;
using DTOs;
using SharedScripts;

public class AbilityQueueService
{

    private static AbilityQueueService singleton;

    public static AbilityQueueService getSingleton()
    {
        if (singleton == null)
        {
            singleton = new AbilityQueueService();
        }
        return singleton;
    }

    private AbilityQueueService()
    {
        queue = new Queue<AbilityObject>();
        pipeline = AbilityPipeline.getSingleton();
    }

    private Queue<AbilityObject> queue;
    private AbilityPipeline pipeline;

    //     Add to stack
    public void addToStack(System.Object item)
    {
        if (SharedCode.verifyTyping(item, typeof(AbilityObject), "")) return;
        AbilityObject castItem = item as AbilityObject;
        queue.Enqueue(castItem);
    }

    // Check stack

    // Process stack Async

    public void processStack()
    {
        // call everything here syncronusly but these methods lead to awaits
        //       awaits include: waiting for choices and targets
        //       awaiting items here should have very clear questions that the user needs to input
        if (queue.Count > 0)
        {
            pipeline.resolveAbility(queue.Dequeue());
        }
    }
}