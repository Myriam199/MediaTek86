-- ============================================================
--  MediaTek86 - Application de gestion du personnel
--  Script complet : creation de la base, des tables,
--  de l'utilisateur applicatif et jeu de donnees de test.
--  SGBD : MySQL (testé sous WampServer / MySQL 8)
-- ============================================================

-- ------------------------------------------------------------
-- 1. Creation de la base
-- ------------------------------------------------------------
DROP DATABASE IF EXISTS mediatek86;
CREATE DATABASE mediatek86 CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE mediatek86;

-- ------------------------------------------------------------
-- 2. Creation des tables
-- ------------------------------------------------------------

-- Table service
CREATE TABLE service (
    idservice INT(11) NOT NULL AUTO_INCREMENT,
    nom       VARCHAR(50) NOT NULL,
    CONSTRAINT pk_service PRIMARY KEY (idservice)
) ENGINE=InnoDB;

-- Table motif
CREATE TABLE motif (
    idmotif INT(11) NOT NULL AUTO_INCREMENT,
    libelle VARCHAR(50) NOT NULL,
    CONSTRAINT pk_motif PRIMARY KEY (idmotif)
) ENGINE=InnoDB;

-- Table personnel
CREATE TABLE personnel (
    idpersonnel INT(11) NOT NULL AUTO_INCREMENT,
    nom         VARCHAR(50) NOT NULL,
    prenom      VARCHAR(50) NOT NULL,
    tel         VARCHAR(50) NULL,
    mail        VARCHAR(50) NULL,
    idservice   INT(11) NOT NULL,
    CONSTRAINT pk_personnel PRIMARY KEY (idpersonnel),
    CONSTRAINT fk_personnel_service FOREIGN KEY (idservice)
        REFERENCES service (idservice)
) ENGINE=InnoDB;

-- Table absence
CREATE TABLE absence (
    idpersonnel INT(11) NOT NULL,
    datedebut   DATETIME NOT NULL,
    datefin     DATETIME NOT NULL,
    idmotif     INT(11) NOT NULL,
    CONSTRAINT pk_absence PRIMARY KEY (idpersonnel, datedebut),
    CONSTRAINT fk_absence_personnel FOREIGN KEY (idpersonnel)
        REFERENCES personnel (idpersonnel),
    CONSTRAINT fk_absence_motif FOREIGN KEY (idmotif)
        REFERENCES motif (idmotif)
) ENGINE=InnoDB;

