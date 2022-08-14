namespace StajTest_2.Models
{
    public class jobListMaster
    {
        public int ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<Jobs> Jobslist { get; set; }
        public jobListMaster()
        {
            ResponseMsg = "";
            Jobslist = new List<Jobs>();
        }
        public jobListMaster (int responseCode, string responseMsg, List<Jobs> jobslist)
        {
            ResponseCode = responseCode;
            ResponseMsg = responseMsg;
            Jobslist = jobslist;
        }
    }

    public class Jobs
    {
        public string? Baslik { get; set; }
        public string? Detay { get; set; }
        public DateTime Gun { get; set; }
        public int HarcananSure { get; set; }
        public string? Musteri { get; set; }
        public string? Durum { get; set; }
        public string? Oncelik { get; set; }

    }

    public class JobId
    {
        public int JobID { get; set; }
    }

    public class saveJob
    {
        public int UserID { get; set; }
        public String? Baslik{ get; set; }
        public int HarcananSure { get; set; }
        public String? Detay { get; set; }
        public int CustomerID { get; set; }
        public int Durum { get; set; }
        public int PriorityID { get; set; }
    }
}
