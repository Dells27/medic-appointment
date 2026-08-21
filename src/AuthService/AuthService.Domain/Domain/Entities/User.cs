namespace AuthService.Domain.Entities
{
    public class User
    {
        public Guid ID { get; private set;}
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }
        public string Name { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Boolean IsActive { get; private set; }

        private User() { }

        public static User Create(string email, string password, string role, string name)
        {
            return new User
            {
                ID = Guid.NewGuid(),
                Email = email.ToLower().Trim(),
                Password = password,
                Role = role,
                Name = name,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

       

    }
}
