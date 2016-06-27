using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PCAuditReader
{
    class DatabaseManager
    {
        SqlConnection Sql = new SqlConnection(@"Server=VHMSSTS-07\DEV,1433;Database=Logan_Test1;User ID=LoginK;Password=Vision@123");
        SqlCommand SqlStr = new SqlCommand();
        SqlDataReader SqlReader = default(SqlDataReader);
        string SQLStmt;

        public DataTable CheckData(string scannedID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditHardware  where ComputerName = @scannedID ";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@scannedID", scannedID);
                    Sql.Open();
                    cmd.CommandTimeout = 30;
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        dt.Load(SqlReader);
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                Console.WriteLine("Error Occured: " + ex.Message);
            }
            return dt;
        }

        public void UpdateInfo(string SQLAuditDate, string SQLAuditorName, string SQLManufacturerName, string SQLModelName, string SQLComputerName, string SQLOperatingSystem, string SQLOperatingArchitecture, string SQLServicePack, string SQLSerialNumber, string SQLProcessorName, int SQLNoProcessors, int SQLRamAmt, double SQLHardDrivesize, double SQLFreeHardDrive, string SQLComments)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditHardwareChangeControl  where ComputerName = @scannedID ";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@scannedID", SQLComputerName);
                    Sql.Open();
                    cmd.CommandTimeout = 30;
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "UPDATE PCAuditHardwareChangeControl SET AuditDate = @SQLAuditDate, AuditorName = @SQLAuditorName, ManufacturerName = @SQLManufacturerName, ModelName = @SQLModelName, OperatingSystem = @SQLOperatingSystem, OperatingArchitecture = @SQLOperatingArchitecture, ServicePack = @SQLServicePack, SerialNumber = @SQLSerialNumber, ProcessorName = @SQLProcessorName, NoProcessors = @SQLNoProcessors, RamAmt = @SQLRamAmt, HardDrivesize = @SQLHardDrivesize, FreeHardDrive = @SQLFreeHardDrive, Comments = @SQLComments WHERE ComputerName = @SQLComputerName";
                        cmd = new SqlCommand(SQLStmt, Sql);
                        using (cmd)
                        {
                            cmd.Parameters.AddWithValue("@SQLAuditDate", SQLAuditDate);
                            cmd.Parameters.AddWithValue("@SQLAuditorName", SQLAuditorName);
                            cmd.Parameters.AddWithValue("@SQLManufacturerName", SQLManufacturerName);
                            cmd.Parameters.AddWithValue("@SQLModelName", SQLModelName);
                            cmd.Parameters.AddWithValue("@SQLComputerName", SQLComputerName);
                            cmd.Parameters.AddWithValue("@SQLOperatingSystem", SQLOperatingSystem);
                            cmd.Parameters.AddWithValue("@SQLOperatingArchitecture", SQLOperatingArchitecture);
                            cmd.Parameters.AddWithValue("@SQLServicePack", SQLServicePack);
                            cmd.Parameters.AddWithValue("@SQLSerialNumber", SQLSerialNumber);
                            cmd.Parameters.AddWithValue("@SQLProcessorName", SQLProcessorName);
                            cmd.Parameters.AddWithValue("@SQLNoProcessors", SQLNoProcessors);
                            cmd.Parameters.AddWithValue("@SQLRamAmt", SQLRamAmt);
                            cmd.Parameters.AddWithValue("@SQLHardDrivesize", SQLHardDrivesize);
                            cmd.Parameters.AddWithValue("@SQLFreeHardDrive", SQLFreeHardDrive);
                            cmd.Parameters.AddWithValue("@SQLComments", SQLComments);
                            Sql.Open();
                            cmd.CommandTimeout = 30;
                            cmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    else
                    {
                        Sql.Close();
                        SQLStmt = "Insert into PCAuditHardwareChangeControl values(@SQLAuditDate, @SQLAuditorName, @SQLManufacturerName, @SQLModelName, @SQLComputerName, @SQLOperatingSystem, @SQLOperatingArchitecture, @SQLServicePack, @SQLSerialNumber, @SQLProcessorName, @SQLNoProcessors, @SQLRamAmt, @SQLHardDrivesize, @SQLFreeHardDrive, @SQLComments)";
                        cmd = new SqlCommand(SQLStmt, Sql);
                        using (cmd)
                        {
                            cmd.Parameters.AddWithValue("@SQLAuditDate", SQLAuditDate);
                            cmd.Parameters.AddWithValue("@SQLAuditorName", SQLAuditorName);
                            cmd.Parameters.AddWithValue("@SQLManufacturerName", SQLManufacturerName);
                            cmd.Parameters.AddWithValue("@SQLModelName", SQLModelName);
                            cmd.Parameters.AddWithValue("@SQLComputerName", SQLComputerName);
                            cmd.Parameters.AddWithValue("@SQLOperatingSystem", SQLOperatingSystem);
                            cmd.Parameters.AddWithValue("@SQLOperatingArchitecture", SQLOperatingArchitecture);
                            cmd.Parameters.AddWithValue("@SQLServicePack", SQLServicePack);
                            cmd.Parameters.AddWithValue("@SQLSerialNumber", SQLSerialNumber);
                            cmd.Parameters.AddWithValue("@SQLProcessorName", SQLProcessorName);
                            cmd.Parameters.AddWithValue("@SQLNoProcessors", SQLNoProcessors);
                            cmd.Parameters.AddWithValue("@SQLRamAmt", SQLRamAmt);
                            cmd.Parameters.AddWithValue("@SQLHardDrivesize", SQLHardDrivesize);
                            cmd.Parameters.AddWithValue("@SQLFreeHardDrive", SQLFreeHardDrive);
                            cmd.Parameters.AddWithValue("@SQLComments", SQLComments);
                            Sql.Open();
                            cmd.CommandTimeout = 30;
                            cmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Sql.Close();
                Console.WriteLine("Error Occured: " + ex.Message);
            }
        }
        public List<ListOSoftware> GetSoftware(string scannedID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditSoftware  where ComputerName = @scannedID ";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@scannedID", scannedID);
                    Sql.Open();
                    cmd.CommandTimeout = 30;
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        var listOfSoftware = new List<ListOSoftware>();
                        while (SqlReader.Read())
                        {
                            var soft = new ListOSoftware();
                            soft.SoftName = SqlReader["SoftwareName"].ToString();
                            soft.SoftVendor = SqlReader["SoftwareVendor"].ToString();
                            soft.SoftVersion = SqlReader["SoftwareVersion"].ToString();
                            listOfSoftware.Add(soft);
                        }
                        return listOfSoftware;
                    }
                    Sql.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                Sql.Close();
                Console.WriteLine("Error Occured: " + ex.Message);
                return null;
            }
        }
        public List<ListOSingle> GetSingleItemList(string scannedID, string itemType)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                if (itemType == "Printers")
                {
                    SQLStmt = "Select * from PCAuditPrinters  where ComputerName = @scannedID ";
                }
                else     //Peripherals
                {
                    SQLStmt = "Select * from PCAuditPeripherals where ComputerName = @scannedID ";
                }
                
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@scannedID", scannedID);
                    Sql.Open();
                    cmd.CommandTimeout = 30;
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        var listOfSingle = new List<ListOSingle>();
                        while (SqlReader.Read())
                        {
                            var sing = new ListOSingle();
                            //if (itemType == "Network")
                            //{
                            //    soft.SingleItem = SqlReader["NetworkVariable"].ToString();
                            //}
                            if (itemType == "Printers")
                            {
                                sing.SingleItem = SqlReader["PrinterName"].ToString();
                            }
                            else     //Peripherals
                            {
                                sing.SingleItem = SqlReader["PeripheralName"].ToString();
                            }
                            
                            listOfSingle.Add(sing);
                        }
                        return listOfSingle;
                    }
                    Sql.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                Sql.Close();
                Console.WriteLine("Error Occured: " + ex.Message);
                return null;
            }

        }

        public List<ListOMulti> GetMultiItemList(string scannedID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditNetwork  where ComputerName = @scannedID ";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@scannedID", scannedID);
                    Sql.Open();
                    cmd.CommandTimeout = 30;
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        var listOfNetwork = new List<ListOMulti>();
                        while (SqlReader.Read())
                        {
                            var soft = new ListOMulti();
                            soft.NetworkVariable = SqlReader["NetworkVariable"].ToString();
                            soft.NetworkType = SqlReader["NetworkField"].ToString();
                            listOfNetwork.Add(soft);
                        }
                        return listOfNetwork;
                    }
                    Sql.Close();
                }
                return null;
            }
            catch (Exception ex)
            {
                Sql.Close();
                Console.WriteLine("Error Occured: " + ex.Message);
                return null;
            }

        }
    }
}