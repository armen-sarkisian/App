using System.ComponentModel.DataAnnotations;

namespace Auth.DAO.Model
{
    public class UserClientsEmployee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z]|[A-Z]).{1,})", ErrorMessage = "Логин должен состоять из латинских букв и цифр")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 40 символов")]
        [Microsoft.AspNetCore.Mvc.Remote(action: "CheckLogin", controller: "Home", ErrorMessage = "Этот логин уже занят. Укажите другой.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{1,})", ErrorMessage = "Пароль должен состоять из латинских букв, цифр и хотя-бы одной заглавной буквы")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 100 символов")]
        public string Password { get; set; }

        public string Date { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*[а-я]|[А-Я]).{1,})", ErrorMessage = "ФИО должно быть написано русскими буквами")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 60 символов")]
        public string SurnameNameFathersName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*[а-я]|[А-Я]).{1,})", ErrorMessage = "Должность должна быть указана русскими буквами")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 60 символов")]
        public string WorkPosition { get; set; }

        public string UsersType { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"([0-9]{1,})", ErrorMessage = "Номер телефона должен содержать только цифры")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 12 символов")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"(^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$)", ErrorMessage = "Укажите ваш Email в формате example@example.com")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 60 символов")]
        public string Email { get; set; }

        public string ParentsCompanyName { get; set; }

        public string AccountType { get; set; }
        public bool isArchived { get; set; }
        public string inArchive { get; set; }
    }

}

