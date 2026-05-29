using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTek86.vue;

namespace MediaTek86
{
    /// <summary>
    /// Application de gestion du personnel des médiathèques du réseau MediaTek86 :
    /// gestion du personnel, de leur affectation à un service et de leurs absences.
    /// </summary>
    internal class NamespaceDoc
    {
    }

    /// <summary>
    /// Classe principale de l'application
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmConnexion());
        }
    }
}