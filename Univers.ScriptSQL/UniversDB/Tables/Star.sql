CREATE TABLE [dbo].[Star]
(
  [Id] INT NOT NULL IDENTITY,
  [Name] NVARCHAR(50) NOT NULL,
  [IsDeath] BIT DEFAULT 0,
  [GalaxyId] INT NOT NULL,
  
  CONSTRAINT [PK_Star] PRIMARY KEY([Id]),
  CONSTRAINT [FK_Star__Galaxy]
    FOREIGN KEY([GalaxyId])
    REFERENCES [Galaxy]([Id]),
)