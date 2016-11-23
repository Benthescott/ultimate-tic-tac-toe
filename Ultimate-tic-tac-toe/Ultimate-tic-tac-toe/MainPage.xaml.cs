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
        //enum WinDirection { DiagonalDownUp, DiagonalUpDown, Horizontal, Vertical };

        private Game game;

        public MainPage()
        {
            this.InitializeComponent();
            game = new Game();
            SetUpBoard();
            //MiniGameTied("top_R_mini");
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
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;

            //var dialog = new MessageDialog("You clicked " + clickedBtn.Name);
            //await dialog.ShowAsync();

            char imgVar = game.PlayerMadeMove(clickedBtn.Name);
            bool uiHasChanged = false;

            if (imgVar == 'X')
            {
                ImageBrush brush1 = new ImageBrush();
                brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/x.png"));
                clickedBtn.Background = brush1;
                uiHasChanged = true;                
            }
            else if (imgVar == 'O')
            {
                ImageBrush brush1 = new ImageBrush();
                brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/o.png"));
                clickedBtn.Background = brush1;
                uiHasChanged = true;
            }

            if (uiHasChanged)
            {
                if (game.isMiniGameWon())
                {
                    // Update UI
                    string gridName = clickedBtn.Name.Substring(0, 6) + "mini";
                    MiniGameWon(gridName);
                }
                else if (game.isBoardTied())
                {
                    // Update UI
                    string gridName = clickedBtn.Name.Substring(0, 6) + "mini";
                    MiniGameTied(gridName);
                }
                // Next, check for big game win and act accordingly
            }
        }
        
        /// <summary>
        /// Changes MiniGame board to show X or O won
        /// </summary>
        /// <param name="targetGridName">Only the beginning of the grid name</param>
        private void MiniGameWon(string targetGridName)
        {
            ImageBrush brush = new ImageBrush();

            if (game.xWon())
            {
                foreach (Grid grid in mainGrid.Children)
                {
                    if (grid.Name.StartsWith(targetGridName))
                    {
                        foreach (Button btn in grid.Children)
                        {
                            if (btn.Name.EndsWith("top_L_btn") || btn.Name.EndsWith("bot_R_btn"))
                            {
                                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xBottomRight.png"));
                                btn.Background = brush;
                                brush = new ImageBrush();
                            }
                            else if (btn.Name.EndsWith("mid_M_btn"))
                            {
                                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xMiddle.png"));
                                btn.Background = brush;
                                brush = new ImageBrush();
                            }
                            else if (btn.Name.EndsWith("bot_L_btn") || btn.Name.EndsWith("top_R_btn"))
                            {
                                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xBottomLeft.png"));
                                btn.Background = brush;
                                brush = new ImageBrush();
                            }
                            else
                            {
                                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                                btn.Background = brush;
                                brush = new ImageBrush();
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Grid grid in mainGrid.Children)
                {
                    if (grid.Name.StartsWith(targetGridName))
                    {
                        foreach (Button btn in grid.Children)
                        {
                            if (btn.Name.Contains("_top_"))
                            {
                                if (btn.Name.EndsWith("_L_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopLeft.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else if (btn.Name.EndsWith("_M_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopMiddle.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopRight.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                            }
                            else if (btn.Name.Contains("_mid_"))
                            {
                                if (btn.Name.EndsWith("_L_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oLeft.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else if (btn.Name.EndsWith("_R_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oRight.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                            }
                            else if (btn.Name.Contains("_bot_"))
                            {
                                if (btn.Name.EndsWith("_L_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomLeft.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else if (btn.Name.EndsWith("_M_btn"))
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomMiddle.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                                else
                                {
                                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomRight.png"));
                                    btn.Background = brush;
                                    brush = new ImageBrush();
                                }
                            }
                            else
                            {
                                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                                btn.Background = brush;
                                brush = new ImageBrush();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Displays "TIE" for a mini game.
        /// </summary>
        /// <param name="targetGridName">Only the beginning of the grid name</param>
        private void MiniGameTied(string targetGridName)
        {
            //Brushes 1-3 are for the images that spell 1 letter of the word "TIE"
            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/t.png"));
            ImageBrush brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///images/i.png"));
            ImageBrush brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///images/e.png"));
            ImageBrush brush4 = new ImageBrush();
            brush4.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));

            foreach (Grid grid in mainGrid.Children)
            {
                if (grid.Name == targetGridName)
                {
                    foreach (Button btn in grid.Children)
                    {
                        if (btn.Name.Contains("mid_L_btn"))
                            btn.Background = brush1;
                        else if (btn.Name.Contains("mid_M_btn"))
                            btn.Background = brush2;
                        else if (btn.Name.Contains("mid_R_btn"))
                            btn.Background = brush3;
                        else
                            btn.Background = brush4;
                    }
                }
            }
        }

        private void NewGame_clicked(object sender, RoutedEventArgs e)
        {
            game = new Game();
            SetUpBoard();
        }

        private void Exit_clicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
