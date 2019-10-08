using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Xamarin_Test
{
    public class EnterDetailsFragment : Android.Support.V4.App.Fragment
    {
        EditText edtBarcode, edtWidth, edtHeight, edtDepth;
        MainActivity mainActivity;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.enter_package_detail, container, false);
            edtBarcode = view.FindViewById<EditText>(Resource.Id.edtBarcode);
            edtWidth = view.FindViewById<EditText>(Resource.Id.edtWidth);
            edtHeight = view.FindViewById<EditText>(Resource.Id.edtHeight);
            edtDepth = view.FindViewById<EditText>(Resource.Id.edtDepth);
            var btnReset = view.FindViewById<Button>(Resource.Id.btnReset);
            var btnSave = view.FindViewById<Button>(Resource.Id.btnSave);
            btnReset.Click += BtnReset_Click;
            btnSave.Click += BtnSave_Click;
            return view;
        }
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            mainActivity = (MainActivity)context;
        }

        private async void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edtBarcode.Text))
            {
                edtBarcode.SetError("Please enter barcode", null);
                Toast.MakeText(this.Activity, "Please enter barcode", ToastLength.Long).Show();
                return;
            }

            else if (string.IsNullOrEmpty(edtWidth.Text))
            {
                edtWidth.SetError("Please enter width", null);
                Toast.MakeText(this.Activity, "Please enter width", ToastLength.Long).Show();
                return;
            }

            else if (string.IsNullOrEmpty(edtHeight.Text))
            {
                edtHeight.SetError("Please enter height", null);
                Toast.MakeText(this.Activity, "Please enter height", ToastLength.Long).Show();
                return;
            }

           else if (string.IsNullOrEmpty(edtDepth.Text))
            {
                edtDepth.SetError("Please enter depth", null);
                Toast.MakeText(this.Activity, "Please enter depth", ToastLength.Long).Show();
                return;
            }
            PackageTable packageTable = new PackageTable();
            packageTable.BarCode = edtBarcode.Text;
            packageTable.Width = (float)Convert.ToDouble(edtWidth.Text);
            packageTable.Height = (float)Convert.ToDouble(edtHeight.Text);
            packageTable.Depth = (float)Convert.ToDouble(edtDepth.Text);
            var id = await SqlService.Instance.SavePackageAsync(packageTable);
            if (id > 0)
            {
                this.Activity.RunOnUiThread(() =>
                {
                    Toast.MakeText(this.Activity, "Dimms " + "( " + packageTable.Width + " x " + packageTable.Height + " x " + packageTable.Depth + " ) " + packageTable.BarCode + " saved", ToastLength.Long).Show();
                });
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            edtBarcode.Text = string.Empty;
            edtWidth.Text = string.Empty;
            edtHeight.Text = string.Empty;
            edtDepth.Text = string.Empty;
        }
    }
}