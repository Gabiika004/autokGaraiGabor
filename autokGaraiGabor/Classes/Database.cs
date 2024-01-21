using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace autokGaraiGabor
{
    internal class Database
    {
        MySqlConnection conn = null;
        MySqlCommand sql = null;

        public Database()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "autok";
            builder.CharacterSet = "utf8";
            conn = new MySqlConnection(builder.ConnectionString);
            sql = conn.CreateCommand();
            try
            {
                ConnectionOpen();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
            finally {ConnectionClose();}
        }

        internal List<Auto> getAllAuto()
        {
            List<Auto> autok = new List<Auto>();
            sql.CommandText = "SELECT * FROM `auto` ORDER BY  `marka`";
            try
            {
                ConnectionOpen();
                using (MySqlDataReader dr = sql.ExecuteReader())
                {
                    while (dr.Read())
                    {
                                string rendszam = dr.GetString("rendszam");
                                string marka = dr.GetString("marka");
                                string modell = dr.GetString("modell");
                                Int16 gyartEv = dr.GetInt16("gyartasiev");
                                DateTime forgalmiErv = dr.GetDateTime("forgalmiErvenyesseg");
                                decimal vetelar = dr.GetDecimal("vetelar");
                                int kmAllas = dr.GetInt32("kmallas");
                                int hengerurtart = dr.GetInt32("hengerűrtartalom");
                                int tomeg = dr.GetInt32("tomeg");
                                int teljesitmeny = dr.GetInt32("teljesitmeny");
                        autok.Add(new Auto(rendszam, marka, modell, gyartEv, forgalmiErv, vetelar, kmAllas, hengerurtart, tomeg, teljesitmeny));
                    }
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally
            {
                ConnectionClose();
            }


            return autok;
        }

        internal void DeleteSelectedAuto(Auto auto)
        {
            sql.CommandText = "DELETE FROM `auto` WHERE `rendszam` = @rendszam";

            MySqlParameterCollection parameters = new MySqlCommand().Parameters;
            parameters.AddWithValue("@rendszam", auto.Rendszam);

            try
            {
                ConnectionOpen();
                sql.Parameters.Clear();
                sql.Parameters.AddRange(parameters.Cast<MySqlParameter>().ToArray());
                sql.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                string userFriendlyMessage = GetUserFriendlyMessage(ex.Number);
                MessageBox.Show(userFriendlyMessage);
            }
            finally
            {
                ConnectionClose();
            }
        }

        private void ConnectionClose()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }

        private void ConnectionOpen()
        {
            if (conn.State!= System.Data.ConnectionState.Open) 
            { 
                conn.Open();
            }

        }

        internal void addNewAuto(Auto newAuto)
        {
            // Ellenőrzés, hogy az azonosítóval létezik-e már rekord
            if (!IsAutoExists(newAuto.Rendszam))
            {
                sql.CommandText = "INSERT INTO `auto` " +
                                  "(`rendszam`, `marka`, `modell`, `gyartasiev`, `forgalmiErvenyesseg`, `vetelar`, `kmallas`, `hengerűrtartalom`, `tomeg`, `teljesitmeny`) " +
                                  "VALUES (@rendszam, @marka, @modell, @gyartasiev, @forgalmiErvenyesseg, @vetelar, @kmallas, @hengerurtartalom, @tomeg, @teljesitmeny)";

                sql.Parameters.AddWithValue("@rendszam", newAuto.Rendszam);
                sql.Parameters.AddWithValue("@marka", newAuto.Marka);
                sql.Parameters.AddWithValue("@modell", newAuto.Modell);
                sql.Parameters.AddWithValue("@gyartasiev", newAuto.GyartEv);
                sql.Parameters.AddWithValue("@forgalmiErvenyesseg", newAuto.ForgalmiErv);
                sql.Parameters.AddWithValue("@vetelar", newAuto.Vetelar);
                sql.Parameters.AddWithValue("@kmallas", newAuto.KmAllas);
                sql.Parameters.AddWithValue("@hengerurtartalom", newAuto.Hengerurtart);
                sql.Parameters.AddWithValue("@tomeg", newAuto.Tomeg);
                sql.Parameters.AddWithValue("@teljesitmeny", newAuto.Teljesitmeny);

                try
                {
                    ConnectionOpen();
                    sql.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    string userFriendlyMessage = GetUserFriendlyMessage(ex.Number);
                    MessageBox.Show(userFriendlyMessage);
                }
                finally
                {
                    ConnectionClose();
                }
            }
            else
            {
                MessageBox.Show("Az autó rendszáma már létezik az adatbázisban.");
            }
        }

        internal void UpdateAuto(Auto updatedAuto)
        {
            // Ellenőrzés, hogy az azonosítóval létezik-e már rekord
            if (IsAutoExists(updatedAuto.Rendszam))
            {
                sql.CommandText = "UPDATE `auto` SET " +
                                  "`rendszam` = @rendszam, " +
                                  "`marka` = @marka, " +
                                  "`modell` = @modell, " +
                                  "`gyartasiev` = @gyartasiev, " +
                                  "`forgalmiErvenyesseg` = @forgalmiErvenyesseg, " +
                                  "`vetelar` = @vetelar, " +
                                  "`kmallas` = @kmallas, " +
                                  "`hengerűrtartalom` = @hengerurtartalom, " +
                                  "`tomeg` = @tomeg, " +
                                  "`teljesitmeny` = @teljesitmeny " +
                                  "WHERE `rendszam` = @rendszam";

                sql.Parameters.Clear();

                sql.Parameters.AddWithValue("@rendszam", updatedAuto.Rendszam);
                sql.Parameters.AddWithValue("@marka", updatedAuto.Marka);
                sql.Parameters.AddWithValue("@modell", updatedAuto.Modell);
                sql.Parameters.AddWithValue("@gyartasiev", updatedAuto.GyartEv);
                sql.Parameters.AddWithValue("@forgalmiErvenyesseg", updatedAuto.ForgalmiErv);
                sql.Parameters.AddWithValue("@vetelar", updatedAuto.Vetelar);
                sql.Parameters.AddWithValue("@kmallas", updatedAuto.KmAllas);
                sql.Parameters.AddWithValue("@hengerurtartalom", updatedAuto.Hengerurtart);
                sql.Parameters.AddWithValue("@tomeg", updatedAuto.Tomeg);
                sql.Parameters.AddWithValue("@teljesitmeny", updatedAuto.Teljesitmeny);

                try
                {
                    ConnectionOpen();
                    sql.ExecuteNonQuery();

                    MessageBox.Show($"A(z) {updatedAuto.Rendszam} rendszámú autó sikeresen módosítva!");
                }
                catch (MySqlException ex)
                {
                    string userFriendlyMessage = GetUserFriendlyMessage(ex.Number);
                    MessageBox.Show(userFriendlyMessage);
                }
                finally
                {
                    ConnectionClose();
                }
            }
            else
            {
                MessageBox.Show("Az autó rendszáma nem található az adatbázisban.");
            }
        }

        internal bool IsAutoExists(string rendszam)
        {
            MySqlCommand checkCmd = conn.CreateCommand();
            checkCmd.CommandText = "SELECT COUNT(*) FROM `auto` WHERE `rendszam` = @rendszam";

            checkCmd.Parameters.AddWithValue("@rendszam", rendszam);

            int count = 0;

            try
            {
                ConnectionOpen();
                count = Convert.ToInt32(checkCmd.ExecuteScalar());
            }
            catch (MySqlException ex)
            {
                string userFriendlyMessage = GetUserFriendlyMessage(ex.Number);
                MessageBox.Show(userFriendlyMessage);
            }
            finally
            {
                ConnectionClose();
            }

            return count > 0;
        }
        internal string GetUserFriendlyMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 1062:
                    return "Az adat már létezik, próbálkozzon egyedi adatokkal.";
                case 1452:
                    return "Az adatokat nem lehet törölni, mert más tábla hivatkozik rá.";
                default:
                    return "Hiba történt az adatbázisművelet közben.";
            }
        }
    }
}
