using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing;
using ZXing.QrCode;

namespace PCAuditReader
{
    [Activity(Label = "PCAuditReader", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        DatabaseManager objDm;

        EditText txtAuditDate;
        EditText txtAuditor;
        EditText txtManu;
        EditText txtModel;
        EditText txtComputerName;
        EditText txtOperSys;
        EditText txtOperSysArch;
        EditText txtServicePack;
        EditText txtSerialNo;
        EditText txtProcessorName;
        EditText txtProcessorsAmt;
        EditText txtRamAmt;
        EditText txtHardDriveSize;
        EditText txtAvailSpace;
        EditText txtComments;

        Button btnViewMore;
        Button btnScan;
        Button btnSave;
        bool UpdateInformation = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            txtAuditDate = FindViewById<EditText>(Resource.Id.txtAuditDate);
            txtAuditor = FindViewById<EditText>(Resource.Id.txtAuditor);
            txtManu= FindViewById<EditText>(Resource.Id.txtManu);
            txtModel = FindViewById<EditText>(Resource.Id.txtModel);
            txtComputerName = FindViewById<EditText>(Resource.Id.txtCompName);
            txtOperSys = FindViewById<EditText>(Resource.Id.txtOS);
            txtOperSysArch = FindViewById<EditText>(Resource.Id.txtOSArch);
            txtServicePack = FindViewById<EditText>(Resource.Id.txtServicePack);
            txtSerialNo = FindViewById<EditText>(Resource.Id.txtSerialNo);
            txtProcessorName = FindViewById<EditText>(Resource.Id.txtProcessName);
            txtProcessorsAmt = FindViewById<EditText>(Resource.Id.txtProcessorsNo);
            txtRamAmt = FindViewById<EditText>(Resource.Id.txtRam);
            txtHardDriveSize = FindViewById<EditText>(Resource.Id.txtHardDriveSize);
            txtAvailSpace = FindViewById<EditText>(Resource.Id.txtAvailSpace);
            txtComments = FindViewById<EditText>(Resource.Id.txtComments);

            btnSave = FindViewById<Button>(Resource.Id.btnUpdate);
            btnViewMore = FindViewById<Button>(Resource.Id.btnViewMore);
            btnScan = FindViewById<Button>(Resource.Id.btnScan);

            btnViewMore.Click += BtnViewMore_Click;
            btnScan.Click += BtnScan_Click;
            btnSave.Click += BtnSave_Click;
        }

        private void BtnViewMore_Click(object sender, EventArgs e)
        {
            
            if (UpdateInformation == true)
            {
                //StartActivity(typeof(ListActivity));
                var startListActivity = new Intent(this, typeof(ListActivity));
                startListActivity.PutExtra("ComputerNameSetting", txtComputerName.Text);
                StartActivity(startListActivity);
            }
            else
            {
                Toast.MakeText(this, "You must scan first", ToastLength.Short).Show();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (UpdateInformation == true)
            {
                objDm = new DatabaseManager();
                objDm.UpdateInfo(txtAuditDate.Text, txtAuditor.Text, txtManu.Text, txtModel.Text, txtComputerName.Text, txtOperSys.Text,txtOperSysArch.Text, txtServicePack.Text,txtSerialNo.Text,txtProcessorName.Text, Convert.ToInt32(txtProcessorsAmt.Text),Convert.ToInt32(txtRamAmt.Text),Convert.ToDouble(txtHardDriveSize.Text),Convert.ToDouble(txtAvailSpace.Text),txtComments.Text);
                Toast.MakeText(this, "Information added to Change Control", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "You must scan first", ToastLength.Short).Show();
            }
        }

        private async void BtnScan_Click(object sender, EventArgs e)
        {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null)
            {
               // Toast.MakeText(this, "Scanned Barcode: " + result.ToString(), ToastLength.Short).Show();       //Show ID
                objDm = new DatabaseManager();
                var PCHardwareInfo = objDm.CheckData(result.ToString()).DefaultView;         //Search Database for ID
                //Change labels to show data
                foreach (System.Data.DataRowView PCHardwareItem in PCHardwareInfo)
                {
                    txtAuditDate.Text = PCHardwareItem.Row[0].ToString();
                    txtAuditor.Text = PCHardwareItem.Row[1].ToString();
                    txtManu.Text = PCHardwareItem.Row[2].ToString();
                    txtModel.Text = PCHardwareItem.Row[3].ToString();
                    txtComputerName.Text = PCHardwareItem.Row[4].ToString();
                    txtOperSys.Text = PCHardwareItem.Row[5].ToString();
                    txtOperSysArch.Text = PCHardwareItem.Row[6].ToString();
                    txtServicePack.Text = PCHardwareItem.Row[7].ToString();
                    txtSerialNo.Text = PCHardwareItem.Row[8].ToString();
                    txtProcessorName.Text = PCHardwareItem.Row[9].ToString();
                    txtProcessorsAmt.Text = PCHardwareItem.Row[10].ToString();
                    txtRamAmt.Text = PCHardwareItem.Row[11].ToString();
                    txtHardDriveSize.Text = PCHardwareItem.Row[12].ToString();
                    txtAvailSpace.Text = PCHardwareItem.Row[13].ToString();
                    txtComments.Text = PCHardwareItem.Row[14].ToString();
                }
                UpdateInformation = true;
            }
            else
                Toast.MakeText(this, "Invalid QRCode", ToastLength.Short).Show();
        }
    }
}

