using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Net;
using Android.Graphics;
using Java.IO;
using Android.Graphics.Drawables;
using Android.Util;
using System.Net;
using System.IO;

namespace PCAuditReader
{
        public class DataAdapter : BaseAdapter<ListOSoftware>
        {
            List<ListOSoftware> items;

            Activity context;
            public DataAdapter(Activity context, List<ListOSoftware> items): base()
            {
                this.context = context;
                this.items = items;
            }
           
            public override long GetItemId (int position)
            {
                return position;
            }
            public override ListOSoftware this[int position]
            {
                get { return items[position]; }
            }
            public override int Count
            {
                get { return items.Count; }
            }
            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = items[position];
                View view = convertView;
                if (view == null) // no view to re-use, create new
                    view = context.LayoutInflater.Inflate(Resource.Layout.CustomRow, null);

            view.FindViewById<TextView>(Resource.Id.lblSoftwareName).Text = "Software Name: " + item.SoftName.ToString(); ;
            view.FindViewById<TextView>(Resource.Id.lblSoftwareVendor).Text = "Software Vendor: " + item.SoftVendor.ToString();
                view.FindViewById<TextView>(Resource.Id.lblSoftwareVersion).Text = "Software Version: " + item.SoftVersion.ToString();
                return view;
            }

        }
    }