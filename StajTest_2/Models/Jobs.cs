namespace StajTest_2.Models
{
    public class jobListMaster
    {
        public string ResponseCode { get; set; }
        public string ResponseMsg { get; set; }
        public List<Jobs> Jobslist { get; set; }
        public jobListMaster()
        {
            ResponseCode = "";
            ResponseMsg = "";
            Jobslist = new List<Jobs>();
        }
        public jobListMaster (string responseCode, string responseMsg, List<Jobs> jobslist)
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
}
