-- ============================================================
--  MEDIATEK86 — Script SQL complet (MySQL)
--  Structure générée par Looping + compléments
-- ============================================================

-- 1. Création de la base de données
-- ============================================================
CREATE DATABASE IF NOT EXISTS mediatek86
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE mediatek86;

-- ============================================================
-- 2. Utilisateur MySQL dédié
-- ============================================================
CREATE USER IF NOT EXISTS 'mediatek86_user'@'localhost' IDENTIFIED BY 'Mediatek2024!';
GRANT SELECT, INSERT, UPDATE, DELETE ON mediatek86.* TO 'mediatek86_user'@'localhost';
FLUSH PRIVILEGES;

-- ============================================================
-- 3. Tables (script Looping)
-- ============================================================

CREATE TABLE service(
   idservice INT AUTO_INCREMENT,
   nom VARCHAR(50),
   PRIMARY KEY(idservice)
);

CREATE TABLE motif(
   idmotif INT AUTO_INCREMENT,
   libelle VARCHAR(128),
   PRIMARY KEY(idmotif)
);

CREATE TABLE personnel(
   idpersonnel INT AUTO_INCREMENT,
   nom VARCHAR(50),
   prenom VARCHAR(50),
   tel VARCHAR(15),
   mail VARCHAR(128),
   idservice INT NOT NULL,
   PRIMARY KEY(idpersonnel),
   FOREIGN KEY(idservice) REFERENCES service(idservice)
);

CREATE TABLE absence(
   idpersonnel INT,
   datedebut DATETIME,
   datefin DATETIME,
   idmotif INT NOT NULL,
   PRIMARY KEY(idpersonnel, datedebut),
   FOREIGN KEY(idpersonnel) REFERENCES personnel(idpersonnel),
   FOREIGN KEY(idmotif) REFERENCES motif(idmotif)
);

-- Table responsable (hors MCD, demandée dans les consignes)
CREATE TABLE responsable(
   login VARCHAR(64) NOT NULL,
   pwd   VARCHAR(64) NOT NULL
);

-- ============================================================
-- 4. Données — responsable
--    login : admin / pwd : Admin2024! (hashé SHA2-256)
-- ============================================================
INSERT INTO responsable (login, pwd) VALUES (
   'admin',
   SHA2('Admin2024!', 256)
);

-- ============================================================
-- 5. Données — motif (4 entrées fixes)
-- ============================================================
INSERT INTO motif (libelle) VALUES
   ('Vacances'),
   ('Maladie'),
   ('Motif familial'),
   ('Congé parental');

-- ============================================================
-- 6. Données — service (3 entrées fixes)
-- ============================================================
INSERT INTO service (nom) VALUES
   ('Administratif'),
   ('Médiation culturelle'),
   ('Prêt');

-- ============================================================
-- 7. Données — personnel (10 agents)
-- ============================================================
INSERT INTO personnel (nom, prenom, tel, mail, idservice) VALUES
   ('Martin',   'Sophie',  '0612345678', 'sophie.martin@mediatek86.fr',   1),
   ('Bernard',  'Lucas',   '0623456789', 'lucas.bernard@mediatek86.fr',   1),
   ('Dubois',   'Emma',    '0634567890', 'emma.dubois@mediatek86.fr',     2),
   ('Thomas',   'Noah',    '0645678901', 'noah.thomas@mediatek86.fr',     2),
   ('Robert',   'Léa',     '0656789012', 'lea.robert@mediatek86.fr',      2),
   ('Richard',  'Hugo',    '0667890123', 'hugo.richard@mediatek86.fr',    3),
   ('Petit',    'Inès',    '0678901234', 'ines.petit@mediatek86.fr',      3),
   ('Durand',   'Louis',   '0689012345', 'louis.durand@mediatek86.fr',    3),
   ('Leroy',    'Chloé',   '0690123456', 'chloe.leroy@mediatek86.fr',     3),
   ('Moreau',   'Ethan',   '0601234567', 'ethan.moreau@mediatek86.fr',    1);

