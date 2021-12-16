using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web;
using System.Linq;
using Ejyle_Assignment_WebApp.Models;

namespace Ejyle_Assignment_WebApp.DataAccess
{
    public class ShipmentDataAccess
    {
        public static string connectionStr;

        // Get all Shipments
        public IEnumerable<Shipment> GetAllShipment()
        {
            List<Shipment> lstShipment = new List<Shipment>();

            using (SqlConnection sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = connectionStr;

                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Shipment", sqlConn);

                sqlConn.Open();

                SqlDataReader sqlReader = sqlCmd.ExecuteReader();
                while (sqlReader.Read())
                {
                    lstShipment.Add(new Shipment
                    {
                        ShipmentId = Convert.ToInt32(sqlReader["ShipmentId"]),
                        SenderName = Convert.ToString(sqlReader["SenderName"]),
                        Description = Convert.ToString(sqlReader["Description"]),
                        RecipientAddress = Convert.ToString(sqlReader["RecipientAddress"]),
                        Expedited = Convert.ToBoolean(sqlReader["Expedited"]),
                        ShipmentType = Convert.ToString(sqlReader["ShipmentType"])
                    });
                }
                sqlReader.Close();

                sqlConn.Close();
            }

            return lstShipment;
        }

        // Get Shipment by ShipmentId
        public Shipment GetShipment(int shipmentId)
        {
            Shipment shipment = new Shipment();
            DataSet ds = new DataSet();

            using (SqlConnection sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = connectionStr;

                SqlCommand sqlCmd = new SqlCommand("SELECT * FROM Shipment where ShipmentId = @ShipmentId", sqlConn);
                sqlCmd.Parameters.AddWithValue("@ShipmentId", shipmentId);

                sqlConn.Open();

                SqlDataAdapter adp = new SqlDataAdapter(sqlCmd);
                adp.Fill(ds);

                sqlConn.Close();
            }

            if (ds != null)
            {
                DataRow dataRow = ds.Tables[0].Rows[0];
                shipment.ShipmentId = dataRow.Field<int>("ShipmentId");
                shipment.SenderName = dataRow.Field<string>("SenderName");
                shipment.Description = dataRow.Field<string>("Description");
                shipment.RecipientAddress = dataRow.Field<string>("RecipientAddress");
                shipment.Expedited = dataRow.Field<bool>("Expedited");
                shipment.ShipmentType = dataRow.Field<string>("ShipmentType");
            }

            return shipment;
        }

        // Create new Shipment
        public int AddShipment(Shipment shipment)
        {
            int returnNum = 0;

            using (SqlConnection sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = connectionStr;

                SqlCommand sqlCmd = new SqlCommand("INSERT INTO Shipment (SenderName, Description, RecipientAddress, Expedited, ShipmentType) " +
                                                   " Values (@SenderName, @Description, @RecipientAddress, @Expedited, @ShipmentType)", sqlConn);
                sqlCmd.Parameters.AddWithValue("@SenderName", shipment.SenderName);
                sqlCmd.Parameters.AddWithValue("@Description", shipment.Description);
                sqlCmd.Parameters.AddWithValue("@RecipientAddress", shipment.RecipientAddress);
                sqlCmd.Parameters.AddWithValue("@Expedited", shipment.Expedited);
                sqlCmd.Parameters.AddWithValue("@ShipmentType", shipment.ShipmentType);

                sqlConn.Open();

                returnNum = sqlCmd.ExecuteNonQuery();

                sqlConn.Close();
            }

            return returnNum;
        }

        // Delete Shipment
        public int DeleteShipment(int shipmentId)
        {
            int returnNum = 0;

            using (SqlConnection sqlConn = new SqlConnection())
            {
                sqlConn.ConnectionString = connectionStr;

                SqlCommand sqlCmd = new SqlCommand("DELETE FROM Shipment where ShipmentId = @ShipmentId", sqlConn);
                sqlCmd.Parameters.AddWithValue("@ShipmentId", shipmentId);

                sqlConn.Open();

                returnNum = sqlCmd.ExecuteNonQuery();

                sqlConn.Close();
            }

            return returnNum;
        }
    }
}
