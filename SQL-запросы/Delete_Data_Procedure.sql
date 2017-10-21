CREATE PROCEDURE DeleteValue
    @first_name nvarchar(50),
	@second_name nvarchar(50),
	@last_name nvarchar(50),
	@name_dep nvarchar(50),
	@name_post nvarchar(50)
AS
BEGIN
DECLARE @id_post int;
DECLARE @id_dep int;

IF NOT EXISTS(SELECT name from Departments WHERE Departments.name = @name_dep) 
RETURN -1
IF NOT EXISTS(SELECT name from Posts WHERE Posts.name = @name_post)
RETURN -1

SELECT @id_dep=Departments.id, @id_post = Posts.id  
from Departments, Posts 
WHERE Departments.name = @name_dep AND Posts.name = @name_post

IF NOT EXISTS(
  SELECT first_name, second_name, last_name, id_post, id_dep 
  FROM People 
  WHERE @first_name = People.first_name and @second_name = People.second_name and @last_name = People.last_name and @id_post = People.id_post and @id_dep = People.id_dep
)
  RETURN -1
ELSE
  DELETE FROM People
  WHERE @first_name = People.first_name and @second_name = People.second_name and @last_name = People.last_name and @id_post = People.id_post and @id_dep = People.id_dep
  RETURN 0
END
GO