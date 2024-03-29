﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Essentials;
using Prism;
using Prism.Ioc;
using Acr.UserDialogs;
using FFImageLoading.Forms.Platform;

namespace HorusApp.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			UserDialogs.Init(this);
			Xamarin.Essentials.Platform.Init(this, savedInstanceState);

			global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(false);
            LoadApplication(new App(new AndroidInitializer()));

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

