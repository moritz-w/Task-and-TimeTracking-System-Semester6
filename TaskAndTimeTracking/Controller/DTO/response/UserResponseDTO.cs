namespace TaskAndTimeTracking.Controller.DTO.response
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set; }

        public int AuthorizationLevel { get; set; }
    }
}