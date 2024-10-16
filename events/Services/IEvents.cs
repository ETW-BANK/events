namespace events.Services
{
    public interface IEvents
    {
        Task<List<object>> GetEvents(string type, string city);
    }
}
