using System.ComponentModel.DataAnnotations;

namespace PetitionBackend.Models
{
    // ▶ Модель токена для оновлення авторизації
    public class RefreshToken
    {
        [Key]
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
