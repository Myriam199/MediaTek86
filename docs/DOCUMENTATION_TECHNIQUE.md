# Documentation technique - MediaTek86

## 1. Présentation

MediaTek86 est une application de bureau développée en C# avec Windows Forms (.NET Framework 4.7.2). Elle permet au responsable du personnel des médiathèques de la Vienne de gérer le personnel et ses absences. Les données sont stockées dans une base MySQL.

L'application suit l'architecture MVC et reprend la même organisation que l'application Habilitations vue en cours.

## 2. Architecture en packages

L'application est organisée en cinq packages :

- vue : les fenêtres de l'application
- controleur : fait le lien entre les vues et la couche d'accès aux données
- modele : les classes métier
- dal : la couche d'accès aux données
- bddmanager : la classe technique de connexion à la base

Le diagramme de paquetages est disponible dans docs/diagramme-paquetages.png.

## 3. La base de données

La base s'appelle mediatek86 et contient cinq tables : service, motif, personnel, absence et responsable.

La connexion à MySQL passe par la classe BddManager. Elle utilise le connecteur MySql.Data. La chaîne de connexion se trouve dans la classe Dao.

## 4. Le package bddmanager

La classe BddManager gère la connexion à MySQL. C'est un singleton, c'est à dire qu'il n'existe qu'une seule instance pour toute l'application.

Elle propose deux méthodes principales :

- ReqSelect pour les requêtes de type SELECT
- ReqUpdate pour les requêtes INSERT, UPDATE et DELETE

Les requêtes utilisent des paramètres, ce qui évite les injections SQL.

## 5. Le package modele

Il contient quatre classes métier qui représentent les données :

- Service (idservice, nom)
- Motif (idmotif, libelle)
- Personnel (idpersonnel, nom, prenom, tel, mail, idservice, service)
- Absence (idpersonnel, datedebut, datefin, idmotif, motif)

## 6. Le package dal

La classe Dao contient toutes les méthodes qui dialoguent avec la base :

- ControleAuthentification : vérifie le login et le mot de passe
- GetLesServices, GetLesMotifs, GetLesPersonnels
- AjouterPersonnel, ModifierPersonnel, SupprimerPersonnel
- GetLesAbsences, AjouterAbsence, ModifierAbsence, SupprimerAbsence

À noter pour la modification d'une absence : comme la clé primaire est composée de idpersonnel et datedebut, on garde l'ancienne date de début pour retrouver la bonne ligne à modifier.

## 7. Le package controleur

La classe Controle reçoit les demandes des vues et appelle les bonnes méthodes de la classe Dao. Elle sert d'intermédiaire pour que les vues ne touchent jamais directement à la base.

## 8. Le package vue

Il contient les trois fenêtres :

- FrmAuthentification : la connexion du responsable
- FrmGestionPersonnel : la liste et la gestion du personnel
- FrmGestionAbsence : la gestion des absences d'un personnel

## 9. La sécurité

Le mot de passe du responsable n'est jamais stocké en clair. Il est chiffré avec SHA2 256 directement dans la base de données. Lors de la connexion, on compare les empreintes chiffrées.

De plus, l'application ne se connecte pas à MySQL avec le compte root. Elle utilise un utilisateur dédié appelé mediatek86user qui a seulement les droits SELECT, INSERT, UPDATE et DELETE sur la base.

## 10. Les contrôles de saisie

Pour le personnel, tous les champs doivent être remplis avant l'enregistrement.

Pour les absences, il y a trois contrôles :

- tous les champs doivent être remplis
- la date de fin ne peut pas être avant la date de début
- une absence ne peut pas chevaucher une absence déjà enregistrée pour la même personne

## 11. Installation rapide

Le détail est dans le README. En résumé :

1. Installer MySQL
2. Exécuter le script sql/mediatek86.sql
3. Ouvrir la solution dans Visual Studio 2022
4. Restaurer le package NuGet MySql.Data
5. Lancer avec F5

La connexion par défaut est login admin et mot de passe admin.
