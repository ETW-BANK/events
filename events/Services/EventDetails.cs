using Newtonsoft.Json;

namespace events.Services
{
    public class EventDetails:IEvents
    {
        public async Task<List<object>> GetEvents(string type, string city)
        {
            using (var client = new HttpClient())
            {
                string apiUrl = $"https://real-time-events-search.p.rapidapi.com/search-events?query={type}%20in%20{city}%20&date=any&is_virtual=false&start=0";

              
                client.DefaultRequestHeaders.Add("x-rapidapi-key", "597c529c02msh24cd8fda8287734p115600jsn5390d57dc0a0");
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "real-time-events-search.p.rapidapi.com");

                var response = await client.GetAsync(apiUrl);

               
                response.EnsureSuccessStatusCode();

             
                var jsonResponse = await response.Content.ReadAsStringAsync();

           
                var eventDetail = JsonConvert.DeserializeObject<Dto.Root>(jsonResponse);

                if (eventDetail?.data == null)
                {
                    return new List<object>();
                }

            
                var result = eventDetail.data
                    .Where(datum => !string.IsNullOrEmpty(datum.thumbnail))
                    .Select(datum => new
                    {
                        EventName = datum.name,
                        EventDescription = datum.description,
                        Venue = datum.venue?.name,
                        VenueCity = datum.venue?.city,
                        VenueAddress = datum.venue?.full_address,
                        StartTime = datum.start_time,
                        EndTime = datum.end_time,
                        TicketLink = datum.ticket_links,
                        Thumbnail = datum.thumbnail
                    })
                    .ToList<object>();

                return result;
            }
        }
    }
}
