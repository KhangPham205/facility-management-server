using backend.Repositories.Interfaces;
using System.Security.Claims;

namespace backend.Middlewares
{
    public class JwtMiddleware : IMiddleware
    {
        private readonly IUserRepository _repo;

        public JwtMiddleware(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var identity = context.User.Identity as ClaimsIdentity;

            if (identity != null && identity.IsAuthenticated)
            {
                var emailClaim = identity.FindFirst(ClaimTypes.Email);

                if (emailClaim != null)
                {
                    string email = emailClaim.Value;
                    var user = _repo.GetByEmail(email);
                    if (user != null)
                    {
                        context.Items["User"] = user;
                    }
                }
            }

            await next(context);
        }
    }
}