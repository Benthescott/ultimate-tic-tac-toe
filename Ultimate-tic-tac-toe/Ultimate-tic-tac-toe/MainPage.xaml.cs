using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Ultimate_tic_tac_toe
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            SetUpBoard();
            
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetUpBoard()
        {
            foreach (Grid grid in mainGrid.Children)
            {
                foreach (Button btn in grid.Children)
                {
                    ImageBrush brush1 = new ImageBrush();
                    brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                    btn.Background = brush1;
                }
            }
            
            foreach (Grid grid in mainGrid.Children)
            {
                /*********************************************************************
                Note: Make this a function of its own. It should receive a grid name 
                    and 3 button name endings. The function checks every button's name 
                    for one of the button name endings passed in, by using the 
                    "contains()" method in button.name property.
                    FOR MINI GAME TIE: Create 3 images that spell TIE (one letter per
                    image) and place them in the middle row of mini game. All other
                    button backgrounds, in mini game, should be changed to blank image.           
                *********************************************************************/
                if (grid.Name == "topMiddleMini")
                {
                    ImageBrush brush1 = new ImageBrush();
                    ImageBrush brush2 = new ImageBrush();
                    ImageBrush brush3 = new ImageBrush();
                    brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinDiagonal.png"));
                    brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///images/o.png"));
                    brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinDiagonal1.png"));                

                    foreach (Button btn in grid.Children)
                    {
                        string test = "top_L_btn";
                        if (btn.Name.Contains(test))
                        {
                            btn.Background = brush3;
                        }
                        else if (btn.Name == "top_M_mid_M_btn" || btn.Name == "top_M_bot_R_btn")
                            btn.Background = brush3;
                        else
                            btn.Background = brush2;   
                    }
                }
            }
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;

            //var dialog = new MessageDialog("You clicked " + clickedBtn.Name);
            //await dialog.ShowAsync();

            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/x.png"));
            clickedBtn.Background = brush1;
        }
    }
}
