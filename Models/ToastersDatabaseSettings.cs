namespace ToasterApi.Models
{
    public class ToastersDatabaseSettings : IToastersDatabaseSettings
    {
        public string ToastersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IToastersDatabaseSettings
    {
        string ToastersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}