using System;
using System.Windows.Forms;
using MediaTek86.controller;

namespace MediaTek86.vue
{
    /// <summary>
    /// Formulaire de connexion
    /// </summary>
    public partial class FrmConnexion : Form
    {
        /// <summary>
        /// Contrôleur du formulaire
        /// </summary>
        private readonly FrmConnexionController controller;

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmConnexion()
        {
            InitializeComponent();
            controller = new FrmConnexionController();
        }

        /// <summary>
        /// Demande de connexion : vérification du login et mot de passe
        /// </summary>
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string pwd = txtMotDePasse.Text;

            if (login.Equals("") || pwd.Equals(""))
            {
                MessageBox.Show("Veuillez remplir tous les champs.",
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (controller.ControleAuthentification(login, pwd))
            {
                FrmPersonnel frmPersonnel = new FrmPersonnel();
                frmPersonnel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect.",
                    "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Package contenant les vues (formulaires) de l'application.
    /// </summary>
    internal class NamespaceDoc
    {
    }
}