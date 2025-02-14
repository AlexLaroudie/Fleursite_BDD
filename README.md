# Fleuriste_BDD
Projet de 2 eme année de SQL et C#

Le problème présenté était de créer une base de données et une interface client/gestionnaire pour 
un fleuriste se nommant « Michel Bellefleur ».  

Pour répondre à cette problématique, nous avons utilisé Visual Studio et C#.  
Dans un premier temps, nous avons créer un document partagé afin de se faire une idée des tables 
qu’on devait créer et de leurs attributs

Ainsi nous avons 7 tables : - - - 

Client : elle représente le client et possède toutes ces informations, y compris celles que le 
client ne rempli pas comme la fidélité ou son identifiant 

Commande : elle représente les informations de la commande du client sans préciser son 
contenu.  

Bouquet Standard : cette table est différente que celle de bouquet personnalisé car elles ne 
possèdent pas les mêmes attributs.  
- 

Bouquet Personnalisé : cette table comprend tous les détails d’une commande personnalisée 
comme les accessoires, leur nombre, les fleurs utilisées, leur nombre… - - - 

Stock : cette table permet de simplifier la gestion du stock chez Michel Bellefleur 

Trésorerie : cette table représente la trésorerie de l’entreprise. Elle augmente pour chaque 
commande et diminue lorsqu’on recommande du stock  

Statistiques : cette table est utile pour le gestionnaire car elle lui permet de connaitre une 
suite de statistiques comme le bouquet le plus vendu ou le client le plus fidèle. 



Nous avons décidé de créer deux interfaces différentes : utilisateur et gestionnaire. Le gestionnaire à 
le pouvoir de gérer les stocks, de voir les statistiques, de supprimer des clients et de regarder les 
informations des clients et de leur commande.  


L’interface la plus importante concerne les clients : on peut décider de soit se connecter soit se créer 
un compte. Lorsqu’on est déjà client, il est possible de regarder son historique de commande : cela 
n’est pas possible si on vient de créer notre compte car l’historique sera vide. Les contraintes 
demandées ont été respectées : si un client déjà existant essaye de se créer un compte avec une 
adresse électronique déjà existante, le code le renvoie à la fenêtre « se connecter », par exemple.  
Par la suite, le client qui demande à faire une commande peut choisir entre commande personnalisée 
ou commande standard. Dans le cas échéant, il devra spécifier le nom de son bouquet et la 
commande sera terminée (une commande = un article). 


Dans le cas de la commande personnalisée, le client donnera un budget et composera son bouquet à 
partir des fleurs disponibles. S’il dépasse le budget donné, il sera averti et l’algorithme lui retirera le 
dernier article qui l’a fait dépasser sa limite budgétaire. Le client pourra après commander des 
accessoires pour finaliser sa commande. 

Etant donné que nous possédons les codes bancaires du client (il s’agit d’un de ses attributs), nous 
considérons qu’il ne doit pas les rentrer de nouveau et que le payement est automatique lors de la 
confirmation de sa commande.  

Lorsqu’un client réalise une commande, la trésorerie est incrémentée directement. Le stock des 
bouquets et des fleurs baisse aussi, et il faudra que le gestionnaire se reconnecte et passe une 
commande à son fournisseur afin de ré-augmenter son stock. De même, si le gestionnaire passe 
commande pour sa boutique, la trésorerie baissera de nouveau. 

La table statistique, qui sert aussi au gestionnaire, s’incrémente seule. Il n’y a pas de manipulation 
nécessaire.
