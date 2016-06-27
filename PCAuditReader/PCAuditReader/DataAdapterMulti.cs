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
    public class DataAdapterMulti : BaseAdapter<ListOMulti>
    {
        List<ListOMulti> items;

        Activity context;
        public DataAdapterMulti(Activity context, List<ListOMulti> items) : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override ListOMulti this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomRowSmall, null);

            view.FindViewById<TextView>(Resource.Id.lblSingle).Text = item.NetworkVariable.ToString();
            view.FindViewById<TextView>(Resource.Id.lblMulti).Text = item.NetworkType.ToString();

            return view;
        }

    }
}