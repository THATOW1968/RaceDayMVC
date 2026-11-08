using System.Text;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApiService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        //LOGIN 
        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, PasswordHash = password };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<User>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (user != null)
                {
                    _httpContextAccessor.HttpContext?.Session.SetString("UserId", user.UserID.ToString());
                    _httpContextAccessor.HttpContext?.Session.SetString("UserRole", user.Role);
                    _httpContextAccessor.HttpContext?.Session.SetString("UserEmail", user.Email);
                    return true;
                }
            }
            return false;
        }

        //REGISTER 
        public async Task<bool> RegisterAsync(User newUser)
        {
            var content = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Auth/register", content);
            return response.IsSuccessStatusCode;
        }

        //GET EVENTS 
        public async Task<List<Event>> GetEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/Event");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Event>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Event>();
            }
            return new List<Event>();
        }

        //CREATE EVENT (Organizer) 
        public async Task<bool> CreateEventAsync(Event newEvent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Event");
            request.Headers.Add("Role", "Organizer");
            request.Content = new StringContent(JsonSerializer.Serialize(newEvent), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        //ENROLL IN EVENT (Participant) 
        public async Task<bool> EnrollAsync(int eventId, int userId)
        {
            var enrolData = new { EventID = eventId, ParticipantID = userId, Category = "Open" };
            var request = new HttpRequestMessage(HttpMethod.Post, "api/Enrollment");
            request.Headers.Add("Role", "Participant");
            request.Content = new StringContent(JsonSerializer.Serialize(enrolData), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }

        //GET ENROLMENTS FOR EVENT (Organizer) 
        public async Task<List<Enrollment>> GetEventEnrollmentsAsync(int eventId)
        {
            var response = await _httpClient.GetAsync($"api/Enrollment/event/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Enrollment>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Enrollment>();
            }
            return new List<Enrollment>();
        }
    }
}