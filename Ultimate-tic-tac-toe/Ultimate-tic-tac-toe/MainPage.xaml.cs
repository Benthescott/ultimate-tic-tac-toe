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

            short bNum = short.Parse(clickedBtn.Name[3].ToString());
            short moveRow = short.Parse(clickedBtn.Name[4].ToString());
            short moveCol = short.Parse(clickedBtn.Name[5].ToString());
            Tuple<bool, char, char> UIinfo = game.MadeMove(bNum, moveRow, moveCol);

            if (UIinfo.Item1)
            {
                // If UI needs to be updated (b/c it was a valid move)
                switch (UIinfo.Item2)
                {
                    case 'X': PlaceMove(UIinfo.Item2, bNum, moveRow, moveCol); break;
                    case 'O': PlaceMove(UIinfo.Item2, bNum, moveRow, moveCol); break;
                    case 'M': MiniGameOver(UIinfo.Item3, bNum); break;
                    default: GameOver(UIinfo.Item3); break;
                }

                Test();
            }
        }

        private async void Test()
        {
            await System.Threading.Tasks.Task.Run(() => Testing());
        }

        private void Testing()
        {
            int x = 1;
            for (int j = 0; j < 1000000; j++)
            {
                for (int i = 0; i < 10000000; i++)
                    x = (x + 1) * (x + -2);
            }
        }

        private void PlaceMove(char player, int boardN, int row, int col)
        {
            ImageBrush brush = new ImageBrush();
            Uri imagePath = new Uri("ms-appx:///images/x.png");

            if (player == 'O')
                imagePath = new Uri("ms-appx:///images/o.png");

            foreach (Grid miniBoard in mainGrid.Children)
                if (miniBoard.Name == "MB" + boardN.ToString())
                    foreach (Button button in miniBoard.Children)
                        if (button.Name == "Btn" + boardN + row + col)
                        {
                            brush.ImageSource = new BitmapImage(imagePath);
                            button.Background = brush;
                            brush = new ImageBrush();
                        }

        }

        private async void GameOver(char winner)
        {
            ContentDialog gameOverDialog = new ContentDialog()
            {
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

        private void MiniGameOver(char player, int boardN)
        {
            if (player == 'T')
            {
                foreach (Grid miniBoard in mainGrid.Children)
                    if (miniBoard.Name == "MB" + boardN.ToString())
                    {
                        PlaceTieMini(miniBoard);
                    }
            }
            else if (player == 'X')
            {
                foreach (Grid miniBoard in mainGrid.Children)
                    if (miniBoard.Name == "MB" + boardN.ToString())
                    {
                        PlaceXMiniWin(miniBoard);
                        break;
                    }
            }
            else
            {
                foreach (Grid miniBoard in mainGrid.Children)
                    if (miniBoard.Name == "MB" + boardN.ToString())
                    {
                        PlaceOMiniWin(miniBoard);
                        break;
                    }
            }
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
            TextBlock developerBox = new TextBlock() { Text = "Authors: Ethan Carrell, Ben Cline", Margin = new Thickness(0, 20, 0, 10) };
            stack.Children.Add(developerBox);
            TextBlock text = new TextBlock()
            {
                Text = "Extra:\nStuff about devs and other stuff... Open (or copy the link below into your browser) to view more information about the game",
                TextWrapping = TextWrapping.WrapWholeWords,
                Margin = new Thickness(0, 15, 0, 0)
            };
            stack.Children.Add(text);
            HyperlinkButton hyperLink = new HyperlinkButton()
            {
                Content = "http://bejofo.net/ttt",
                NavigateUri = new System.Uri("http://bejofo.net/ttt"),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            stack.Children.Add(hyperLink);

            ContentDialog aboutDialog = new ContentDialog()
            {
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

        private void PlaceXMiniWin(Grid miniBoard)
        {
            ImageBrush brush = new ImageBrush();

            foreach (Button button in miniBoard.Children)
            {
                if (button.Name.Contains("_top_"))
                {
                    if (button.Name.EndsWith("_L_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopLeft.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else if (button.Name.EndsWith("_M_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopMiddle.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopRight.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                }
                else if (button.Name.Contains("_mid_"))
                {
                    if (button.Name.EndsWith("_L_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oLeft.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else if (button.Name.EndsWith("_R_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oRight.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                }
                else if (button.Name.Contains("_bot_"))
                {
                    if (button.Name.EndsWith("_L_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomLeft.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else if (button.Name.EndsWith("_M_btn"))
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomMiddle.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomRight.png"));
                        button.Background = brush;
                        brush = new ImageBrush();
                    }
                }
                else
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
            }
        }

        private void PlaceOMiniWin(Grid miniBoard)
        {
            ImageBrush brush = new ImageBrush();

            foreach (Button button in miniBoard.Children)
            {
                if (button.Name.EndsWith("top_L_btn") || button.Name.EndsWith("bot_R_btn"))
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xBottomRight.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
                else if (button.Name.EndsWith("mid_M_btn"))
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xMiddle.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
                else if (button.Name.EndsWith("bot_L_btn") || button.Name.EndsWith("top_R_btn"))
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xBottomLeft.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
                else
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
            }
        }

        private void PlaceTieMini(Grid miniBoard)
        {
            //Brushes 0-3 are for the images that spell 1 letter of the word "TIE"
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/t.png"));
            ImageBrush brush1 = new ImageBrush();
            brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///images/i.png"));
            ImageBrush brush2 = new ImageBrush();
            brush2.ImageSource = new BitmapImage(new Uri("ms-appx:///images/e.png"));
            ImageBrush brush3 = new ImageBrush();
            brush3.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png"));

            foreach (Button button in miniBoard.Children)
            {
                if (button.Name.Contains("mid_L_btn"))
                    button.Background = brush;
                else if (button.Name.Contains("mid_M_btn"))
                    button.Background = brush1;
                else if (button.Name.Contains("mid_R_btn"))
                    button.Background = brush2;
                else
                    button.Background = brush3;
            }
        }
    }
}