-- Table responsable (connexion a l'application)
-- Pas d'identifiant, une seule ligne : login + mot de passe chiffre.
CREATE TABLE responsable (
    login VARCHAR(64) NOT NULL,
    pwd   VARCHAR(64) NOT NULL
) ENGINE=InnoDB;

-- ------------------------------------------------------------
-- 3. Creation de l'utilisateur applicatif (acces securise)
-- ------------------------------------------------------------
-- Cet utilisateur est celui que l'application utilise pour se
-- connecter a MySQL (chaine de connexion de l'application).
DROP USER IF EXISTS 'mediatek86user'@'localhost';
CREATE USER 'mediatek86user'@'localhost' IDENTIFIED BY 'mediatek86pwd';
GRANT SELECT, INSERT, UPDATE, DELETE ON mediatek86.* TO 'mediatek86user'@'localhost';
FLUSH PRIVILEGES;

-- ------------------------------------------------------------
-- 4. Alimentation des tables (jeu de tests)
-- ------------------------------------------------------------

-- Services (issus du contexte : administratif, mediation culturelle, pret)
INSERT INTO service (idservice, nom) VALUES
(1, 'administratif'),
(2, 'mediation culturelle'),
(3, 'pret');

-- Motifs (contenu fixe, non modifie par l'application)
INSERT INTO motif (idmotif, libelle) VALUES
(1, 'vacances'),
(2, 'maladie'),
(3, 'motif familial'),
(4, 'conge parental');

-- Responsable : login = admin / mot de passe = admin (chiffre en SHA2 256)
INSERT INTO responsable (login, pwd) VALUES
('admin', SHA2('admin', 256));

-- Personnels (une dizaine d'exemples)
INSERT INTO personnel (idpersonnel, nom, prenom, tel, mail, idservice) VALUES
(1,  'Martin',   'Sophie',   '0612345678', 'sophie.martin@mediatek86.fr',   1),
(2,  'Bernard',  'Lucas',    '0623456789', 'lucas.bernard@mediatek86.fr',   2),
(3,  'Dubois',   'Emma',     '0634567890', 'emma.dubois@mediatek86.fr',     3),
(4,  'Thomas',   'Hugo',     '0645678901', 'hugo.thomas@mediatek86.fr',     1),
(5,  'Robert',   'Lea',      '0656789012', 'lea.robert@mediatek86.fr',      2),
(6,  'Richard',  'Nathan',   '0667890123', 'nathan.richard@mediatek86.fr',  3),
(7,  'Petit',    'Chloe',    '0678901234', 'chloe.petit@mediatek86.fr',     1),
(8,  'Durand',   'Louis',    '0689012345', 'louis.durand@mediatek86.fr',    2),
(9,  'Leroy',    'Manon',    '0690123456', 'manon.leroy@mediatek86.fr',     3),
(10, 'Moreau',   'Jules',    '0601234567', 'jules.moreau@mediatek86.fr',    1),
(11, 'Simon',    'Camille',  '0611223344', 'camille.simon@mediatek86.fr',   2),
(12, 'Laurent',  'Tom',      '0622334455', 'tom.laurent@mediatek86.fr',     3);

-- Absences (une cinquantaine d'exemples, sans chevauchement par personnel)
INSERT INTO absence (idpersonnel, datedebut, datefin, idmotif) VALUES
(1,  '2025-01-06 00:00:00', '2025-01-10 00:00:00', 1),
(1,  '2025-03-03 00:00:00', '2025-03-04 00:00:00', 2),
(1,  '2025-07-14 00:00:00', '2025-07-25 00:00:00', 1),
(1,  '2025-11-10 00:00:00', '2025-11-12 00:00:00', 3),
(2,  '2025-02-17 00:00:00', '2025-02-21 00:00:00', 1),
(2,  '2025-04-07 00:00:00', '2025-04-08 00:00:00', 2),
(2,  '2025-08-04 00:00:00', '2025-08-22 00:00:00', 1),
(2,  '2025-12-01 00:00:00', '2025-12-05 00:00:00', 3),
(3,  '2025-01-20 00:00:00', '2025-01-24 00:00:00', 2),
(3,  '2025-05-12 00:00:00', '2025-05-16 00:00:00', 1),
(3,  '2025-09-08 00:00:00', '2025-09-19 00:00:00', 1),
(3,  '2025-10-13 00:00:00', '2025-10-14 00:00:00', 2),
(4,  '2025-02-03 00:00:00', '2025-02-07 00:00:00', 1),
(4,  '2025-06-02 00:00:00', '2025-06-06 00:00:00', 4),
(4,  '2025-07-28 00:00:00', '2025-08-08 00:00:00', 1),
(4,  '2025-11-24 00:00:00', '2025-11-26 00:00:00', 2),
(5,  '2025-01-13 00:00:00', '2025-01-17 00:00:00', 1),
(5,  '2025-03-24 00:00:00', '2025-03-25 00:00:00', 2),
(5,  '2025-08-11 00:00:00', '2025-08-29 00:00:00', 1),
(5,  '2025-12-22 00:00:00', '2025-12-31 00:00:00', 3),
(6,  '2025-02-10 00:00:00', '2025-02-14 00:00:00', 1),
(6,  '2025-04-21 00:00:00', '2025-04-22 00:00:00', 2),
(6,  '2025-07-07 00:00:00', '2025-07-18 00:00:00', 1),
(6,  '2025-10-06 00:00:00', '2025-10-08 00:00:00', 3),
(7,  '2025-01-27 00:00:00', '2025-01-31 00:00:00', 2),
(7,  '2025-05-05 00:00:00', '2025-05-09 00:00:00', 1),
(7,  '2025-09-15 00:00:00', '2025-09-26 00:00:00', 1),
(7,  '2025-11-17 00:00:00', '2025-11-18 00:00:00', 2),
(8,  '2025-03-10 00:00:00', '2025-03-14 00:00:00', 1),
(8,  '2025-06-16 00:00:00', '2025-06-20 00:00:00', 4),
(8,  '2025-08-18 00:00:00', '2025-08-29 00:00:00', 1),
(8,  '2025-12-08 00:00:00', '2025-12-10 00:00:00', 2),
(9,  '2025-02-24 00:00:00', '2025-02-28 00:00:00', 1),
(9,  '2025-04-14 00:00:00', '2025-04-15 00:00:00', 2),
(9,  '2025-07-21 00:00:00', '2025-08-01 00:00:00', 1),
(9,  '2025-10-20 00:00:00', '2025-10-22 00:00:00', 3),
(10, '2025-01-15 00:00:00', '2025-01-16 00:00:00', 2),
(10, '2025-05-19 00:00:00', '2025-05-23 00:00:00', 1),
(10, '2025-09-01 00:00:00', '2025-09-12 00:00:00', 1),
(10, '2025-11-03 00:00:00', '2025-11-05 00:00:00', 3),
(11, '2025-02-05 00:00:00', '2025-02-07 00:00:00', 2),
(11, '2025-06-09 00:00:00', '2025-06-13 00:00:00', 1),
(11, '2025-08-25 00:00:00', '2025-09-05 00:00:00', 1),
(11, '2025-12-15 00:00:00', '2025-12-19 00:00:00', 4),
(12, '2025-01-08 00:00:00', '2025-01-09 00:00:00', 2),
(12, '2025-04-28 00:00:00', '2025-05-02 00:00:00', 1),
(12, '2025-07-01 00:00:00', '2025-07-11 00:00:00', 1),
(12, '2025-10-27 00:00:00', '2025-10-29 00:00:00', 3),
(12, '2025-11-19 00:00:00', '2025-11-20 00:00:00', 2),
(1,  '2025-09-22 00:00:00', '2025-09-23 00:00:00', 2);

-- ============================================================
--  Fin du script
-- ============================================================
