using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using ToDoList.Models;

namespace ToDoList
{
  public class Task
  {
    private int _id;
    private string _description;

    public Task(string Description, int Id = 0)
    {
      _id = Id;
      _description = Description;
    }

    //...GETTERS AND SETTERS WILL GO HERE...

    public int GetId()
    {
      return _id;
    }

    public string GetDescription()
    {
      return _description;
    }
        public override bool Equals(System.Object otherTask)
        {
          if (!(otherTask is Task))
          {
            return false;
          }
          else
          {
            Task newTask = (Task) otherTask;
            bool idEquality = (this.GetId() == newTask.GetId());
            bool descriptionEquality = (this.GetDescription() == newTask.GetDescription());
            return (idEquality && descriptionEquality);
          }
        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
           conn.Open();

           var cmd = conn.CreateCommand() as MySqlCommand;
           cmd.CommandText = @"INSERT INTO `tasks` (`description`) VALUES (@TaskDescription);";

           MySqlParameter description = new MySqlParameter();
           description.ParameterName = "@TaskDescription";
           description.Value = this._description;
           cmd.Parameters.Add(description);

           cmd.ExecuteNonQuery();
           _id = (int) cmd.LastInsertedId;


            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }


        public static List<Task> GetAll()
        {
            List<Task> allTasks = new List<Task> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tasks;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int taskId = rdr.GetInt32(0);
              string taskDescription = rdr.GetString(1);
              Task newTask = new Task(taskDescription, taskId);
              allTasks.Add(newTask);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTasks;
        }

          public static void DeleteAll()
          {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM tasks;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
          }

  }
}
