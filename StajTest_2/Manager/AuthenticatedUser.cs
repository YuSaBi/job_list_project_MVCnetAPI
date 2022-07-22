using System.Security.Principal;

namespace StajTest_2.Manager
{
    public class AuthenticatedUser : IIdentity
    {
        public AuthenticatedUser(string authenticateType, bool isAuthenticated, string name)
        {
            AuthenticationType = authenticateType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        public string? AuthenticationType { get; }
        public bool IsAuthenticated { get; }

        public string? Name { get; }
    }
}
