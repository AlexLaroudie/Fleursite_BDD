// See https://aka.ms/new-console-template for more information
using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Resources;

internal class program
{
    static void Main(string[] args)
    {
        string connectionString = "SERVER=localhost;PORT=3306;DATABASE=Fleuriste;UID=root;PASSWORD= 5rueMayet@;";
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        //EspaceGestionnaire(connection);
      
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("                  Bienvenue chez Michel Bellefleur !                  ");
        Console.Write("\n                         APPUYEZ SUR ENTREE                           ");
        Console.ResetColor();
        while (Console.ReadKey().Key != ConsoleKey.Enter) { }
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        int continuation = 0;

        while (continuation == 0)
        {

            Console.WriteLine("                  INFORMATIONS DE CONNEXION                  ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n (1) Espace Gestionnaire");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n (2) Espace client");





            Console.ForegroundColor = ConsoleColor.White;

            int espace = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
            Console.Clear();
            switch (espace)
            {
                case 1:
                    EspaceGestionnaire(connection);
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("        INFORMATION DE CONNEXION        ");
                    Console.ResetColor();
                    Console.Write("\n        Première connexion ?");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(" Tapez 1        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n        Client existant ?");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" Tapez 2        ");
                    Console.ForegroundColor = ConsoleColor.White;
                    int choix = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
                    int choixcommande = 0;
                    int idClientConnexion = -1;
                    while (choix != 1 && choix != 2)
                    {
                        Console.WriteLine("Commande non compatible");
                        choix = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
                    }


                    Console.Clear();
                    MySqlCommand command = connection.CreateCommand();

                    MySqlDataReader reader;

                    switch (choix)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("                              BIENVENUE A VOUS                             ");
                            Console.ResetColor();

                            Console.WriteLine("Nous allons avoir besoin de quelques informations afin de créer votre compte");
                            Console.WriteLine("\nQuel est votre nom ?");
                            string nom = Console.ReadLine();
                            Console.WriteLine("Quel est votre prénom ?");
                            string prenom = Console.ReadLine();
                            Console.WriteLine("Quel est votre email ?");
                            string email = Console.ReadLine();
                            Console.WriteLine("Quel est votre numéro de téléphone ?");
                            int numeroTel = DemandeInt("erreur de syntaxe : veuillez entrer un int ", 20000000);
                            Console.WriteLine("Quelles sont vos coordonnees bancaires ?");
                            int coordonneeBancaire = DemandeInt("erreur de syntaxe : veuillez entrer un int", 20000000);
                            Console.WriteLine("Quelle est votre adresse ?");
                            string adresse = Console.ReadLine();
                            Console.WriteLine("Quelle est votre ville ?");
                            string ville = Console.ReadLine();
                            Console.WriteLine("Choississez un mot de passe");
                            string mdp = Console.ReadLine();
                            int nbAchat = 1;
                            string fidelite = "normal";

                            command.CommandText = "SELECT id_C FROM Client ORDER BY id_C DESC;";


                            reader = command.ExecuteReader();
                            int id_client;
                            reader.Read();
                            id_client = reader.GetInt32(0) + 1;
                            idClientConnexion = id_client;
                            reader.Close();



                            command.CommandText = "SELECT courriel FROM Client WHERE courriel = '" + email + "' ";
                            reader = command.ExecuteReader();
                            while (reader.HasRows)
                            {
                                reader.Close();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Cet email existe déjà. Entrez un nouvel email ");
                                Console.ForegroundColor = ConsoleColor.White;
                                email = Console.ReadLine();
                                command.CommandText = "SELECT courriel FROM Client WHERE courriel = '" + email + "' ";
                                reader = command.ExecuteReader();
                            }

                            reader.Close();
                            command.CommandText = "INSERT INTO Client VALUES ('" + id_client + "', '" + nom + "', '" + prenom + "', '" + numeroTel + "', '" + email + "', '" + mdp + "', '" + adresse + "', '" + ville + "', '" + coordonneeBancaire + "', '" + nbAchat + "', '" + fidelite + "')";
                            reader = command.ExecuteReader();
                            reader.Close();

                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("                              BRAVO, VOUS ETES MAINTENANT CONNECTES !                             ");
                            Console.ResetColor();
                            choixcommande = 1;
                            Console.WriteLine("                          APPUYEZ SUR ENTREE POUR EFFECTUER UNE COMMANDE                             ");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            Console.Clear();
                            break;

                        case 2:

                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("                              VEUILLEZ VOUS CONNECTER                             ");
                            Console.ResetColor();
                            Console.WriteLine("Quel est votre email ?");
                            string emailrentre = Console.ReadLine();
                            int test = 0;
                            while (test == 0)
                            {
                                command.CommandText = "SELECT courriel FROM Client WHERE courriel = '" + emailrentre + "' ";
                                reader = command.ExecuteReader();
                                if (!reader.HasRows)
                                {
                                    reader.Close();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Cet email n'existe pas. Veuillez vous reconnecter et créer un compte");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Quel est votre email ? ");
                                    emailrentre = Console.ReadLine();



                                }
                                else
                                {
                                    test = 1;
                                    reader.Close();
                                }
                            }


                            Console.WriteLine("\nQuel est votre mot de passe ?");
                            string mdprentre = Console.ReadLine();
                            test = 0;
                            while (test == 0)
                            {
                                command.CommandText = "SELECT mdp FROM Client WHERE mdp = '" + mdprentre + "' ";
                                reader = command.ExecuteReader();
                                if (!reader.HasRows)
                                {
                                    reader.Close();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Ce mot de passe n'est pas correct. Veuillez vous reconnecter");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Quel est votre mot de passe ? ");
                                    mdprentre = Console.ReadLine();


                                }
                                else
                                {
                                    test = 1;
                                    reader.Close();
                                }
                            }

                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("                                 BRAVO, VOUS ETES MAINTENANT CONNECTE !                                ");
                            Console.ResetColor();
                            Console.WriteLine("      APPUYEZ SUR ENTREE POUR EFFECTUER UNE COMMANDE OU REGARDER VOTRE HISTORIQUE DE COMMANDE                             ");
                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                            Console.Clear();


                            Console.Write("\n        Voulez-vous faire une commande ?");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" Tapez 1        ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\n        Voulez-vous regarder votre historique de commande ?");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" Tapez 2        ");
                            Console.ForegroundColor = ConsoleColor.White;
                            choixcommande = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
                            while (choixcommande != 1 && choixcommande != 2)
                            {
                                Console.WriteLine("Numéro non valide. Réessayez");
                                choixcommande = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                            }
                            command.CommandText = "SELECT id_C FROM Client WHERE courriel = '" + emailrentre + "' ";
                            reader = command.ExecuteReader();

