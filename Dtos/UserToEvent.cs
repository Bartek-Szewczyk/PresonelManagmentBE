namespace PresonelManagmentBE.Dtos
{
    public class UserToEvent
    {
        public int eventId { get; set; }
        public string userId { get; set; }
        public bool approved { get; set; }
    }
}