-- ============================================================
-- 8. Données — absence (50 entrées)
-- ============================================================
INSERT INTO absence (idpersonnel, datedebut, datefin, idmotif) VALUES
(3,  '2023-01-09 00:00:00', '2023-01-20 00:00:00', 1),
(7,  '2023-01-16 00:00:00', '2023-01-18 00:00:00', 2),
(1,  '2023-02-13 00:00:00', '2023-02-24 00:00:00', 1),
(5,  '2023-02-20 00:00:00', '2023-02-21 00:00:00', 3),
(9,  '2023-03-06 00:00:00', '2023-03-08 00:00:00', 2),
(2,  '2023-04-03 00:00:00', '2023-04-14 00:00:00', 1),
(6,  '2023-04-17 00:00:00', '2023-04-18 00:00:00', 3),
(10, '2023-05-02 00:00:00', '2023-07-28 00:00:00', 4),
(4,  '2023-05-15 00:00:00', '2023-05-17 00:00:00', 2),
(8,  '2023-06-19 00:00:00', '2023-06-30 00:00:00', 1),
(1,  '2023-07-03 00:00:00', '2023-07-28 00:00:00', 1),
(3,  '2023-07-17 00:00:00', '2023-08-04 00:00:00', 1),
(5,  '2023-07-24 00:00:00', '2023-07-25 00:00:00', 2),
(7,  '2023-08-07 00:00:00', '2023-08-18 00:00:00', 1),
(9,  '2023-08-28 00:00:00', '2023-08-29 00:00:00', 3),
(2,  '2023-09-04 00:00:00', '2023-09-06 00:00:00', 2),
(6,  '2023-10-23 00:00:00', '2023-11-03 00:00:00', 1),
(4,  '2023-10-30 00:00:00', '2023-10-31 00:00:00', 3),
(8,  '2023-11-13 00:00:00', '2023-11-15 00:00:00', 2),
(1,  '2023-12-26 00:00:00', '2024-01-05 00:00:00', 1),
(3,  '2024-01-08 00:00:00', '2024-01-10 00:00:00', 2),
(5,  '2024-02-19 00:00:00', '2024-03-01 00:00:00', 1),
(7,  '2024-02-26 00:00:00', '2024-02-27 00:00:00', 3),
(9,  '2024-03-11 00:00:00', '2024-03-13 00:00:00', 2),
(2,  '2024-03-18 00:00:00', '2024-06-07 00:00:00', 4),
(10, '2024-04-22 00:00:00', '2024-05-03 00:00:00', 1),
(4,  '2024-04-29 00:00:00', '2024-04-30 00:00:00', 2),
(6,  '2024-05-13 00:00:00', '2024-05-14 00:00:00', 3),
(8,  '2024-06-17 00:00:00', '2024-06-28 00:00:00', 1),
(1,  '2024-07-08 00:00:00', '2024-08-02 00:00:00', 1),
(3,  '2024-07-22 00:00:00', '2024-08-09 00:00:00', 1),
(5,  '2024-07-29 00:00:00', '2024-07-31 00:00:00', 2),
(7,  '2024-08-05 00:00:00', '2024-08-16 00:00:00', 1),
(9,  '2024-08-19 00:00:00', '2024-08-20 00:00:00', 3),
(6,  '2024-09-02 00:00:00', '2024-09-04 00:00:00', 2),
(4,  '2024-10-21 00:00:00', '2024-11-01 00:00:00', 1),
(8,  '2024-10-28 00:00:00', '2024-10-29 00:00:00', 3),
(10, '2024-11-04 00:00:00', '2024-11-06 00:00:00', 2),
(1,  '2024-12-23 00:00:00', '2025-01-03 00:00:00', 1),
(3,  '2024-12-30 00:00:00', '2024-12-31 00:00:00', 3),
(7,  '2025-01-13 00:00:00', '2025-01-15 00:00:00', 2),
(5,  '2025-02-17 00:00:00', '2025-02-28 00:00:00', 1),
(9,  '2025-02-24 00:00:00', '2025-05-16 00:00:00', 4),
(2,  '2025-03-10 00:00:00', '2025-03-12 00:00:00', 2),
(6,  '2025-04-07 00:00:00', '2025-04-08 00:00:00', 3),
(4,  '2025-04-22 00:00:00', '2025-05-02 00:00:00', 1),
(8,  '2025-05-05 00:00:00', '2025-05-07 00:00:00', 2),
(10, '2025-05-12 00:00:00', '2025-05-13 00:00:00', 3),
(1,  '2025-07-07 00:00:00', '2025-08-01 00:00:00', 1),
(3,  '2025-09-22 00:00:00', '2025-09-24 00:00:00', 2);

-- ============================================================
-- 9. Vérification
-- ============================================================
SELECT 'responsable' AS table_name, COUNT(*) AS nb FROM responsable
UNION ALL SELECT 'service',   COUNT(*) FROM service
UNION ALL SELECT 'motif',     COUNT(*) FROM motif
UNION ALL SELECT 'personnel', COUNT(*) FROM personnel
UNION ALL SELECT 'absence',   COUNT(*) FROM absence;
