using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class UserClients
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z]|[A-Z]).{1,})", ErrorMessage = "Логин должен состоять из латинских букв и цифр")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 40 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{1,})", ErrorMessage = "Пароль должен состоять из латинских букв, цифр и хотя-бы одной заглавной буквы")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина пароля должна быть от 10 до 100 символов")]
        public string Password { get; set; }

        public string Date { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[а-я]|[А-Я]).{1,})", ErrorMessage = "Название компании должно состоять из букв кириллицы и цифр")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Длина строки должна быть от 10 до 100 символов")]
        public string CompanyName { get; set; }

        public string OwnershipType { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 300 символов")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "Длина строки должна быть от 5 до 300 символов")]
        public string LegalAdress { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z]).{1,})", ErrorMessage = "Рассчетный счет должен состоять из заглавных латинских букв и цифр")]
        [StringLength(40, MinimumLength = 12, ErrorMessage = "Длина строки должна быть от 12 до 40 символов")]
        public string CheckingAccount { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*[а-я]|[А-Я]|[a-z]|[A-Z].{1,}[0-9]).{0,})", ErrorMessage = "Название банка должно быть указано кириллицей либо латиницей")]
        [StringLength(100, MinimumLength = 12, ErrorMessage = "Длина строки должна быть от 12 до 40 символов")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"((?=.*\d)(?=.*[A-Z]).{1,})", ErrorMessage = "Банк бин должен состоять из заглавных латинских букв и цифр")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 20 символов")]
        public string BankBin { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"([0-9]{9})", ErrorMessage = "Введите 9 цифр")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Строка должна состоять из 9-ти цифр")]
        public string UNP { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"(\d{6,})", ErrorMessage = "Введите от 6 до 20 цифр")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 20 цифр")]
        public string OKPO { get; set; }

        [Required(ErrorMessage = "Заполните это поле")]
        [RegularExpression(@"(\d{6,})", ErrorMessage = "Введите от 6 до 20 цифр")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Длина строки должна быть от 6 до 20 цифр")]
        public string ONPF { get; set; }

        public string FolderLanguage { get; set; }
        public bool isArchived { get; set; }
        public string inArchive { get; set; }
        public string ParentCompany { get; set; }
    }

}
