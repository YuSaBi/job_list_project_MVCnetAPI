namespace StajTest_2.Models
{
    public class MailResp
    {
        public int ResponseCode { get; set; }
        public List<Mail> mails { get; set; }

        public MailResp()
        {
            mails = new List<Mail>();
        }
    }
    public class Mail
    {
        public int MailID { get; set; }
        public int MailFromID{ get; set; }
        public string? MailFromName { get; set; }
        public int MailToID { get; set; }
        public string? MailToName { get; set; }
        public string? Title{ get; set; }
        public string? Message{ get; set; }
        public bool IsRead { get; set; }
        public DateTime MailDateTime{ get; set; }
    }
    
    public class MailIDRead
    {
        public int ID { get; set; }
        public bool IsRead { get; set; }
    }

    public class MailAdd
    {
        public int MailFromID { get; set; }
        public int MailToID{ get; set; }
        public string? Title { get; set; }
        public string? Message{ get; set; }
    }
}
