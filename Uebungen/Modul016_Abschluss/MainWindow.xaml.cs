using System;
using System.Collections.Generic;
using System.IO;
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
using Modul016_Abschluss.Enums;
using Modul016_Abschluss.Models;
using Newtonsoft.Json;

namespace Modul016_Abschluss
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string stammordner;
        event EventHandler OnNeuerMitarbeiter;

        List<Mitarbeiter> mitarbeiterList;
        public MainWindow()
        {
            InitializeComponent();
            stammordner = DateTime.Now.ToString("HH_mm_ss");
            mitarbeiterList = new List<Mitarbeiter>();
            dgMitarbeiter.ItemsSource = mitarbeiterList;

            OnNeuerMitarbeiter += new EventHandler(MitarbeiterOrdnerAnlegen);
            OnNeuerMitarbeiter += new EventHandler(MitarbeiterStammdatenAnlegen);
            OnNeuerMitarbeiter += new EventHandler(UpdateDataGrid);
        }

        private void btnSpeichern_Click(object sender, RoutedEventArgs e)
        {
            decimal gehalt;

            if (decimal.TryParse(tbGehalt.Text, out gehalt))
            {
                Mitarbeiter neuerMitarbeiter = new Mitarbeiter(
                    tbVorname.Text,
                    tbNachname.Text,
                    dpGeburtstag.DisplayDate,
                    (Abteilung)cbAbteilung.SelectedItem,
                    tbJobtitel.Text,
                    gehalt,
                    dpEinstiegsdatum.DisplayDate);

                MitarbeiterHinzufuegen(neuerMitarbeiter);
            }
            else
            {
                MessageBox.Show("Das Gehalt muss eine Zahl sein!");
            }
        }
        private void MitarbeiterHinzufuegen(Mitarbeiter mitarbeiter)
        {
            mitarbeiter.MitarbeiterIdGenerieren(mitarbeiterList.Select(x => x.Mitarbeiteridentifizierung));

            mitarbeiterList.Add(mitarbeiter);
            OnNeuerMitarbeiter(mitarbeiter, EventArgs.Empty);
        }
        private void MitarbeiterOrdnerAnlegen(object sender, EventArgs eventArgs)
        {
            if (sender is Mitarbeiter)
            {
                Mitarbeiter mitarbeiter = (Mitarbeiter)sender;
                Directory.CreateDirectory($"{stammordner}/{mitarbeiter.Mitarbeiteridentifizierung}");
            }
        }
        private void MitarbeiterStammdatenAnlegen(object sender, EventArgs eventArgs)
        {
            if (sender is Mitarbeiter)
            {
                Mitarbeiter mitarbeiter = (Mitarbeiter)sender;

                using (StreamWriter sw = new StreamWriter($"{stammordner}/{mitarbeiter.Mitarbeiteridentifizierung}/stammdaten.json"))
                {
                    string result = JsonConvert.SerializeObject(mitarbeiter);
                    sw.Write(result);
                }
            }
        }
        private void UpdateDataGrid(object sender, EventArgs eventArgs)
        {
            dgMitarbeiter.Items.Refresh();
        }
    }
}
