using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AuditManager am = new AuditManager();
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        string CompName;
        public int MWNoProcessors;
        public double MWRamAmt;
        public double MWHardDriveSize;
        public double MWFreeHardDrive;

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            CompName = am.ComputerName;
            
        }

        private void btnAddPeripheral_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddPeri.Text))
            {
                MessageBox.Show("No text detected");
            }
            else
            {
                lbPeripherals.Items.Add(txtAddPeri.Text);
                txtAddPeri.Text = "";
            }

        }

        private void btnRemovePeripherals_Click(object sender, RoutedEventArgs e)
        {
            if (lbPeripherals.SelectedIndex != -1)
            {
                lbPeripherals.Items.RemoveAt(lbPeripherals.SelectedIndex);
            }
            else
            {
                MessageBox.Show("You must select an item from the peripherals list");
            }

        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAddPrinter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddPrinter.Text))
            {
                MessageBox.Show("No text detected");
            }
            else
            {
                lbPrinters.Items.Add(txtAddPrinter.Text);
                txtAddPrinter.Text = "";
            }
        }

        private void btnRemovePrinter_Click(object sender, RoutedEventArgs e)
        {
            if (lbPrinters.SelectedIndex != -1)
            {
                lbPrinters.Items.RemoveAt(lbPrinters.SelectedIndex);
            }
            else
            {
                MessageBox.Show("You must select an item from the printers list");
            }
        }

        private void btnExportcsv_Click(object sender, RoutedEventArgs e)
        {
            FieldsNotEmpty();

            string auditname = txtAuditorName.Text;
            string commentsString = new TextRange(rtxtComments.Document.ContentStart, rtxtComments.Document.ContentEnd).Text;

            // SAVING
            SaveFileDialog SaveCSV = new SaveFileDialog()
            {
                Filter = "csv file(*.csv)|*.csv|All files (*.*)|*.*"
            };

            Nullable<bool> result = SaveCSV.ShowDialog();
            if (result == true)
            {
                string filepath = SaveCSV.FileName;     //gets file path

                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine("$$$###**!!Hardware!!**###$$$," + "," + txtAuditDate.Text.Replace(',', '-') + "," + txtAuditorName.Text.Replace(',', '-') + "," + txtManufacturer.Text.Replace(',', '-') + "," + txtModel.Text.Replace(',', '-') + "," + txtComputerName.Text.Replace(',', '-') + "," + txtOS.Text.Replace(',', '-') + "," + txtOSArch.Text.Replace(',', '-') + "," + txtServicePack.Text.Replace(',', '-') + "," + txtSerialNumber.Text.Replace(',', '-') + "," + txtRam.Text.Replace(',', '-') + "," + txtProcessor.Text.Replace(',', '-') + "," + txtNoProcessors.Text.Replace(',', '-') + "," + txtHardDriveSize.Text.Replace(',', '-') + "," + txtFreeHardDrive.Text.Replace(',', '-') + "," + commentsString.Replace(',', '-'));

                    foreach (string ipAddressName in cbIPAddress.Items)
                    {
                        sw.WriteLine("$$$###*!!IPAddress!!**###$$$," + ipAddressName);
                    }

                    //Getting each Mac address and putting it into one line
                    foreach (string macAddressName in cbMacAddress.Items)
                    {
                        sw.WriteLine("$$$###**!!MACAddress!!**###$$$," + macAddressName);
                    }

                    //ADDING PERIPHERAL
                    foreach (string PeripheralItem in lbPeripherals.Items)
                    {
                        sw.WriteLine("$$$###**!!Peripherals!!**###$$$," + PeripheralItem);
                    }



                    //ADDING PRINTER
                    foreach (string PrinterItem in lbPrinters.Items)
                    {
                        sw.WriteLine("$$$###**!!Printers!!**###$$$," + PrinterItem);
                    }

                    sw.Flush();
                    sw.Close();
                    //ADDING SOFTWARE
                    if (dgSoftware.HasItems)
                    {
                        am.GetSoftwareInfoAndSaveCSV(filepath);
                    }
                }
            }
        }

        public void FieldsNotEmpty()
        {
            //Insurances so that the script doesn't break
            if (string.IsNullOrWhiteSpace(txtAuditDate.Text))
            {
                txtAuditDate.Text = Convert.ToString(System.DateTime.Now);
            }
            if (string.IsNullOrWhiteSpace(txtManufacturer.Text))
            {
                txtManufacturer.Text = "Null";
            }
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                txtModel.Text = "Null";
            }
            if (string.IsNullOrWhiteSpace(txtComputerName.Text))
            {
                txtComputerName.Text = "Null";
            }
            if (string.IsNullOrWhiteSpace(txtOS.Text))
            {
                txtOS.Text = "Null";
            }
            if (string.IsNullOrWhiteSpace(txtOSArch.Text))
            {
                txtOSArch.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtServicePack.Text))
            {
                txtServicePack.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                txtSerialNumber.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtRam.Text))
            {
                txtRam.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtProcessor.Text))
            {
                txtProcessor.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtNoProcessors.Text))
            {
                txtNoProcessors.Text = "";
            }
            if (string.IsNullOrWhiteSpace(cbIPAddress.Text))
            {
                cbIPAddress.Text = "";
            }
            if (string.IsNullOrWhiteSpace(cbMacAddress.Text))
            {
                cbMacAddress.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtHardDriveSize.Text))
            {
                txtHardDriveSize.Text = "";
            }
            if (string.IsNullOrWhiteSpace(txtFreeHardDrive.Text))
            {
                txtFreeHardDrive.Text = "";
            }
            if (string.IsNullOrWhiteSpace(new TextRange(rtxtComments.Document.ContentStart, rtxtComments.Document.ContentEnd).Text))
            {
                rtxtComments.Document.Blocks.Clear();
                rtxtComments.Document.Blocks.Add(new Paragraph(new Run("")));
            }
        }

        private void btnScan_Click(object sender, RoutedEventArgs e)
        {
            LoadingScreen ls = new LoadingScreen();
            if (chkAuditSoftware.IsChecked == true)
            {
                ls.auditsoftware = true;
                dgSoftware.Items.Clear();
                lbPrinters.Items.Clear();
                lbPeripherals.Items.Clear();
                rtxtComments.Document.Blocks.Clear();
                txtAddPrinter.Text = "";
                cbIPAddress.Items.Clear();
                cbMacAddress.Items.Clear();
            }
            else
            {
                dgSoftware.Items.Clear();
                lbPrinters.Items.Clear();
                lbPeripherals.Items.Clear();
                rtxtComments.Document.Blocks.Clear();
                txtAddPrinter.Text = "";
                cbIPAddress.Items.Clear();
                cbMacAddress.Items.Clear();
                ls.auditsoftware = false;
            }
            ls.Show();
            
        }

        private void btnAddDataBase_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAuditorName.Text))
            {
                MessageBox.Show("Please enter your name into the Auditor Name textbox");
            }
            else
            {
                am.CheckIfCompInHardDatabase(txtComputerName.Text); //Checks to see if PC already exists and deletes it if it does - this is to stop mulitple records of the same machine
                am.CheckIfCompInNetDatabase(txtComputerName.Text);
                am.CheckIfCompInPrinDatabase(txtComputerName.Text);
                am.CheckIfCompInPeriDatabase(txtComputerName.Text);
                am.CheckIfCompInSoftDatabase(txtComputerName.Text);

                ExportingInformation ei = new ExportingInformation();
                FieldsNotEmpty();
                string CommentsToString = (new TextRange(rtxtComments.Document.ContentStart, rtxtComments.Document.ContentEnd).Text);

                am.AuditComputerData(txtAuditDate.Text, txtAuditorName.Text, txtManufacturer.Text, txtModel.Text, txtComputerName.Text, txtOS.Text, txtOSArch.Text, txtServicePack.Text, txtSerialNumber.Text, MWRamAmt, txtProcessor.Text, MWNoProcessors, MWHardDriveSize, MWFreeHardDrive, CommentsToString);

                foreach (var ipAddress in cbIPAddress.Items)
                {
                    //Will add every IP address in the IPAddress Combobox to the networking table
                    am.AuditNetworkInfo(txtComputerName.Text, "IPAddress", ipAddress.ToString());
                }

                foreach (var macAddress in cbMacAddress.Items)
                {
                    //Will add every MAC address in the MACAddress Combobox to the networking table
                    am.AuditNetworkInfo(txtComputerName.Text, "MACAddress", macAddress.ToString());
                }

                foreach (var printer in lbPrinters.Items)
                {
                    //Will add every Printer in the Printer listbox to the Printers table
                    am.AuditPrintersInfo(txtComputerName.Text, printer.ToString());
                }

                foreach (var peripheral in lbPeripherals.Items)
                {
                    //Will add every Peripheral in the Peripheral listbox to the Peripheral table
                    am.AuditPeripherals(txtComputerName.Text, peripheral.ToString());
                }

                if (dgSoftware.HasItems)
                {
                    ei.Softcomputername = txtComputerName.Text;
                    ei.ExportingToDatabase = true;
                    ei.Show();
                }
                else
                {
                    MessageBox.Show("Information Added");
                }
            }
        }
        private void rtxtComments_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Stops user from typing in more than 300 characters
            TextRange textRange = new TextRange(rtxtComments.Document.ContentStart, rtxtComments.Document.ContentEnd);
            var text = textRange.Text.Trim();

            if (text.Length > 300)
            {
                int gap = 0;
                while (rtxtComments.CaretPosition.DeleteTextInRun(-1) == 0)
                {
                    rtxtComments.CaretPosition = rtxtComments.CaretPosition.GetPositionAtOffset(--gap);
                }

            }
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {

             OpenFileDialog OpenCSV = new OpenFileDialog()

            {
                Filter = "csv file(*.csv)|*.csv|All files (*.*)|*.*"
            };

            Nullable<bool> result = OpenCSV.ShowDialog();
            if (result == true)
            {

                btnAddDataBase.IsEnabled = true;
                dgSoftware.Items.Clear();
                cbIPAddress.Items.Clear();
                cbMacAddress.Items.Clear();
                lbPeripherals.Items.Clear();
                lbPrinters.Items.Clear();

                string filepath = OpenCSV.FileName;
                StreamReader FileRead = new StreamReader(filepath);

                string line;

                while ((line = FileRead.ReadLine()) != null)
                {
                    if (line.Contains("$$$###**!!Hardware!!**###$$$"))
                    {
                        string[] HardwareNames = line.Split(',');   //Splits the line by comma

                        txtAuditDate.Text = HardwareNames[2];
                        txtAuditorName.Text = HardwareNames[3];
                        txtManufacturer.Text = HardwareNames[4];
                        txtModel.Text = HardwareNames[5];
                        txtComputerName.Text = HardwareNames[6];
                        txtOS.Text = HardwareNames[7];
                        txtOSArch.Text = HardwareNames[8];
                        txtServicePack.Text = HardwareNames[9];
                        txtSerialNumber.Text = HardwareNames[10];
                        txtRam.Text = HardwareNames[11];
                        txtProcessor.Text = HardwareNames[12];
                        txtNoProcessors.Text = HardwareNames[13];
                        txtHardDriveSize.Text = HardwareNames[14];
                        txtFreeHardDrive.Text = HardwareNames[15];

                        //Comments
                        FlowDocument ObjFdoc = new FlowDocument();
                        Paragraph ObjPara1 = new Paragraph();
                        ObjPara1.Inlines.Add(new Run(HardwareNames[16]));
                        ObjFdoc.Blocks.Add(ObjPara1);
                        rtxtComments.Document = ObjFdoc;

                    }
                    else if (line.Contains("$$$###*!!IPAddress!!**###$$$"))
                    {
                        string[] NetworkIP = line.Split(',');
                        cbIPAddress.Items.Add(NetworkIP[1]);
                        cbIPAddress.SelectedIndex = 0;
                    }
                    else if (line.Contains("$$$###**!!MACAddress!!**###$$$"))
                    {
                        string[] NetworkMAC = line.Split(',');
                        cbMacAddress.Items.Add(NetworkMAC[1]);
                        cbMacAddress.SelectedIndex = 0;
                    }
                    else if (line.Contains("$$$###**!!Printers!!**###$$$"))
                    {
                        string[] PrinterNames = line.Split(',');
                        lbPrinters.Items.Add(PrinterNames[1]);
                    }
                    else if (line.Contains("$$$###**!!Peripherals!!**###$$$"))
                    {
                        string[] PeripheralNames = line.Split(',');
                        lbPeripherals.Items.Add(PeripheralNames[1]);
                    }
                    else if (line.Contains("$$$###**!!Software!!**###$$$"))
                    {
                        string[] SoftwareDetails = line.Split(',');
                        dgSoftware.Items.Add(new { SoftwareName = SoftwareDetails[1], Vendor = SoftwareDetails[2], Version = SoftwareDetails[3] });
                    } 
                }
               
            }
        }
        private void btnReadDatabase_Click(object sender, RoutedEventArgs e)
        {
            ReadDatabase rd = new ReadDatabase();
            rd.Show();
        }

        private void btnChangeControl_Click(object sender, RoutedEventArgs e)
        {
            LoginToChangeControl logCC = new LoginToChangeControl();
            logCC.Show();
        }
    }
}
