namespace CassettaDiSicurezza
{
    class OggettoSegreto
    {

        private string identificatore;
        private double valoreDichiarato;
        private double valoreAssicurato;

        public OggettoSegreto(string identificatore, double valoreDichiarato)
        {
            this.identificatore = identificatore;
            this.valoreDichiarato = valoreDichiarato;
            this.valoreAssicurato = 0;
        }

        public string Identificatore
        {
            get { return identificatore; }
        }

        public double ValoreDichiarato
        {
            get { return valoreDichiarato; }
        }

        public double ValoreAssicurato
        {
            get { return valoreAssicurato; }
            protected set { valoreAssicurato = value; }
        }

    }
    class DocumentoLegale : OggettoSegreto
    {
        private string tipo;
        public DocumentoLegale(string identificatore, double valoreDichiarato, string tipo) : base(identificatore, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            if (ValoreDichiarato >= 100)
            {
                ValoreAssicurato = ValoreDichiarato;
            }
            else
            {
                ValoreAssicurato = 50;
            }
        }
    }

    class GioielloPrezioso : OggettoSegreto
    {
        private string tipo;
        public GioielloPrezioso(string identificatore, double valoreDichiarato, string tipo) : base(identificatore, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            ValoreAssicurato = ValoreDichiarato * 5;
        }
    }

    class ChiaveDiAccesso : OggettoSegreto
    {
        private string tipo;
        public ChiaveDiAccesso(string identificatore, double valoreDichiarato, string tipo) : base(identificatore, valoreDichiarato)
        {
            this.tipo = tipo;
            CalcolaValoreAssicurato();
        }

        public void CalcolaValoreAssicurato()
        {
            ValoreAssicurato = ValoreDichiarato * 1000;
        }
    }

    class CassettaDiSicurezza
    {
        private string codiceSeriale;
        private string pin;
        private string produttore;
        private string codiceSegreto;
        private OggettoSegreto oggetto;

        public string CodiceSeriale
        {
            get { return codiceSeriale; }
        }

        public OggettoSegreto Oggetto
        {
            get { return oggetto; }
        }

        public CassettaDiSicurezza(string codiceSeriale, string pin)
        {
            this.codiceSeriale = codiceSeriale;
            this.pin = pin;
            this.oggetto = null;
        }

        public void InserisciOggetto(OggettoSegreto oggetto, string pin)
        {
            if (this.pin == pin && this.oggetto == null)
            {
                this.oggetto = oggetto;
            }
            return;
        }

        public void RimuoviOggetto(string pin)
        {
            if (this.pin == pin && this.oggetto != null)
            {
                this.oggetto = null;
            }
            return;
        }
    }

    class CassettaDiSicurezzaSpeciale : CassettaDiSicurezza
    {
        private double valoreAssicurato;

        public double ValoreAssicurato
        {
            get { return valoreAssicurato; }
        }

        public CassettaDiSicurezzaSpeciale(string codiceSeriale, string pin) :
base(codiceSeriale, pin)
        { }

        public void CalcolaValoreAssicurato()
        {
            OggettoSegreto oggetto = Oggetto;

            if (oggetto == null)
            {
                valoreAssicurato = 0;
            }
            else if (oggetto is GioielloPrezioso)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.9;
            }
            else if (oggetto is DocumentoLegale)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.8;
            }
            else if (oggetto is ChiaveDiAccesso)
            {
                valoreAssicurato = oggetto.ValoreAssicurato * 0.7;
            }
            else
            {
                valoreAssicurato = 0;
            }
        }
    }


    class Program
    {
        static void Main()
        {
            CassettaDiSicurezza cassetta1 = new CassettaDiSicurezza("123", "09876");
            CassettaDiSicurezza cassetta2 = new CassettaDiSicurezza("456", "54321");
            CassettaDiSicurezza cassetta3 = new CassettaDiSicurezza("789", "11121");

            GioielloPrezioso gioiello = new GioielloPrezioso("R098", 1000, "collana");

            DocumentoLegale documento = new DocumentoLegale("T076", 50, "testamento");

            ChiaveDiAccesso chiave = new ChiaveDiAccesso("K054", 5, "fisico");

            cassetta1.InserisciOggetto(gioiello, "09876");
            cassetta2.InserisciOggetto(documento, "54321");
            cassetta3.InserisciOggetto(chiave, "11121");

            Console.WriteLine("Cassette di Sicurezza Normali:");
            VisualizzaCassetta(cassetta1);
            VisualizzaCassetta(cassetta2);
            VisualizzaCassetta(cassetta3);

            CassettaDiSicurezzaSpeciale cassettaSpec1 = new CassettaDiSicurezzaSpeciale("A100", "11111");
            CassettaDiSicurezzaSpeciale cassettaSpec2 = new CassettaDiSicurezzaSpeciale("A200", "22222");
            CassettaDiSicurezzaSpeciale cassettaSpec3 = new CassettaDiSicurezzaSpeciale("A300", "33333");

            OggettoSegreto oggettoRimosso;

            oggettoRimosso = cassetta1.Oggetto;
            cassetta1.RimuoviOggetto("09876");
            cassettaSpec1.InserisciOggetto(oggettoRimosso, "11111");

            oggettoRimosso = cassetta2.Oggetto;
            cassetta2.RimuoviOggetto("54321");
            cassettaSpec2.InserisciOggetto(oggettoRimosso, "22222");

            oggettoRimosso = cassetta3.Oggetto;
            cassetta3.RimuoviOggetto("11121");
            cassettaSpec3.InserisciOggetto(oggettoRimosso, "33333");


            cassettaSpec1.CalcolaValoreAssicurato();
            cassettaSpec2.CalcolaValoreAssicurato();
            cassettaSpec3.CalcolaValoreAssicurato();

            Console.WriteLine("Cassette di Sicurezza Speciali:");
            VisualizzaCassettaSpeciale(cassettaSpec1);
            VisualizzaCassettaSpeciale(cassettaSpec2);
            VisualizzaCassettaSpeciale(cassettaSpec3);
            Console.ReadLine();
        }

        static void VisualizzaCassetta(CassettaDiSicurezza cassetta)
        {
            if (cassetta.Oggetto != null)
            {
                OggettoSegreto oggetto = cassetta.Oggetto;
                Console.WriteLine(
                    "identificatore: " + oggetto.Identificatore +
                    ", Tipo: " + oggetto.GetType().Name +
                    ", Valore Dichiarato: " + oggetto.ValoreDichiarato +
                    ", Valore Assicurato: " + oggetto.ValoreAssicurato
                );
            }
            else
            {
                Console.WriteLine("La cassetta è vuota.");
            }
        }

        static void VisualizzaCassettaSpeciale(CassettaDiSicurezzaSpeciale cassettaSpeciale)
        {
            if (cassettaSpeciale.Oggetto != null)
            {
                OggettoSegreto oggetto = cassettaSpeciale.Oggetto;
                Console.WriteLine(
                    "identificatore: " + oggetto.Identificatore +
                    ", Tipo: " + oggetto.GetType().Name +
                    ", Valore Dichiarato: " + oggetto.ValoreDichiarato +
                    ", Valore Assicurato Oggetto: " + oggetto.ValoreAssicurato +
                    ", Valore Assicurato Cassetta: " + cassettaSpeciale.ValoreAssicurato
                );
            }
            else
            {
                Console.WriteLine("La cassetta speciale è vuota.");
            }
        }
    }


}