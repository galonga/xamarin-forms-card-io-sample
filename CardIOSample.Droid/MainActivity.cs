﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Card.IO;
using XamFormsCardIO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CardIOSample.Models;

namespace CardIOSample.Droid
{
    [Activity(Label = "CardIOSample.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // Feel free to extend the CreditCard_PCL object to include more than what's here.
            var ccPCL = new CreditCard_PCL();

            if (data != null) {

                // Be sure to JavaCast to a CreditCard (normal cast won't work)		 
                var card = data.GetParcelableExtra(CardIOActivity.ExtraScanResult).JavaCast<CreditCard>();

                Console.WriteLine("Scanned: " + card.RedactedCardNumber);

                ccPCL.cardNumber = card.CardNumber;
                ccPCL.ccv = card.Cvv;
                ccPCL.expr = card.ExpiryMonth.ToString() + card.ExpiryYear.ToString();
                ccPCL.redactedCardNumber = card.RedactedCardNumber;
                ccPCL.cardholderName = card.CardholderName;

                MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanSuccess");

            } else {
                MessagingCenter.Send<CreditCard_PCL>(ccPCL, "CreditCardScanCancelled");
            }
        }
    }
}
