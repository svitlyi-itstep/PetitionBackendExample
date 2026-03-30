namespace PetitionBackend.Models
{
    // ▶ Об'єкт для передачі даних користувача, 
    // необхідних для авторизації
    public class UserLoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
