using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
    public class Restaurant
    {
        private string _name;
        private int _id;
        private int _cuisineId;
        private string _hours;

        public Restaurant(string name, int cuisineId, string hours, int id = 0)
        {
            _name = name;
            _cuisineId = cuisineId;
            _id = id;
            _hours = hours;
        }

        public override bool Equals(System.Object otherRestaurant)
        {
          if (!(otherRestaurant is Restaurant))
          {
            return false;
          }
          else
          {
             Restaurant newRestaurant = (Restaurant) otherRestaurant;
             bool idEquality = this.GetId() == newRestaurant.GetId();
             bool nameEquality = this.GetName() == newRestaurant.GetName();
             bool cuisineEquality = this.GetcuisineId() == newRestaurant.GetcuisineId();
             bool hoursEquality = this.GetHours() == newRestaurant.GetHours();
             return (idEquality && nameEquality && cuisineEquality && hoursEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetName().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetcuisineId()
        {
            return _cuisineId;
        }

        public string GetHours()
        {
            return _hours;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO restaurants (name, hours) VALUES (@name, @hours);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@cuisine_id";
            cuisineId.Value = this._cuisineId;
            cmd.Parameters.Add(cuisineId);

            MySqlParameter hours = new MySqlParameter();
            hours.ParameterName = "@hours";
            hours.Value = this._hours;
            cmd.Parameters.Add(hours);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Restaurant> GetAll()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants ORDER BY hours;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int restaurantId = rdr.GetInt32(0);
              string restaurantname = rdr.GetString(1);
              int restaurantcuisineId = rdr.GetInt32(2);
              string restauranthours = rdr.GetString(3);
              Restaurant newRestaurant = new Restaurant(restaurantname, restaurantcuisineId, restauranthours, restaurantId);
              allRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRestaurants;
        }
        public static Restaurant Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurantss WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int restaurantId = 0;
            string restaurantName = "";
            string restauranthours = "";
            int restaurantcuisineId = 0;

            while(rdr.Read())
            {
              restaurantId = rdr.GetInt32(0);
              restaurantName = rdr.GetString(1);
              restaurantcuisineId = rdr.GetInt32(2);
              restauranthours = rdr.GetString(3);
            }
            Restaurant newRestaurant = new Restaurant(restaurantName, restaurantcuisineId, restauranthours, restaurantId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newRestaurant;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurants;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteTasks(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM restaurantss WHERE cuisine_id = @id;";

            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@id";
            cuisineId.Value = id;
            cmd.Parameters.Add(cuisineId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public static List<Restaurant> GetAlphaList()
        {
            List<Restaurant> allRestaurants = new List<Restaurant> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM restaurants ORDER BY hours;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int restaurantId = rdr.GetInt32(0);
              string restaurantname = rdr.GetString(1);
              int restaurantcuisineId = rdr.GetInt32(2);
              string restauranthours = rdr.GetString(3);
              Restaurant newRestaurant = new Restaurant(restaurantname, restaurantcuisineId, restauranthours, restaurantId);
              allRestaurants.Add(newRestaurant);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRestaurants;
        }

    }
}
