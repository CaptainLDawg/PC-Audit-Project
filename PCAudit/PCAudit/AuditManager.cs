using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace PCAudit
{
    class AuditManager
    {
        SqlConnection Sql = new SqlConnection(@"Server=VHMSSTS-07\DEV;Database=Logan_Test1;Integrated Security=True");
        SqlCommand SqlStr = new SqlCommand();
        SqlDataReader SqlReader = default(SqlDataReader);
        string SQLStmt;

        public string ComputerName = System.Environment.MachineName;
        public string ManufacturerName;
        public string ModelName;
        public string OperatingSystem;
        public string OperatingArchitecture;
        public string ServicePack;
        public string SerialNumber;
        public string ProcessorName;
        public int NoProcessors;
        public double RamAmt;
        public double HardDrivesize;
        public double FreeHardDrive;

        public DataTable getChangeControlInfo(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                //Select * From Student s Full Outer Join Student_Papers p on s.Student_ID = p.Student_ID	
                SQLStmt = "Select * from PCAuditHardwareChangeControl  where ComputerName like '%' + @SrchComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }

        public void UpdateMainHardwareTable(string action, string SQLAuditDate, string SQLAuditorName, string SQLManufacturerName, string SQLModelName, string SQLComputerName, string SQLOperatingSystem, string SQLOperatingArchitecture, string SQLServicePack, string SQLSerialNumber, double SQLRamAmt, string SQLProcessorName, int SQLNoProcessors, double SQLHardDrivesize, double SQLFreeHardDrive, string SQLComments)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Delete from PCAuditHardwareChangeControl where ComputerName like '%' + @SQLComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SQLComputerName", SQLComputerName);
                    Sql.Open();
                    cmd.ExecuteNonQuery();
                    Sql.Close();
                        if (action == "Approve")
                        {
                            SQLStmt = "Update PCAuditHardware Set AuditDate=@SQLAuditDate,AuditorName=@SQLAuditorName,ManufacturerName=@SQLManufacturerName,ModelName=@SQLModelName,OperatingSystem=@SQLOperatingSystem,OperatingArchitecture=@SQLOperatingArchitecture,ServicePack=@SQLServicePack,SerialNumber=@SQLSerialNumber,ProcessorName=@SQLProcessorName,NoProcessors=@SQLNoProcessors,RamAmt= @SQLRamAmt,HardDrivesize=@SQLHardDrivesize,FreeHardDrive=@SQLFreeHardDrive,Comments=@SQLComments WHERE ComputerName = @SQLComputerName";
                            SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                            using (dcmd)
                            {
                                dcmd.Parameters.AddWithValue("@SQLAuditDate", SQLAuditDate);
                                dcmd.Parameters.AddWithValue("@SQLAuditorName", SQLAuditorName);
                                dcmd.Parameters.AddWithValue("@SQLManufacturerName", SQLManufacturerName);
                                dcmd.Parameters.AddWithValue("@SQLModelName", SQLModelName);
                                dcmd.Parameters.AddWithValue("@SQLComputerName", SQLComputerName);
                                dcmd.Parameters.AddWithValue("@SQLOperatingSystem", SQLOperatingSystem);
                                dcmd.Parameters.AddWithValue("@SQLOperatingArchitecture", SQLOperatingArchitecture);
                                dcmd.Parameters.AddWithValue("@SQLServicePack", SQLServicePack);
                                dcmd.Parameters.AddWithValue("@SQLSerialNumber", SQLSerialNumber);
                                dcmd.Parameters.AddWithValue("@SQLProcessorName", SQLProcessorName);
                                dcmd.Parameters.AddWithValue("@SQLNoProcessors", SQLNoProcessors);
                                dcmd.Parameters.AddWithValue("@SQLRamAmt", SQLRamAmt);
                                dcmd.Parameters.AddWithValue("@SQLHardDrivesize", SQLHardDrivesize);
                                dcmd.Parameters.AddWithValue("@SQLFreeHardDrive", SQLFreeHardDrive);
                                dcmd.Parameters.AddWithValue("@SQLComments", SQLComments);
                                Sql.Open();
                                dcmd.ExecuteNonQuery();
                                Sql.Close();
                            }
                        }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }

        public string ChangeControlLogin(string username, string password)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditChangeControlLogin where CCusername = @ccUsername and CCpassword = @ccPassword";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@ccUsername", username);
                    cmd.Parameters.AddWithValue("@ccPassword", password);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        return "yes";
                    }
                    Sql.Close();
                }
                return "no";
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
                return "no";
            }
        }


        /////////////////////////////////// OLD ASSESSMENT ////////////////////////////////////////////


        public void CheckIfCompInHardDatabase(string chkComputerName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditHardware where ComputerName like '%' + @chkComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "Delete from PCAuditHardware where ComputerName like '%' + @chkComputerName + '%'";
                        SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                        using (dcmd)
                        {
                            dcmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                            Sql.Open();
                            dcmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void CheckIfCompInNetDatabase(string chkComputerName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditNetwork where ComputerName like '%' + @chkComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "Delete from PCAuditNetwork where ComputerName like '%' + @chkComputerName + '%'";
                        SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                        using (dcmd)
                        {
                            dcmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                            Sql.Open();
                            dcmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void CheckIfCompInPrinDatabase(string chkComputerName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditPrinters where ComputerName like '%' + @chkComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "Delete from PCAuditPrinters where ComputerName like '%' + @chkComputerName + '%'";
                        SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                        using (dcmd)
                        {
                            dcmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                            Sql.Open();
                            dcmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void CheckIfCompInSoftDatabase(string chkComputerName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditSoftware where ComputerName like '%' + @chkComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "Delete from PCAuditSoftware where ComputerName like '%' + @chkComputerName + '%'";
                        SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                        using (dcmd)
                        {
                            dcmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                            Sql.Open();
                            dcmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void CheckIfCompInPeriDatabase(string chkComputerName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditPeripherals where ComputerName like '%' + @chkComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                    Sql.Open();
                    SqlReader = cmd.ExecuteReader();
                    if (SqlReader.HasRows)
                    {
                        Sql.Close();
                        SQLStmt = "Delete from PCAuditPeripherals where ComputerName like '%' + @chkComputerName + '%'";
                        SqlCommand dcmd = new SqlCommand(SQLStmt, Sql);
                        using (dcmd)
                        {
                            dcmd.Parameters.AddWithValue("@chkComputerName", chkComputerName);
                            Sql.Open();
                            dcmd.ExecuteNonQuery();
                            Sql.Close();
                        }
                    }
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void GetCompInfo()
        {
            //GET COMPUTER INFORMATION
            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject process in searcher.Get())
                {
                    process.Get();
                    ManufacturerName = Convert.ToString(process["Manufacturer"]);
                    ModelName = Convert.ToString(process["Model"]);
                    NoProcessors = Convert.ToInt32(process["NumberOfProcessors"]);

                }
            }

            //GET OS INFORMATION
            SelectQuery OS_query = new SelectQuery(@"Select * from Win32_OperatingSystem");
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(OS_query))
            {
                foreach (ManagementObject process in searcher.Get())
                {
                    process.Get();
                    OperatingSystem = Convert.ToString(process["Caption"]);
                    ServicePack = "Service Pack " + Convert.ToString(process["ServicePackMajorVersion"]);
                }
            }

            //GET PROCESSOR INFORMATION
            string proc_query = "SELECT * FROM Win32_Processor";
            ManagementObjectSearcher proc_searcher = new ManagementObjectSearcher(proc_query);
            foreach (ManagementObject process in proc_searcher.Get())
            {
                OperatingArchitecture = Convert.ToString(process["AddressWidth"]) + "-bit";
                ProcessorName = Convert.ToString(process["Name"]);
            }

            //GET BIOS INFORMATION
            string bios_query = "SELECT * FROM Win32_BIOS";
            ManagementObjectSearcher bios_searcher = new ManagementObjectSearcher(bios_query);
            foreach (ManagementObject process in bios_searcher.Get())
            {
                SerialNumber = Convert.ToString(process["SerialNumber"]);
            }

            //GET RAM INFORMATION
            string ram_query = "SELECT * FROM Win32_PhysicalMemory";
            ManagementObjectSearcher ram_searcher = new ManagementObjectSearcher(ram_query);
            foreach (ManagementObject process in ram_searcher.Get())
            {
                RamAmt = (((Convert.ToInt64(process["Capacity"]) / 1024) / 1024) / 1024);
            }

            //GET HardDrive INFORMATION
            string har_query = "SELECT * FROM Win32_LogicalDisk WHERE Name='C:'";
            ManagementObjectSearcher har_searcher = new ManagementObjectSearcher(har_query);
            foreach (ManagementObject process in har_searcher.Get())
            {
                HardDrivesize = (((Convert.ToInt64(process["Size"]) / 1024) / 1024) / 1024);
                FreeHardDrive = (((Convert.ToInt64(process["FreeSpace"]) / 1024) / 1024) / 1024);
            }

            //GET NETWORK INFORMATION
            string net_query = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'";
            ManagementObjectSearcher net_searcher = new ManagementObjectSearcher(net_query);
            foreach (ManagementObject process in net_searcher.Get())
            {
                string tempmac = Convert.ToString(process["MACAddress"]);
                string[] addresses = (string[])process["IPAddress"];

                foreach (string ipaddress in addresses)
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).cbIPAddress.Items.Add(ipaddress);
                        }
                    }
                }
                //MacAddress = Convert.ToString(process["MACAddress"]);
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).cbMacAddress.Items.Add(tempmac);
                    }
                }
            }

            //GET PRINTER INFORMATION
            string prin_query = "SELECT * FROM Win32_Printer";
            ManagementObjectSearcher prin_searcher = new ManagementObjectSearcher(prin_query);
            foreach (ManagementObject process in prin_searcher.Get())
            {
                string tempprin = Convert.ToString(process["Name"]);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        if ((Convert.ToString(process["Default"]) == "True"))
                        {
                            (window as MainWindow).lbPrinters.Items.Add("* " + tempprin);
                        }
                        else
                        {
                            (window as MainWindow).lbPrinters.Items.Add(tempprin);
                        }
                    }
                }
            }
        }
        public void GetSoftwareInfo()
        {
            //GET SOFTWARE INFORMATION

            string sof_query = "SELECT * FROM Win32_Product";
            ManagementObjectSearcher sof_searcher = new ManagementObjectSearcher(sof_query);
            foreach (ManagementObject process in sof_searcher.Get())
            {
                string tempSoftwareName = Convert.ToString(process["Name"]);
                string tempSoftwareVendor = Convert.ToString(process["Vendor"]);
                string tempSoftwareVersion = Convert.ToString(process["Version"]);

                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(MainWindow))
                    {
                        (window as MainWindow).dgSoftware.Items.Add(new { SoftwareName = tempSoftwareName, Vendor = tempSoftwareVendor, Version = tempSoftwareVersion });
                    }
                }

            }
        }
        public void AuditComputerData(string SQLAuditDate, string SQLAuditorName, string SQLManufacturerName, string SQLModelName, string SQLComputerName, string SQLOperatingSystem, string SQLOperatingArchitecture, string SQLServicePack, string SQLSerialNumber, double SQLRamAmt, string SQLProcessorName, int SQLNoProcessors, double SQLHardDrivesize, double SQLFreeHardDrive, string SQLComments)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Insert into PCAuditHardware values(@SQLAuditDate, @SQLAuditorName, @SQLManufacturerName, @SQLModelName, @SQLComputerName, @SQLOperatingSystem, @SQLOperatingArchitecture, @SQLServicePack, @SQLSerialNumber, @SQLProcessorName, @SQLNoProcessors, @SQLRamAmt, @SQLHardDrivesize, @SQLFreeHardDrive, @SQLComments)";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
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

                    cmd.ExecuteNonQuery();

                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void AuditNetworkInfo(string NtwComputerName, string NtwNetworkField, string NtwNetworkVariable)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Insert into PCAuditNetwork values(@NtwComputerName, @NtwNetworkField, @NtwNetworkVariable)";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@NtwComputerName", NtwComputerName);
                    cmd.Parameters.AddWithValue("@NtwNetworkField", NtwNetworkField);
                    cmd.Parameters.AddWithValue("@NtwNetworkVariable", NtwNetworkVariable);
                    Sql.Open();
                    cmd.ExecuteNonQuery();
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void AuditPrintersInfo(string prtrComputerName, string prtrPrinterName)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Insert into PCAuditPrinters values(@prtrComputerName, @prtrPrinterName)";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@prtrComputerName", prtrComputerName);
                    cmd.Parameters.AddWithValue("@prtrPrinterName", prtrPrinterName);
                    Sql.Open();
                    cmd.ExecuteNonQuery();
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void AuditSoftwareInfo(string sftComputerName, string sftSoftwareName, string sftSoftwareVendor, string sftSoftwareVersion)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Insert into PCAuditSoftware values(@sftComputerName, @sftSoftwareName, @sftSoftwareVendor, @sftSoftwareVersion)";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@sftComputerName", sftComputerName);
                    cmd.Parameters.AddWithValue("@sftSoftwareName", sftSoftwareName);
                    cmd.Parameters.AddWithValue("@sftSoftwareVendor", sftSoftwareVendor);
                    cmd.Parameters.AddWithValue("@sftSoftwareVersion", sftSoftwareVersion);
                    Sql.Open();
                    cmd.ExecuteNonQuery();
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void AuditPeripherals(string periComputerName, string periPeripherals)
        {
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Insert into PCAuditPeripherals values(@periComputerName, @periPeripherals)";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@periComputerName", periComputerName);
                    cmd.Parameters.AddWithValue("@periPeripherals", periPeripherals);
                    Sql.Open();
                    cmd.ExecuteNonQuery();
                    Sql.Close();
                }
            }
            catch (Exception ex)
            {
                Sql.Close();
                MessageBox.Show("Error Occured: " + ex.Message);
            }
        }
        public void GetSoftwareInfoAndAudit(string sftComputerName)
        {
            //GET SOFTWARE INFORMATION

            string sof_query = "SELECT * FROM Win32_Product";
            ManagementObjectSearcher sof_searcher = new ManagementObjectSearcher(sof_query);
            foreach (ManagementObject process in sof_searcher.Get())
            {
                string tempSoftwareName = Convert.ToString(process["Name"]);
                string tempSoftwareVendor = Convert.ToString(process["Vendor"]);
                string tempSoftwareVersion = Convert.ToString(process["Version"]);

                AuditSoftwareInfo(sftComputerName, tempSoftwareName, tempSoftwareVendor, tempSoftwareVersion);

            }
        }
        public DataTable SearchComputerNameInfo(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                //Select * From Student s Full Outer Join Student_Papers p on s.Student_ID = p.Student_ID	
                SQLStmt = "Select * from PCAuditHardware  where ComputerName like '%' + @SrchComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public DataTable GetIPAddressInfo(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                //Select * From Student s Full Outer Join Student_Papers p on s.Student_ID = p.Student_ID	
                SQLStmt = "Select * from PCAuditNetwork  where ComputerName like '%' + @SrchComputerName + '%' AND NetworkField = 'IPAddress'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public DataTable GetMACAddressInfo(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                //Select * From Student s Full Outer Join Student_Papers p on s.Student_ID = p.Student_ID	
                SQLStmt = "Select * from PCAuditNetwork  where ComputerName like '%' + @SrchComputerName + '%' AND NetworkField = 'MACAddress'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public DataTable GetPeripheralFromDatabase(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditPeripherals  where ComputerName like '%' + @SrchComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public DataTable GetPrintersFromDatabase(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditPrinters  where ComputerName like '%' + @SrchComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public DataTable GetSoftwareFromDatabase(string SrchComputerName)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlStr.Connection = Sql;
                SQLStmt = "Select * from PCAuditSoftware where ComputerName like '%' + @SrchComputerName + '%'";
                SqlCommand cmd = new SqlCommand(SQLStmt, Sql);
                using (cmd)
                {
                    cmd.Parameters.AddWithValue("@SrchComputerName", SrchComputerName);
                    Sql.Open();
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
                MessageBox.Show("Error Occured: " + ex.Message);
            }
            return dt;
        }
        public void GetSoftwareInfoAndSaveCSV(string sftFilePath)
        {
            //GET SOFTWARE INFORMATION

            string sof_query = "SELECT * FROM Win32_Product";
            ManagementObjectSearcher sof_searcher = new ManagementObjectSearcher(sof_query);
            ExportingInformation ei = new ExportingInformation();
            ei.Show();
            foreach (ManagementObject process in sof_searcher.Get())
            {
                string tempSoftwareName = Convert.ToString(process["Name"]);
                string tempSoftwareVendor = Convert.ToString(process["Vendor"]);
                string tempSoftwareVersion = Convert.ToString(process["Version"]);

                using (StreamWriter sw = File.AppendText(sftFilePath))
                {
                    sw.WriteLine("$$$###**!!Software!!**###$$$," + tempSoftwareName.Replace(',', '-') + "," + tempSoftwareVendor.Replace(',', '-') + "," + tempSoftwareVersion.Replace(',', '-'));
                    sw.Flush();
                    sw.Close();
                }
            }
            ei.Hide();
        }

    }
}
