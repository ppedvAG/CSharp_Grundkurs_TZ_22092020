using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modul016_Abschluss.Enums;

namespace Modul016_Abschluss.Models
{
    public class Mitarbeiter : Person
    {
        public string Mitarbeiteridentifizierung { get; private set; }
        public Abteilung Abteilung { get; set; }
        public string Jobtitel { get; set; }
        public decimal Gehalt { get; set; }
        public DateTime Einstiegsdatum { get; set; }

        public Mitarbeiter(string vorname, string nachname, DateTime geburtstag, Abteilung abteilung, string jobtitel, decimal gehalt, DateTime einstiegsdatum) : base(vorname, nachname, geburtstag)
        {
            this.Abteilung = abteilung;
            this.Jobtitel = jobtitel;
            this.Gehalt = gehalt;
            this.Einstiegsdatum = einstiegsdatum;
        }

        public void MitarbeiterIdGenerieren(IEnumerable<string> mitarbeiterIds)
        {

            string mitarbeiterId = $"{this.Vorname[0]}{this.Nachname[0]}";
            string mitarbeiterIdSuffix = "";

            if (mitarbeiterIds != null)
            {
                mitarbeiterIds = mitarbeiterIds.Where(x => x.StartsWith(mitarbeiterId));
                if (mitarbeiterIds != null)
                {
                    for (int i = 0; i <= mitarbeiterIds.Count(); i++)
                    {
                        string suffix = i.ToString().PadLeft(4, '0');
                        if (!mitarbeiterIds.Contains($"{mitarbeiterId}_{suffix}"))
                        {
                            mitarbeiterIdSuffix = $"_{suffix}";
                            break;
                        }
                    }
                }
            }
            if (mitarbeiterIdSuffix == "")
                mitarbeiterIdSuffix = "_0000";

            this.Mitarbeiteridentifizierung = mitarbeiterId + mitarbeiterIdSuffix;
        }
    }
}