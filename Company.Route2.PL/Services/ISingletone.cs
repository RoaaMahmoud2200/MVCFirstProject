namespace Company.Route2.PL.Services
{
    public interface ISingletone
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
