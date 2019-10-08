using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Database;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Xamarin_Test
{
    public class DetailsFragment : Android.Support.V4.App.Fragment
    {
        MainActivity mainActivity;
        ListView barCodeList;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // 

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.package_details_list, container, false);
             barCodeList = view.FindViewById<ListView>(Resource.Id.lstPackages);

            BindPackageDetails();
            return view;
        }
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            mainActivity = (MainActivity)context;
        }
        async void BindPackageDetails()
        {
            List<PackageTable> packageList =  await SqlService.Instance.GetPackageDetailssAsync();
            barCodeList.Adapter = new PackagesAdapter(mainActivity, packageList);
            
        }
    }
    public class PackagesAdapter : BaseAdapter<string>
    {
        List<PackageTable> _packages;
        Activity _context;
        public PackagesAdapter(Activity context, List<PackageTable> packages) : base()
        {
            this._packages = packages;
            this._context = context;
        }

        public override string this[int position] => this._packages[position].BarCode;

        public override int Count => this._packages.Count;


        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = _context.LayoutInflater.Inflate(Resource.Layout.list_item, null);
            view.FindViewById<TextView>(Resource.Id.txtBarcode).Text = _packages[position].BarCode;
            view.FindViewById<TextView>(Resource.Id.txtDimms).Text = _packages[position].Width.ToString() + " x "
                + _packages[position].Height.ToString() + " x " + _packages[position].Depth.ToString();
            return view;
        }
    }
}