using DrewKestellSite.Data;
using DrewKestellSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace DrewKestellSite.Concerns
{
    public class Authentication : IAuthentication
    {
        readonly ApplicationContext context;
        readonly IPasswordHasher<User> passwordHasher;

        public Authentication(ApplicationContext context, IPasswordHasher<User> passwordHasher)
        {
            this.context = context;
            this.passwordHasher = passwordHasher;
        }

        public async Task<int> AuthenticateUser(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                throw new AuthenticationException("Account not found.");

            var verificationResult = passwordHasher.VerifyHashedPassword(user, user.HashedPassword, password);

            switch (verificationResult)
            {
                case PasswordVerificationResult.SuccessRehashNeeded:
                    user.HashedPassword = passwordHasher.HashPassword(user, password);
                    await context.SaveChangesAsync();
                    break;
                case PasswordVerificationResult.Failed:
                    throw new AuthenticationException("Invalid password.");
            }

            return user.Id;
        }
    }
}
