namespace Persistence;

using System;
using System.Text;
using MySql.Data.MySqlClient;
using Tichu;

public class TichuRepositoryMySQL : ITichuRepository
{
    private readonly string connectionString = "server=localhost:3306;uid=admin;pwd=Adminpassword!23;database=tichu";
    private readonly MySqlConnection conn;
    private readonly ITichuFactory factory;

    public TichuRepositoryMySQL(ITichuFactory factory)
    {
        conn = new(connectionString);
        this.factory = factory;
    }

    public void SaveGame(string key, ITichuFacade tichu)
    {   
        if (OpenConnection())
        {
            string dbContainsGame = (string) new MySqlCommand("CALL containsGame('" + key + "');", conn).ExecuteScalar();
            if (dbContainsGame == "1"){
                MySqlCommand cmd = new(GetUpdateQuery(tichu), conn);
                cmd.ExecuteNonQuery();
            }
            else {
                MySqlCommand cmd = new(GetSaveQuery(tichu), conn);
                cmd.ExecuteNonQuery();
            }
            CloseConnection();
        }
    }

    public ITichuFacade GetGame(string key)
    {
        if (OpenConnection())
        {
            MySqlCommand cmd = new("CALL getGame('" + key +  "');", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            string names = (string)reader["player_names"];
            string hands = (string)reader["player_hands"];
            string leader = (string)reader["leader_name"];
            string lastPlayed = (string)reader["last_played"];
            int turn = int.Parse((string)reader["turn"]);
            CloseConnection();

            string[] nameList = names.Split(' ');
            string[] handList = hands.Split(' ');
            return factory.CreateExistingGame(nameList, handList, leader, lastPlayed, turn);
        }
        throw new Exception("Connection to the database failed");
    }

    public void DeleteGame(string key)
    {
        if (OpenConnection())
        {
            MySqlCommand cmd = new("CALL deleteGame('" + key +  "');", conn);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }
    }

    public bool ContainsGame(string key)
    {
        if (OpenConnection())
        {
            string dbContainsGame = (string)new MySqlCommand("CALL containsGame('" + key + "');", conn).ExecuteScalar();
            CloseConnection();
            return dbContainsGame.Equals("1");
        }
        Console.WriteLine("Could not access database");
        return false;
    }

    private static string GetSaveQuery(ITichuFacade tichu)
    {
        string names = GetNames(tichu);
        string hands = GetHands(tichu);
        string leader = tichu.GetCurrentLeader();
        string lastPlayed = tichu.GetLastPlayed();
        string turn = tichu.GetTurn().ToString();
        string query = CreateProcedureCall("saveGame", [names, hands, leader, lastPlayed, turn]);
        return query;
    }

    private static string GetUpdateQuery(ITichuFacade tichu)
    {
        string hands = GetHands(tichu);
        string leader = tichu.GetCurrentLeader();
        string lastPlayed = tichu.GetLastPlayed();
        string turn = tichu.GetTurn().ToString();
        string query = CreateProcedureCall("updateGame", [hands, leader, lastPlayed, turn]);
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

    private bool OpenConnection()
    {
        try
        {
            conn.Open();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private bool CloseConnection()
    {
        try
        {
            conn.Close();
            return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

}