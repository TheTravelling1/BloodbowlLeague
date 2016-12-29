namespace BloodbowlLeague.Data
{
    public class CoachStorage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public byte[] HashedPassword { get; set; }
        public string[] Teams { get; set; }
    }
}