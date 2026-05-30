using System;
using System.Windows.Forms;
using MediaTek86.controller;
using MediaTek86.modele;

namespace MediaTek86.vue
{
    /// <summary>
    /// Formulaire d'ajout ou de modification d'une absence
    /// </summary>
    public partial class FrmAbsenceModif : Form
    {
        /// <summary>
        /// Contrôleur du formulaire
        /// </summary>
        private readonly FrmAbsenceController controller;

        /// <summary>
        /// Personnel concerné
        /// </summary>
        private readonly Personnel personnel;

        /// <summary>
        /// Absence à modifier (null si ajout)
        /// </summary>
        private readonly Absence absence;

        /// <summary>
        /// Constructeur pour l'ajout
        /// </summary>
        public FrmAbsenceModif(Personnel personnel)
        {
            InitializeComponent();
            controller = new FrmAbsenceController();
            this.personnel = personnel;
            this.Text = "Ajouter une absence";
            RemplirComboMotifs();
        }

        /// <summary>
        /// Constructeur pour la modification
        /// </summary>
        public FrmAbsenceModif(Personnel personnel, Absence absence)
        {
            InitializeComponent();
            controller = new FrmAbsenceController();
            this.personnel = personnel;
            this.absence = absence;
            this.Text = "Modifier une absence";
            RemplirComboMotifs();
            dtpDateDebut.Value = absence.DateDebut;
            dtpDateFin.Value = absence.DateFin;
            cboMotif.SelectedItem = absence.Motif;
        }

        /// <summary>
        /// Remplit la ComboBox des motifs
        /// </summary>
        private void RemplirComboMotifs()
        {
            cboMotif.DataSource = controller.GetLesMotifs();
        }

        /// <summary>
        /// Clic sur Enregistrer
        /// </summary>
        private void btnEnregistrer_Click(object sender, EventArgs e)
        {
            // Vérification cohérence des dates
            if (dtpDateFin.Value < dtpDateDebut.Value)
            {
                MessageBox.Show("La date de fin doit être postérieure à la date de début.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Vérification chevauchement
            DateTime? ancienneDateDebut = absence?.DateDebut;
            if (controller.AbsenceChevauche(personnel, dtpDateDebut.Value, dtpDateFin.Value, ancienneDateDebut))
            {
                MessageBox.Show("Une absence est déjà programmée sur ce créneau.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Motif motif = (Motif)cboMotif.SelectedItem;
            Absence nouvelle = new Absence(personnel, dtpDateDebut.Value, dtpDateFin.Value, motif);

            if (absence == null)
            {
                controller.AddAbsence(nouvelle);
            }
            else
            {
                DialogResult confirm = MessageBox.Show(
                    "Confirmer l'enregistrement des modifications ?",
                    "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                controller.UpdateAbsence(absence, nouvelle);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Clic sur Annuler
        /// </summary>
        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}