namespace PetitionBackend.Models
{
    // ▶ Об'єкт для передачі даних користувача, 
    // необхідних для реєстрації
    public class UserRegisterDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
}
