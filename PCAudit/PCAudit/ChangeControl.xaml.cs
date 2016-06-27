using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Data;
using System.Text.RegularExpressions;

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for ChangeControl.xaml
    /// </summary>
    public partial class ChangeControl : Window
    {
        AuditManager am = new AuditManager();

        public int MWNoProcessors;
        public double MWRamAmt;
        public double MWHardDriveSize;
        public double MWFreeHardDrive;
        bool selectionChangedSafe;
        public ChangeControl()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            updateSelectField();
        }

        private void updateSelectField()
        {
            var computernames = am.getChangeControlInfo("%").DefaultView;
            foreach (DataRowView computeritem in computernames)
            {
                lbComputers.Items.Add(computeritem.Row[4].ToString());
            }
            selectionChangedSafe = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void lbComputers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (selectionChangedSafe == true)
            {
                string ComputerNameList = lbComputers.SelectedItem.ToString();

                var computerHardnames = am.getChangeControlInfo(ComputerNameList).DefaultView;
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
                    rtxtComments.Document = ObjFdoc;
                }
            }
        }

        private void ApproveOrDeny(string action)
        {
            MWNoProcessors = Convert.ToInt32(txtNoProcessors.Text);
            MWRamAmt = Convert.ToDouble(txtRam.Text);
            MWHardDriveSize = Convert.ToDouble(txtHardDriveSize.Text);
            MWFreeHardDrive = Convert.ToDouble(txtFreeHardDrive.Text);
            string CommentsToString = (new TextRange(rtxtComments.Document.ContentStart, rtxtComments.Document.ContentEnd).Text);

            am.UpdateMainHardwareTable(action, txtAuditDate.Text, txtAuditorName.Text, txtManufacturer.Text, txtModel.Text, txtComputerName.Text, txtOS.Text, txtOSArch.Text, txtServicePack.Text, txtSerialNumber.Text, MWRamAmt, txtProcessor.Text, MWNoProcessors, MWHardDriveSize, MWFreeHardDrive, CommentsToString);
            if (action == "Approve")
            {
                MessageBox.Show("Record Approved");
            }
            else
            {
                MessageBox.Show("Record Denied");
            }

            //Clear all fields and refresh selection field

            txtAuditDate.Text = "";
            txtAuditorName.Text = "";
            txtManufacturer.Text = "";
            txtModel.Text = "";
            txtComputerName.Text = "";
            txtOS.Text = "";
            txtOSArch.Text = "";
            txtServicePack.Text = "";
            txtSerialNumber.Text = "";
            txtProcessor.Text = "";
            txtNoProcessors.Text = "";
            txtRam.Text = "";
            txtHardDriveSize.Text = "";
            txtFreeHardDrive.Text = "";
            rtxtComments.Document.Blocks.Clear();
            selectionChangedSafe = false;
            lbComputers.Items.Clear();
            updateSelectField();

        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            ApproveOrDeny("Approve");
        }

        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            ApproveOrDeny("Deny");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
