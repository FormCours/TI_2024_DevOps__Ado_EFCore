CREATE TABLE [dbo].[Rel__Star_Planet]
(
  [StarId] INT NOT NULL,
  [PlanetId] INT NOT NULL,

  CONSTRAINT [PK_Rel__Star_Planet] PRIMARY KEY([StarId], [PlanetId]),
  CONSTRAINT [FK_Rel__Star_Planet__Star]
    FOREIGN KEY([StarId])
    REFERENCES [Star]([Id]),
  CONSTRAINT [FK_Rel__Star_Planet__Planet]
    FOREIGN KEY([PlanetId])
    REFERENCES [Planet]([Id]),
)
