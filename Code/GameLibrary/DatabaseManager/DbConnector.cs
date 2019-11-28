﻿using System.IO;
using System.Data.SQLite;
using System;
using System.Collections.Generic;

namespace GameLibrary
{
    public class DbConnector
    {
        /// <summary>
        /// Connect to the SQLite database
        /// </summary>
        /// <param name="close">If true : Close database connection. If false : Open database connection</param>
        /// <returns>SQLiteCommand cmd if database is getting opened.</returns>
        /// <returns>Null if database is getting closed</returns>
        internal static SQLiteCommand ConnectToDatabase(bool close)
        {
            bool createTable = false;
            if (!File.Exists("GLdb.db3"))
            {
                CreateDatabase();
                createTable = true;
            }

            System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=GLdb.db3");
            System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn);

            if (!close)
            {
                conn.Open(); //Open connection to the SQLite database
                if (createTable)
                {
                    CreateTable(cmd);
                }
                return cmd;
            }
            else
            {
                cmd = null;
                conn.Close();
                return cmd;
            }
        }

        /// <summary>
        /// Create the DB File
        /// </summary>
        private static void CreateDatabase()
        {
            System.Data.SQLite.SQLiteConnection.CreateFile(
                    Path.Combine(
                        Directory.GetCurrentDirectory(), "GLdb.db3")); //Create the database
        }
        /// <summary>
        /// Create the tables needed fot the database to works properly
        /// </summary>
        /// <param name="cmd">SQLiteCommand cmd</param>
        private static void CreateTable(SQLiteCommand cmd)
        {
            List<string> createTableQuery = new List<string>
            {
                @"CREATE TABLE IF NOT EXISTS
                                [Users]([idUser] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Email] VARCHAR(255) NOT NULL,
                                [Password] VARCHAR(255) NOT NULL)",

                @"CREATE TABLE IF NOT EXISTS 
                                [Games]([idGame] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Title] VARCHAR(255) NOT NULL)",

                @"CREATE TABLE IF NOT EXISTS
                                [Library]([idUser] INTERGER NOT NULL FOREIGN KEY,
                                [idGame] INTEGER NOT NULL FOREIGN KEY,
                                [DateAdded] DATE NOT NULL)",

                @"CREATE TABLE IF NOT EXISTS
                                [Platforms]([idPlatform] INTEGER NOT NULL PRIMARY KEY,
                                [Name] VARCHAR(255) NOT NULL)",

                @"CREATE TABLE IF NOT EXISTS
                                [GamesPlatforms]([idGame] INTEGER NOT NULL FOREIGN KEY,
                                [idPlatform] INTEGER NOT NULL FOREIGN KEY)"
            };

            foreach(string query in createTableQuery)
            {
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
            }
        }
    }
}