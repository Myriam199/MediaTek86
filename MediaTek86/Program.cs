using System;
using System.Windows.Forms;
using MediaTek86.controleur;
using MediaTek86.vue;

namespace MediaTek86
{
    /// <summary>
    /// Classe principale contenant le point d'entree de l'application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Point d'entree principal de l'application.
        /// Cree le controleur et affiche la fenetre d'authentification.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controle controle = new Controle();
            Application.Run(new FrmAuthentification(controle));
        }
    }
}
