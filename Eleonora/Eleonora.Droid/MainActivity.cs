using Android.App;
using Android.Widget;
using Android.OS;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Support.Design.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Eleonora.Core.Models;
using Eleonora.Droid.Adapters;
using System;
using Android.Util;
using Plugin.Media.Abstractions;
using Eleonora.Droid.Services;
using System.IO;
using System.Threading.Tasks;
using Android.Views;
using System.Collections.Generic;
using Eleonora.PCL.Services;
using Newtonsoft.Json;
using System.Linq;
// using Gcm.Client;
#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
#endif

namespace Eleonora.Droid
{
    [Activity(Label = "Eleonora.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        ListView resultsListView;
        EditText cajaBusqueda;
        FloatingActionButton fab;
        static Stream streamCopy;

        // Adapter to map the items list to the view
        private SearchAdapter adapter;
        private List<Search> listSearch;

        // URL of the mobile app backend.
        const string applicationURL = @"http://kiradev.azurewebsites.net";
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            // Elements
            var toolbar = FindViewById<Toolbar>(Resource.Id.searchToolBar);
            SetSupportActionBar(toolbar);

            //  Para jugar con el texto escrito
            cajaBusqueda = FindViewById<EditText>(Resource.Id.searchBox);
            cajaBusqueda.Text = "TORRE EIFFEL";
            cajaBusqueda.KeyPress += TxtSearch_KeyPress;

            // Mobile Center 
            MobileCenter.Start("ff86a1b3-7d26-44e3-930a-42699b0404f4",
                   typeof(Analytics), typeof(Crashes));

            // Plugin Conectivity
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += Current_ConnectivityChanged;

            fab = FindViewById<FloatingActionButton>(Resource.Id.floatingActionButton1);
            fab.Click += Fab_Click;

            resultsListView = FindViewById<ListView>(Resource.Id.chat_list_view);
            // Create an adapter to bind the items with the view
            listSearch = new List<Search>();
            adapter = new SearchAdapter(this, listSearch);
            resultsListView.Adapter = adapter;

        }

        private async void TxtSearch_KeyPress(object sender, View.KeyEventArgs e)
        {
            try
            {
                e.Handled = false;
                if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
                {
                    AddItem();
                    // var res = await BingService.FetchTokenAsync(cajaBusqueda.Text);
                    // Toast.MakeText(this, cajaBusqueda.Text, ToastLength.Short);
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
            }
        }
        
        public async void AddItem()
        {
            if (string.IsNullOrWhiteSpace(cajaBusqueda.Text))
            {
                return;
            }

            // Create a new item
            var item = new Search
            {
                Text = cajaBusqueda.Text,
                Email = "demo@microsoft.com"
            };

            try
            {
                adapter.Add(item);
            }
            catch (Exception e)
            {
                Snackbar
                .Make(fab, e.Message, Snackbar.LengthShort)
                .Show();
            }

            cajaBusqueda.Text = "";
        }
        private async void Fab_Click(object sender, EventArgs e)
        {
            MediaFile file = null;
            try
            {
                file = await ServiceImage.TakePicture(true);
            }
            catch (Android.OS.OperationCanceledException)
            {
            }
            SetImageToControl(file);
        }

        private void SetImageToControl(MediaFile file)
        {
            if (file == null)
            {
                return;
            }
            ImageView imgImage = FindViewById<ImageView>(Resource.Id.ImageCam);
            imgImage.SetImageURI(Android.Net.Uri.Parse(file.Path));
            var stream = file.GetStream();
            streamCopy = new MemoryStream();
            stream.CopyTo(streamCopy);
            stream.Seek(0, SeekOrigin.Begin);
            file.Dispose();
            Analize();
        }

        private async void Analize()
        {
            try
            {
                if (streamCopy != null)
                {
                    try
                    {
                        streamCopy.Seek(0, SeekOrigin.Begin);
                        var serviceCV = new ServiceComputerVision();
                        var models = await serviceCV.GetAvailableDomainModels();
                        var selectedModel = models.Models[1];
                        var result = await serviceCV.MakeAnalysisRequest(streamCopy, selectedModel);
                        if (result == null)
                        {
                            Log.Info("MakeAnalysisRequestResult", "null");
                            return;
                        }

                        if (result.Metadata != null)
                        {
                            Log.Info("MakeAnalysisRequestResult", "Image Format : " + result.Metadata.Format);
                            Log.Info("MakeAnalysisRequestResult", "Image Dimensions : " + result.Metadata.Width + " x " + result.Metadata.Height);
                        }

                        if (result.Result != null)
                        {
                            Log.Info("MakeAnalysisRequestResult", "Result : " + result.Result.ToString());
                        }
                        Landmarks landmarks = JsonConvert.DeserializeObject<Landmarks>(result.Result.ToString());
                        Landmark landmark = landmarks.Items.FirstOrDefault();
                        var item = new Search() { Email = "matrix549_8@hotmail.com", Text = landmark.Name};
                        adapter.Add(item);
                        cajaBusqueda.Text = landmark.Name;
                        FindViewById<ImageView>(Resource.Id.ImageCam).Visibility = ViewStates.Visible;
                    }
                    catch (Exception ex)
                    {
                        // Toast.MakeText(this, "Se ha presentado un error al conectar con los servicios", ToastLength.Short).Show();
                        Snackbar
                        .Make(fab, ex.Message, Snackbar.LengthShort)
                        .Show();
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Snackbar
                .Make(fab, e.Message, Snackbar.LengthShort)
                .Show();
                return;
            }
        }

        private void Current_ConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            // var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
            }
            else
            {
                Snackbar
                        .Make(fab, "Esta aplicacion requiere de Internet para trabajar", Snackbar.LengthShort)
                        .Show();
                fab.Enabled = false;
            }
        }

        private void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        private void CreateAndShowDialog(string message, string title)
        {
            Snackbar
            .Make(fab, message, Snackbar.LengthShort)
            .Show();
        }
    }
}

