CREATE PROCEDURE GetAllValues
AS
BEGIN
SELECT first_name ,second_name ,last_name,Departments.name AS DepName,Posts.name AS PostName
FROM People 
JOIN Departments ON People.id_dep = Departments.id
JOIN posts ON People.id_post = Posts.id
END
GO
