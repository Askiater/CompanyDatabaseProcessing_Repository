CREATE TABLE People(
	[id] [int] IDENTITY(1,1) NOT NULL,
      [first_name] [varchar](100) NOT NULL,
	[second_name] [varchar](100) NOT NULL,
	[last_name] [varchar](100) NOT NULL,
      [id_dep] [int] NOT NULL,
	[id_post] [int] NOT NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([id])
  ) ON [PRIMARY]
GO

CREATE TABLE Posts(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_posts] PRIMARY KEY CLUSTERED ([id])
  ) ON [PRIMARY]
GO

CREATE TABLE Departments(
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
CONSTRAINT [PK_deps] PRIMARY KEY CLUSTERED ([id])
  ) ON [PRIMARY]
GO

ALTER TABLE People
WITH CHECK ADD CONSTRAINT FK1_PERSON_DEPS FOREIGN KEY (id_dep) REFERENCES Departments (id) 
ON UPDATE CASCADE 
ON DELETE CASCADE
GO 

ALTER TABLE People
WITH CHECK ADD CONSTRAINT FK2_PERSON_POSTS FOREIGN KEY (id_post) REFERENCES Posts (id) 
ON UPDATE CASCADE 
ON DELETE CASCADE
GO 

INSERT INTO Departments (name) VAlUES
('Management department'),('Programming department'),('Technical department'),('Economic department'),('Accounting department')
GO

INSERT INTO Posts (name) VAlUES
('General Director'),('Assistant to General Director'),('General Programmer'),('Programmer'),('General Technician'),('Technician'),('General Economist'),('Economist'),('General Accountant'),('Accountant')
GO

INSERT INTO People(first_name,second_name,last_name,id_dep,id_post) VAlUES
('Alice', 'Zdunova', 'Urievna','1','1'),
('Oleg', 'Zhukov', 'Vladislavovish','1','1'),
('Mila', 'Rogacheva', 'Ivanovna','1','2'),
('Sergey', 'Kusmin', 'Alexandrovich','2','3'),
('Valentin', 'Akaki', 'Akakievich','2','4'),
('Makar', 'Zhukov', 'Vladislavovich','2','4'),
('Alexey', 'Gogol', 'Vlastislavovich','3','5'),
('Maksim', 'Novoselov', 'Rastislavovich','3','6'),
('Maria', 'Acsonovs', 'Ilizarovns','3','6'),
('Arentiva', 'Kirasinovns', 'Artomovna','3','6'),
('Danil', 'Nikiforov', 'Dmitrievich','4','7'),
('Vlasov', 'Nikulin', 'Alexandrovich','4','8'),
('Danila', 'Nikulin', 'Alexandrovich','4','8'),
('Kinil', 'Gashmut', 'Ifnipovich','5','9'),
('Rostislav', 'Ivanov', 'Ivanovich','5','10'),
('Usmanov', 'Kustin', 'Lefanovich','5','10'),
('Kirill', 'Arcadey', 'Dimidovich','5','10'),
('Ruslan', 'Usachev', 'Dmitrievich','5','10'),
('Kirill', 'Michalev', 'Alexeevich','5','10'),
('Boris', 'Levin', 'Andreevich','5','10')
GO

CREATE PROCEDURE GetAllValues
AS
BEGIN
SELECT first_name ,second_name ,last_name,Departments.name AS DepName,Posts.name AS PostName
FROM People 
JOIN Departments ON People.id_dep = Departments.id
JOIN posts ON People.id_post = Posts.id
END
GO

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

CREATE PROCEDURE FindValue
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
SELECT first_name ,second_name ,last_name,Departments.name AS DepName,Posts.name AS PostName
FROM People 
JOIN Departments ON People.id_dep = Departments.id
JOIN posts ON People.id_post = Posts.id
WHERE @first_name = People.first_name and @second_name = People.second_name and @last_name = People.last_name and @id_post = People.id_post and @id_dep = People.id_dep
END
GO

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