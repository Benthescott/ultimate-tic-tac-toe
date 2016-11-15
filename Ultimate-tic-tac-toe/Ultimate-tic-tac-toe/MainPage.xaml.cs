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
        enum WinDirection { DiagonalDownUp, DiagonalUpDown, Horizontal, Vertical };

        public MainPage()
        {
            this.InitializeComponent();
            SetUpBoard();
            
        }

        /// <summary>
        /// Initiliazes the GUI game board buttons background to blank image.
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

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;

            //var dialog = new MessageDialog("You clicked " + clickedBtn.Name);
            //await dialog.ShowAsync();

            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/x.png"));
            clickedBtn.Background = brush1;
        }
        
        /// <summary>
        /// Adds a line through the 3 winning moves to a mini game.
        /// </summary>
        /// <param name="targetGridName"></param>
        /// <param name="targetBtnName1"></param>
        /// <param name="targetBtnName2"></param>
        /// <param name="targetBtnName3"></param>
        /// <param name="xWon">X won = true, O won = false</param>
        /// <param name="direction"></param>
        private void MiniGameWon(string targetGridName, string targetBtnName1, string targetBtnName2, string targetBtnName3, bool xWon, WinDirection direction)
        {
            ImageBrush brush1 = new ImageBrush();

            if (xWon)
            {
                switch (direction)
                {
                    case WinDirection.DiagonalDownUp: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinDiagonal.png")); break;
                    case WinDirection.DiagonalUpDown: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinDiagonal1.png")); break;
                    case WinDirection.Horizontal: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinHorizontal.png")); break;
                    case WinDirection.Vertical: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinVertical.png")); break;
                }
            }
            else
            {
                switch (direction)
                {
                    case WinDirection.DiagonalDownUp: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oWinDiagonal.png")); break;
                    case WinDirection.DiagonalUpDown: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oWinDiagonal1.png")); break;
                    case WinDirection.Horizontal: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oWinHorizontal.png")); break;
                    case WinDirection.Vertical: brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oWinVertical.png")); break;
                }
            }

            foreach (Grid grid in mainGrid.Children)
            {
                if (grid.Name == targetGridName)
                {
                    foreach (Button btn in grid.Children)
                    {
                        if (btn.Name.Contains(targetBtnName1) || btn.Name == targetBtnName2 || btn.Name == targetBtnName3)
                            btn.Background = brush1;
                    }
                }
            }
        }

        /// <summary>
        /// Displays "TIE" for a mini game.
        /// </summary>
        /// <param name="targetGridName"></param>
        /// <param name="targetBtnName1">Middle Left Button Name</param>
        /// <param name="targetBtnName2">Middle Middle Button Name</param>
        /// <param name="targetBtnName3">Middle Right Button Name</param>
        private void MiniGameTied(string targetGridName, string targetBtnName1, string targetBtnName2, string targetBtnName3)
        {
            //Brushes 1-3 are for the images that spell 1 letter of the word "TIE"
            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinVertical.png"));
            ImageBrush brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinVertical.png"));
            ImageBrush brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xWinVertical.png"));
            ImageBrush brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));

            foreach (Grid grid in mainGrid.Children)
            {
                if (grid.Name == targetGridName)
                {
                    foreach (Button btn in grid.Children)
                    {
                        if (btn.Name.Contains(targetBtnName1))
                            btn.Background = brush1;
                        else if (btn.Name.Contains(targetBtnName2))
                            btn.Background = brush2;
                        else if (btn.Name.Contains(targetBtnName3))
                            btn.Background = brush3;
                        else
                            btn.Background = brush4;
                    }
                }
            }
        }
    }
}
