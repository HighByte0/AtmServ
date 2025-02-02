using System;
using System.Windows.Forms;

namespace ATMLicenceMgt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }


        private void btn_GenerateData_Click(object sender, EventArgs e)
        {
            string bankName = txt_BankName.Text;
            string atmNumber = txt_AtmNumber.Text;
            string errorMsg = string.Empty;
            if (string.IsNullOrEmpty(bankName))
            {
                errorMsg += "Merci de saisir le nom de la banque ";
            }

            if (string.IsNullOrEmpty(atmNumber))
            {
                errorMsg += " Merci de saisir le nombre d'ATM ";
            }

            //verifier que atmNumber est numerique
            if (!string.IsNullOrEmpty(atmNumber))
            {
                int result = 0;
                if (!Int32.TryParse(atmNumber, out result))
                    errorMsg += "Le nombre d'ATM doit etre un entier ";
            }
            if (string.IsNullOrEmpty(errorMsg))
            {
                lblError.Text = "";

                //string filepath = ConfigurationManager.AppSettings["LicenceRequestPath"];

                //string licenceRequestPath = "d:";

                //string filepath = licenceRequestPath + "\\LicenceRequest.txt";

                string filepath = Properties.Settings.Default.LicenceRequestPath;

                try
                {
                    ContactWriter.ToFile(@filepath, CreateContact(atmNumber, bankName));
                    lblError.Text = "La génération du fichier LicenceRequest a été bien effectuée ";
                }
                catch (Exception ex)
                {
                    lblError.Text = "Un problème est survenu lors de la génération du fichier LicenceRequest: " + ex.Message;
                }

            }
            else
            {
                lblError.Text = errorMsg;

            }

        }

        private static Contact CreateContact(string atmNumber, string bankName)
        {
            return ContactFactory.Create<Contact>(atmNumber, bankName);
        }

    }
}
