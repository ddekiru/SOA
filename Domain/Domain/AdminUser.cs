using System.Text.RegularExpressions;

namespace Domain.Domain
{
    public class AdminUser
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }

        public async static Task Validate(string username, string companyName,
            CancellationToken cancellationToken = default)
        {
            await Task.Run(() => { }, cancellationToken);
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Enter an username");
            if (username.Trim().Length < 3)
                throw new ArgumentException("Username length cannot be less than 3 characters");
            if (string.IsNullOrWhiteSpace(companyName))
                throw new ArgumentException("Enter a company name");
        }
    }
}
