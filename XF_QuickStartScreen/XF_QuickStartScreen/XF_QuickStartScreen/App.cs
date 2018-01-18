﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_QuickStartScreen
{
    public class App : Application
    {
        public App()
        {
            #region // スタイル設定
            Application.Current.Resources = new ResourceDictionary();

            var fsi = "20";
            var fsa = "30";
            var fswp = "40";

            var appTitleLabel = new Style(typeof(Label))
            {
                

                Setters =
                {
                    new Setter { Property = Label.TextColorProperty, Value = Color.FromHex("AA3333") }, // 薄紫
                    new Setter { Property = Label.FontSizeProperty, Value = Device.OnPlatform(fsi,fsa,fswp) },
                }
            };
            Application.Current.Resources.Add("TitleLabel", appTitleLabel);

            var nav = new NavigationPage(new StartPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class StartPage : ContentPage
    {
        StackLayout ml;
        ContentView qsl; // iOS/Android 用
        ContentView bl; // 黒背景
        bool qslVisible = true;

        public StartPage()
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "検索",
                Icon = "Search.png",
                Command = new Command(() => DisplayAlert("Search", "Search is tapped.", "OK")),
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "設定",
                Icon = "Setting.png",
                Command = new Command(() => DisplayAlert("Setting", "Setting is tapped.", "OK")),
            });

            AbsoluteLayout abs = new AbsoluteLayout { };

            ml = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = 15,
                Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Welcome to Xamarin Forms!",
                        TextColor = Color.Black,
					},
                    new Button {
                        Text = "Show QuickStart",
                        TextColor = Color.Black,
                        BackgroundColor = Color.FromHex("CCC"),
                        BorderColor = Color.Gray,
                        BorderRadius = 5,
                        BorderWidth = 1,
                        Command = new Command(()=>{
                            qslVisible = true;
                            Application.Current.Properties["qsl"] = qslVisible;
                            DisplayAlert("Show QuickStart", "Show QuickStart when you re-launch this app.", "OK");
                        }),
                    },
				},
            };

            bl = new ContentView
            {
                BackgroundColor = Color.Black,
                Opacity = 0.65d,
            };

            qsl = new ContentView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(10, 0, 10, 10),
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Children = {
                        new Image {
                            Source = "QuickStart.png",
                            WidthRequest = 250,
                            HorizontalOptions = LayoutOptions.End,
                        },
                        new Image {
                            Source = "QuickStartSwipe.png",
                            WidthRequest = 340,
                        },
                        new Button {
                            Text = "閉じる",
                            TextColor = Color.White,
                            BackgroundColor = Color.FromHex("49d849"),
                            BorderRadius = 5,
                            VerticalOptions = LayoutOptions.EndAndExpand,
                            Command = new Command (()=>{
                                qsl.IsVisible = false;
                                bl.IsVisible = false;
                                qslVisible = false;
                                Application.Current.Properties["qsl"] = qslVisible;
                            }),
                        },
                    },
                },
            };

            
            abs.Children.Add(ml);
            if (Application.Current.Properties.ContainsKey("qsl"))
            {
                var bqsl = (bool)Application.Current.Properties["qsl"];
                if (bqsl)
                {
                    abs.Children.Add(bl);
                    if (Device.OS == TargetPlatform.WinPhone)
                    {
                    }
                    else
                    {
                        abs.Children.Add(qsl);
                    }
                    
                }
            }
            else
            {
                abs.Children.Add(bl);
                if (Device.OS == TargetPlatform.WinPhone)
                {
                }
                else
                {
                    abs.Children.Add(qsl);
                }
            }
            

            Title = "QuickStartLayer";
            Content = abs;

            SizeChanged += OnPageSizeChanged;
        }

        /// <summary>
        /// 画面サイズ変更時に呼び出されます。各コントロールの場所を指定します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void OnPageSizeChanged(object sender, EventArgs args)
        {
            var w = this.Width;
            var h = this.Height;

            AbsoluteLayout.SetLayoutFlags(ml, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(ml, new Rectangle(0d, 0d, w, h));

            AbsoluteLayout.SetLayoutFlags(bl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(bl, new Rectangle(0d, 0d, w, h));

            AbsoluteLayout.SetLayoutFlags(qsl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(qsl, new Rectangle(0d, 0d, w, h));
        }
    }
}
#endregion