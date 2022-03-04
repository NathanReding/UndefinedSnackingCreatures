using System.Collections.Generic;
using System.Linq;

namespace DTOs
{
    public class AbilityTerm
    {

        public string name;

        public bool isDeclaritive;
        public bool nonSingular;

        public List<string> subterms;
        public List<string> longSubtermsFull;
        public List<string> longSubtermsCompact;

        // TODO add in constructors

        public AbilityTerm()
        {

        }

        public AbilityTerm(string name, bool isDeclaritive, bool nonSingular, string content)
        {
            this.name = name;
            this.isDeclaritive = isDeclaritive;
            this.nonSingular = nonSingular;
            if (nonSingular)
            {
                longSubtermsCompact = content.Split(',').ToList();
                extractLongSubterms();
            }
            else
            {
                subterms = content.Split(',').ToList();
            }
        }

        private void extractLongSubterms()
        {
            foreach (string segment in longSubtermsCompact)
            {
                string temp = segment.Replace("{", " { ").Replace("}", " } ");

                Queue<string> queue = new Queue<string>(temp.Split(' '));
                if (!subterms.Contains(queue.Peek())) subterms.Add(queue.Peek());
                foreach (string item in splitCompactToFull(queue))
                {
                    longSubtermsFull.Add(item);
                }
            }
        }

        private List<string> splitCompactToFull(Queue<string> queue)
        {
            List<string> myValues = new List<string>();
            if (queue.Peek() != "{")
            {
                myValues.Add(queue.Peek());
            }
            else
            {
                queue.Dequeue();
                while (queue.Peek() != "}")
                {
                    myValues.Add(queue.Dequeue());
                }
                queue.Dequeue();
            }
            if (queue.Count > 0)
            {
                List<string> subValues = splitCompactToFull(queue);
                List<string> result = new List<string>();
                foreach (string primary in myValues)
                {
                    foreach (string secondary in subValues)
                    {
                        result.Add(primary + " " + secondary);
                    }
                }
                return result;
            }
            return myValues;
        }
    }
}
