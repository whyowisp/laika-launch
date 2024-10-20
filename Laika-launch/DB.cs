using System;
using System.Data;
using System.Data.SQLite;

namespace Laika_launch
{
    class DB
    {
        private static string filename;
        private static string tablename;
        static DB()
        {
            //haetaan app.configista tiedostonnimi ja taulun nimi;
            filename = Laika_launch.Properties.Settings.Default.tietokanta;
        }
        public static void CreateToSQLite()
        {
            try
            {
                if (!System.IO.File.Exists(filename))
                {
                    SQLiteConnection.CreateFile(filename);
                    SQLiteConnection conn = new SQLiteConnection($"Data source={filename};Version=3;New=False;Compress=True");
                    conn.Open();

                    string sql = "CREATE TABLE topscores (Player varchar(20), Score int)";

                    SQLiteCommand command = new SQLiteCommand(sql, conn);
                    command.ExecuteNonQuery();

                    sql = "INSERT INTO topscores (Player, Score) VALUES ('Dr.CCCP', 0)";

                    command = new SQLiteCommand(sql, conn);
                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
            catch
            {
                throw;
            }
        }
        //CRUD 1.osa tietojen haku
        public static DataTable ReadFromSQLite()
        {
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    SQLiteConnection conn = new SQLiteConnection($"Data source={filename};Version=3;New=False;Compress=True");
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM topscores ORDER BY Score DESC LIMIT 10";

                    //tiedon lukemista varten voidaan käyttää DataReader
                    SQLiteDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    // "se mikä avataan, myös suljetaan"
                    rdr.Close();
                    conn.Close();
                    return dt;
                }
                else
                {
                    throw new System.IO.FileNotFoundException("Problem reading from database");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool AddToSQLite(string player, int score)
        {
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    SQLiteConnection conn = new SQLiteConnection($"Data source={filename}");
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"INSERT INTO topscores (Player, Score) VALUES ('{player}',{score})";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {
                    throw new System.IO.FileNotFoundException("Cannot find database.");
                }
            }
            catch
            {
                throw;
            }

        }
        public static bool UpdateInSQLite(string player, int score)
        {
            try
            {
                if (System.IO.File.Exists(filename))
                {
                    SQLiteConnection conn = new SQLiteConnection($"Data source={filename}");
                    conn.Open();
                    SQLiteCommand cmd = conn.CreateCommand();
                    cmd.CommandText = $"UPDATE topscores SET Score = {score} WHERE Player LIKE '{player}'";
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    return true;
                }
                else
                {
                    throw new System.IO.FileNotFoundException("Cannot find database.");
                }
            }
            catch
            {
                throw;
            }
        }
        public static int GetHighestScore()
        {
            int highestScore = 0;

            SQLiteConnection conn = new SQLiteConnection($"Data source={filename};Version=3;New=False;Compress=True");
            conn.Open();
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT MAX(Score) FROM topscores";

            string stringHS = cmd.ExecuteScalar().ToString(); //Tähän täytyy olla jokin muukin keino...
            highestScore = Int32.Parse(stringHS);
            conn.Close();
            return highestScore;
        }
        
    }
}
