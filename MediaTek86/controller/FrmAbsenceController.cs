using System.Collections.Generic;
using MediaTek86.dal;
using MediaTek86.modele;

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
    }
}