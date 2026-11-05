namespace RaceDayMVC.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        public string EventType { get; set; } = "Run";
        public string ImageUrl { get; set; } = string.Empty; 
    }
}
