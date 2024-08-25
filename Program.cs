using System;
using System.Data.SQLite;

class Program
{
    static void Main(string[] args)
    {
        // Connexion à la base de données en mémoire
        using (var connection = new SQLiteConnection("Data Source=:memory:"))
        {
            connection.Open();

            // Création de la table des allocations avec la colonne Deces
            string createTableQuery = @"
                CREATE TABLE Allocations (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ExConjoint TEXT,
                    Enfants TEXT,
                    Montant INTEGER,
                    Periode TEXT,
                    Deces BOOLEAN
                )";
            using (var command = new SQLiteCommand(createTableQuery, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Table créée avec succès!");
            }

            // Insertion des données avec plusieurs exemples, incluant le cas de décès
            string insertDataQuery = @"
                INSERT INTO Allocations (ExConjoint, Enfants, Montant, Periode, Deces) VALUES
                ('A', 'Maroua et Jihane', 1800, 'Juillet, Août et Septembre 2024', 0),
                ('C', 'Mohamed Yassine', 900, 'Juillet, Août et Septembre 2024', 0),
                ('B', 'Meryem', 900, 'Juillet, Août et Septembre 2024', 0),
                ('D', 'Mohamed Adam', 900, 'Juillet, Août et Septembre 2024', 1),  -- Décédé
                ('E', 'Khadija et Ikram', 1800, 'Juillet, Août et Septembre 2024', 0),
                ('S', 'Rawane', 1800, 'Avril, Mai et Juin 2024', 0),
                ('B', 'Ibtyhal', 900, 'Avril, Mai et Juin 2024', 0),
                ('C', 'Douae', 900, 'Avril, Mai et Juin 2024', 0),
                ('D', 'Nour El Imane ', 900, 'Avril, Mai et Juin 2024', 1),  -- Décédé
                ('E', 'Imad', 1800, 'Avril, Mai et Juin 2024', 0),
                ('F', 'Hafsa et Abderrahmane', 900, 'Avril, Mai et Juin 2024', 0),
                ('G', 'Akram', 900, 'Avril, Mai et Juin 2024', 0),
                ('K', 'Arij', 1800, 'Avril, Mai et Juin 2024', 0),
                ('L', 'Yahya', 900, 'Avril, Mai et Juin 2024', 0),
                ('F', 'Sahar', 900, 'Avril, Mai et Juin 2024', 1)";
            using (var command = new SQLiteCommand(insertDataQuery, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Données insérées avec succès!");
            }

            // Extraction des données pour vérification
            string selectDataQuery = "SELECT * FROM Allocations";
            using (var command = new SQLiteCommand(selectDataQuery, connection))
            using (var reader = command.ExecuteReader())
            {
                int count = 1;
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string exConjoint = reader.GetString(1);
                    string enfants = reader.GetString(2);
                    int montant = reader.GetInt32(3);
                    string periode = reader.GetString(4);
                    bool deces = reader.GetBoolean(5);

                    // Déterminer le montant attendu en fonction de l'ex-conjoint
                    int expectedMontant = 0;
                    if (exConjoint == "A" || exConjoint == "E")
                    {
                        expectedMontant = 1800;
                    }
                    else if (exConjoint == "C" || exConjoint == "D")
                    {
                        expectedMontant = 900;
                    }

                    // Déterminer la période correcte
                    bool isCorrectPeriod = PeriodeCorrecte(periode);

                    // Vérifier le montant pour chaque période de 3 mois
                    if (deces)
                    {
                        Console.WriteLine($"L'ex-conjoint {exConjoint} est décédé. Montant invalide pour l'enregistrement {count}.");
                    }
                    else
                    {
                        if (montant == expectedMontant && isCorrectPeriod)
                        {
                            Console.WriteLine($"Montant correct pour enregistrement {count}");
                        }
                        else
                        {
                            Console.WriteLine($"Montant incorrect pour enregistrement {count}");
                        }
                    }
                    count++;
                }
            }
        }
    }

    // Fonction pour vérifier si la période est correcte (chaque 3 mois)
    static bool PeriodeCorrecte(string periode)
    {
        string[] periods = { "Janvier, Février et Mars", "Avril, Mai et Juin", "Juillet, Août et Septembre", "Octobre, Novembre et Décembre" };
        foreach (var p in periods)
        {
            if (periode.Contains(p))
            {
                return true;
            }
        }
        return false;
    }
}
