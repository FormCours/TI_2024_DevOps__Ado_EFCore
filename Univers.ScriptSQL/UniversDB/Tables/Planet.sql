CREATE TABLE [dbo].[Planet]
(
  [Id] INT NOT NULL IDENTITY, 
  [Name] NVARCHAR(50) NOT NULL,
  [Satelite] INT DEFAULT 0,
  [Gravity] DECIMAL(7,4) NOT NULL,
  
  CONSTRAINT [PK_Planet] PRIMARY KEY([Id])
)


