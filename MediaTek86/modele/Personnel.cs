namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant un personnel
    /// </summary>
    public class Personnel
    {
        /// <summary>
        /// Identifiant du personnel
        /// </summary>
        public int IdPersonnel { get; set; }

        /// <summary>
        /// Nom du personnel
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Prénom du personnel
        /// </summary>
        public string Prenom { get; set; }

        /// <summary>
        /// Téléphone du personnel
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// Mail du personnel
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Service du personnel
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="idPersonnel">Identifiant</param>
        /// <param name="nom">Nom</param>
        /// <param name="prenom">Prénom</param>
        /// <param name="tel">Téléphone</param>
        /// <param name="mail">Mail</param>
        /// <param name="service">Service</param>
        public Personnel(int idPersonnel, string nom, string prenom,
                         string tel, string mail, Service service)
        {
            IdPersonnel = idPersonnel;
            Nom = nom;
            Prenom = prenom;
            Tel = tel;
            Mail = mail;
            Service = service;
        }

        /// <summary>
        /// Retourne nom et prénom du personnel
        /// </summary>
        /// <returns>Nom et prénom</returns>
        public override string ToString()
        {
            return Nom + " " + Prenom;
        }
    }
}