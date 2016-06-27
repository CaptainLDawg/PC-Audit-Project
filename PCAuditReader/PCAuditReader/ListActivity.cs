using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Data.Common;

namespace PCAuditReader
{
    [Activity(Label = "ListActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ListActivity : Activity
    {
        Button btnBack;
        Button btnPeri;
        Button btnPrint;
        Button btnSoft;
        Button btnNet;
        ListView lstItems;

        string listComputerName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ListInfo);

            listComputerName = Intent.GetStringExtra("ComputerNameSetting") ?? "Data not available";

            btnBack = FindViewById<Button>(Resource.Id.btnBack);
            btnPeri = FindViewById<Button>(Resource.Id.btnPeripherals);
            btnPrint = FindViewById<Button>(Resource.Id.btnPrinters);
            btnSoft = FindViewById<Button>(Resource.Id.btnSoftware);
            btnNet = FindViewById<Button>(Resource.Id.btnNetwork);
            lstItems = FindViewById<ListView>(Resource.Id.lstItems);


            btnBack.Click += BtnBack_Click;
            btnPeri.Click += BtnUpdateList;
            btnPrint.Click += BtnUpdateList;
            btnSoft.Click += BtnUpdateList;
            btnNet.Click += BtnUpdateList;
        }
        private void BtnUpdateList(object sender, EventArgs e)
        {
            string ButtonText = (sender as Button).Text;
            
            if (ButtonText == "Software")
            {
                DatabaseManager objDM = new DatabaseManager();
                var dt = objDM.GetSoftware(listComputerName);
                if (dt != null)
                {
                    lstItems.Adapter = new DataAdapter(this, dt);
                }
                else
                {
                    Toast.MakeText(this, "No records found", ToastLength.Short).Show();
                }            
            }
            else if (ButtonText == "Network")
            {
                DatabaseManager objDM = new DatabaseManager();
                var dt = objDM.GetMultiItemList(listComputerName);
                if (dt != null)
                {
                    lstItems.Adapter = new DataAdapterMulti(this, dt);
                }
                else
                {
                    Toast.MakeText(this, "No records found", ToastLength.Short).Show();
                }
            }
            else
            {
                DatabaseManager objDM = new DatabaseManager();
                var dt = objDM.GetSingleItemList(listComputerName, ButtonText);
                if (dt != null)
                {
                    lstItems.Adapter = new DataAdapterSingle(this, dt);
                }
                else
                {
                    Toast.MakeText(this, "No records found", ToastLength.Short).Show();
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}