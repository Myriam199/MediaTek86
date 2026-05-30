using System;
using System.Windows.Forms;
using MediaTek86.controleur;

namespace MediaTek86.vue
{
    /// <summary>
    /// Fenetre d'authentification du responsable (cas d'utilisation "se connecter").
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        /// <summary>
        /// Controleur de l'application.
        /// </summary>
        private readonly Controle controle;

        /// <summary>
        /// Constructeur : initialise la fenetre et le controleur.
        /// </summary>
        /// <param name="controle">Le controleur de l'application.</param>
        public FrmAuthentification(Controle controle)
        {
            InitializeComponent();
            this.controle = controle;
        }

        /// <summary>
        /// Tentative de connexion lors du clic sur le bouton "Se connecter".
        /// Verifie que les champs sont remplis puis controle le couple login/pwd.
        /// </summary>
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string pwd = txtPwd.Text;
            if (login == "" || pwd == "")
            {
                MessageBox.Show("Tous les champs doivent etre remplis.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (controle.ControleAuthentification(login, pwd))
            {
                FrmGestionPersonnel frmGestionPersonnel = new FrmGestionPersonnel(controle);
                frmGestionPersonnel.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Login et/ou mot de passe incorrect(s).", "Erreur",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPwd.Clear();
                txtLogin.Focus();
            }
        }

        /// <summary>
        /// Fermeture de l'application lors du clic sur le bouton "Annuler".
        /// </summary>
        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
