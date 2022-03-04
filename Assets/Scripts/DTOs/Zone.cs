namespace DTOs
{
    [System.Serializable]
    class Zone : DTODisplayable
    {
        public Zone(
            string name,
            string description
        ) : base(name, description){
        }
    }
}