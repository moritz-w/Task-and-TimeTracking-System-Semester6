namespace TaskAndTimeTracking.Controller.interfaces
{
    public interface IAuthControllerConfiguration
    {
        string Secret { get; set; }
        
        int ExpirationDuration { get; set; }
    }
}