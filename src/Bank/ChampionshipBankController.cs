﻿using _5by5_ChampionshipController.src.Entity;
using Microsoft.Data.SqlClient;
using System.Data;

namespace _5by5_ChampionshipController.src.Bank
{
    public class ChampionshipBankController : BankController<Championship>
    {
        public ChampionshipBankController() : base() { }

        public override bool Insert(Championship championship)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", SqlDbType.VarChar).Value = championship.Name;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = championship.Season;
            sqlCommand.Parameters.AddWithValue("@startDate", SqlDbType.Date).Value = championship.StartDate;
            sqlCommand.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = championship.EndDate;

            return BooleanQuery("spCreateNewChampionship");
        }

        public bool SetEndDate(string championshipName, string cSeason) => SetEndDate(championshipName, cSeason, DateOnly.FromDateTime(DateTime.Now));

        public bool SetEndDate(string championshipName, string cSeason, DateOnly date)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = cSeason;
            sqlCommand.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = date;

            return BooleanQuery("spEndChampionship");
        }

        public Championship? GetByNameAndSeason(string championshipName, string season)
        {
            sqlCommand.CommandText = "spRetrieveChampionship";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@championshipName", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = season;

            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
                return new(reader.GetString(0), reader.GetString(1), DateOnly.Parse(reader.GetDateTime(2).ToString()), DateOnly.Parse(reader.GetDateTime(3).ToString()));

            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return null;
        }

        public override List<Championship> GetAll()
        {
            List<Championship> list = new();

            sqlCommand.CommandText = "spRetrieveAllChampionships";
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Connection = sqlConnection;

            sqlConnection.Open();

            SqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Championship aux = new(reader.GetString(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));

                if (!reader.IsDBNull(3))
                    aux.EndDate = DateOnly.FromDateTime(reader.GetDateTime(3));
                else
                    aux.EndDate = null;

                list.Add(aux);
            }
            
            sqlCommand.Parameters.Clear();
            sqlConnection.Close();

            return list;
        }

        public bool EndByNameAndSeason(string championshipName, string season) => EndByNameAndSeason(championshipName, season, DateOnly.FromDateTime(DateTime.Now));

        public bool EndByNameAndSeason(string championshipName, string season, DateOnly end)
        {
            sqlCommand.Parameters.AddWithValue("@championshipName", SqlDbType.VarChar).Value = championshipName;
            sqlCommand.Parameters.AddWithValue("@season", SqlDbType.VarChar).Value = season;
            sqlCommand.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = end;

            return BooleanQuery("spEndChampionship");
        }
    }
}