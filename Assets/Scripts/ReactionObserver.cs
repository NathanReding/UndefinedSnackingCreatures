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

    Dictionary<string, List<ReactionObject>> removalTriggeredEvents;
    Dictionary<int, List<ReactionObject>> timeTriggeredRemoval;
    int currentTurn = 1;

    Dictionary<CreatureObject, List<ReactionObject>> modificationReactions;
    Dictionary<CreatureObject, List<ReactionObject>> replacementReactions;
    Dictionary<CreatureObject, List<ReactionObject>> absoluteReactions;

    private ReactionObserver()
    {
        removalEvents = new Dictionary<string, List<ReactionObject>>();

        modificationReactions = new Dictionary<CreatureObject, List<ReactionObject>>();
        replacementReactions = new Dictionary<CreatureObject, List<ReactionObject>>();
        absoluteReactions = new Dictionary<CreatureObject, List<ReactionObject>>();
    }


    // data: (effectTarget, effect change)  TODO: give keywords counting parameters (on off, up to 3, up to 5, -1 to 1)
    public List<(string, string)> getModReactions(AbilityObject ability){
        // check reciever list for this creature
        modificationReactions
        // check 
    }
    /*
        The reactions has a list that will be removed at x turns
        The reactions need to know what triggers it
        The reaction needs to know if they are on recieve or on apply
        The reaction needs to know what type it is
    */

    // TODO: EXPANSION: location based reactions. Might leverage board instead

    /*

    */

    public void addReaction(string trigger, string ractionType, CreatureObject owner, bool typeReciever, string persistFor, int durationValue, List<EffectObject> effects){
        ReactionObject newReaction = new ReactionObject(
            trigger, reactionType, owner, typeReciever, persistFor, durationValue, effects
        )
        
        Dictionary<CreatureObject, List<ReactionObject>> dictionary = getDictionary(ractionType);
        if(dictionary.ContainsKey(newReaction.owner)){
            dictionary[newReaction.owner].Add(newReaction);
        }
        else{
            dictionary.Add(owner, new List<ReactionObject>(){newReaction});
        }

        if(newReaction.persistFor == "forTurns"){
            int turnValue = currentTurn + durationValue;
            if(durationValue <= 0){
                Debug.ErrorLog("durationValue not valid. Continued equil to 1");
                turnValue = currentTurn + 1;
            }
            if(timeTriggeredRemoval.ContainsKey(turnValue
            )){
                timeTriggeredRemoval[turnValue].Add(newReaction);
            }
            else{
                timeTriggeredRemoval.Add(turnValue, new List<ReactionObject>(){newReaction});
            }
        }
        else if(removalEvents.ContainsKey(newReaction.persistFor
        )){
            removalEvents[newReaction.persistFor].Add(newReaction);
        }
        else{
            removalEvents.Add(persistFor, new List<ReactionObject>(){newReaction});
        }

    }

    private class ReactionObject{
        public string trigger;
        public string reactionType;
        public  CreatureObject owner;
        public  bool typeReciever;
        public string persistFor;
        public int durationValue;
        public List<EffectObject> effects;

        public ReactionObject(string trigger, string reactionType, CreatureObject owner, bool typeReciever, string persistFor, int durationValue, List<EffectObject> effects){
            this.trigger = trigger;
            this.reactionType = reactionType;
            this.owner = owner;
            this.typeReciever = typeReciever;
            this.persistFor = persistFor;
            this.durationValue = durationValue;
            this.effects = effects;
        }
    }


    private Dictionary<CreatureObject, List<ReactionObject>> getDictionary(string ractionType){
            switch(ractionType){
            case "modification":
                return modificationReactions;
            case "replacement":
                return replacementReactions;
            case "absolute":
                return absoluteReactions;
        }
    }



    public void moveToNextTurn(){
        currentTurn++;
        if(timeTriggeredRemoval.ContainsKey(currentTurn)){
            foreach(ReactionObject obj in timeTriggeredRemoval[currentTurn]){
                removeEvent(obj);
            }
            timeTriggeredRemoval.Remove(currentTurn);
        }
    }

    public void removeEndOfturnReactions(){
        foreach(ReactionObject obj in removalTriggeredEvents["tillEndOfTurn"]){
            removeEvent(obj);
        }
        removalTriggeredEvents["tillEndOfTurn"] // replace this field with a new list
    }

    public void removeEndOfMatchReactions(){
        foreach(ReactionObject obj in removalTriggeredEvents["tillEndOfMatch"]){
            removeEvent(obj);
        }
        removalTriggeredEvents["tillEndOfMatch"] // replace this field with a new list
    }


    private void removeEvent(ReactionObject obj){
        removeEventFromDictionary(obj, getDictionary(obj.reactionType));
    }

    private void removeEventFromDictionary(ReactionObject obj, Dictionary<CreatureObject, List<ReactionObject>> dictionary){
        dictionary[obj.owner].Remove(obj);
        if(dictionary[obj.owner].Count <= 0) dictionary.Remove(obj.owner);
    }
}