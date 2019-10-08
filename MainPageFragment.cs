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
    public class MainPageFragment : Android.Support.V4.App.Fragment
    {
        MainActivity mainActivity;
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
            var view= inflater.Inflate(Resource.Layout.main_page, container, false);
            var btnEnterPackage = view.FindViewById<Button>(Resource.Id.btnEnterPackage);
            var btnShowPackage = view.FindViewById<Button>(Resource.Id.btnShowPackage);
            btnEnterPackage.Click += BtnEnterPackage_Click;
            btnShowPackage.Click += BtnShowPackage_Click;
            return view;
        }
        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            mainActivity = (MainActivity)context;
        }
        private void BtnShowPackage_Click(object sender, EventArgs e)
        {
            DetailsFragment detailsFragment = new DetailsFragment();
            Android.Support.V4.App.FragmentTransaction fragmentTransaction = mainActivity.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.frameContainer, detailsFragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();
        }

        private void BtnEnterPackage_Click(object sender, EventArgs e)
        {
            EnterDetailsFragment enterDetailsFragment = new EnterDetailsFragment();
            Android.Support.V4.App.FragmentTransaction fragmentTransaction = mainActivity.SupportFragmentManager.BeginTransaction();
            fragmentTransaction.Replace(Resource.Id.frameContainer, enterDetailsFragment);
            fragmentTransaction.AddToBackStack(null);
            fragmentTransaction.Commit();

        }
    }
}