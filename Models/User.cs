namespace RaceDayMVC.Models
{//start of namespace
    public class User
    {//start of class
        public int UserID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Participant";
    }//end of class
}//end of namespace
