using System.Collections.Generic;
using DTOs;
using UnityEngine;

namespace DTOs
{
    [System.Serializable]
    class AbilityObject
    {
        public AbilityTemplate template;
        public ConditionalArgSet incomingArgSet;
        public List<ITargetableObject> targets;
        public ITargetableObject origin;
        public List<EffectObject> effects;

        public AbilityObject(AbilityTemplate template, System.Object incomingArgSet){
            if(incomingArgSet.GetType() != typeof(ConditionalArgSet)){
                Debug.LogError("AbilityObject constructed with illegal type for incomingArgSet. type: " + incomingArgSet.GetType());
                return;
            }
            effects = new List<EffectObject>();
            targets = new List<ITargetableObject>();
        }

    }
}