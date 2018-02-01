using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics;

namespace CircleGraph
{
    [Activity(Label = "CircleGraph", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);
            GraphCircular gCircle = FindViewById<GraphCircular>(Resource.Id.view1);
            Dictionary<Color, float> colorandvalue = new Dictionary<Color, float>();
            colorandvalue.Add(Color.Blue, 9);
            colorandvalue.Add(Color.Red, 8);
            colorandvalue.Add(Color.Yellow, 90);
            GraphSpecs specs1 = new GraphSpecs
            {
                percentageValuesWithColor = colorandvalue,
                strokeWidth = 15,
                space = 20,
                startAngle = 90,
                animSteps = 30
            };
            gCircle.setGraphSpecs(specs1);
            button.Click += delegate { button.Text = $"{count++} clicks!"; };
        }
    }
}

