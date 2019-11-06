﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace TripERP.Common
{
    /**
      * MySQL DB Helper by Jungil Lim
      * Version : 1.6.0
      */

    class DbHelper
    {
        // localhost
       
        

        static public int ExecuteNonQuery(string query)
        {
            int retVal = -1;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    retVal = command.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public long ExecuteNonQueryAndGetLastId(string query)
        {
            long retVal = -1;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
               
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    retVal = (long)command.ExecuteNonQuery();

                    if (retVal != -1)
                        retVal = command.LastInsertedId;
                }
                catch (MySqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);                   
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public long ExecuteScalar(string query)
        {
            long retVal = -1;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(query, connection);
                    retVal = Convert.ToInt64(command.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Write(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        static public DataSet SelectQuery(string query)
        {
            DataSet dataSet = new DataSet();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                MySqlDataAdapter adpt = new MySqlDataAdapter(query, connection);
                    adpt.Fill(dataSet);
            }

            return dataSet;
        }



        static public string GetValue(string query, string field, string defaultValue)
        {
            string retVal = "";
            DataSet dataSet = DbHelper.SelectQuery(query);
            if (dataSet != null)
            {
                DataTableCollection collection = dataSet.Tables;
                if (collection.Count > 0)
                {
                    DataTable table = collection[0];
                    if (table.Rows.Count > 0)
                    {
                        DataRow dataRow = table.Rows[0];
                        retVal = (System.DBNull.Value == dataRow[field]) ? defaultValue : dataRow[field].ToString();
                    }
                    else
                    {
                        retVal = defaultValue;
                    }

                }
            }

            return retVal;
        }


        /**
         * Transaction 안에서 여러 쿼리 실행
         * 성공시 마지막 실행 쿼리 결과를 리턴
         */
        static public int ExecuteNonQueryWithTransaction(string[] queries)
        {
            int retVal = -1;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    for (int i = 0; i < queries.Length; i++)
                    {
                        command.CommandText = queries[i];
                        retVal = command.ExecuteNonQuery();
                        if (retVal == -1) // 어떤 에러든 발생되면 이 절까지 오지 않고 catch 로 빠질 것 같다. 
                            throw new Exception("Occurs an error");
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        retVal = -1;
                        transaction.Rollback();
                    }
                    catch (MySqlException sqlEx)
                    {
                        if (transaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + sqlEx.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + ex.GetType() +
                    " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");
                }
                finally
                {
                    connection.Close();
                }

            }
            return retVal;
        }

        /**
         * Transaction 안에서 여러 쿼리 실행하는데, Insert 후 그 밖의 쿼리를 실행한다. 
         * 
         * */
        static public long ExecuteScalarAndNonQueryWithTransaction(string[] queries)
        {
            long lastId = -1; 
            int retVal = -1;
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    for (int i = 0; i < queries.Length; i++)
                    {
                        if (queries[i] != "" && queries[i] != null)
                        {
                            command.CommandText = queries[i];
                            if (i == 0)
                            {
                                lastId = Convert.ToInt64(command.ExecuteScalar());
                                if (lastId == -1)
                                    throw new Exception("Occurs an error");
                            }
                            else
                            {
                                retVal = command.ExecuteNonQuery();
                                if (retVal == -1) // 어떤 에러든 발생되면 이 절까지 오지 않고 catch 로 빠질 것 같다. 
                                    throw new Exception("Occurs an error");
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        lastId = -1; 
                        retVal = -1;
                        transaction.Rollback();
                    }
                    catch (MySqlException sqlEx)
                    {
                        if (transaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + sqlEx.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + ex.GetType() +
                    " was encountered while inserting the data.");
                    Console.WriteLine("Neither record was written to database.");
                }
                finally
                {
                    connection.Close();
                }

            }
            return lastId;
        }


        /**
         * Transaction 안에서 여러 쿼리를 실행하고, 건별로 리턴값을 받아 배열로 리턴한다 (2019-08-09 hih)
         * 
         * */
        static public Tuple<int , string[]> ExecuteScalarAndReturnByWithTransaction(string[] queries)
        {
            string queryReturnCode = "";
            int retVal = 0;

            // 실행할 쿼리의 개수를 리턴값 배열 크기로 선언
            string[] queryResult = new string[queries.Length];

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                MySqlTransaction transaction;

                // Start a local transaction
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    for (int i = 0; i < queries.Length; i++)
                    {
                        command.CommandText = queries[i];

                        queryReturnCode = Convert.ToString(command.ExecuteScalar());
                        queryResult[i] = queryReturnCode;
                        retVal = 0;
                        if (queryReturnCode.Equals("ERROR"))
                        {
                            retVal = -1;
                            throw new Exception("Occurs an error");
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        queryReturnCode = "ERROR";
                        retVal = -1;
                        transaction.Rollback();
                    }
                    catch (MySqlException sqlEx)
                    {
                        if (transaction.Connection != null)
                        {
                            Console.WriteLine("An exception of type " + sqlEx.GetType() +
                            " was encountered while attempting to roll back the transaction.");
                        }
                    }

                    Console.WriteLine("An exception of type " + ex.GetType() + " was encountered while querying the data.");
                    Console.WriteLine("Neither record was written to database.");
                    Console.WriteLine("ex="+ex);
                }
                finally
                {
                    connection.Close();
                }

                return new Tuple<int, string[]>(retVal, queryResult);
            }
        }
    }
}
