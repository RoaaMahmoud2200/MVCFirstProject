namespace Company.Route2.PL.Services
{
    public interface ITransient
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
