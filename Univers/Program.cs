using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Univers.Common.Models;
using Univers.Common.Repositories;
using Univers.DAL.ADO.Repositories;
using Univers.DAL.EFCore;

Console.WriteLine("Demo univers !!!");

string connectionString = "Data Source=Form1806\\DEVOPSDB;Integrated Security=True;Trust Server Certificate=True;Database=UniversDB";

using (DbConnection connection = new SqlConnection(connectionString))
{
    // Connexion vers la DB
    connection.Open();

    // Tester la DB via ADO
    IGalaxyRepository galaxyRepository = new GalaxyRepository(connection);
    IStarRepository starRepository = new StarRepository(connection);
    IPlanetRepository planetRepository = new PlanetRepository(connection);

    /*
    Console.WriteLine("Création d'une galaxie : ");
    Galaxy g1 = galaxyRepository.Create(new Galaxy()
    {
        Name = "Voie Lactée",
        Description = "C'est chez nous !",
    });

    Console.WriteLine("Liste des galaxies : ");
    IEnumerable<Galaxy> galaxies = galaxyRepository.GetAll();
    foreach(Galaxy g in galaxies)
    {
        Console.WriteLine($" - [{g.Id}] {g.Name}");
    }

    //Console.WriteLine("Ajouter une étoile");
    Star sun = starRepository.Create(new Star()
    {
        Name = "Soleil",
        IsDeath = false,
        GalaxyId = g1.Id,
    });
    Console.WriteLine($" - Étoile {sun.Name} créé avec l'id {sun.Id}!");


    Console.WriteLine("Ajouter des planetes");
    Planet p1 = planetRepository.Create(new Planet()
    {
        Name = "Terre",
        Satelite = 1,
        Gravity = 9.81
    });
    Planet p2 = planetRepository.Create(new Planet()
    {
        Name = "Mercure",
        Satelite = 0,
        Gravity = 3.7
    });
    Planet p3 = planetRepository.Create(new Planet()
    {
        Name = "Pluton",
        Satelite = 5,
        Gravity = 0.625
    });
    Planet p4 = planetRepository.Create(new Planet()
    {
        Name = "Saturne",
        Satelite = 274,
        Gravity = 10.44
    });

    Console.WriteLine("Ajouter le lien entre les planetes et son étoile");
    starRepository.AddPlanet(sun.Id, p1.Id);
    starRepository.AddPlanets(sun.Id, [p2.Id, p3.Id, p4.Id]);
    

    Star s1 = starRepository.GetById(11);
    Console.WriteLine(s1);
    */
}


// TODO Tester la DB via EFCore
using (UniversDataContext context = new UniversDataContext(connectionString))
{
    //Console.WriteLine("Galaxie qui commence par \"Voie\"");
    //foreach(Galaxy g in context.Galaxy.Where(g => g.Name.StartsWith("Voie")))
    //{
    //    Console.WriteLine($" - [{g.Id}] {g.Name}");
    //}
    //Console.WriteLine();
    ///*
    //    context.Galaxy.Where(g => g.Name.StartsWith("Voie")
    //        ↓
    //    SELECT [g].[Id], [g].[Description], [g].[Name]
    //    FROM [Galaxy] AS [g]
    //    WHERE [g].[Name] LIKE N'Voie%'
    //    go
    //*/

    //Console.WriteLine("Liste des planetes");
    //foreach (Planet p in context.Planet)
    //{
    //    Console.WriteLine($" - [{p.Id}] {p.Name}");
    //}
    //Console.WriteLine();

    Console.WriteLine("Détails de l'etoile \"Soleil\"");
    Star? sun = context.Star.Where(s => s.Name == "Soleil")
                            .Include(s => s.Galaxy)
                            .Include(s => s.Planets)
                            .SingleOrDefault();
    if(sun is not null)
    {
        Console.WriteLine($"Nom : {sun.Name}");
        Console.WriteLine($"Est mouru : {sun.IsDeath}");
        Console.WriteLine($"Galaxie : {sun.Galaxy!.Name}");
        Console.WriteLine($"Nombre de planet : {sun.Planets!.Count()}");
    }
    else
    {
        Console.WriteLine("Le soleil se trouve dans une autre galaxie");
    }
    <
    /*
        SELECT [t].[Id], [t].[GalaxyId], [t].[IsDeath], [t].[Name], [t].[Id0], [t].[Description], [t].[Name0], [t0].[StarId], [t0].[PlanetId], [t0].[Id], [t0].[Gravity], [t0].[Name], [t0].[Satelite]
        FROM (
            SELECT TOP(2) [s].[Id], [s].[GalaxyId], [s].[IsDeath], [s].[Name], [g].[Id] AS [Id0], [g].[Description], [g].[Name] AS [Name0]
            FROM [Star] AS [s]
            INNER JOIN [Galaxy] AS [g] ON [s].[GalaxyId] = [g].[Id]
            WHERE [s].[Name] = N'Soleil'
        ) AS [t]
        LEFT JOIN (
            SELECT [r].[StarId], [r].[PlanetId], [p].[Id], [p].[Gravity], [p].[Name], [p].[Satelite]
            FROM [Rel__Star_Planet] AS [r]
            INNER JOIN [Planet] AS [p] ON [r].[PlanetId] = [p].[Id]
        ) AS [t0] ON [t].[Id] = [t0].[StarId]
        ORDER BY [t].[Id], [t].[Id0], [t0].[StarId], [t0].[PlanetId]
    */
}