using Map360Task.Domain.Entities;

namespace Map360Task.UI.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
        public List<Company> Companies { get; set; }
    }
}
