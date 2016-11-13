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
