using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace loginAcquisti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtUtente.Focus();
            txtPrezzo.IsEnabled = false;
            txtQuantita.IsEnabled = false;
            cmbProdotto.IsEnabled = false;
            btnPulisci.IsEnabled = false;
            btnStampa.IsEnabled = false;
            ltbRisultato.IsEnabled = false;
            btnRimuoviSelezione.IsEnabled = false;
        }

        private const string PASSWORD = "password";
        private string[] prodotti = new string[] { "Felpa", "T-Shirt", "Polo", "Pantalone", "Calzini", "Mutande" };
        private string utente;

        private void btnAccedi_Click(object sender, RoutedEventArgs e)
        {
            utente = txtUtente.Text;
            string pass = txtPassword.Text;

            if (utente != "" && utente != null && pass == PASSWORD)
            {
                txtUtente.IsEnabled = false;
                txtPassword.IsEnabled = false;
                btnAccedi.IsEnabled = false;

                txtPrezzo.IsEnabled = true;
                txtQuantita.IsEnabled = true;
                cmbProdotto.IsEnabled = true;
                btnPulisci.IsEnabled = true;
                btnStampa.IsEnabled = true;
            }
            else if (utente == "" || utente == null)
            {
                MessageBox.Show("Inserire un utente valido", "RITENTA", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUtente.Text = "";
                txtPassword.Text = "";
                txtUtente.Focus();
            }
            else if (pass != PASSWORD)
            {
                MessageBox.Show("Password errata", "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                txtPassword.Text = "";
                txtPassword.Focus();
            }
        }

        private void cmbProdotto_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string p in prodotti)
            {
                cmbProdotto.Items.Add(p);
            }
        }

        private void btnStampa_Click(object sender, RoutedEventArgs e)
        {
            if (txtQuantita.Text != "" && txtPrezzo.Text != "" && cmbProdotto.SelectedIndex >= 0)
            {
                try
                {
                    int quantità = int.Parse(txtQuantita.Text);
                    double prezzo = double.Parse(txtPrezzo.Text);

                    double totale = quantità * prezzo;

                    ltbRisultato.Items.Add($"Il cliente {txtUtente.Text} ha acquistato il prodotto {cmbProdotto.SelectedItem} per un totale di {totale}€");

                    txtQuantita.Text = "";
                    txtPrezzo.Text = "";
                    cmbProdotto.SelectedIndex = -1;

                    ltbRisultato.IsEnabled = true;
                    btnRimuoviSelezione.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    txtQuantita.Text = "";
                    txtPrezzo.Text = "";
                    cmbProdotto.SelectedIndex = -1;
                }
            }
            else
            {
                MessageBox.Show("Inserisci i valori!", "ATTENZIONE", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                txtQuantita.Text = "";
                txtPrezzo.Text = "";
                cmbProdotto.SelectedIndex = -1;
            }
        }

        private void btnPulisci_Click(object sender, RoutedEventArgs e)
        {
            txtQuantita.Text = "";
            txtPrezzo.Text = "";
            cmbProdotto.SelectedIndex = -1;
        }

        private void btnRimuoviSelezione_Click(object sender, RoutedEventArgs e)
        {
            int selezione = ltbRisultato.SelectedIndex;
            if (selezione >= 0)
            {
                ltbRisultato.Items.RemoveAt(selezione);
            }
            else
            {
                MessageBox.Show("Non è stato selezionato nessun elemento", "ERRORE", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (ltbRisultato.Items.Count == 0)
            {
                ltbRisultato.IsEnabled = false;
                btnRimuoviSelezione.IsEnabled = false;
            }
        }
    }
}
