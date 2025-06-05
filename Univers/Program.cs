using Microsoft.Data.SqlClient;
using System.Data.Common;
using Univers.Common.Models;
using Univers.Common.Repositories;
using Univers.DAL.ADO.Repositories;

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
    */

    Star s1 = starRepository.GetById(11);
    Console.WriteLine(s1);


    // TODO Tester la DB via EFCore
}