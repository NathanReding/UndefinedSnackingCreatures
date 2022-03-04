using System.Collections.Generic;

namespace DTOs
{
    public class ConditionalArgSet
    {
        public List<ITargetableObject> predisesors;

        public Dictionary<string, System.Object> arguments;

        public ConditionalArgSet()
        {
            arguments = new Dictionary<string, object>();
            predisesors = new List<ITargetableObject>();
        }

        public ConditionalArgSet(List<ITargetableObject> predisesors)
        {
            this.predisesors = new List<ITargetableObject>(predisesors);
            arguments = new Dictionary<string, object>();
        }

        public ConditionalArgSet(Dictionary<string, System.Object> arguments)
        {
            predisesors = new List<ITargetableObject>();
            this.arguments = arguments.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        public ConditionalArgSet(List<ITargetableObject> predisesors, Dictionary<string, System.Object> arguments)
        {
            this.predisesors = new List<ITargetableObject>(predisesors);
            this.arguments = arguments.ToDictionary(entry => entry.Key, entry => entry.Value);
        }

        public ConditionalArgSet clone(ConditionalArgSet cArgSet)
        {
            return new ConditionalArgSet(cArgSet.predisesors, cArgSet.arguments);
        }

    }
}
