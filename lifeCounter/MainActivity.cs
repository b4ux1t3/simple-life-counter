using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace lifeCounter
{
    [Activity(Label = "Simple Life Counter", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@style/Theme.AppCompat")]
    public class MainActivity : AppCompatActivity
    {
        private Player playerOne;
        private Player playerTwo;
        private TextView playerOneLife;
        private TextView playerTwoLife;
        private int lifeTotal;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Set up the players and the original life total
            playerOne = new Player();
            playerTwo = new Player();
            lifeTotal = 20;

            #region Register click handlers
            // Get our buttons and our text views from the layout resource
            ImageButton resetButton = FindViewById<ImageButton>(Resource.Id.btnReset);
            Button playerOneLifeUpButton = FindViewById<Button>(Resource.Id.playerOneLifeUp);
            Button playerOneLifeDownButton = FindViewById<Button>(Resource.Id.playerOneLifeDown);
            Button playerTwoLifeUpButton = FindViewById<Button>(Resource.Id.playerTwoLifeUp);
            Button playerTwoLifeDownButton = FindViewById<Button>(Resource.Id.playerTwoLifeDown);

            playerOneLife = FindViewById<TextView>(Resource.Id.playerOneLife);
            playerTwoLife = FindViewById<TextView>(Resource.Id.playerTwoLife);

            // Register click handler delegates for the buttons.
            resetButton.Click               += delegate { playerOne.SetLife(lifeTotal); playerTwo.SetLife(lifeTotal); UpdateLife(playerOne, playerOneLife); UpdateLife(playerTwo, playerTwoLife); };
            playerOneLifeUpButton.Click     += delegate { playerOne.IncrementLife(); UpdateLife(playerOne, playerOneLife); };
            playerOneLifeDownButton.Click   += delegate { playerOne.DecrementLife(); UpdateLife(playerOne, playerOneLife); };
            playerTwoLifeUpButton.Click     += delegate { playerTwo.IncrementLife(); UpdateLife(playerTwo, playerTwoLife); };
            playerTwoLifeDownButton.Click   += delegate { playerTwo.DecrementLife(); UpdateLife(playerTwo, playerTwoLife); };

            // Register LongClick handlers
            playerOneLife.LongClick             += PlayerOneLife_LongClick;
            playerTwoLife.LongClick             += PlayerTwoLife_LongClick;
            playerOneLifeDownButton.LongClick   += PlayerOneLifeDownButton_LongClick;
            playerOneLifeUpButton.LongClick     += PlayerOneLifeUpButton_LongClick;
            playerTwoLifeDownButton.LongClick   += PlayerTwoLifeDownButton_LongClick;
            playerTwoLifeUpButton.LongClick     += PlayerTwoLifeUpButton_LongClick;
            resetButton.LongClick               += HardReset;
            #endregion
        }
        #region LongClick Handlers
        private void PlayerOneLifeUpButton_LongClick(object sender, View.LongClickEventArgs e)
        {
            playerOne.IncrementLife(10);
            UpdateLife(playerOne, playerOneLife);
        }

        private void PlayerOneLifeDownButton_LongClick(object sender, View.LongClickEventArgs e)
        {
            playerOne.DecrementLife(10);
            UpdateLife(playerOne, playerOneLife);
        }

        private void PlayerTwoLifeUpButton_LongClick(object sender, View.LongClickEventArgs e)
        {
            playerTwo.IncrementLife(10);
            UpdateLife(playerTwo, playerTwoLife);
        }

        private void PlayerTwoLifeDownButton_LongClick(object sender, View.LongClickEventArgs e)
        {
            playerTwo.DecrementLife(10);
            UpdateLife(playerTwo, playerTwoLife);
        }

        private void PlayerOneLife_LongClick(object sender, View.LongClickEventArgs e)
        {
            NotImplementedToast();
        }

        private void PlayerTwoLife_LongClick(object sender, View.LongClickEventArgs e)
        {
            NotImplementedToast();
        }
        #endregion
        private void UpdateLife(Player player, TextView lifeDisplay)
        {
            lifeDisplay.Text = player.lifeTotal.ToString();
        }

        private void HardReset(object sender, View.LongClickEventArgs e)
        {
            int tempLifeTotal = lifeTotal;

            // Create the inflater
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View resetDialog = layoutInflater.Inflate(Resource.Layout.ResetLifeTotal, null);

            // Create the dialog builder
            Android.Support.V7.App.AlertDialog.Builder lifeTotalDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            lifeTotalDialogBuilder.SetView(resetDialog);

            // Initialize the spinner
            Spinner lifeTotalSpinner = resetDialog.FindViewById<Spinner>(Resource.Id.lifeTotalSpinner);
            lifeTotalSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

            //Fill the spinner
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.lifeTotalArray, Android.Resource.Layout.SimpleSpinnerItem);
            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            lifeTotalSpinner.Adapter = adapter;

            // Set up the dialog
            lifeTotalDialogBuilder.SetCancelable(false).SetPositiveButton("Okay", delegate
                {
                    playerOne.SetLife(lifeTotal);
                    playerTwo.SetLife(lifeTotal);
                    UpdateLife(playerOne, playerOneLife);
                    UpdateLife(playerTwo, playerTwoLife);
                }).SetNegativeButton("Cancel", delegate
                {
                    lifeTotal = tempLifeTotal;
                    lifeTotalDialogBuilder.Dispose();
                });

            // Initialize the dialog and display it.
            Android.Support.V7.App.AlertDialog resetLifeDialog = lifeTotalDialogBuilder.Create();
            resetLifeDialog.Show();
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            lifeTotal = Convert.ToInt32(spinner.GetItemAtPosition(e.Position).ToString());
        }

        private void NotImplementedToast()
        {
            string toast = string.Format("Sorry, this feature isn't implemented yet! Check back later.");
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }
    }
}

