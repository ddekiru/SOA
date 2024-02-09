namespace Domain.Domain
{
    public class RegularUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static async Task CheckFields(string userName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { }, cancellationToken);
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Enter an username");
            if (userName.Trim().Length < 3)
                throw new ArgumentException("Username length cannot be less than 3 characters");
        }
    }
}
