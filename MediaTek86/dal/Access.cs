using System;
using System.Collections.Generic;
using MediaTek86.bddmanager;
using MediaTek86.modele;
using MySql.Data.MySqlClient;

namespace MediaTek86.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        private static readonly string stringConnect =
            "server=localhost;user id=mediatek86_user;" +
            "password=Mediatek2024!;database=mediatek86";

        private readonly BddManager bddManager;
        private static Access instance = null;

        private Access()
        {
            bddManager = BddManager.GetInstance(stringConnect);
        }

        public static Access GetInstance()
        {
            if (instance == null) instance = new Access();
            return instance;
        }

        public bool ControleAuthentification(string login, string pwd)
        {
            string req = "SELECT * FROM responsable WHERE login=@login AND pwd=SHA2(@pwd, 256);";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pwd", pwd);
                MySqlDataReader reader = command.ExecuteReader();
                bool authentifie = reader.HasRows;
                connection.Close();
                return authentifie;
            }
            catch (Exception e) { Console.WriteLine(e.Message); return false; }
        }

        public List<Service> GetLesServices()
        {
            List<Service> lesServices = new List<Service>();
            string req = "SELECT * FROM service ORDER BY nom;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lesServices.Add(new Service((int)reader["idservice"], (string)reader["nom"]));
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return lesServices;
        }

        public List<Personnel> GetLePersonnel()
        {
            List<Personnel> lePersonnel = new List<Personnel>();
            string req = "SELECT p.idpersonnel, p.nom, p.prenom, p.tel, p.mail, ";
            req += "s.idservice, s.nom AS nomservice ";
            req += "FROM personnel p JOIN service s ON p.idservice = s.idservice ";
            req += "ORDER BY p.nom, p.prenom;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Service service = new Service((int)reader["idservice"], (string)reader["nomservice"]);
                    Personnel personnel = new Personnel(
                        (int)reader["idpersonnel"],
                        (string)reader["nom"],
                        (string)reader["prenom"],
                        (string)reader["tel"],
                        (string)reader["mail"],
                        service);
                    lePersonnel.Add(personnel);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return lePersonnel;
        }

        public void AddPersonnel(Personnel personnel)
        {
            string req = "INSERT INTO personnel(nom, prenom, tel, mail, idservice) ";
            req += "VALUES (@nom, @prenom, @tel, @mail, @idservice);";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@nom", personnel.Nom);
                command.Parameters.AddWithValue("@prenom", personnel.Prenom);
                command.Parameters.AddWithValue("@tel", personnel.Tel);
                command.Parameters.AddWithValue("@mail", personnel.Mail);
                command.Parameters.AddWithValue("@idservice", personnel.Service.IdService);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void UpdatePersonnel(Personnel personnel)
        {
            string req = "UPDATE personnel SET nom=@nom, prenom=@prenom, ";
            req += "tel=@tel, mail=@mail, idservice=@idservice WHERE idpersonnel=@idpersonnel;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@nom", personnel.Nom);
                command.Parameters.AddWithValue("@prenom", personnel.Prenom);
                command.Parameters.AddWithValue("@tel", personnel.Tel);
                command.Parameters.AddWithValue("@mail", personnel.Mail);
                command.Parameters.AddWithValue("@idservice", personnel.Service.IdService);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void DeletePersonnel(Personnel personnel)
        {
            string reqAbs = "DELETE FROM absence WHERE idpersonnel=@idpersonnel;";
            string req = "DELETE FROM personnel WHERE idpersonnel=@idpersonnel;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand cmdAbs = new MySqlCommand(reqAbs, connection);
                cmdAbs.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                cmdAbs.ExecuteNonQuery();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public List<Motif> GetLesMotifs()
        {
            List<Motif> lesMotifs = new List<Motif>();
            string req = "SELECT * FROM motif ORDER BY libelle;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lesMotifs.Add(new Motif((int)reader["idmotif"], (string)reader["libelle"]));
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return lesMotifs;
        }

        public List<Absence> GetLesAbsences(Personnel personnel)
        {
            List<Absence> lesAbsences = new List<Absence>();
            string req = "SELECT a.datedebut, a.datefin, m.idmotif, m.libelle ";
            req += "FROM absence a JOIN motif m ON a.idmotif = m.idmotif ";
            req += "WHERE a.idpersonnel = @idpersonnel ";
            req += "ORDER BY a.datedebut DESC;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Motif motif = new Motif((int)reader["idmotif"], (string)reader["libelle"]);
                    Absence absence = new Absence(personnel,
                        (DateTime)reader["datedebut"],
                        (DateTime)reader["datefin"],
                        motif);
                    lesAbsences.Add(absence);
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
            return lesAbsences;
        }

        public void AddAbsence(Absence absence)
        {
            string req = "INSERT INTO absence(idpersonnel, datedebut, datefin, idmotif) ";
            req += "VALUES (@idpersonnel, @datedebut, @datefin, @idmotif);";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", absence.Personnel.IdPersonnel);
                command.Parameters.AddWithValue("@datedebut", absence.DateDebut);
                command.Parameters.AddWithValue("@datefin", absence.DateFin);
                command.Parameters.AddWithValue("@idmotif", absence.Motif.IdMotif);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void UpdateAbsence(Absence ancienne, Absence modifiee)
        {
            string req = "UPDATE absence SET datedebut=@nouvDateDebut, datefin=@datefin, idmotif=@idmotif ";
            req += "WHERE idpersonnel=@idpersonnel AND datedebut=@ancDateDebut;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@nouvDateDebut", modifiee.DateDebut);
                command.Parameters.AddWithValue("@datefin", modifiee.DateFin);
                command.Parameters.AddWithValue("@idmotif", modifiee.Motif.IdMotif);
                command.Parameters.AddWithValue("@idpersonnel", ancienne.Personnel.IdPersonnel);
                command.Parameters.AddWithValue("@ancDateDebut", ancienne.DateDebut);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public void DeleteAbsence(Absence absence)
        {
            string req = "DELETE FROM absence WHERE idpersonnel=@idpersonnel AND datedebut=@datedebut;";
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", absence.Personnel.IdPersonnel);
                command.Parameters.AddWithValue("@datedebut", absence.DateDebut);
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }
        /// <summary>
        /// Vérifie si un personnel a déjà une absence qui chevauche le créneau donné
        /// </summary>
        /// <param name="personnel">Le personnel concerné</param>
        /// <param name="dateDebut">Date de début du nouveau créneau</param>
        /// <param name="dateFin">Date de fin du nouveau créneau</param>
        /// <param name="ancienneDateDebut">Date de début de l'absence en cours de modification (null si ajout)</param>
        /// <returns>true s'il y a un chevauchement</returns>
        public bool AbsenceChevauche(Personnel personnel, DateTime dateDebut, DateTime dateFin, DateTime? ancienneDateDebut)
        {
            string req = "SELECT * FROM absence WHERE idpersonnel=@idpersonnel ";
            req += "AND NOT (datefin < @datedebut OR datedebut > @datefin) ";
            if (ancienneDateDebut.HasValue)
            {
                req += "AND datedebut <> @ancienneDateDebut;";
            }
            else
            {
                req += ";";
            }
            try
            {
                MySqlConnection connection = bddManager.GetConnection();
                connection.Open();
                MySqlCommand command = new MySqlCommand(req, connection);
                command.Parameters.AddWithValue("@idpersonnel", personnel.IdPersonnel);
                command.Parameters.AddWithValue("@datedebut", dateDebut);
                command.Parameters.AddWithValue("@datefin", dateFin);
                if (ancienneDateDebut.HasValue)
                {
                    command.Parameters.AddWithValue("@ancienneDateDebut", ancienneDateDebut.Value);
                }
                MySqlDataReader reader = command.ExecuteReader();
                bool chevauche = reader.HasRows;
                connection.Close();
                return chevauche;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}