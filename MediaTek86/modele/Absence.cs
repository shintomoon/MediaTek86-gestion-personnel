using System;

namespace MediaTek86.modele
{
    /// <summary>
    /// Classe métier représentant une absence
    /// </summary>
    public class Absence
    {
        /// <summary>
        /// Personnel concerné par l'absence
        /// </summary>
        public Personnel Personnel { get; set; }

        /// <summary>
        /// Date de début de l'absence
        /// </summary>
        public DateTime DateDebut { get; set; }

        /// <summary>
        /// Date de fin de l'absence
        /// </summary>
        public DateTime DateFin { get; set; }

        /// <summary>
        /// Motif de l'absence
        /// </summary>
        public Motif Motif { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="personnel">Personnel</param>
        /// <param name="dateDebut">Date de début</param>
        /// <param name="dateFin">Date de fin</param>
        /// <param name="motif">Motif</param>
        public Absence(Personnel personnel, DateTime dateDebut,
                       DateTime dateFin, Motif motif)
        {
            Personnel = personnel;
            DateDebut = dateDebut;
            DateFin = dateFin;
            Motif = motif;
        }

        /// <summary>
        /// Retourne les informations de l'absence
        /// </summary>
        /// <returns>Informations de l'absence</returns>
        public override string ToString()
        {
            return DateDebut.ToShortDateString() + " - " +
                   DateFin.ToShortDateString() + " (" + Motif + ")";
        }
    }
}