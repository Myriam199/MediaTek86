 MediaTek86 - Application de gestion du personnel

Application de bureau (Windows Forms, C#, .NET Framework) développée pour le réseau MediaTek86, qui gère les médiathèques de la Vienne. Elle permet au responsable du personnel de gérer le personnel de chaque médiathèque, son affectation à un service et ses absences.

Application monoposte, installée sur un poste du service administratif. Elle est construite sur le même modèle que l'application Habilitations (architecture MVC, classe de connexion BddManager, organisation en packages).

 Contexte et but de l'application

Le réseau MediaTek86 fédère les prêts de livres, DVD et CD et développe la médiathèque numérique du département. L'ESN InfoTech Services 86 a remporté le marché pour différentes interventions, dont le développement de cette application de gestion du personnel.

L'application permet de :

- se connecter (responsable du personnel)
- gérer les personnels (ajout, modification, suppression) et leur service d'affectation
- gérer les absences de chaque personnel (affichage, ajout, modification, suppression) en évitant les chevauchements

 Modèle Conceptuel de Données (MCD)

![MCD MediaTek86](docs/mcd.png)

Tables de la base mediatek86 :

- service (idservice, nom)
- motif (idmotif, libelle)
- personnel (idpersonnel, nom, prenom, tel, mail, #idservice)
- absence (#idpersonnel, datedebut, datefin, #idmotif) - clé primaire (idpersonnel, datedebut)
- responsable (login, pwd) - une seule ligne, mot de passe chiffré en SHA2(..., 256)

Le script complet de la base (CREATE + INSERT + création de l'utilisateur applicatif) est disponible dans [sql/mediatek86.sql](sql/mediatek86.sql).

 Interfaces

| Fenêtre | Rôle |
|---|---|
| Authentification | Connexion du responsable (login / mot de passe). |
| Gestion du personnel | Liste des personnels + ajout / modification / suppression + accès aux absences. |
| Gestion des absences | Liste des absences d'un personnel (de la plus récente à la plus ancienne) + ajout / modification / suppression. |

![Fenêtre d'authentification](docs/interface-authentification.png)
![Gestion du personnel](docs/interface-personnel.png)
![Gestion des absences](docs/interface-absence.png)

 Diagramme de paquetages

L'application suit le modèle MVC et s'organise en packages :

![Diagramme de paquetages](docs/diagramme-paquetages.png)

- vue : les fenêtres (FrmAuthentification, FrmGestionPersonnel, FrmGestionAbsence)
- controleur : la classe Controle, qui fait le lien entre les vues et la couche d'accès aux données
- modele : les classes métier (Service, Motif, Personnel, Absence)
- dal : la classe Dao, qui répond aux demandes du contrôleur en exploitant BddManager
- bddmanager : la classe technique singleton BddManager (connexion à la base et exécution des requêtes)

 Étapes de construction et commits

Le développement a été réalisé en plusieurs étapes, chacune sauvegardée par un ou plusieurs commits clairement commentés et suivie sur le tableau Kanban du dépôt.

| Étape | Description | Exemples de commits |
|---|---|---|
| 1 | Préparation de l'environnement et création de la base de données | Ajout du script SQL complet de la base mediatek86 |
| 2 | Structuration MVC, dépôt GitHub, codage du visuel | Création de la structure MVC (packages) · Codage des interfaces (vue) |
| 3 | Modèle, outils de connexion, documentation technique | Ajout du package bddmanager (BddManager) · Ajout du package dal (Dao) · Ajout des classes métier (modele) · Documentation technique |
| 4 | Codage des fonctionnalités à partir des cas d'utilisation | CU se connecter · CU ajouter/modifier/supprimer personnel · CU gérer les absences (affichage, ajout, modification, suppression) · Contrôle du chevauchement des absences |
| 6 | Déploiement, Readme, portfolio | Ajout de l'installateur · Rédaction du Readme |

Le détail réel des commits est visible dans l'onglet Commits et le Kanban (Projects) du dépôt.

 Cas d'utilisation couverts

- Se connecter
- Ajouter un personnel
- Modifier un personnel
- Supprimer un personnel
- Afficher les absences d'un personnel
- Ajouter une absence (contrôle date de fin ≥ date de début et non-chevauchement)
- Modifier une absence (mêmes contrôles)
- Supprimer une absence

 Installation

 Pré-requis
- Windows
- MySQL Community Server
- .NET Framework 4.7.2

 Mise en place de la base de données
1. Installer MySQL Community Server et MySQL Workbench.
2. Ouvrir MySQL Workbench et se connecter à l'instance locale (Local instance MySQL).
3. Ouvrir et exécuter le script [sql/mediatek86.sql](sql/mediatek86.sql). Il crée la base, les tables, l'utilisateur applicatif mediatek86user et le jeu de données de test.

 Installation de l'application
1. Récupérer l'installateur dans le dossier installeur/ du dépôt.
2. Lancer setup.exe et suivre l'assistant.
3. Lancer l'application MediaTek86.

 Connexion par défaut
- Login : admin
- Mot de passe : admin

La chaîne de connexion utilisée par l'application correspond à l'utilisateur mediatek86user / mediatek86pwd créé par le script SQL. Elle se trouve dans dal/Dao.cs et peut être adaptée si besoin.

 Technologies

- C# / .NET Framework 4.7.2
- Windows Forms
- MySQL (connecteur MySql.Data)
- Architecture MVC

 Documentation

- Documentation technique : voir [docs/DOCUMENTATION_TECHNIQUE.md](docs/DOCUMENTATION_TECHNIQUE.md)
- Documentation utilisateur : vidéo de démonstration (lien dans la page du portfolio)