                            Console.Clear();

                            if (reader.Read()) // si la requête a retourné au moins une ligne
                            {
                                idClientConnexion = reader.GetInt32(0); // récupération de la valeur de la première colonne (indice 0)
                            }
                            reader.Close();

                            break;
                    }

                    int choixcommandebouquet = 0;
                    int choixbouquet = 0;
                    string choixbouquetnom = "";
                    int moisActuel = DateTime.Now.Month;
                    int anactuel = DateTime.Now.Year;
                    switch (choixcommande)
                    {
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                            Console.ResetColor();
                            Console.Write("\n        Voulez-vous une commande standard ?");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(" Tapez 1        ");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("\n        Voulez-vous une commande personalisée ?");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(" Tapez 2        ");
                            Console.ForegroundColor = ConsoleColor.White;

                            choixcommandebouquet = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
                            while (choixcommandebouquet != 1 && choixcommandebouquet != 2)
                            {
                                Console.WriteLine("Numéro non valide. Réessayez");
                                choixcommande = DemandeInt("erreur de syntaxe : veuillez choisir entre 1 et 2 ", 2);
                            }

                            Console.Clear();
                            switch (choixcommandebouquet)
                            {
                                case 1:

                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                    Console.ResetColor();
                                    Console.WriteLine("\n Vous avez choisi de commander un bouquet standard");
                                    Console.WriteLine("\n Quel est le bouquet que vous voulez choisir ? \n - (1) Gros Merci \n - (2) L'Amoureux \n - (3) L'Exotique \n - (4) Vive La Mariée \n - (5) Maman");
                                    AffichageCataB();
                                    choixbouquet = DemandeInt("erreur de syntaxe : veuillez choisir un entier entre 1 et 5 ", 5);
                                    Console.Clear();
                                    int test = 0;
                                    while (test == 0)
                                    {
                                        switch (choixbouquet)
                                        {
                                            case 1:
                                                choixbouquetnom = "Gros Merci";
                                                test = 1;
                                                break;
                                            case 2:
                                                choixbouquetnom = "L Amoureux";
                                                if (moisActuel != 2)
                                                {
                                                    Console.WriteLine("Ce bouquet n'est malheureusement pas disponible à cette période de l'année. Veuillez en choisir un autre");
                                                    Console.WriteLine("\n Quel est le bouquet que vous voulez choisir ? \n - (1) Gros Merci \n - (2) L'Amoureux \n - (3) L'Exotique \n - (4) Vive La Mariée \n - (5) Maman");
                                                    choixbouquet = DemandeInt("erreur de syntaxe : veuillez choisir un entier entre 1 et 5 ", 5);
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    test = 1;
                                                }
                                                break;
                                            case 3:
                                                choixbouquetnom = "L Exotique";
                                                test = 1;
                                                break;
                                            case 4:
                                                choixbouquetnom = "Vive La Mariée";
                                                test = 1;
                                                break;
                                            case 5:
                                                choixbouquetnom = "Maman";
                                                if ((moisActuel != 5 || moisActuel != 6))
                                                {
                                                    Console.WriteLine("Ce bouquet n'est malheureusement pas disponible à cette période de l'année. Veuillez en choisir un autre");
                                                    Console.WriteLine("\n Quel est le bouquet que vous voulez choisir ? \n - (1) Gros Merci \n - (2) L'Amoureux \n - (3) L'Exotique \n - (4) Vive La Mariée \n - (5) Maman");
                                                    choixbouquet = DemandeInt("erreur de syntaxe : veuillez choisir un entier entre 1 et 5 ", 5);
                                                    Console.Clear();
                                                }
                                                else
                                                {
                                                    test = 1;
                                                }
                                                break;
                                        }
                                        MySqlDataReader readerrr;
                                        command.CommandText = "SELECT stock FROM BouquetStandart WHERE nom = '" + choixbouquetnom + "' ";
                                        readerrr = command.ExecuteReader();
                                        int stockBouquet;
                                        readerrr.Read();
                                        stockBouquet = readerrr.GetInt32(0);
                                        readerrr.Close();
                                        if (stockBouquet == 0)
                                        {
                                            Console.WriteLine("Nous n'avons malheureusement plus ce bouquet en stock. Veuillez sélectionner une nouveau bouquet");
                                            Console.WriteLine("\n Quel est le bouquet que vous voulez choisir ?\n - (1) Gros Merci \n - (2) L'Amoureux \n - (3) L'Exotique \n - (4) Vive La Mariée \n - (5) Maman");
                                            choixbouquet = DemandeInt("erreur de syntaxe : veuillez choisir un entier entre 1 et 5 ", 5);
                                            Console.Clear();
                                            test = 0;
                                        }
                                    }
                                    Console.WriteLine(" A quelle date voulez vous etre livré ?   (format  yyyy-MM-dd) : ");
                                    string date_liv = Console.ReadLine();
                                    string etat_commande = "CVIN";
                                    Console.Clear();
                                    //string date_liv = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd ");
                                    string date_commande = DateTime.Now.ToString("yyyy-MM-dd");
                                    Console.WriteLine("Voulez vous livrer votre bouquet à votre adresse enregistrée ?\n 1 pour oui \n 2 pour non ");
                                    int r = DemandeInt("erreur de syntaxe : veuillez choisir un entier entre 1 et 2 ", 2);
                                    Console.Clear();
                                    string adresse_liv = "";
                                    string ville = "";
                                    if (r == 1)
                                    {
                                        command.CommandText = "SELECT adresse_fact FROM Client where id_C = " + idClientConnexion + ";";


                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        adresse_liv = reader.GetString(0);
                                        reader.Close();
                                        command.CommandText = "SELECT ville FROM Client where id_C = " + idClientConnexion + ";";


                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        ville = reader.GetString(0);
                                        reader.Close();

                                    }
                                    else
                                    {
                                        Console.WriteLine("A quelle adresse voulez vous envoyer le bouquet ?");
                                        adresse_liv = Console.ReadLine();
                                        Console.WriteLine("Dans quelle ville ?");
                                        ville = Console.ReadLine();
                                        Console.Clear();
                                    }
                                    int? id_Perso = null;
                                    Console.WriteLine("Quel message voulez-vous associer à votre bouquet ?");
                                    string message = Console.ReadLine();
                                    Console.Clear();
                                    command.CommandText = "SELECT id_commande FROM Commande ORDER BY id_Commande DESC;";


                                    reader = command.ExecuteReader();
                                    int id_commande;
                                    reader.Read();
                                    id_commande = reader.GetInt32(0) + 1;
                                    reader.Close();
                                    command.CommandText = "INSERT into Commande (id_commande, nom_Bouquet, adresse_liv, message, ville, date_liv, date_commande, etat_commande, id_client) VALUES ('" + id_commande + "','" + choixbouquet + "','" + adresse_liv + "','" + message + "','" + ville + "', '" + date_liv + "','" + date_commande + "','" + etat_commande + "','" + idClientConnexion + "')";


                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    reader.Close();
                                    command.CommandText = "UPDATE client SET nb_achats = nb_achats+1 where id_C = " + idClientConnexion;
                                    reader = command.ExecuteReader();
                                    reader.Close();
                                    Console.WriteLine("Votre commande à bien été enregistrée, nous nous en occuperons dans les plus brefs délais");

                                    command.CommandText = "UPDATE BouquetStandart SET stock = stock-1 where nom = '" + choixbouquetnom + "';";
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    reader.Close();


                                    command.CommandText = "SELECT COUNT(*) FROM commande WHERE id_client = " + idClientConnexion + " AND MONTH(date_commande) = MONTH(NOW())";

                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    int statut = reader.GetInt32(0);
                                    reader.Close();

                                    if (statut >= 3)
                                    {
                                        command.CommandText = "UPDATE client SET fidelite = 'or' WHERE id_C =" + idClientConnexion;
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                    }
                                    else
                                    {
                                        if (statut >= 1)
                                        {
                                            command.CommandText = "UPDATE client SET fidelite = 'bronze' WHERE id_C =" + idClientConnexion;
                                            reader = command.ExecuteReader();
                                            reader.Read();
                                            reader.Close();
                                        }
                                        else
                                        {
                                            command.CommandText = "UPDATE client SET fidelite = 'normal' WHERE id_C =" + idClientConnexion;
                                            reader = command.ExecuteReader();
                                            reader.Read();
                                            reader.Close();
                                        }
                                    }
                                    command.CommandText = "SELECT fidelite FROM client WHERE id_C =" + idClientConnexion + ";";
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    string fidelite = reader.GetString(0);
                                    reader.Close();
                                    command.CommandText = "SELECT Prix FROM BouquetStandart WHERE nom ='" + choixbouquetnom + "';";
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    double prix = reader.GetDouble(0);
                                    reader.Close();
                                    if (fidelite == "or")
                                    {
                                        prix = prix * 0.85;
                                        Console.Write(" Votre statut est ");
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine(fidelite);
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        if (fidelite == "bronze")
                                        {
                                            prix = prix * 0.95;
                                            Console.Write(" Votre statut est ");
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine(fidelite);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.Write(" Votre statut est ");
                                            Console.ForegroundColor = ConsoleColor.Blue;
                                            Console.WriteLine(fidelite);
                                            Console.ResetColor();
                                        }
                                    }
                                    Console.Write("Le prix final a payer est de ");
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(prix);
                                    Console.ResetColor();
                                    Console.WriteLine(" euros.");




                                    command.CommandText = "UPDATE Tresorerie SET montant = montant + " + prix + " WHERE id_treso = '001'";
                                    reader = command.ExecuteReader();
                                    reader.Read();
                                    reader.Close();

                                    /*string PRix = Convert.ToString(prix);
                                    double numericValue = double.Parse(PRix.Replace(',', '.'));*/
                                    //verifier le stock du bouquet CREER STOCK BOUQUET STANDARD 
                                    break;

                                case 2:
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                    Console.ResetColor();
                                    Console.WriteLine("\n Vous avez choisi de commander un bouquet personalisé");
                                    Console.WriteLine("Quel est votre budget pour ce bouquet ?");
                                    double Prix_Client = DemandeDouble("Erreur veuillez entrer un double", 50000000);

                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                    Console.ResetColor();
                                    int fin = 0;
                                    double Prix_Final = 0.0;
                                    double prix_fleurs = 0.0;
                                    double prix_accessoires = 0.0;
                                    List<string> fleur_bouquet = new List<string>();
                                    List<string> accessoires_bouquet = new List<string>();
                                    int nb_fleurs_bouquet = 0;
                                    int nb_accessoires_bouquet = 0;
                                    while (fin == 0)                                                               // On s'occupe des fleurs
                                    {
                                        Console.WriteLine("Quelles fleurs voulez vous mettre dans votre bouquet ? \n - (1) Pivoines  6$/pièce\n - (2) Roses rouges  5$/pièce\n - (3) Roses blanches  5$/pièce\n - (4) Tulipes  4$/pièce\n - (5) Orchidées  8.50$/pièce\n - (6) Gerberas  5$/pièce\n - (7) Gingers  4$/pièce\n - (8) Gaieuls  1$/pièce\n - (9) Marguerites  2.25$/pièce");
                                        AffichageCata();
                                        string fleurs = "";
                                        int reponsess = DemandeInt("Erreur veuillez entrer un entier entre 1 et 9", 9);

                                        switch (reponsess)
                                        {
                                            case 1:
                                                fleurs = "Pivoines";
                                                break;
                                            case 2:
                                                fleurs = "Roses rouges";
                                                break;
                                            case 3:
                                                fleurs = "Roses blanches";
                                                break;
                                            case 4:
                                                fleurs = "Tulipes";
                                                break;
                                            case 5:
                                                fleurs = "Orchidées";
                                                break;
                                            case 6:
                                                fleurs = "Gerberas";
                                                break;
                                            case 7:
                                                fleurs = "Gingers";
                                                break;
                                            case 8:
                                                fleurs = "Gaieuls";
                                                break;
                                            case 9:
                                                fleurs = "Marguerites";
                                                break;
                                        }
                                        fleur_bouquet.Add(fleurs);
                                        Console.WriteLine("\nCombien en voulez vous ?");
                                        int nb_fleurs = DemandeInt("Veuillez entrer un entier", 100000000);

                                        Console.Clear();
                                        nb_fleurs_bouquet += nb_fleurs;
                                        command.CommandText = "SELECT Prix FROM Stock_Fleurs WHERE nom_fleurs = '" + fleurs + "' ";
                                        reader = command.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            prix_fleurs = reader.GetDouble(0);

                                            Prix_Final = Prix_Final + (prix_fleurs * nb_fleurs);
                                        }

                                        reader.Close();
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                        Console.ResetColor();
                                        Console.WriteLine("Le montant actuel de votre commande est : " + Prix_Final + " $");
                                        if (Prix_Final > Prix_Client)
                                        {
                                            Console.WriteLine("\nErreur ! Vous avez dépassé le budget entré précédement ! \n  Annulons la dernière manipulation et retournons au prix précédent");
                                            Prix_Final = Prix_Final - (prix_fleurs * nb_fleurs);
                                            Console.WriteLine("\nLe montant actuel de votre commande est : " + Prix_Final + " $");
                                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                            Console.Clear(); ;
                                        }
                                        Console.WriteLine("Souhaitez vous rajouter des fleurs à votre bouquet ? :  \n(1) OUI\n(2) NON");
                                        int reponse = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);

                                        Console.Clear();
                                        if (reponse == 1)
                                        {

                                        }
                                        else
                                        {
                                            fin = 1;
                                        }
                                        Console.Clear();
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                        Console.ResetColor();
                                    }
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                    Console.ResetColor();
                                    fin = 0;
                                    while (fin == 0)                                                                    // on s'occupe des accesoires
                                    {
                                        Console.WriteLine("Quels accessoires voulez-vous mettre dans votre bouquet ? \n - (1) ruban   2$/pièces\n - (2)  paillettes  2$/pièces\n - (3) lutin  10$/pièces");
                                        int reponses = DemandeInt("Veuillez entrer un entier entre 1 et 3", 3);
                                        string accessoires = "";
                                        switch (reponses)
                                        {
                                            case 1:
                                                accessoires = "ruban";
                                                break;
                                            case 2:
                                                accessoires = "paillettes";
                                                break;
                                            case 3:
                                                accessoires = "lutin";
                                                break;
                                        }
                                        accessoires_bouquet.Add(accessoires);
                                        Console.WriteLine("Combien en voulez vous ?");
                                        int nb_accessoires = DemandeInt("Veuillez entrer un entier ", 2000000);
                                        Console.Clear();
                                        nb_accessoires_bouquet += nb_accessoires;
                                        command.CommandText = "SELECT Prix FROM Stock_Accessoires WHERE nom_accessoires = '" + accessoires + "' ";
                                        reader = command.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            prix_accessoires = reader.GetDouble(0);

                                            Prix_Final = Prix_Final + (prix_accessoires * nb_accessoires);
                                        }

                                        reader.Close();
                                        Console.BackgroundColor = ConsoleColor.Blue;
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                        Console.ResetColor();
                                        Console.WriteLine("Le montant actuel de votre commande est : " + Prix_Final + " $");
                                        if (Prix_Final > Prix_Client)
                                        {
                                            Console.WriteLine("Erreur ! Vous avez dépassé le budget entré précédement ! \n  Annulons la dernière manipulation et retournons au prix précédent");
                                            Prix_Final = Prix_Final - (prix_accessoires * nb_accessoires);
                                            Console.WriteLine("Le montant actuel de votre commande est : " + Prix_Final + " $");
                                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                            Console.Clear();
                                        }
                                        Console.WriteLine("Souhaitez vous rajouter des accessoires à votre bouquet ?  \n(1) OUI\n(2) NON");
                                        int reponse = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                                        if (reponse == 1)
                                        {

                                        }
                                        else
                                        {
                                            fin = 1;
                                        }
                                    }
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Blue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("                              INTERFACE DE COMMANDE                             ");
                                    Console.ResetColor();
                                    Console.WriteLine(" Le montant final hors frais de port est de " + Prix_Final + " euros. \n Souhaitez vous valider votre commande ?   \n(1) OUI\n(2) NON");
                                    int validation = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                                    Console.Clear();
                                    if (validation == 2)
                                    {
                                        Console.WriteLine("Votre commande bien été annulée \n Merci Beaucoup pour votre visite en espérant vous revoir bientot !");
                                    }
                                    else
                                    {
                                        command.CommandText = "SELECT id_commande FROM Commande ORDER BY id_Commande DESC;";



                                        reader = command.ExecuteReader();
                                        int id_commandes;
                                        reader.Read();
                                        id_commandes = reader.GetInt32(0) + 1;
                                        reader.Close();
                                        command.CommandText = "SELECT id_BouquetPerso FROM BouquetPerso ORDER BY id_BouquetPerso DESC;";

                                        string etat_commande_ = "CVIN";
                                        Console.WriteLine(" A quelle date voulez vous etre livré ?   (format  yyyy-MM-dd) : ");
                                        string date_liv_ = Console.ReadLine();
                                        string date_commande_ = DateTime.Now.ToString("yyyy-MM-dd");
                                        Console.Clear();
                                        Console.WriteLine("Voulez vous livrer votre bouquet à votre adresse enregistrée ? \n1 pour oui \n2 pour non ");
                                        int re = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                                        string adresse_liv_ = "";
                                        string ville_ = "";
                                        Console.Clear();
                                        if (re == 1)
                                        {
                                            command.CommandText = "SELECT adresse_fact FROM Client where id_C = " + idClientConnexion + ";";


                                            reader = command.ExecuteReader();
                                            reader.Read();
                                            adresse_liv_ = reader.GetString(0);
                                            reader.Close();
                                            command.CommandText = "SELECT ville FROM Client where id_C = " + idClientConnexion + ";";


                                            reader = command.ExecuteReader();
                                            reader.Read();
                                            ville_ = reader.GetString(0);
                                            reader.Close();

                                        }
                                        else
                                        {
                                            Console.WriteLine("A quelle adresse voulez_vous envoyer le bouquet ? :");
                                            adresse_liv_ = Console.ReadLine();
                                            Console.WriteLine("Dans quelle ville ? :");
                                            ville_ = Console.ReadLine();
                                            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                            Console.Clear();
                                        }
                                        string description = "";
                                        string description_fleurs = "";
                                        for (int i = 0; i < accessoires_bouquet.Count(); i++)
                                        {
                                            description = description + accessoires_bouquet[i];
                                        }
                                        for (int i = 0; i < fleur_bouquet.Count(); i++)
                                        {
                                            description_fleurs = description_fleurs + fleur_bouquet[i];
                                        }
                                        command.CommandText = "SELECT id_BouquetPerso FROM BouquetPerso ORDER BY id_BouquetPerso DESC;";
                                        reader = command.ExecuteReader();
                                        int id_BouquetPerso;
                                        reader.Read();
                                        id_BouquetPerso = reader.GetInt32(0) + 1;
                                        reader.Close();
                                        Console.WriteLine("Quel message voulez_vous associer à votre bouquet ?");
                                        string message_ = Console.ReadLine();
                                        command.CommandText = "INSERT into BouquetPerso (id_BouquetPerso,prix_client,prix_final,fleurs,nb_fleurs,accessoires,nb_accessoires,id_client) VALUES ('" + id_BouquetPerso + "', '" + Prix_Client + "', '" + Prix_Final + "', '" + description_fleurs + "', '" + nb_fleurs_bouquet + "', '" + description + "', '" + nb_accessoires_bouquet + "', '" + idClientConnexion + "')";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        command.CommandText = "INSERT into Commande (id_commande, adresse_liv, message, ville, date_liv, date_commande, etat_commande, id_client, id_BouquetPerso) VALUES ('" + id_commandes + "','" + adresse_liv_ + "','" + message_ + "','" + ville_ + "', '" + date_liv_ + "','" + date_commande_ + "','" + etat_commande_ + "','" + idClientConnexion + "', '" + id_BouquetPerso + "')";


                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        command.CommandText = "UPDATE client SET nb_achats = nb_achats + 1 where id_C = " + idClientConnexion;
                                        reader = command.ExecuteReader();
                                        reader.Close();
                                        Console.WriteLine(" Votre commande à bien été enregistrée, nous nous en occupons dans les plus brefs delais");



                                        for (int i = 0; i < accessoires_bouquet.Count; i++)
                                        {
                                            command.CommandText = "UPDATE Stock_Accessoires SET nb_accessoires = nb_accessoires - 1 where nom_accessoires = '" + accessoires_bouquet[i] + "';";                           // update des stocks des accessoires
                                            reader = command.ExecuteReader();
                                            reader.Close();
                                        }
                                        for (int i = 0; i < fleur_bouquet.Count; i++)
                                        {
                                            command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock - 1 where nom_fleurs = '" + fleur_bouquet[i] + "';";                           // update des stocks des accessoires
                                            reader = command.ExecuteReader();
                                            reader.Close();
                                        }




                                        /*command.CommandText = "Select nb_achats from client where id_C =" + idClientConnexion;
                                        command.CommandText = "Select date_commande from commande where id_client =" + idClientConnexion;
                                        command.CommandText = "SELECT c.nb_achats, cmd.date_commande FROM client c INNER JOIN commande cmd ON c.id_C = cmd.id_client WHERE c.id_C = idClientConnexion;";*/
                                        command.CommandText = "SELECT COUNT(*) FROM commande WHERE id_client = " + idClientConnexion + " AND MONTH(date_commande) = MONTH(NOW())";

                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        int statute = reader.GetInt32(0);
                                        reader.Close();

                                        if (statute >= 3)
                                        {
                                            command.CommandText = "UPDATE client SET fidelite = 'or' WHERE id_C =" + idClientConnexion;
                                            reader = command.ExecuteReader();
                                            reader.Read();
                                            reader.Close();
                                        }
                                        else
                                        {
                                            if (statute >= 1)
                                            {
                                                command.CommandText = "UPDATE client SET fidelite = 'bronze' WHERE id_C =" + idClientConnexion;
                                                reader = command.ExecuteReader();
                                                reader.Read();
                                                reader.Close();
                                            }
                                            else
                                            {
                                                command.CommandText = "UPDATE client SET fidelite = 'normal' WHERE id_C =" + idClientConnexion;
                                                reader = command.ExecuteReader();
                                                reader.Read();
                                                reader.Close();
                                            }
                                        }
                                        command.CommandText = "SELECT fidelite FROM client WHERE id_C =" + idClientConnexion + ";";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        string fidelitee = reader.GetString(0);
                                        reader.Close();

                                        if (fidelitee == "or")
                                        {
                                            Prix_Final = Prix_Final * 0.85;
                                            Console.Write(" Votre statut est ");
                                            Console.ForegroundColor = ConsoleColor.Yellow;
                                            Console.WriteLine(fidelitee);
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            if (fidelitee == "bronze")
                                            {
                                                Prix_Final = Prix_Final * 0.95;
                                                Console.Write(" Votre statut est ");
                                                Console.ForegroundColor = ConsoleColor.Red;
                                                Console.WriteLine(fidelitee);
                                                Console.ResetColor();
                                            }
                                            else
                                            {
                                                Console.Write(" Votre statut est ");
                                                Console.ForegroundColor = ConsoleColor.Blue;
                                                Console.WriteLine(fidelitee);
                                                Console.ResetColor();
                                            }
                                        }
                                        Console.Write("Le prix final a payer est de ");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.Write(Prix_Final);
                                        Console.ResetColor();
                                        Console.WriteLine(" euros.");

                                        command.CommandText = command.CommandText = "UPDATE Tresorerie SET montant = montant + " + Prix_Final + " WHERE id_treso = '001'"; ;
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                    }


                                    break;

                            }
                            break;

                        case 2:



                            command.CommandText = "SELECT nom_Bouquet,adresse_liv,message ,ville , date_liv,etat_commande FROM Commande WHERE id_client = '" + idClientConnexion + "' ";
                            MySqlDataReader readerr;
                            Console.WriteLine("");
                            readerr = command.ExecuteReader();
                            while (readerr.Read())
                            {
                                string currentRowAsString = "";

                                for (int i = 0; i < readerr.FieldCount; i++)
                                {
                                    string valueAsString = readerr.GetValue(i).ToString();
                                    currentRowAsString += valueAsString + ",";
                                }

                                Console.WriteLine(currentRowAsString);
                            }
                            readerr.Close();

                            break;
                    }
                    break;
            }
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
            Console.WriteLine("Voulez vous effectuer d'autres actions ?  (1) OUI /  (2) NON");
            int reponseee = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
            if (reponseee == 2)
            {
                continuation = 1;
                Console.Clear();
            }
            else
            {
                Console.Clear();
            }


        }
        




        
        connection.Close();
        Console.ReadLine();
    }


    static void EspaceGestionnaire(MySqlConnection connection)
        {
        int continuationn = 0;
        while (continuationn == 0)
        {



            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                              BIENVENUE DANS L'ESPACE GESTIONNAIRE                             ");
            Console.ResetColor();
            Console.WriteLine(" \n(1) Voir tous les clients enregistrés \n(2) Voir toutes les commandes enregistrées \n(3) Consulter les statistiques enregistrées \n(4) Effacer les clients inactifs depuis plus de 1 an \n(5) Consulter l'état de livraison des commandes \n(6) Liste des nombres de clients par ville \n(7) Gestion des stocks \n(8) Consulter la trésorerie \n(9) Recherhcer les clients d'une ville");
            int réponse = DemandeInt("Veuillez entrer un entier entre 1 et 9", 9);
            MySqlCommand command = connection.CreateCommand();
            Console.Clear();
            MySqlDataReader reader;
            switch (réponse)
            {
                case 1:

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    command.CommandText = "SELECT nom,prenom,ville,nb_achats,fidelite FROM Client;";
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            currentRowAsString += valueAsString + "  ;  ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    break;
                case 2:


                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    command.CommandText = "SELECT id_commande, nom_bouquet,ville,date_commande,etat_commande FROM Commande;";
                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            currentRowAsString += valueAsString + ", ";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    break;
                case 3:
                    int test = 0;
                    while (test == 0)
                    {

                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                        Console.ResetColor();
                        Console.WriteLine("\nQuelles statistiques voulez-vous consulter ? : \n - (1) Nombre de ventes par an \n - (2) Nombre de ventes par mois \n - (3) Nombre de clients et ville avec le plus de clients \n - (4) Nombre de commandes \n - (5) Nombre de Vente par bouquets et bouquet le plus vendu ");
                        int st = DemandeInt("Veuillez entrer un entier entre 1 et 5", 5);
                        Console.Clear();
                        switch (st)
                        {
                            case 1:

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                                Console.ResetColor();
                                command.CommandText = "SELECT COUNT(nb_achats) FROM client,commande WHERE id_C = id_client AND Year(date_commande) > " + (DateTime.Now.Year - 1);
                                reader = command.ExecuteReader();
                                int nb_ventes_an;
                                reader.Read();
                                nb_ventes_an = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("\nIl y a eu " + nb_ventes_an + " ventes cette année ");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                Console.Clear();

                                break;
                            case 2:

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                                Console.ResetColor();
                                command.CommandText = "SELECT COUNT(nb_achats) FROM client,commande WHERE id_C = id_client AND Month(date_commande) > " + (DateTime.Now.Month - 1);
                                reader = command.ExecuteReader();
                                int nb_ventes_mois;
                                reader.Read();
                                nb_ventes_mois = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("\nIl y a eu " + nb_ventes_mois + " ventes ce mois ci ");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                Console.Clear();

                                break;
                            case 3:

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                                Console.ResetColor();
                                command.CommandText = "SELECT COUNT(id_C) FROM client";
                                reader = command.ExecuteReader();
                                int nb_clients;
                                reader.Read();
                                nb_clients = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("\nIl y a eu " + nb_clients + " clients ");







                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                Console.Clear();

                                break;
                            case 4:

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                                Console.ResetColor();
                                command.CommandText = "SELECT COUNT(id_commande) FROM commande ";
                                reader = command.ExecuteReader();
                                int nb_commande;
                                reader.Read();
                                nb_commande = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("\nIl y a eu " + nb_commande + " commandes ");
                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                Console.Clear();

                                break;
                            case 5:

                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                                Console.ResetColor();
                                command.CommandText = "SELECT COUNT(nom) FROM BouquetStandart WHERE nom =  'Gros Merci'";
                                reader = command.ExecuteReader();
                                int nb_Gros_Merci;
                                reader.Read();
                                nb_Gros_Merci = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("\nLe nombre de Gros Merci vendu est : " + nb_Gros_Merci);


                                command.CommandText = "SELECT COUNT(nom) FROM BouquetStandart WHERE nom = 'L Exotique'";
                                reader = command.ExecuteReader();
                                int nb_LExotique;
                                reader.Read();
                                nb_LExotique = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("Le nombre de L'Exotique vendu est : " + nb_LExotique);


                                command.CommandText = "SELECT COUNT(nom) FROM BouquetStandart WHERE nom =  'L Amoureux'";
                                reader = command.ExecuteReader();
                                int nb_LAmoureux;
                                reader.Read();
                                nb_LAmoureux = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("Le nombre de L'Amoureux vendu est : " + nb_LAmoureux);

                                command.CommandText = "SELECT COUNT(nom) FROM BouquetStandart WHERE nom = 'Maman'";
                                reader = command.ExecuteReader();
                                int nb_Maman;
                                reader.Read();
                                nb_Maman = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("Le nombre de Maman vendu est : " + nb_Maman);


                                command.CommandText = "SELECT COUNT(nom) FROM BouquetStandart WHERE nom = 'Vive la mariée'";
                                reader = command.ExecuteReader();
                                int nb_ViveLaMariee;
                                reader.Read();
                                nb_ViveLaMariee = reader.GetInt32(0);
                                reader.Close();
                                Console.WriteLine("Le nombre de Vive La Mariée vendu est : " + nb_ViveLaMariee);

                                int max = new[] { nb_Gros_Merci, nb_ViveLaMariee, nb_Maman, nb_LAmoureux, nb_LExotique }.Max();
                                string nomMax = "valeur" + (new[] { nb_Gros_Merci, nb_ViveLaMariee, nb_Maman, nb_LAmoureux, nb_LExotique }.ToList().IndexOf(max) + 1);

                                Console.WriteLine("La plus grande valeur est " + max + "provenant de " + nomMax);

                                while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                                Console.Clear();
                                break;

                        }
                    }
                    break;
                case 4:

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    command.CommandText = "SELECT nom, prenom,id_C FROM client, commande WHERE (commande.date_commande < DATE_SUB(NOW(), INTERVAL 1 YEAR)) AND commande.id_client = client.id_C;";
                    reader = command.ExecuteReader();
                    int idclientasup = 0;
                    Console.WriteLine("\nLes clients suivants vont être supprimés pour inactivité :");
                    while (reader.Read())
                    {
                        string nom = reader.GetString(0);
                        string prenom = reader.GetString(1);
                        idclientasup = reader.GetInt32(2);
                        Console.WriteLine(" - " + nom + " " + prenom);
                    }
                    reader.Close();
                    command.CommandText = "DELETE FROM commande WHERE id_client =" + idclientasup + ";";
                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();

                    command.CommandText = "DELETE FROM client WHERE id_C =" + idclientasup + ";";
                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Console.Clear();


                    break;
                case 5:

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    string datenow = DateTime.Now.ToString("yyyy-MM-dd");
                    command.CommandText = "UPDATE commande SET etat_commande = 'CAL' WHERE etat_commande <> 'CAL' AND DATEDIFF('" + datenow + "', date_commande) >= 2";

                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    command.CommandText = "UPDATE commande SET etat_commande = 'CL' WHERE etat_commande <> 'CL' AND DATEDIFF('" + datenow + "', date_commande) >= 7";
                    reader = command.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    Console.WriteLine("Voici toutes les commandes en vérification d'inventaire :\n");
                    command.CommandText = "SELECT * FROM Commande WHERE etat_commande = 'CVIN';";


                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            currentRowAsString += valueAsString + ",";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    Console.WriteLine("");
                    reader.Close();
                    Console.WriteLine("Voici toutes les commandes à livrer :\n");
                    command.CommandText = "SELECT * FROM Commande WHERE etat_commande = 'CAL';";


                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            currentRowAsString += valueAsString + ",";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    Console.WriteLine("");
                    reader.Close();
                    Console.WriteLine("Voici toutes les commandes livrées :\n");
                    command.CommandText = "SELECT * FROM Commande WHERE etat_commande = 'CL';";


                    reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        string currentRowAsString = "";
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string valueAsString = reader.GetValue(i).ToString();
                            currentRowAsString += valueAsString + ",";
                        }
                        Console.WriteLine(currentRowAsString);
                    }
                    Console.WriteLine("");
                    reader.Close();

                    break;

                case 6:

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    command.CommandText = "SELECT c1.ville, COUNT(c2.id_C) AS nb_clients " +
                          "FROM client c1 " +
                          "INNER JOIN client c2 ON c1.ville = c2.ville " +
                          "GROUP BY c1.ville";
                    reader = command.ExecuteReader();
                    Console.WriteLine("\nVoici la liste des villes où on a au moins 1 client et le nombre de clients associé :");
                    while (reader.Read())
                    {
                        string ville = reader.GetString("ville");
                        int nbClients = reader.GetInt32("nb_clients");
                        Console.WriteLine("Ville : " + ville + " - Nombre de clients : " + nbClients);
                    }
                    reader.Close();


                    // **Auto jointure** compte la ville avec le plus de client 

                    command.CommandText = "SELECT count(*) FROM client client1, client client2 WHERE client1.ville=client2.ville AND client1.nom<client2.nom;";
                    reader = command.ExecuteReader();
                    int nb_clientmaxville;
                    reader.Read();
                    nb_clientmaxville = reader.GetInt32(0);
                    reader.Close();
                    command.CommandText = "SELECT distinct client1.ville FROM client client1, client client2 WHERE client1.ville=client2.ville AND client1.nom<client2.nom;";
                    reader = command.ExecuteReader();
                    string villemaxclient;
                    reader.Read();
                    villemaxclient = reader.GetValue(0).ToString();
                    reader.Close();
                    Console.WriteLine("\n \n la ville avec les plus de client est : " + villemaxclient + " avec " + nb_clientmaxville + " clients.");


                    break;
                case 7:

                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                              VOUS ETES DANS L'ESPACE GESTIONNAIRE                             ");
                    Console.ResetColor();
                    command.CommandText = "SELECT nom_fleurs AS Nom, nb_fleurs_stock AS Stock FROM Stock_Fleurs " +
                               "UNION " +                                                                                   // demander aux collegues si on gardde union on si on separe les fleurs des accesoires
                               "SELECT nom_accessoires AS Nom, nb_accessoires AS Stock FROM Stock_Accessoires " +
                               "ORDER BY Nom";
                    reader = command.ExecuteReader();



                    // Affichage des résultats
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}: {1}", reader.GetString("Nom"), reader.GetInt32("Stock"));
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Console.Clear();

                    Console.WriteLine("Que souhaitez vous faire :\n - (1) Réaprovisionner les marchandises\n - (2) Quitter cette page");
                    int approvisionnement = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                    int nb = 0;
                    Console.Clear();
                    int finito = 0;
                    while (finito == 0)
                    {
                        switch (approvisionnement)
                        {

                            case 1:
                                Console.WriteLine("Quel produit voulez vous réaprovisionner ? :\n\n - (1) Pivoines  \n - (2) Roses rouges  \n - (3) Roses blanches  \n - (4) Tulipes \n - (5) Orchidées  \n - (6) Gerberas  \n - (7) Gingers \n - (8) Gaieuls  \n - (9) Marguerites \n - (10) ruban  \n - (11)  paillettes \n - (12) lutin  ");
                                int reapro = DemandeInt("Veuillez entrer un entier entre 1 et 12", 12);
                                double fournisseur = 0;
                                Console.Clear();
                                switch (reapro)
                                {
                                    case 1:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Pivoines'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Pivoines'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 2:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Roses rouges'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();


                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Roses rouges'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 3:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Roses blanches'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Roses blanches'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();



                                        break;
                                    case 4:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Tulipes'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Tulipes'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 5:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Orchidées'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Orchidées'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 6:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Gerberas'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Gerberas'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 7:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Gingers'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Gingers'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 8:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Gaieuls'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Gaieuls'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 9:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Fleurs SET nb_fleurs_stock = nb_fleurs_stock +" + nb + " WHERE nom_fleurs = 'Marguerites'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Fleurs where nom_fleurs = 'Marguerites'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 10:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Accessoires SET nb_accessoires = nb_accessoires +" + nb + " WHERE nom_accessoires = 'ruban'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Accessoires where nom_accessoires = 'ruban'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 11:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Accessoires SET nb_accessoires = nb_accessoires +" + nb + " WHERE nom_accessoires = 'paillettes'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Accessoires where nom_accessoires = 'paillettes'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;
                                    case 12:
                                        Console.WriteLine("Combien voulez vous en racheter ?");
                                        nb = DemandeInt("Veuillez entrer un entier ", 20000000);
                                        reader.Close();
                                        command.CommandText = "UPDATE Stock_Accessoires SET nb_accessoires = nb_accessoires +" + nb + " WHERE nom_accessoires = 'lutin'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();

                                        command.CommandText = "Select prix FROM Stock_Accessoires where nom_accessoires = 'lutin'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        fournisseur = reader.GetDouble(0);
                                        fournisseur = fournisseur * nb;
                                        reader.Close();


                                        command.CommandText = "UPDATE Tresorerie SET montant = montant -" + fournisseur + " WHERE id_treso = '001'";
                                        reader = command.ExecuteReader();
                                        reader.Read();
                                        reader.Close();
                                        break;

                                }

                                break;

                        }
                        Console.Clear();
                        Console.WriteLine("Souhaitez vous commander autre chose ?\n - (1)  Oui\n - (2)  Non");
                        int lareponse = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
                        if (lareponse != 1)
                        {
                            finito = 1;
                        }
                        Console.Clear();
                    }
                    Console.WriteLine("Les nouveaux stocks sont donc : \n");
                    command.CommandText = "SELECT nom_fleurs AS Nom, nb_fleurs_stock AS Stock FROM Stock_Fleurs " +
                               "UNION " +                                                                                   // demander aux collegues si on gardde union on si on separe les fleurs des accesoires
                               "SELECT nom_accessoires AS Nom, nb_accessoires AS Stock FROM Stock_Accessoires " +
                               "ORDER BY Nom";
                    reader = command.ExecuteReader();



                    // Affichage des résultats
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}: {1}", reader.GetString("Nom"), reader.GetInt32("Stock"));
                    }
                    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
                    Console.Clear();
                    reader.Close();

                    break;

                case 8:
                    command.CommandText = "Select * From Tresorerie";
                    reader = command.ExecuteReader();
                    reader.Read();
                    double treso = reader.GetDouble(0);
                    Console.Write("Le montant total présent dans la trésorerie est ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(treso);
                    Console.ResetColor();
                    Console.WriteLine(" euros");

                    break;

                case 9:
                    // **procédure** : client d'une ville en particulier

                     Console.WriteLine("Ecrivez une ville et nous vous donnerons les nom et prénom des clients de cette ville");
                     string villec = Console.ReadLine();

                     command.CommandText = "CALL ville_client('" + villec + "');";
                     reader = command.ExecuteReader();

                     while (reader.Read())
                     {
                         string currentRowAsString = "";
                         for (int i = 0; i < reader.FieldCount; i++)
                         {
                             string valueAsString = reader.GetValue(i).ToString();
                             currentRowAsString += valueAsString + ", ";
                         }
                         Console.WriteLine(currentRowAsString);
                     }
                    break;

                    break;


            }
            while (Console.ReadKey().Key != ConsoleKey.Enter) { }
            Console.Clear();
            Console.WriteLine("Voulez vous effectuer d'autres actions ?  (1) OUI /  (2) NON");
            int reponseee = DemandeInt("Veuillez entrer un entier entre 1 et 2", 2);
            if (reponseee == 2)
            {
                continuationn = 1;
                Console.Clear();
            }
            else
            {
                Console.Clear();
            }
        }

        
        // Geston desstocks les montrer et recomaander auto
    }

    static void Exo1(MySqlConnection connection)
    {

        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Client;";

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            string currentRowAsString = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string valueAsString = reader.GetValue(i).ToString();
                currentRowAsString += valueAsString + ",";
            }
            Console.WriteLine(currentRowAsString);
        }
    }
    static int DemandeInt(string msgerreur, int limite)
    {
        int r = 0;
        
        while (!(int.TryParse(Console.ReadLine(), out r) && r > 0 && r <= limite))
        {
            Console.WriteLine(msgerreur);
        }
        return r;

    }
    static double DemandeDouble(string msgerreur, double limite)
    {
        double r = 0;

        while (!(double.TryParse(Console.ReadLine(), out r) && r > 0 && r <= limite))
        {
            Console.WriteLine(msgerreur);
        }
        return r;

    }

    static void AffichageCata()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "mspaint.exe";
        startInfo.Arguments = "cat.png"; ;
        startInfo.WorkingDirectory = @"./Images";
        Process.Start(startInfo);
    }

    static void AffichageCataB()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "mspaint.exe";
        startInfo.Arguments = "catb.png"; ;
        startInfo.WorkingDirectory = @"./Images";
        Process.Start(startInfo);
    }
    static void Exo2(MySqlConnection connection)
    {

        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Commande;";

        MySqlDataReader reader;
        reader = command.ExecuteReader();

        while (reader.Read())
        {
            string currentRowAsString = "";
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string valueAsString = reader.GetValue(i).ToString();
                currentRowAsString += valueAsString + ",";
            }
            Console.WriteLine(currentRowAsString);
        }
    }
}
