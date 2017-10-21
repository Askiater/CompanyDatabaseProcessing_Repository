DROP TABLE People
GO

DROP TABLE Posts
GO

DROP TABLE Departments
GO

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