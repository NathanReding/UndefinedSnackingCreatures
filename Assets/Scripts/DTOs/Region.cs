using System.Collections.Generic;

namespace DTOs
{
    [System.Serializable]
    class Region : DTODisplayable
    {
        public Region(string name, string description) : base(name, description)
        {

        }
        List<Zone> zones;
    }
}
