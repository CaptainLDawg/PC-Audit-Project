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
using System.Windows.Shapes;
using System.Data;

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for ReadDatabase.xaml
    /// </summary>
    public partial class ReadDatabase : Window
    {
        AuditManager am = new AuditManager();
        public ReadDatabase()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var computernames = am.SearchComputerNameInfo("%").DefaultView;
            foreach (DataRowView computeritem in computernames)
            {
                lbComputers.Items.Add(computeritem.Row[4].ToString());
            }
        }

        private void lbComputers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgSoftware.Items.Clear();
            cbIPAddress.Items.Clear();
            cbMacAddress.Items.Clear();
            lbPeripherals.Items.Clear();
            lbPrinters.Items.Clear();

            string ComputerNameList = lbComputers.SelectedItem.ToString();

            var computerHardnames = am.SearchComputerNameInfo(ComputerNameList).DefaultView;
            foreach (DataRowView computeritem in computerHardnames)
            {
                //Adds all Hardware information to textbox
                txtAuditDate.Text = computeritem.Row[0].ToString();
                txtAuditorName.Text = computeritem.Row[1].ToString();
                txtManufacturer.Text = computeritem.Row[2].ToString();
                txtModel.Text = computeritem.Row[3].ToString();
                txtComputerName.Text = computeritem.Row[4].ToString();
                txtOS.Text = computeritem.Row[5].ToString();
                txtOSArch.Text = computeritem.Row[6].ToString();
                txtServicePack.Text = computeritem.Row[7].ToString();
                txtSerialNumber.Text = computeritem.Row[8].ToString();
                txtProcessor.Text = computeritem.Row[9].ToString();
                txtNoProcessors.Text = computeritem.Row[10].ToString();
                txtRam.Text = computeritem.Row[11].ToString();
                txtHardDriveSize.Text = computeritem.Row[12].ToString();
                txtFreeHardDrive.Text = computeritem.Row[13].ToString();

                //Read comments column and add it to PCAuditTool
                FlowDocument ObjFdoc = new FlowDocument();
                Paragraph ObjPara1 = new Paragraph(); 
                ObjPara1.Inlines.Add(new Run(computeritem.Row[14].ToString()));
                ObjFdoc.Blocks.Add(ObjPara1);
                rtxtComments.Document=ObjFdoc;
            }
            var computerNetIPnames = am.GetIPAddressInfo(ComputerNameList).DefaultView;
            foreach (DataRowView ipitem in computerNetIPnames)
            {
                cbIPAddress.Items.Add(ipitem.Row[2].ToString());
            }
            cbIPAddress.SelectedIndex = 0;
            var computerNetMACnames = am.GetMACAddressInfo(ComputerNameList).DefaultView;
            foreach (DataRowView ipitem in computerNetMACnames)
            {
                cbMacAddress.Items.Add(ipitem.Row[2].ToString());
            }
            cbMacAddress.SelectedIndex = 0;

            var ComputerPeriNames = am.GetPeripheralFromDatabase(ComputerNameList).DefaultView;
            foreach (DataRowView periitem in ComputerPeriNames)
            {
                lbPeripherals.Items.Add(periitem.Row[1].ToString());
            }

            var ComputerPrintNames = am.GetPrintersFromDatabase(ComputerNameList).DefaultView;
            foreach (DataRowView printitem in ComputerPrintNames)
            {
                lbPrinters.Items.Add(printitem.Row[1].ToString());
            }

            //Reads info from database to PCAuditTool
            var ComputerSoftNames = am.GetSoftwareFromDatabase(ComputerNameList).DefaultView;
            foreach (DataRowView softitem in ComputerSoftNames)
            {
                dgSoftware.Items.Add(new { SoftwareName = softitem.Row[1].ToString(), Vendor = softitem.Row[2].ToString(), Version = softitem.Row[3].ToString() });
            }
        }
        }
}
