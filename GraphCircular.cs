
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using static Android.Views.Animations.Animation;

namespace CircleGraph
{
    public class GraphSpecs
    {
        public Dictionary< Color,float> percentageValuesWithColor = new Dictionary< Color,float>();
        public int strokeWidth { get; set; }
        public float startAngle { get; set; }
        public float space { get; set; }
        public int animSteps { get; set; }
    }
    public class GraphCircular : View
    {
        public GraphSpecs graphSpecs = new GraphSpecs();
        public GraphSpecs graphSpecsSet = new GraphSpecs();
        int steps = 0;
        Handler mHandler = null;
        public GraphCircular(Context context) :
            base(context)
        {
            Initialize();
        }

        public GraphCircular(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public GraphCircular(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }
        public void setGraphSpecs(GraphSpecs specs){
            graphSpecsSet = specs;
            graphSpecs.space = graphSpecsSet.space;
            graphSpecs.startAngle = graphSpecsSet.startAngle;
            graphSpecs.strokeWidth = graphSpecsSet.strokeWidth;
            mHandler = new Handler();
            int count = 0;
            steps = graphSpecsSet.animSteps;
            //maxValue = 100 - maxValue;
            animateView(count);
        }
        void animateView(int count){
            
            mHandler.PostDelayed(() => {
                Dictionary<Color, float> colorandvalue = new Dictionary<Color, float>();
                count++;
                for (int i = 0; i < graphSpecsSet.percentageValuesWithColor.Count(); i++)
                {
                    KeyValuePair<Color, float> kvp = graphSpecsSet.percentageValuesWithColor.ElementAt(i);
                    colorandvalue.Add(kvp.Key, count * (kvp.Value /steps));
                }
                graphSpecs.percentageValuesWithColor = colorandvalue;
                this.Invalidate();

                if(count<steps)
                    animateView(count);
            }, 20);
        }
        public GraphSpecs getGraphSpecs()
        {
            return graphSpecs;
        }

        void Initialize()
        {
            //Dictionary<Color, float> colorandvalue = new Dictionary<Color, float>();
            //colorandvalue.Add( Color.Blue,1);
            //colorandvalue.Add(Color.Red,1);
            //colorandvalue.Add(Color.Yellow,1);
            //GraphSpecs specs1 = new GraphSpecs
            //{
            //    percentageValuesWithColor = colorandvalue,
            //    strokeWidth = 15,
            //    space = 20,
            //    startAngle = 90
            //};
            //graphSpecs = specs1;
        }

        public override void Draw(Android.Graphics.Canvas canvas)
        {
            base.Draw(canvas);
            int min = Math.Min(Width, Height)-graphSpecs.strokeWidth;
            int canvasW = Width;
            int canvasH = Height;
            Point centerOfCanvas = new Point(canvasW / 2, canvasH / 2);
            int count = 0;
            foreach(KeyValuePair<Color,float> turns in graphSpecs.percentageValuesWithColor){
                float rectW = min - 2*count * graphSpecs.strokeWidth - count * graphSpecs.space;
                float rectH = min - 2*count*graphSpecs.strokeWidth - count*graphSpecs.space;
                float left = centerOfCanvas.X - (rectW / 2);
                float top = centerOfCanvas.Y - (rectH / 2);
                float right = centerOfCanvas.X + (rectW / 2);
                float bottom = centerOfCanvas.Y + (rectH / 2);
                RectF rect = new RectF(left, top, right, bottom);
                Paint p = new Paint();
                p.AntiAlias = true;
                p.SetStyle(Paint.Style.Stroke);
                p.StrokeWidth = graphSpecs.strokeWidth;
                p.Color = (turns.Key);
                float swipeAngle = (turns.Value * 18) / 5;
                canvas.DrawArc(rect, graphSpecs.startAngle, swipeAngle, false, p);
                count++;
            }
        }
    }
}
