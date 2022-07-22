using Microsoft.AspNetCore.Authorization;
namespace StajTest_2.Manager
    
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public BasicAuthorizeAttribute()
        {
            Policy = "Basic Authentication";
        }
    }
}
