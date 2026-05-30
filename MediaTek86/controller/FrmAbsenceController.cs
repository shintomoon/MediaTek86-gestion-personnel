using MediaTek86.dal;
using MediaTek86.modele;
using System;
using System.Collections.Generic;

namespace MediaTek86.controller
{
    /// <summary>
    /// Contrôleur du formulaire de gestion des absences
    /// </summary>
    public class FrmAbsenceController
    {
        private readonly Access access;

        public FrmAbsenceController()
        {
            access = Access.GetInstance();
        }

        public List<Motif> GetLesMotifs()
        {
            return access.GetLesMotifs();
        }

        public List<Absence> GetLesAbsences(Personnel personnel)
        {
            return access.GetLesAbsences(personnel);
        }

        public void AddAbsence(Absence absence)
        {
            access.AddAbsence(absence);
        }

        public void UpdateAbsence(Absence ancienne, Absence modifiee)
        {
            access.UpdateAbsence(ancienne, modifiee);
        }

        public void DeleteAbsence(Absence absence)
        {
            access.DeleteAbsence(absence);
        }
        /// <summary>
        /// Vérifie si une absence chevauche une autre
        /// </summary>
        public bool AbsenceChevauche(Personnel personnel, DateTime dateDebut, DateTime dateFin, DateTime? ancienneDateDebut)
        {
            return access.AbsenceChevauche(personnel, dateDebut, dateFin, ancienneDateDebut);
        }
    }
}