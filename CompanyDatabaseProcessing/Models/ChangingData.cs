using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompanyDatabaseProcessing.Models
{
    /// <summary>
    /// Особый вид данных для для работы с формой ChangeItemForm. Возвращает 2 экземпляра полей (1 - для добавляемого элемента, другой для удаляемого элемента). 
    /// Выполняет компиляцию полей в стандартный вид PersonView посредством вызова метода CreateListOfPersonView
    /// </summary>
    public class ChangedData
    {
        public List<PersonView> CreateListOfPersonView()
        {
            return new List<PersonView>
            {
                new PersonView
                {
                    first_name = first_name_del,
                    second_name = second_name_del,
                    last_name = last_name_del,
                    dep = dep_del,
                    post = post_del
                },
                new PersonView
                {
                    first_name = first_name_add,
                    second_name = second_name_add,
                    last_name = last_name_add,
                    dep = dep_add,
                    post = post_add
                }
            };
        }

        [Required(ErrorMessage = "Пожалуйста, введите имя добавляемого")]
        public string first_name_add { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите фамилию добавляемого")]
        public string second_name_add { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите отчество добавляемого")]
        public string last_name_add { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите отдел добавляемого")]
        public string dep_add { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пост добавляемого")]
        public string post_add { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите имя заменяемого")]
        public string first_name_del { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите фамилию заменяемого")]
        public string second_name_del { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите отчество заменяемого")]
        public string last_name_del { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите отдел заменяемого")]
        public string dep_del { get; set; }

        [Required(ErrorMessage = "Пожалуйста, введите пост заменяемого")]
        public string post_del { get; set; }
    }
}