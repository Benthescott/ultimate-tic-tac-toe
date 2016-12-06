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
            InitializeComponent();
            RestartGame();
        }

        /// <summary>
        /// Initiliazes the GUI game board buttons background to blank image.
        /// </summary>
        private void SetUpBoard()
        {
            foreach (Grid grid in mainGrid.Children)
                foreach (Button btn in grid.Children)
                {
                    ImageBrush brush1 = new ImageBrush();
                    brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                    btn.Background = brush1;
                }
        }

        private void RestartGame()
        {
            game = new Game();
            SetUpBoard();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;
            char imgVar = game.PlayerMadeMove(clickedBtn.Name);
            bool uiHasChanged = false;

            if (imgVar == 'X')
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/x.png"));
                clickedBtn.Background = brush;
                uiHasChanged = true;
                turnText.Text = "O's turn";
                locationText.Text = NextMiniGameLoc(clickedBtn.Name);
            }
            else if (imgVar == 'O')
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/o.png"));
                clickedBtn.Background = brush;
                uiHasChanged = true;
                turnText.Text = "X's turn";
                locationText.Text = NextMiniGameLoc(clickedBtn.Name);
            }

            // If next game location is mini game that has been won or tied, change next location label appropriately

            if (uiHasChanged)
            {
                if (game.isMiniGameOver())
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

                char gameState = game.IsGameOver();
                // Next, check for big game win and act accordingly
                if (gameState != 'B')
                    GameOver(gameState);
            }
        }

        private async void GameOver(char winner)
        {
            ContentDialog gameOverDialog = new ContentDialog() {
                Title = "Game Over",
                Content = "\n" + winner + " won the game!\n\nThe game will now reset.",
                PrimaryButtonText = "Ok",
                Opacity = 0.85
            };

            if (winner == 'T')
                gameOverDialog.Content = "\nThe game is tied!";

            gameOverDialog.PrimaryButtonClick += GameOverDialog_PrimaryButtonClick;

            await gameOverDialog.ShowAsync();
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
                    if (grid.Name.StartsWith(targetGridName))
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
            else
            {
                foreach (Grid grid in mainGrid.Children)
                    if (grid.Name.StartsWith(targetGridName))
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

        /// <summary>
        /// This function returns the name of the minigame that must be played in next.
        /// </summary>
        /// <param name="btnName">Full button name</param>
        /// <returns></returns>
        private string NextMiniGameLoc(string btnName)
        {
            string targetGameName = "Next move in ";

            if (btnName.Contains("_top_"))
                targetGameName += "Top ";
            else if (btnName.Contains("_mid_"))
                targetGameName += "Middle ";
            else
                targetGameName += "Bottom ";

            if (btnName.EndsWith("_L_btn"))
                targetGameName += "Left";
            else if (btnName.EndsWith("_M_btn"))
                targetGameName += "Middle";
            else
                targetGameName += "Right";

            targetGameName += " mini game";
            return targetGameName;
        }

        private void NewGame_clicked(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void Exit_clicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void About_clicked(object sender, RoutedEventArgs e)
        {
            StackPanel stack = new StackPanel();
            TextBlock developerBox = new TextBlock() { Text = "Authors: Ethan Carrell, Ben Cline", Margin = new Thickness(0,20,0,10) };
            stack.Children.Add(developerBox);
            TextBlock text = new TextBlock() {
                Text = "Extra:\nStuff about devs and other stuff... Open (or copy the link below into your browser) to view more information about the game",
                TextWrapping = TextWrapping.WrapWholeWords,
                Margin = new Thickness(0,15,0,0)
            };
            stack.Children.Add(text);
            HyperlinkButton hyperLink = new HyperlinkButton() { Content = "http://bejofo.net/ttt", NavigateUri = new System.Uri("http://bejofo.net/ttt"),
                HorizontalAlignment = HorizontalAlignment.Center };
            stack.Children.Add(hyperLink);

            ContentDialog aboutDialog = new ContentDialog() {
                Title = "About",
                Content = stack,
                PrimaryButtonText = "Ok",
            };

            await aboutDialog.ShowAsync();
        }

        private void GameOverDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            RestartGame();
        }
    }
}