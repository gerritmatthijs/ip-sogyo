namespace Persistence;

using System;
using System.Text;
using MySql.Data.MySqlClient;
using Tichu;

public class TichuRepositoryMySQL : ITichuRepository
{
    private readonly string connectionString = "Server=localhost;Port=3306;Database=tichu;Uid=admin;Pwd=Adminpassword!23";
    private readonly MySqlConnection conn;
    private readonly ITichuFactory factory;

    public TichuRepositoryMySQL(ITichuFactory factory)
    {
        conn = new(connectionString);
        this.factory = factory;
    }

    public void SaveGame(string key, ITichuFacade tichu)
    {   
        try
        {
            conn.Open();
            var dbContainsGame = (int)(long) new MySqlCommand("CALL containsGame('" + key + "');", conn).ExecuteScalar();
            if (dbContainsGame == 1){
                MySqlCommand cmd = new(GetUpdateQuery(tichu, key), conn);
                cmd.ExecuteNonQuery();
            }
            else {
                MySqlCommand cmd = new(GetSaveQuery(tichu, key), conn);
                cmd.ExecuteNonQuery();
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally{
            conn.Close();
        }
    }

    public ITichuFacade GetGame(string key)
    {
        try
        {
            conn.Open();
            MySqlCommand cmd = new("CALL getGame('" + key +  "');", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            string names = (string)reader["player_names"];
            string hands = (string)reader["player_hands"];
            string leader = (string)reader["leader_name"];
            string lastPlayed = (string)reader["last_played"];
            int turn = (int)reader["turn"];

            string[] nameList = names.Split(' ');
            string[] handList = hands.Split(' ');
            return factory.CreateExistingGame(nameList, handList, leader, lastPlayed, turn);
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Connection to the database failed");
        }
        finally{
            conn.Close();
        }
    }

    public void DeleteGame(string key)
    {
        try
        {
            conn.Open();
            MySqlCommand cmd = new("CALL deleteGame('" + key +  "');", conn);
            cmd.ExecuteNonQuery();
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally{
            conn.Close();
        }

    }

    public bool ContainsGame(string key)
    {
        try
        {
            conn.Open();
            int dbContainsGame = (int)(long)new MySqlCommand("CALL containsGame('" + key + "');", conn).ExecuteScalar();
            return dbContainsGame.Equals(1);
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Connection to the database failed");

        }
        finally{
            conn.Close();
        }
    }

    private static string GetSaveQuery(ITichuFacade tichu, string key)
    {
        string names = GetNames(tichu);
        string hands = GetHands(tichu);
        string leader = tichu.GetCurrentLeader();
        string lastPlayed = tichu.GetLastPlayed();
        string turn = tichu.GetTurn().ToString();
        string query = CreateProcedureCall("saveGame", [key, names, hands, leader, lastPlayed, turn]);
        return query;
    }

    private static string GetUpdateQuery(ITichuFacade tichu, string key)
    {
        string hands = GetHands(tichu);
        string leader = tichu.GetCurrentLeader();
        string lastPlayed = tichu.GetLastPlayed();
        string turn = tichu.GetTurn().ToString();
        string query = CreateProcedureCall("updateGame", [key, hands, leader, lastPlayed, turn]);
        return query;
    }

    private static string GetHands(ITichuFacade tichu)
    {
        StringBuilder handsBuilder = new();
        for (int i = 0; i < 4; i++)
        {
            string name = tichu.GetPlayerName(i);
            handsBuilder.Append(' ').Append(tichu.GetPlayerHand(name));
        }
        string hands = handsBuilder.ToString()[1..];
        return hands;
    }

    private static string GetNames(ITichuFacade tichu)
    {
        StringBuilder namesBuilder = new();
        for (int i = 0; i < 4; i++)
        {
            namesBuilder.Append(' ').Append(tichu.GetPlayerName(i));
        }
        string names = namesBuilder.ToString()[1..];
        return names;
    }

    private static string CreateProcedureCall(string procedure, List<string> data)
    {
        return "CALL "  + procedure + "('" + string.Join("', '", data) + "');";
    }
}