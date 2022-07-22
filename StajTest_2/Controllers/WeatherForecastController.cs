using Microsoft.AspNetCore.Mvc;
using StajTest_2.Models;
using System.Data.SqlClient;

namespace StajTest_2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /*
        [HttpGet(Name ="Jobss")]
        public List<Jobs> JobGetir(int UserID)
        {
            
            List<Jobs> joblar = new List<Jobs>();// veri listesi

            string cs = "Data Source=DESKTOP-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek
            using(SqlConnection Con = new SqlConnection(cs))// SqlConnection bulamazsa SqlClient Expension yüklenmeli
            {
                SqlCommand cmd = new SqlCommand("SP_View",Con);// procedure ismi ve baðlantý girildi
                cmd.CommandType = System.Data.CommandType.StoredProcedure;// bir procedure oldugu için bunu SP olarak tipini belirliyoruz

                SqlParameter parameter = new SqlParameter();// sql parametresþ oluþturuluyor
                parameter.ParameterName = "@UserID";// parametre adý giriliyor
                parameter.Value = UserID;// parametre verileri giriliyor

                cmd.Parameters.Add(parameter);// oluþturulan parametre sql komutuna gönderiliyor
                Con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())// okuduðumuz verileri oluþturduðumuz deðiþkene atýyoruz
                {
                    Jobs job = new Jobs();// tekli veri

                    job.Baslik = dr["Baslik"].ToString();
                    job.Detay = dr["Detay"].ToString();
                    job.Gun = Convert.ToDateTime(dr["Gun"]);
                    job.HarcananSure = Convert.ToInt32(dr["HarcananSure"]);
                    job.Musteri = dr["CustomerName"].ToString();
                    job.Durum = dr["DurumAd"].ToString();
                    job.Oncelik = dr["PriorityName"].ToString();
                    joblar.Add(job);
                }
                
                Con.Close();
            }


            return joblar;// en sonda liste geri döndürüldü
        }
        */

        /*
        [HttpGet(Name = "Jobs")]
        public IEnumerable<Jobs> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Jobs
            {


                
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                
            })
            .ToArray();
        }
        */


    }


    /*
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        */


}