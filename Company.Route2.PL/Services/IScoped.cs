namespace Company.Route2.PL.Services
{
    public interface IScoped
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
