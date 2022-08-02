namespace StajTest_2.Models
{
    public class ResponseUID : Response
    {
        public int UserID { get; set; }
    }

    public class Response
    {
        public int ResponseCode { get; set; }
        public string ResponseMsg { get; set; } = "";
    }
}
