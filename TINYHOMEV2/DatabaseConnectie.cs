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
            server = "studmysql01.fhict.local";
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
                connectie.Open();
                return true;
            }
            catch (MySqlException ex)
            {

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
                connectie.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public Gebruiker inloggen(string gebruikersnaam, string wachtwoord)
        {
            string versleuteldWachtwoord = EncodePasswordToBase64(wachtwoord);
            try
            {
                if (this.OpenConnectie())
                {
                    string query = "SELECT * FROM Gebruiker WHERE Gebruikersnaam=@naam AND Wachtwoord=@wachtwoord";

                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    cmd.Parameters.AddWithValue("@naam", gebruikersnaam);
                    cmd.Parameters.AddWithValue("@wachtwoord", versleuteldWachtwoord);

                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    int rowCount = 0;
                    Gebruiker gebruiker = new Gebruiker();

                    while (dataReader.Read())
                    {
                        gebruiker = new Gebruiker
                        {
                            Id = (int)dataReader["idGebruiker"],
                            Naam = (string)dataReader["Gebruikersnaam"]
                        };
                        rowCount++;
                    }
                    this.SluitConnectie();
                    if (rowCount == 1 && gebruiker.Naam != "") return gebruiker;
                    else return null;
                }
            }
            catch(Exception exc)
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
                if (this.OpenConnectie())
                {
                    string query = "INSERT INTO arduino (Verbinding, Poort, BaudRate, CommandBegin," +
                   "CommandEnd)" +
                   "VALUES(@VerbindingNaam, @Poort, @BaudRate, @CommandBegin," +
                   "@CommandEnd)";

                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    cmd.Parameters.AddWithValue("@VerbindingNaam", arduino.Naam);
                    cmd.Parameters.AddWithValue("@Poort", arduino.Poort);
                    cmd.Parameters.AddWithValue("@BaudRate", arduino.Baudrate);
                    cmd.Parameters.AddWithValue("@CommandBegin", arduino.Commandbegin);
                    cmd.Parameters.AddWithValue("@CommandEnd", arduino.Commandend);

                    cmd.ExecuteNonQuery();
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
            List<Arduino> arduino = new List<Arduino>();

            string query = "SELECT * FROM arduino";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read())
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
                        arduino.Add(arduinos);
                    }
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return arduino;
        }
        public Arduino Arduinolaatste()
        {
            Arduino arduino = new Arduino();

            string query = "SELECT * from arduino ORDER BY idArduino DESC LIMIT 1;";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read())
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
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
            return arduino;
        }

        private string ConverteerString(Object obj)
        {
            return (DBNull.Value != obj) ? (string)obj : null;
        }

        public void Updatelampen(lamp lamp)
        {
            string query = "UPDATE lamp SET Staat = @staat WHERE Naam = @naam";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    cmd.Parameters.AddWithValue("@Staat", lamp.Status);
                    cmd.Parameters.AddWithValue("@naam", lamp.Naam);

                    cmd.ExecuteNonQuery();
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public void UpdateRGB(string hex, string naam)
        {
            string query = "UPDATE lamp SET HEXWaarde = @hex WHERE Naam = @naam";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    cmd.Parameters.AddWithValue("@hex", hex);
                    cmd.Parameters.AddWithValue("@naam", naam);

                    cmd.ExecuteNonQuery();
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public void Thermostaat(decimal luchtvochtigheid, decimal temperatuur)
        {
            string query = "UPDATE thermostaat SET Luchtvochtigheid = @Luchtvochtigheid, Temperatuur = @Temperatuur";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    cmd.Parameters.AddWithValue("@Luchtvochtigheid", luchtvochtigheid);
                    cmd.Parameters.AddWithValue("@Temperatuur", temperatuur);

                    cmd.ExecuteNonQuery();
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public bool FillCheckBox(string naam)
        {
            string query = "SELECT Staat from lamp WHERE Naam = @naam";
            byte staat = 0;
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);
                    cmd.Parameters.AddWithValue("@naam", naam);
                    MySqlDataReader datareader = cmd.ExecuteReader();

                    while (datareader.Read())
                    {
                        staat = (byte)(datareader["staat"]);
                    }
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }

            if (staat == 1)
            {
                return true;
            }
            else { return false; }
        }

        public void GebruikerToevoegen(Gebruiker gb, string wachtwoord)
        {
            string versleuteldWachtwoord = EncodePasswordToBase64(wachtwoord);
            string query = "INSERT INTO gebruiker (Gebruikersnaam, Wachtwoord) VALUES (@gebruikersnaam, @wachtwoord)";
            try
            {
                if (this.OpenConnectie())
                {
                    MySqlCommand cmd = new MySqlCommand(query, connectie);

                    cmd.Parameters.AddWithValue("@gebruikersnaam", gb.Naam);
                    cmd.Parameters.AddWithValue("@wachtwoord", versleuteldWachtwoord);

                    cmd.ExecuteNonQuery();
                    this.SluitConnectie();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
