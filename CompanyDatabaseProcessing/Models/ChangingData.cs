using System.Collections.Generic;

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

        public string first_name_add { get; set; }
        public string second_name_add { get; set; }
        public string last_name_add { get; set; }
        public string dep_add { get; set; }
        public string post_add { get; set; }

        public string first_name_del { get; set; }
        public string second_name_del { get; set; }
        public string last_name_del { get; set; }
        public string dep_del { get; set; }
        public string post_del { get; set; }
    }
}