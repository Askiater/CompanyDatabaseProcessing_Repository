namespace CompanyDatabaseProcessing.Models
{
    //Комментарии: Была выбрана подобная модель представления для того чтобы работа с 3 сущностями в коде обьединялась в вид 
    //Имя, Фамилия, Отчество, Отдел, Должность (для создания стого-типизированного представления) без какой-либо информации об id.

    /// <summary>
    /// Класс для представления элементов из таблицы Person (создание таблицы описано в SQL-запросе при создание БД) в виде: Имя, Фамилия, Отчество, Отдел, Должность
    /// </summary>
    public class PersonView 
    {
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string last_name { get; set; }
        public string dep { get; set; }
        public string post { get; set; }
    }
}