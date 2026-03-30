using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetitionBackend.Models
{
    // ▶ Перелічення можливих ролей користувача
    public enum UserRole
    {
        User,
        Moderator,
        Admin
    }

    // ▶ Модель користувача
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrement
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }
    }
}
