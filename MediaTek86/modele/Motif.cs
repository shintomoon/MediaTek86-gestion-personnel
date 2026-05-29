namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant un motif d'absence
    /// </summary>
    public class Motif
    {
        /// <summary>
        /// Identifiant du motif
        /// </summary>
        public int IdMotif { get; set; }

        /// <summary>
        /// Libellé du motif
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="idMotif">Identifiant</param>
        /// <param name="libelle">Libellé du motif</param>
        public Motif(int idMotif, string libelle)
        {
            IdMotif = idMotif;
            Libelle = libelle;
        }

        /// <summary>
        /// Retourne le libellé du motif
        /// </summary>
        /// <returns>Libellé du motif</returns>
        public override string ToString()
        {
            return Libelle;
        }
    }
}