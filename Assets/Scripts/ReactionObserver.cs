using System.Collections.Generic;
using DTOs;
using SharedScripts;
using UnityEngine;

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

    Dictionary<ITargetableObject, List<ReactionObject>> modificationReactions;
    Dictionary<ITargetableObject, List<ReactionObject>> replacementReactions;
    Dictionary<ITargetableObject, List<ReactionObject>> absoluteReactions;

    private ReactionObserver()
    {
        removalTriggeredEvents = new Dictionary<string, List<ReactionObject>>();
        timeTriggeredRemoval = new Dictionary<int, List<ReactionObject>>();

        modificationReactions = new Dictionary<ITargetableObject, List<ReactionObject>>();
        replacementReactions = new Dictionary<ITargetableObject, List<ReactionObject>>();
        absoluteReactions = new Dictionary<ITargetableObject, List<ReactionObject>>();
    }

    // data: (effectTarget, effect change)  TODO: give keywords counting parameters (on off, up to 3, up to 5, -1 to 1)
    public List<EffectObject> getModReactions(System.Object obj)
    {
        if (SharedCode.verifyTyping(obj, typeof(AbilityObject), "")) return null;
        AbilityObject ability = obj as AbilityObject;
        List<EffectObject> result = new List<EffectObject>();
        List<string> triggers = new List<string>();
        foreach (EffectObject effect in ability.effects)
        {
            triggers.Add(effect.name);
        }
        // check reciever list for this creature
        foreach (ITargetableObject targitable in ability.targets)
        {
            if (modificationReactions.ContainsKey(targitable))
            {
                result.AddRange(getReliventReactions(modificationReactions[targitable], true, triggers));
            }
        }
        if (modificationReactions.ContainsKey(ability.origin))
        {
            result.AddRange(getReliventReactions(modificationReactions[ability.origin], false, triggers));
        }
        return result;
    }


    private List<EffectObject> getReliventReactions(List<ReactionObject> reactions, bool typeReciever, List<string> triggers)
    {
        List<EffectObject> result = new List<EffectObject>();
        foreach (ReactionObject reaction in reactions)
        {
            if (reaction.typeReciever == typeReciever && triggers.Contains(reaction.trigger))
            {
                result.AddRange(reaction.effects);
            }
        }
        return result;
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

    public void addReaction(string trigger, string reactionType, ITargetableObject owner, bool typeReciever, string persistFor, int durationValue, List<EffectObject> effects)
    {
        ReactionObject newReaction = new ReactionObject(
            trigger, reactionType, owner, typeReciever, persistFor, durationValue, effects
        );

        Dictionary<ITargetableObject, List<ReactionObject>> dictionary = getDictionary(reactionType);
        if (dictionary.ContainsKey(newReaction.owner))
        {
            dictionary[newReaction.owner].Add(newReaction);
        }
        else
        {
            dictionary.Add(owner, new List<ReactionObject>() { newReaction });
        }

        if (newReaction.persistFor == "forTurns")
        {
            int turnValue = currentTurn + durationValue;
            if (durationValue <= 0)
            {
                Debug.LogError("durationValue not valid. Continued equil to 1");
                turnValue = currentTurn + 1;
            }
            if (timeTriggeredRemoval.ContainsKey(turnValue
            ))
            {
                timeTriggeredRemoval[turnValue].Add(newReaction);
            }
            else
            {
                timeTriggeredRemoval.Add(turnValue, new List<ReactionObject>() { newReaction });
            }
        }
        else if (removalTriggeredEvents.ContainsKey(newReaction.persistFor
        ))
        {
            removalTriggeredEvents[newReaction.persistFor].Add(newReaction);
        }
        else
        {
            removalTriggeredEvents.Add(persistFor, new List<ReactionObject>() { newReaction });
        }

    }

    private class ReactionObject
    {
        public string trigger;
        public string reactionType;
        public ITargetableObject owner;
        public bool typeReciever;
        public string persistFor;
        public int durationValue;
        public List<EffectObject> effects;

        public ReactionObject(string trigger, string reactionType, ITargetableObject owner, bool typeReciever, string persistFor, int durationValue, List<EffectObject> effects)
        {
            this.trigger = trigger;
            this.reactionType = reactionType;
            this.owner = owner;
            this.typeReciever = typeReciever;
            this.persistFor = persistFor;
            this.durationValue = durationValue;
            this.effects = effects;
        }
    }

    // TODO
    private Dictionary<ITargetableObject, List<ReactionObject>> getDictionary(string reactionType)
    {
        switch (reactionType)
        {
            case "modification":
                return modificationReactions;
            case "replacement":
                return replacementReactions;
            case "absolute":
                return absoluteReactions;
            default:
                Debug.LogError("ReactionObserver.getDictionary() not given valid string: " + reactionType);
                return null;
        }
    }



    public void moveToNextTurn()
    {
        currentTurn++;
        if (timeTriggeredRemoval.ContainsKey(currentTurn))
        {
            foreach (ReactionObject obj in timeTriggeredRemoval[currentTurn])
            {
                removeEvent(obj);
            }
            timeTriggeredRemoval.Remove(currentTurn);
        }
    }

    public void removeEndOfturnReactions()
    {
        foreach (ReactionObject obj in removalTriggeredEvents["tillEndOfTurn"])
        {
            removeEvent(obj);
        }
        removalTriggeredEvents["tillEndOfTurn"] = new List<ReactionObject>(); // replace this field with a new list
    }

    public void removeEndOfMatchReactions()
    {
        foreach (ReactionObject obj in removalTriggeredEvents["tillEndOfMatch"])
        {
            removeEvent(obj);
        }
        removalTriggeredEvents["tillEndOfMatch"] = new List<ReactionObject>(); // replace this field with a new list
    }


    private void removeEvent(ReactionObject obj)
    {
        removeEventFromDictionary(obj, getDictionary(obj.reactionType));
    }

    private void removeEventFromDictionary(ReactionObject obj, Dictionary<ITargetableObject, List<ReactionObject>> dictionary)
    {
        dictionary[obj.owner].Remove(obj);
        if (dictionary[obj.owner].Count <= 0) dictionary.Remove(obj.owner);
    }
}