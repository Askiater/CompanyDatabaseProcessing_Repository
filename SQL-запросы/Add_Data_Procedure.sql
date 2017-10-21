CREATE PROCEDURE AddValue
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
INSERT INTO Departments(name) VALUES
(@name_dep)

IF NOT EXISTS(SELECT name from Posts WHERE Posts.name = @name_post)
INSERT INTO Posts(name) VALUES
(@name_post)

SELECT @id_dep=Departments.id, @id_post = Posts.id  
from Departments, Posts 
WHERE Departments.name = @name_dep AND Posts.name = @name_post

INSERT INTO People(first_name,second_name,last_name,id_dep,id_post) VAlUES
(@first_name, @second_name, @last_name,@id_dep,@id_post)
END
GO

