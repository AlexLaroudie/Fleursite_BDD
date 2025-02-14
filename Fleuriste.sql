DROP database IF EXISTS Fleuriste;
create database Fleuriste;

use Fleuriste;

DROP TABLE IF EXISTS Commande;
DROP TABLE IF EXISTS Client;
DROP TABLE IF EXISTS Stock;
DROP TABLE IF EXISTS Statistique;
DROP TABLE IF EXISTS BouquetPerso;
DROP TABLE IF EXISTS BouquetStandard;

create table Client(
                id_C int PRIMARY KEY,
    nom VARCHAR(45),
    prenom VARCHAR(45),
    num_tel INTEGER,
    courriel VARCHAR(45),
    mdp VARCHAR(45),
    adresse_fact VARCHAR(45),
    ville VARCHAR(45),
    carte_credit INTEGER,
    nb_achats INTEGER,
    fidelite Enum('normal','bronze','or')
);  
create table BouquetStandart(
                
    nom ENUM("Gros Merci","L Exotique","L Amoureux","Maman","Vive La Mariée"),
    Prix DOUBLE,
    Stock Integer,
    Disponibilite ENUM("A l annee","Fevrier","Mai/Juin"),
    
    
    
                PRIMARY KEY (nom)
);
create table BouquetPerso(
                id_BouquetPerso INTEGER PRIMARY KEY,
    
    prix_client Double,
    prix_Final Double,
    fleurs Varchar (45),
    nb_fleurs Integer,
    accessoires Varchar(45),
    nb_accessoires INTEGER,
    id_client Integer,
    FOREIGN KEY (id_client) REFERENCES client (id_C)
    
);    
create table Commande(
                id_commande INT PRIMARY KEY,
                nom_Bouquet ENUM('Gros Merci','L Amoureux','L Exotique','Maman','Vive La Mariée'),
    adresse_liv VARCHAR(45),
    message VARCHAR(45),
    ville VARCHAR(45),
    date_liv DATE,
    date_commande DATE,
    etat_commande VARCHAR(4),
    id_client Integer,
    
    id_BouquetPerso INTEGER,
    Foreign Key (nom_Bouquet) REFERENCES BouquetStandart (nom),
    Foreign Key (id_BouquetPerso) REFERENCES BouquetPerso (id_BouquetPerso),
    Foreign Key (id_client) REFERENCES client (id_C)
    # on doit vraiment mettre fleur ici sachant qu'elle déja dans bouquetperso?
);
create table Statistiques(
nb_ventes_mois INTEGER,
nb_ventes_an INTEGER,
nb_clients INTEGER,
nb_commandes INTEGER,
                
    Primary Key (nb_ventes_mois,nb_ventes_an,nb_clients,nb_commandes)
    
);    
create table Stock_Fleurs(
nom_fleurs VArchar(45),
nb_fleurs_stock INTEGER,
prix Double,
prix_fournisseur DOUBLE,



      Primary Key (nb_fleurs_stock,nom_fleurs)           
   
    
);    
create table Stock_Accessoires(


nom_accessoires varchar(45),
nb_accessoires Integer,
Prix Double,
prix_fournisseur DOUBLE,

      Primary Key (nb_accessoires,nom_accessoires)           
   
    
);    
create table Tresorerie(
montant Double,
id_treso INT Primary Key
);


