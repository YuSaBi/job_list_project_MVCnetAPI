using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using StajTest_2.Models;
using Dapper;

namespace StajTest_2.Manager
{
    public class SqlManager
    {
        public string cs = "Data Source=DESKTO-SN2L41M;Initial Catalog=StajProje_1; Integrated Security=True";// appsetting.json dan çekilecek
        public Response AddUser(User item)
        {
            Response resp = new Response();
            try
            {
                using (var connection= new SqlConnection(cs))
                {
                    var param = new DynamicParameters();
                    //param.Add("UserName", dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
                    param.Add("UserName", item.UserName);
                    param.Add("UserPassword", item.UserPassword);
                    //param.Add("ResultCode", dbType: DbType.Int32, direction: ParameterDirection.Output); // Out
                    //param.Add("ResultMsg", SqlDbType.VarChar, direction: ParameterDirection.Output); // Out
                    //SqlParameter job1 = param.Parameters.Add("@ResultMsg", SqlDbType.VarChar, 50);

                    resp = connection.Query<Response>("SP_Register", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    
                    //var ret = connection.QueryMultiple("SP_Register",param, commandType: CommandType.StoredProcedure);
                    

                    //int ResCode = param.Get<int>("ResultCode"); // Out
                    //string ResMsg = param.Get<string>("ResultMsg"); // Out
                    
                    //resp.ResponseMsg = ResMsg;
                }
            }
            catch (Exception ex)
            {
                resp.ResponseMsg = ex.Message;
            }
            

            return resp;
        }
        

    }
}
