namespace MediaTek86.modele
{
    /// <summary>
    /// Package contenant les classes métier correspondant aux tables de la base de données.
    /// </summary>
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Classe métier représentant un service
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Identifiant du service
        /// </summary>
        public int IdService { get; set; }

        /// <summary>
        /// Nom du service
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="idService">Identifiant</param>
        /// <param name="nom">Nom du service</param>
        public Service(int idService, string nom)
        {
            IdService = idService;
            Nom = nom;
        }

        /// <summary>
        /// Retourne le nom du service
        /// </summary>
        /// <returns>Nom du service</returns>
        public override string ToString()
        {
            return Nom;
        }
    }
}