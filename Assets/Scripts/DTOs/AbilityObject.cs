using System.Collections.Generic;
using DTOs;
using UnityEngine;

namespace DTOs
{
    [System.Serializable]
    class AbilityObject
    {
        public EffectTermSegment template; // change over to the ability type made in decription
        // EffectTermSegment   from    AbilityTemplate
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