namespace StajTest_2.Manager
{
    public class LogManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Dosya Yolu</param>
        /// <param name="body">Log icerigi</param>
        public string logNotepad(string path,string body)
        {
            try
            {
                if (!File.Exists(path))
                {
                    //Dosya oluşturulur.
                    File.Create(path).Dispose();
                }
                //Verileri alt alta yazmak için, true parametresi gönderilir.
                //Eğer false gönderilse sadece en son yazılan veriyi kaydeder.
                StreamWriter write = new StreamWriter(path, true);

                //içerik notepade yazılır.
                write.WriteLine(body);
                write.Close();

                return "Log kayit basarili";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }
    }
}