INSERT INTO client VALUES ('001', 'Castagnon', 'Julie', 0654345678, 'julie.castagnon@gmail.com', 'lemotdepassedejulie', 'rue vaugirard','Paris','2345','1','or');
INSERT INTO client VALUES (003, 'Durand', 'Jean', '0123456789', 'jean.durand@gmail.com', 'motdepasse', '10 rue des Roses',' Paris', 123456, 5, 'bronze');
INSERT INTO client VALUES('002','Laroudie','Alexandre','0631734078','laroudiealexandre@gmail.com','lemotdepasse','5 rue aurguste mayet','asnieres','12343',1,'or');
INSERT INTO client VALUES (004, 'Lefevre', 'Marie', '0607080910', 'marie.lefevre@hotmail.com', '12345', '20 avenue des Champs-Élysées',' Paris', 234567, 10, 'or');
INSERT INTO client VALUES (005, 'Dupont-Durand', 'Alice', '0678901234', 'alice.dupontdurand@gmail.com', 'monmdp', '5 rue de la Paix',' Lyon', 345678, 2, 'normal');
INSERT INTO client VALUES (006, 'Girard', 'Lucas', '0123456789', 'lucas.girard@entreprise.fr', 'motdepasse123', '30 avenue des Vosges',' Strasbourg', 456789, 8, 'bronze');
INSERT INTO client VALUES (007, 'Martin', 'Julie', '0607080910', 'julie.martin@yahoo.fr', 'password', '15 rue de la Liberté',' Marseille', 567890, 3, 'normal');
INSERT INTO client VALUES (008, 'Fournier', 'Nicolas', '056432454', 'nicolas.fournier@gmail.com', 'monmotdepasse', '25 rue de la République',' Toulouse', 678901, 12, 'or');
INSERT INTO client VALUES (009, 'Petit', 'Emma','056435433', 'emma.petit@gmail.com', 'motdepasse456', '5 avenue du Maine',' Nantes', 789012, 6, 'bronze');
INSERT INTO client VALUES (010, 'De La Fontaine Du Bois', 'Paul', '0123456789', 'paul.delafontaine@gmail.com', 'password123', '40 boulevard des Pyrénées',' Pau', 890123, 1, 'normal');
INSERT INTO client VALUES (011, 'Dupuis', 'Sophie', '0607080910', 'sophie.dupuis@gmail.com', 'monpassword', '12 rue du Bac',' Paris', 901234, 9, 'bronze');
INSERT INTO client VALUES (012, 'Smith', 'John', '0123456789', 'john.smith@yahoo.com', 'mypassword', '30 Oxford Street',' Caen', 345678, 4, 'normal');


Insert INTO BouquetStandart Values("Gros Merci",45,25,"A l annee");
Insert INTO BouquetStandart Values("L Amoureux",65,20,"Fevrier");
Insert INTO BouquetStandart Values("L Exotique",40,15,"A l annee");
Insert INTO BouquetStandart Values("Maman",80,15,"Mai/Juin");
Insert INTO BouquetStandart Values("Vive La Mariée",120,20,"A l annee");

Insert INTO BouquetPerso VALUES (01,13.0,12.0,'Pivoine',7,'ruban',2,002);

Insert into Stock_Fleurs VALUES("Pivoines",40,6.0,3.0);
Insert into Stock_Fleurs VALUES("Roses rouges",30,5.0,3.0);
Insert into Stock_Fleurs VALUES("Roses blanches",20,5.0,3.0);
Insert into Stock_Fleurs VALUES("Tulipes",10,4.0,2.0);
Insert into Stock_Fleurs VALUES("Orchidées",20,8.50,5.0);
Insert into Stock_Fleurs VALUES("Gerberas",20,5.0,3.0);
Insert into Stock_Fleurs VALUES("Gingers",15,4.0,2.0);
Insert into Stock_Fleurs VALUES("Glaieuls",30,1.0,0.5);
Insert into Stock_Fleurs VALUES("Marguerites",20,2.25,1.0);
Insert into Stock_Accessoires VALUES("ruban",100,2.0,1.0);
Insert into Stock_Accessoires VALUES("paillettes",100,2.0,1.0);
Insert into Stock_Accessoires VALUES("Lutin",10,10.0,5.0);

INSERT INTO Commande VALUES (1,'Gros Merci', '10 rue des Fleurs', 'Fragile', 'Paris', '2023-04-14', '2023-04-20', 'CVI', 001, nULL);
INSERT INTO Commande VALUES (2,'Maman', '25 avenue des Roses', 'Ne pas sonner', 'Lyon', '2023-04-26', '2023-04-21', 'CVIC', 002,  null);
INSERT INTO Commande VALUES (3,'Vive La Mariée', '8 rue du Muguet', NULL, 'Toulouse', '2023-04-28', '2023-04-22', 'CVIC', 003,  NULL);
INSERT INTO Commande VALUES (4,'L Amoureux', '15 avenue des Tulipes', 'Sonner deux fois', 'Marseille', '2023-04-30', '2023-04-23', 'CVe', 004,  null);
INSERT INTO Commande VALUES (5,'L Exotique', '12 rue des Lilas', NULL, 'Lille', '2023-05-01', '2023-04-24', 'CVIC', 005,  NULL);
INSERT INTO Commande VALUES (6,'L Exotique', '12 rue des Lilas', NULL, 'Lille', '2021-05-01', '2021-04-24', 'CVIC', 012,  NULL);



INSERT INTO Tresorerie VALUES(5000.00,001);

DELIMITER //
CREATE PROCEDURE ville_client(IN vill CHAR(20))
BEGIN
SELECT nom,prenom
FROM client
WHERE ville = vill;
END //



use Fleuriste
