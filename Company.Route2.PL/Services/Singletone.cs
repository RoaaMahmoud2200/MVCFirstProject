namespace Company.Route2.PL.Services
{
    public class Singletone:ISingletone
    {
        public Guid Guid { get; set; }

        public Singletone()
        {
            Guid = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Guid.ToString();
        }
    }

}
