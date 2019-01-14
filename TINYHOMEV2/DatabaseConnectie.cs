using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINYHOMEV2
{
    class DatabaseConnectie
    {
        private MySqlConnection connectie;
        private string server;
        private string database;
        private string uid;
        private string wachtwoord;

        public DatabaseConnectie()
        {
            Initialize();
        }

        private void Initialize()
        {
            // database gegevens (connectiestring)

            //server = "studmysql01.fhict.local";
            server = "192.168.15.54";
            database = "dbi416683";
            uid = "dbi416683";
            wachtwoord = "Tinyhome";
            string connectieString;
            connectieString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + wachtwoord + ";";

            connectie = new MySqlConnection(connectieString);
        }

        private bool OpenConnectie()
        {
            try
            {
                // maak connectie met de database
                connectie.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //foutmeldingen worden hier behandeld (getoond in een messagebox)
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password for database, please try again");
                        break;
                    default:
                        Console.WriteLine(ex.Message);
                        break;
                }
                return false;
            }
        }

        private bool SluitConnectie()
        {
            try
            {
                // connectie met de database wordt gesloten
                connectie.Close();
                return true;
            }
            catch (MySqlException ex) // foutmelding wordt opgevangen en getoond in een messagebox
            {
                // foutmelding wordt hier behandeld (getoond in messagebox)
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Gebruiker inloggen(string gebruikersnaam, string wachtwoord)
        {
            string versleuteldWachtwoord = EncodePasswordToBase64(wachtwoord); // versleutel het wachtwoord
            try
            {
                if (this.OpenConnectie())
                {
                    // sql-query om gegevens op te halen uit de database
                    string query = "SELECT * FROM Gebruiker WHERE Gebruikersnaam=@naam AND Wachtwoord=@wachtwoord";

                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    // query wordt hier aangevuld met gegevens
                    cmd.Parameters.AddWithValue("@naam", gebruikersnaam);
                    cmd.Parameters.AddWithValue("@wachtwoord", versleuteldWachtwoord);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int rowCount = 0;
                    Gebruiker gebruiker = new Gebruiker();

                    while (dataReader.Read()) // zolang er gegevens zijn, worden er nieuwe gebruikers aangemaakt
                    {
                        gebruiker = new Gebruiker
                        {
                            Id = (int)dataReader["idGebruiker"],
                            Naam = (string)dataReader["Gebruikersnaam"]
                        };
                        rowCount++;
                    }
                    this.SluitConnectie();
                    if (rowCount == 1 && gebruiker.Naam != "") return gebruiker; // als er meer dan 1 gebruiker is, wordt deze doorgestuurd, anders null
                    else return null;
                }
            }
            catch(Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
            return null;
        }

        public static string EncodePasswordToBase64(string password)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] inArray = HashAlgorithm.Create("SHA1").ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        public void nieuweArduino(Arduino arduino)
        {
            try
            {
                if (this.OpenConnectie()) // als de connectie met de database geopend is dan ...
                {                    
                    // sql-query om gegevens op te halen uit de database
                    string query = "INSERT INTO arduino (Verbinding, Poort, BaudRate, CommandBegin," +
                   "CommandEnd)" +
                   "VALUES(@VerbindingNaam, @Poort, @BaudRate, @CommandBegin," +
                   "@CommandEnd)";

                    // query wordt hier aangevuld met gegevens
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    cmd.Parameters.AddWithValue("@VerbindingNaam", arduino.Naam);
                    cmd.Parameters.AddWithValue("@Poort", arduino.Poort);
                    cmd.Parameters.AddWithValue("@BaudRate", arduino.Baudrate);
                    cmd.Parameters.AddWithValue("@CommandBegin", arduino.Commandbegin);
                    cmd.Parameters.AddWithValue("@CommandEnd", arduino.Commandend);

                    // query wordt hier uitgevoerd
                    cmd.ExecuteNonQuery();
                    // connectie met database wordt weer gesloten.
                    this.SluitConnectie();

                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
        } 

        public List<Arduino> arduinos()
        {
            // nieuwe lijst van het object Arduino wordt aangemaakt
            List<Arduino> arduino = new List<Arduino>();

            // sql-query om gegevens op te halen uit de database
            string query = "SELECT * FROM arduino";
            try 
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    // datareader wordt gestart
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read()) // zolang er data wordt uitgelezen worden er nieuwe objecten aangemaakt van de klasse Arduino 
                    {
                        Arduino arduinos = new Arduino
                        {
                            Id = (int)(datareader["idArduino"]),
                            Baudrate = (int)(datareader["BaudRate"]),
                            Commandbegin = ConverteerString(datareader["CommandBegin"]),
                            Commandend = ConverteerString(datareader["CommandEnd"]),
                            Naam = ConverteerString(datareader["Verbinding"]),
                            Poort = ConverteerString(datareader["Poort"])
                        };
                        // het object wordt toegevoegd in de list
                        arduino.Add(arduinos);
                    }
                    // connectie met database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
            return arduino;
        }
        public Arduino Arduinolaatste()
        {
            Arduino arduino = new Arduino();

            // sql-query om gegevens op te halen uit de database
            string query = "SELECT * from arduino ORDER BY idArduino DESC LIMIT 1;";
            try
            {
                if (this.OpenConnectie()) // als de connectie met de database geopend is
                {
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    // datareader wordt gestart
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read()) // zolang er data wordt uitgelezen worden er nieuwe objecten aangemaakt van de klasse Arduino 
                    {
                        Arduino arduinos = new Arduino
                        {
                            Id = (int)(datareader["idArduino"]),
                            Baudrate = (int)(datareader["BaudRate"]),
                            Commandbegin = ConverteerString(datareader["CommandBegin"]),
                            Commandend = ConverteerString(datareader["CommandEnd"]),
                            Naam = ConverteerString(datareader["Verbinding"]),
                            Poort = ConverteerString(datareader["Poort"])
                        };
                        arduino = arduinos;
                    }
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
            return arduino;
        }

        private string ConverteerString(Object obj)
        {
            // als de database een lege waarde bevat wordt deze hier geconverteerd naar null, en anders naar een string
            return (DBNull.Value != obj) ? (string)obj : null;
        }

        public void Updatelampen(lamp lamp)
        {
            // sql-query om gegevens op te halen uit de database
            string query = "UPDATE lamp SET Staat = @staat WHERE Naam = @naam";
            try
            {
                if (this.OpenConnectie()) // als de connectie geopend is
                {
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    // sql query wordt met waardes aangevuld
                    cmd.Parameters.AddWithValue("@Staat", lamp.Status);
                    cmd.Parameters.AddWithValue("@naam", lamp.Naam);

                    // query wordt hier uitgevoerd
                    cmd.ExecuteNonQuery();
                    // connectie met database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
        }

        public void UpdateRGB(string hex, string naam)
        {
            // sql-query om gegevens op te halen uit de database
            string query = "UPDATE lamp SET HEXWaarde = @hex WHERE Naam = @naam";
            try
            {
                if (this.OpenConnectie())
                {           
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    // query wordt aangevuld met waarden
                    cmd.Parameters.AddWithValue("@hex", hex);
                    cmd.Parameters.AddWithValue("@naam", naam);
                    // query wordt uitgevoerd
                    cmd.ExecuteNonQuery();
                    // connectie met de database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
        }

        public void Thermostaat(decimal luchtvochtigheid, decimal temperatuur)
        {
            // sql-query om gegevens op te halen uit de database
            string query = "UPDATE thermostaat SET Luchtvochtigheid = @Luchtvochtigheid, Temperatuur = @Temperatuur";
            try
            {
                if (this.OpenConnectie())
                {                    
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    // query wordt aangevuld met waarden
                    cmd.Parameters.AddWithValue("@Luchtvochtigheid", luchtvochtigheid);
                    cmd.Parameters.AddWithValue("@Temperatuur", temperatuur);
                    // query wordt uitgevoerd
                    cmd.ExecuteNonQuery();                   
                    // connectie met de database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
        }

        public bool FillCheckBox(string naam)
        {                   
            // nieuw sqlcommand aangemaakt met de query en de connectiestring
            string query = "SELECT Staat from lamp WHERE Naam = @naam";
            byte staat = 0;
            try
            {
                if (this.OpenConnectie()) // als de connectie geopend is dan ...
                {
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    // query aangevuld met waarden
                    cmd.Parameters.AddWithValue("@naam", naam);
                    // datareader wordt gestart
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read()) // zolang de datareader informatie kan uitlezen wordt de staat opgeslagen
                    {
                        staat = (byte)(datareader["staat"]);
                    }
                    // connectie met database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }

            if (staat == 1) // als de staat gelijk is aan 1 dan wordt true teruggestuurd, anders false
            {
                return true;
            }
            else { return false; }
        }

        public void GebruikerToevoegen(Gebruiker gb, string wachtwoord)
        {
            // wachtwoord wordt versleuteld
            string versleuteldWachtwoord = EncodePasswordToBase64(wachtwoord);
            // nieuw sqlcommand aangemaakt met de query en de connectiestring
            string query = "INSERT INTO gebruiker (Gebruikersnaam, Wachtwoord) VALUES (@gebruikersnaam, @wachtwoord)";
            try
            {
                if (this.OpenConnectie()) // als de connectie geopend is
                {
                    // nieuw sqlcommand aangemaakt met de query en de connectiestring
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    // query aangevuld met waarden
                    cmd.Parameters.AddWithValue("@gebruikersnaam", gb.Naam);
                    cmd.Parameters.AddWithValue("@wachtwoord", versleuteldWachtwoord);
                    // query wordt uitgevoerd
                    cmd.ExecuteNonQuery();
                    // connectie met database wordt gesloten
                    this.SluitConnectie();
                }
            }
            catch (Exception exc) // foutmelding wordt opgevangen en getoond in het output venster
            {
                Console.WriteLine(exc);
            }
        }
    }
}
