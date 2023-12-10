using ServerBlazor.Models.Entities;

namespace ServerBlazor.Models
{
    public interface IAccountRepository
    {
        Task<BlogUser> VerifyCredentials(BlogUser user);
        string GenerateJwtToken(BlogUser user);
        Task<bool> ChangePasswd(BlogUser u, string oldP, string newP);
        Task<BlogUser> ChangeRole(BlogUser u, string newR);
        Task<bool> DeleteUser(BlogUser u);
        Task LogoutUser();
        Task<BlogUser> AddUser(BlogUser u);
        Task<List<BlogUser>> GetAllUsers();
    }
}