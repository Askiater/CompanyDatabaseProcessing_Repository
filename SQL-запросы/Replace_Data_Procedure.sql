CREATE PROCEDURE ReplaceValue
    @first_name_add nvarchar(50),
	@second_name_add nvarchar(50),
	@last_name_add nvarchar(50),
	@name_dep_add nvarchar(50),
	@name_post_add nvarchar(50),
	@first_name_del nvarchar(50),
	@second_name_del nvarchar(50),
	@last_name_del nvarchar(50),
	@name_dep_del nvarchar(50),
	@name_post_del nvarchar(50)
AS
BEGIN
DECLARE @id_post_del int;
DECLARE @id_dep_del int;
DECLARE @id_post_add int;
DECLARE @id_dep_add int;

IF NOT EXISTS(SELECT name from Departments WHERE Departments.name = @name_dep_del) 
RETURN -1
IF NOT EXISTS(SELECT name from Posts WHERE Posts.name = @name_post_del)
RETURN -1

SELECT @id_dep_del=Departments.id, @id_post_del = Posts.id  
from Departments, Posts 
WHERE Departments.name = @name_dep_del AND Posts.name = @name_post_del

IF NOT EXISTS(
  SELECT first_name, second_name, last_name, id_post, id_dep 
  FROM People 
  WHERE @first_name_del = People.first_name and @second_name_del = People.second_name and @last_name_del = People.last_name and @id_post_del = People.id_post and @id_dep_del = People.id_dep
)
  RETURN -1

IF NOT EXISTS(SELECT name from Departments WHERE Departments.name = @name_dep_add)
INSERT INTO Departments(name) VALUES
(@name_dep_add)

IF NOT EXISTS(SELECT name from Posts WHERE Posts.name = @name_post_add)
INSERT INTO Posts(name) VALUES
(@name_post_add)

SELECT @id_dep_add=Departments.id, @id_post_add = Posts.id  
from Departments, Posts 
WHERE Departments.name = @name_dep_add AND Posts.name = @name_post_add


UPDATE People SET first_name = @first_name_add, second_name = @second_name_add, last_name = @last_name_add, id_post = @id_post_add, id_dep = @id_dep_add
WHERE @first_name_del = People.first_name and @second_name_del = People.second_name and @last_name_del = People.last_name and @id_post_del = People.id_post and @id_dep_del = People.id_dep
RETURN 0
END
GO