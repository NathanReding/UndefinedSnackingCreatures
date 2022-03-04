using System;
using SharedScripts;

namespace DTOs
{
    public class EffectObject
    {
        public string name;
        public string effectDataString;
        public int effectDataInt;
        public float effectDataFloat;

        public EffectObject(string name, string value)
        {
            this.name = name;
            switch (SharedCode.getEffectDataType(name))
            {
                case "int":
                    effectDataInt = int.Parse(value);
                    break;
                case "string":
                    effectDataString = value;
                    break;
                case "float":
                    effectDataFloat = float.Parse(value);
                    break;
            }
        }

        public void modify(string value)
        {
            switch (SharedCode.getEffectDataType(name))
            {
                case "int":
                    effectDataInt += int.Parse(value);
                    break;
                case "string":
                    // effectDataString = value; // TODO figure out if these should even be modified
                    break;
                case "float":
                    effectDataFloat += float.Parse(value);
                    break;
            }
        }

        public (string, int, float) getValue(){
            return (effectDataString, effectDataInt, effectDataFloat);
        }
    }
}
