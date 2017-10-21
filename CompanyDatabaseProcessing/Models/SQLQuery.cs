using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Management;

namespace CompanyDatabaseProcessing.Models
{
    public static class SqlQuery
    {
        /// <summary>
        /// Вызывает хранимую процедуру в БД осуществляющую поиск по БД для нахождения элементов совпадающих с параметром item. Если item == null, то поиска не происходит, а производиться вывод всех значений
        /// </summary>
        /// <param name="procedureName">Имя хранимой процедуры</param>
        /// <param name="item">Параметр PersonView по которому производиться поиск. В случае если элемент не найден в БД метод вызывает исключение SqlExecutionException</param>
        /// <param name="connString">Строка соединения с сервером</param>
        /// <returns></returns>
        public static List<PersonView> ViewData(string procedureName, PersonView item, string connString)
        {
            var tableOfPerson = new List<PersonView>();
            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand(procedureName, databaseConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                if (item != null)
                {
                    #region Add Parameters to procedure
                    cmd.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar));
                    cmd.Parameters["@first_name"].Value = item.first_name;
                    cmd.Parameters.Add(new SqlParameter("@second_name", SqlDbType.VarChar));
                    cmd.Parameters["@second_name"].Value = item.second_name;
                    cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar));
                    cmd.Parameters["@last_name"].Value = item.last_name;
                    cmd.Parameters.Add(new SqlParameter("@name_dep", SqlDbType.VarChar));
                    cmd.Parameters["@name_dep"].Value = item.dep;
                    cmd.Parameters.Add(new SqlParameter("@name_post", SqlDbType.VarChar));
                    cmd.Parameters["@name_post"].Value = item.post;
                    #endregion
                }

                databaseConnection.Open();
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new SqlExecutionException("Данные которые вы пытаетесь найти не существует :(");
                }
                while (reader.Read())
                {
                    tableOfPerson.Add(new PersonView
                    {
                        first_name = reader.GetString(0),
                        second_name = reader.GetString(1),
                        last_name = reader.GetString(2),
                        dep = reader.GetString(3),
                        post = reader.GetString(4),
                    });
                }
            }
            return tableOfPerson;  //Комментарий: Если item == null, то в выходную таблицу производитсья запись всех значений из БД
        }
        /// <summary>
        /// Вызывает процедуру для произведения удаления данных в БД
        /// </summary>
        /// <param name="procedureName">Имя вызываемой процедуры</param>
        /// <param name="item">Параметр PersonView который подлежит удалению. В случае если элемент не найден в БД метод вызывает исключение SqlExecutionException</param>
        /// <param name="connString">Строка соединения с сервером</param>
        public static void ChangeData(string procedureName, PersonView item, string connString) //Комментарий: Название выбрано из соображений возможной расширямости методы в дальнейшем
        {

            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand(procedureName, databaseConnection)             
                {
                   CommandType = CommandType.StoredProcedure
                };

                #region Add Parameters to procedure

                cmd.Parameters.Add(new SqlParameter("@first_name", SqlDbType.VarChar));
                cmd.Parameters["@first_name"].Value = item.first_name;
                cmd.Parameters.Add(new SqlParameter("@second_name", SqlDbType.VarChar));
                cmd.Parameters["@second_name"].Value = item.second_name;
                cmd.Parameters.Add(new SqlParameter("@last_name", SqlDbType.VarChar));
                cmd.Parameters["@last_name"].Value = item.last_name;
                cmd.Parameters.Add(new SqlParameter("@name_dep", SqlDbType.VarChar));
                cmd.Parameters["@name_dep"].Value = item.dep;
                cmd.Parameters.Add(new SqlParameter("@name_post", SqlDbType.VarChar));
                cmd.Parameters["@name_post"].Value = item.post;

                #endregion
                
                databaseConnection.Open();
                var result = cmd.ExecuteNonQuery();
                if (result == -1)
                {
                    throw new SqlExecutionException("Данные которые вы пытаетесь изменить не существует :(");
                }
            }

        }
        /// <summary>
        /// Вызывает процедуру для произведения замены данных в БД
        /// </summary>
        /// <param name="procedureName">Имя вызываемой процедуры</param>
        /// <param name="deleteItem">Заменяемый элемент в базе данных. В случае если элемент не найден в БД метод вызывает исключение SqlExecutionException</param>
        /// <param name="addItem">Элемент на которой производитсья замена</param>
        /// <param name="connString">Строка соединения с сервером</param>
        public static void ChangeData(string procedureName, PersonView deleteItem, PersonView addItem, string connString)
        {
            using (var databaseConnection = new SqlConnection(connString))
            {
                var cmd = new SqlCommand(procedureName, databaseConnection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                #region Add Parameters to procedure

                cmd.Parameters.Add(new SqlParameter("@first_name_add", SqlDbType.VarChar));
                cmd.Parameters["@first_name_add"].Value = addItem.first_name;
                cmd.Parameters.Add(new SqlParameter("@second_name_add", SqlDbType.VarChar));
                cmd.Parameters["@second_name_add"].Value = addItem.second_name;
                cmd.Parameters.Add(new SqlParameter("@last_name_add", SqlDbType.VarChar));
                cmd.Parameters["@last_name_add"].Value = addItem.last_name;
                cmd.Parameters.Add(new SqlParameter("@name_dep_add", SqlDbType.VarChar));
                cmd.Parameters["@name_dep_add"].Value = addItem.dep;
                cmd.Parameters.Add(new SqlParameter("@name_post_add", SqlDbType.VarChar));
                cmd.Parameters["@name_post_add"].Value = addItem.post;

                cmd.Parameters.Add(new SqlParameter("@first_name_del", SqlDbType.VarChar));
                cmd.Parameters["@first_name_del"].Value = deleteItem.first_name;
                cmd.Parameters.Add(new SqlParameter("@second_name_del", SqlDbType.VarChar));
                cmd.Parameters["@second_name_del"].Value = deleteItem.second_name;
                cmd.Parameters.Add(new SqlParameter("@last_name_del", SqlDbType.VarChar));
                cmd.Parameters["@last_name_del"].Value = deleteItem.last_name;
                cmd.Parameters.Add(new SqlParameter("@name_dep_del", SqlDbType.VarChar));
                cmd.Parameters["@name_dep_del"].Value = deleteItem.dep;
                cmd.Parameters.Add(new SqlParameter("@name_post_del", SqlDbType.VarChar));
                cmd.Parameters["@name_post_del"].Value = deleteItem.post;

                #endregion

                databaseConnection.Open();
                var result = cmd.ExecuteNonQuery();
                if (result == -1)
                {
                    throw new SqlExecutionException("Данных которые вы пытаетесь заменить не существует :(");
                }
            }
        }
    }
}


