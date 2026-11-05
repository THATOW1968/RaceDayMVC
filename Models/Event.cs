namespace RaceDayMVC.Models
{//start of namespace
    public class Event
    {//start of class
        public int EventID { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Distance { get; set; } = string.Empty;
        public string EventType { get; set; } = "Run";
        public string ImageUrl { get; set; } = string.Empty; 
    }//end of class
}//end of namespace
