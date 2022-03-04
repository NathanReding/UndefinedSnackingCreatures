using System.Collections.Generic;
using DTOs;
using UnityEngine;

namespace DTOs
{
    [System.Serializable]
    class AbilityObject
    {
        AbilityTemplate template;
        ConditionalArgSet incomingArgSet;
        List<ITargetableObject> targets;
        ITargetableObject origin;
        List<EffectObject> effects;

        public AbilityObject(AbilityTemplate template, System.Object incomingArgSet){
            if(incomingArgSet.GetType() != typeof(ConditionalArgSet)){
                Debug.LogError("AbilityObject constructed with illegal type for incomingArgSet. type: " + incomingArgSet.GetType());
                return;
            }
            effects = new List<(string, string)>();
            targets = new List<ITargetableObject>();
        }



    }
}