namespace RaceDayMVC.Models
{//start of name space
    public class Enrollment
    {//start of class
        public int EnrollmentID { get; set; }
        public int EventID { get; set; }
        public int ParticipantID { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }//end of class
}//end of namespace
