//*****************************************************************//
//  Copyright (C) HamGuy 2013 All rights reserved.
//
//  The information contained herein is confidential, proprietary
//  to HamGuy. Use of this information by anyone other than 
//  authorized employees of HamGuy is granted only under a 
//  written non-disclosure agreement, expressly prescribing the 
//  scope and manner of such use.
//
//****************************************************************//
//  SideBar Create by HamGuy at 2013/4/11 0:28:10
//  Version 1.0
//  wangrui15@gmail.com
//  http://www.hamguy.info
//****************************************************************//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SideBarDemo.WP8.Models;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace SideBarDemo.WP8
{
    public partial class SideBar : UserControl
    {


        public SideBar()
        {
            InitializeComponent();
            InitLongList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowOrHidePage(false, null);
        }

        void ShowOrHidePage(bool isKeyframe, Action action = null)
        {
            if (!isKeyframe)
            {
                double to = (uptansform.TranslateX == 0) ? 380 : 0;
                TranslateStory(uptansform.TranslateX, to, TimeSpan.FromSeconds(0.3), action);
            }
            else
            {
                TranslateWithKeyframe(TimeSpan.FromSeconds(0.6), action);
            }
        }

        void TranslateStory(double from, double to, Duration duration, Action action, bool isout = true)
        {
            Storyboard sb = new Storyboard();
            sb.Duration = duration;

            var da = new DoubleAnimation();
            da.From = from;
            da.To = to;
            da.Duration = duration;
            da.EasingFunction = new CubicEase { EasingMode = isout ? EasingMode.EaseOut : EasingMode.EaseIn };
            Storyboard.SetTarget(da, uptansform);
            Storyboard.SetTargetProperty(da, new PropertyPath(CompositeTransform.TranslateXProperty));
            sb.Children.Add(da);
            sb.Begin();
            sb.Completed += (o, a) =>
            {
                sb.Stop();
                uptansform.TranslateX = to;
                sb.Children.Clear();
                sb = null;
                if (action != null)
                    action();
            };
        }

        void TranslateWithKeyframe(Duration duration, Action action)
        {
            var sb = new Storyboard();
            sb.Duration = duration;

            var frames = new DoubleAnimationUsingKeyFrames();
            frames.Duration = duration;
            var keyFrame = new EasingDoubleKeyFrame { Value = uptansform.TranslateX, KeyTime = TimeSpan.FromSeconds(0) };
            frames.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame { Value = 480, KeyTime = TimeSpan.FromSeconds(0.2) };
            frames.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame { Value = 480, KeyTime = TimeSpan.FromSeconds(0.4) };
            frames.KeyFrames.Add(keyFrame);

            keyFrame = new EasingDoubleKeyFrame { Value = 0, KeyTime = TimeSpan.FromSeconds(0.6) };
            frames.KeyFrames.Add(keyFrame);

            Storyboard.SetTarget(frames, uptansform);
            Storyboard.SetTargetProperty(frames, new PropertyPath(CompositeTransform.TranslateXProperty));
            sb.Children.Add(frames);
            sb.Begin();
            sb.Completed += (o, a) =>
            {
                sb.Stop();
                sb.Children.Clear();
                sb = null;
                frames.KeyFrames.Clear();
                frames = null;
                uptansform.TranslateX = 0;
                if (action != null)
                    action();
            };
        }

        private void InitLongList()
        {
            List<DemoModel> model = new List<DemoModel>();
            model.Add(new DemoModel { Title = "Title 1", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 1", ItemName = "Item 2" });
            model.Add(new DemoModel { Title = "Title 2", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 2", ItemName = "Item 2" });
            model.Add(new DemoModel { Title = "Title 2", ItemName = "Item 3" });
            model.Add(new DemoModel { Title = "Title 2", ItemName = "Item 4" });
            model.Add(new DemoModel { Title = "Title 3", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 3", ItemName = "Item2" });
            model.Add(new DemoModel { Title = "Title 4", ItemName = "Item1" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 2" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 3" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 4" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 5" });
            model.Add(new DemoModel { Title = "Title 5", ItemName = "Item 6" });
            model.Add(new DemoModel { Title = "Title 7", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 7", ItemName = "Item 2" });
            model.Add(new DemoModel { Title = "Title 7", ItemName = "Item 3" });
            model.Add(new DemoModel { Title = "Title 8", ItemName = "Item 1" });
            model.Add(new DemoModel { Title = "Title 8", ItemName = "Item 2" });
            model.Add(new DemoModel { Title = "Title 8", ItemName = "Item 3" });
            model.Add(new DemoModel { Title = "Title 8", ItemName = "       " });
            var sortedList = from m in model
                             orderby m.Title
                             group m by m.Title into s
                             select new KeyedList<string, DemoModel>(s);

            MenuList.ItemsSource = new ObservableCollection<KeyedList<string, DemoModel>>(sortedList);
        }

        private void Skp_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var skp = sender as StackPanel;
            if (skp != null)
            {
                var model = skp.Tag as DemoModel;
                if (model != null)
                {
                    ShowOrHidePage(true, () =>
                    {
                        ContenTbk.Text = model.Title + "#" + model.ItemName;
                    });
                }
            }
        }

        private void GestureListener_DragStarted(object sender, DragStartedGestureEventArgs e)
        {

        }

        private void GestureListener_DragCompleted(object sender, DragCompletedGestureEventArgs e)
        {
            if (uptansform.TranslateX == 0 || uptansform.TranslateX == 380)
                return;
            if (e.HorizontalChange < 0)
                //uptansform.TranslateX = 0;
                TranslateStory(uptansform.TranslateX, 0, TimeSpan.FromSeconds(0.3), null);
            else
            {
                if (uptansform.TranslateX < 120)
                    TranslateStory(uptansform.TranslateX, 0, TimeSpan.FromSeconds(0.2), null);
                else
                    TranslateStory(uptansform.TranslateX, 380, TimeSpan.FromSeconds(0.3), null);
            }
        }

        private void GestureListener_DragDelta(object sender, DragDeltaGestureEventArgs e)
        {
            if (uptansform.TranslateX >= 0 && uptansform.TranslateX <= 380)
            {
                uptansform.TranslateX += e.HorizontalChange;
                uptansform.TranslateX = (uptansform.TranslateX < 0) ? 0 : uptansform.TranslateX;
                uptansform.TranslateX = (uptansform.TranslateX > 380) ? 380 : uptansform.TranslateX;
            }
        }
    }
}
