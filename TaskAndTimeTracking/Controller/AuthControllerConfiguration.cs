using TaskAndTimeTracking.Controller.interfaces;

namespace TaskAndTimeTracking.Controller
{
    public class AuthControllerConfiguration : IAuthControllerConfiguration
    {
        public string Secret { get; set; }
        public int ExpirationDuration { get; set; }
    }
}