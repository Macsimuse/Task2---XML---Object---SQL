using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using ClassContainer;

namespace SerializingDocument
{
   public class ADO_Connection
    {
       // Add information to Data Sql 
       public static void AddToDataBase(item [] items)
       {
           using (SqlConnection connection = new SqlConnection())
           {
               SqlCommand command = null;
               try
               {
                   connection.ConnectionString = @"Data Source=localhost;Initial Catalog=ADO-Net;Integrated Security=True;Pooling=False";
                   connection.Open();
                   foreach (item it in items)
                   {
                       string addDatainItem = @"Insert into Item(FirstName,LastName,Age)Values('" + it.FirstName + "','" + it.LastName + "'," + it.Age + ")";
                       foreach (Job job in it.listJob)
                       {
                           string addDatainJob = @"Insert into Job(Age,TimeTicks)Values('" + job.Age + "'," + job.Time + ")";
                           command = new SqlCommand(addDatainJob, connection);
                           command.ExecuteNonQuery();
                       }
                       foreach (Position position in it.listPosition)
                       {
                           string addDatainPosition = @"Insert into Position(Latitude,Altitude,Time)Values(" + position.Latitude + "," + position.Altitude + ",'" + position.Time + "')";
                           command = new SqlCommand(addDatainPosition, connection);
                           command.ExecuteNonQuery();
                       }
                       command = new SqlCommand(addDatainItem, connection);
                       command.ExecuteNonQuery();
                   }
               }

              catch (Exception ex) { Console.WriteLine(ex.Message); }
               
               finally
               {
                   connection.Close();
               }
           }      
       }
       public static void CleanData()
       {
           using (SqlConnection connection = new SqlConnection())
           {
               try
               {
                   SqlCommand command = null;
                   connection.ConnectionString = @"Data Source=localhost;Initial Catalog=ADO-Net;Integrated Security=True;Pooling=False";
                   connection.Open();
                   string[] ariseData = { @"Delete from item", @"Delete from Job", @"Delete from Position" };
                   for (int count = 0; count < ariseData.Length; count++)
                   {
                       command = new SqlCommand(ariseData[count], connection);
                       command.ExecuteNonQuery();
                   }
               }
               catch (Exception ex) { Console.WriteLine(ex.Message); }
               finally { connection.Close(); }
           }
       }
    }
}
