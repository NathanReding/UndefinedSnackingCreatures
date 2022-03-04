using DTOs;
using UnityEngine;

namespace DTOs
{
    [System.Serializable]
    class AbilityObject
    {
        AbilityTemplate template;
        ConditionalArgSet incomingArgSet;

        public AbilityObject(AbilityTemplate template, System.Object incomingArgSet){
            if(incomingArgSet.GetType() != typeof(ConditionalArgSet)){
                Debug.LogError("AbilityObject constructed with illegal type for incomingArgSet. type: " + incomingArgSet.GetType());
                return;
            }
        }

    }
}