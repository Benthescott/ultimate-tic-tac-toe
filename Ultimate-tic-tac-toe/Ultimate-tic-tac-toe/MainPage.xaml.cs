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
using System.Threading.Tasks;
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
        private bool firstTime;

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

            turnText.Text = "X's turn";
            locationText.Text = "Any board";
        }

        private void RestartGame()
        {
            game = new Game();
            SetUpBoard();
            firstTime = true;
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedBtn = sender as Button;
            if (game.isXturn())
            {
                short bNum = short.Parse(clickedBtn.Name[3].ToString());
                short moveRow = short.Parse(clickedBtn.Name[4].ToString());
                short moveCol = short.Parse(clickedBtn.Name[5].ToString());
                Tuple<bool, char, char> UIinfo = game.MadeMove(bNum, moveRow, moveCol);

                if (UIinfo.Item1)
                {
                    UpdateTurnLabels('X', game.GetBNTPO());

                    // If UI needs to be updated (b/c it was a valid move)
                    switch (UIinfo.Item2)
                    {
                        case 'X': PlaceMove(UIinfo.Item2, bNum, moveRow, moveCol); break;
                        case 'O': PlaceMove(UIinfo.Item2, bNum, moveRow, moveCol); break;
                        case 'M': MiniGameOver(UIinfo.Item3, bNum); break;
                        default: PlaceMove(UIinfo.Item2, bNum, moveRow, moveCol); GameOver(UIinfo.Item3); break;
                    }

                    UpdateTurnLabels('O', game.GetBNTPO());

                    if (firstTime)
                    {
                        firstTime = false;
                        Task x = new ContentDialog()
                        {
                            Content = "Please wait until the AI has made a move before clicking on the board",
                            PrimaryButtonText = "Ok"
                        }.ShowAsync().AsTask();
                    }
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var AIMoveInfo = await Task.Run(() => AIThread());
                    sw.Stop();
                    Debug.WriteLine((sw.ElapsedMilliseconds / 1000) + " seconds\n");

                    switch (AIMoveInfo.Item1)
                    {
                        case 'O': PlaceMove(AIMoveInfo.Item1, AIMoveInfo.Item3, AIMoveInfo.Item4, AIMoveInfo.Item5); break;
                        case 'M': MiniGameOver(AIMoveInfo.Item2, AIMoveInfo.Item3); break;
                        default: PlaceMove(AIMoveInfo.Item1, AIMoveInfo.Item3, AIMoveInfo.Item4, AIMoveInfo.Item5); GameOver(AIMoveInfo.Item2); break;
                    }

                    UpdateTurnLabels('X', game.GetBNTPO());
                    //await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => { Testing(); });
                    //await this.MyTest(async () => await Testing());
                    /*Tuple<char, char, short, short, short> AIMoveInfo = game.MakeAIMove();
                    switch (AIMoveInfo.Item1)
                    {
                        case 'O': PlaceMove(AIMoveInfo.Item1, AIMoveInfo.Item3, AIMoveInfo.Item4, AIMoveInfo.Item5); break;
                        case 'M': MiniGameOver(AIMoveInfo.Item2, AIMoveInfo.Item3); break;
                        default: PlaceMove(AIMoveInfo.Item1, AIMoveInfo.Item3, AIMoveInfo.Item4, AIMoveInfo.Item5); GameOver(AIMoveInfo.Item2); break;
                    }*/

                    //UpdateTurnLabels('X', game.GetBNTPO());
                }
            }
        }

        private Tuple<char, char, short, short, short> AIThread()
        {
            //    AIMoveInfo:
            //    Tuple (char1, char2, short1, short2, short3)
            //    {
            //        char1 = either 'O' for AI's move, or game status (see game.MadeMove code for reference)
            //        char2 = only used if game is over or mini game is complete (see game.MadeMove code for reference)
            //        short1 = the board number the AI played on
            //        short2 = the AI's move row number
            //        short3 = the AI's move col number
            //    }
            Tuple<char, char, short, short, short> moveInfo = game.MakeAIMove();
            return moveInfo;
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
                short row = short.Parse(button.Name[4].ToString());
                short col = short.Parse(button.Name[5].ToString());

                if ((row == 0 && col == 0) || (row == 2 && col == 2))
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xBottomRight.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
                else if (row == 1 && col == 1)
                {
                    brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/xMiddle.png"));
                    button.Background = brush;
                    brush = new ImageBrush();
                }
                else if ((row == 2 && col == 0) || (row == 0 && col == 2))
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

        private void PlaceOMiniWin(Grid miniBoard)
        {
            ImageBrush brush = new ImageBrush();

            foreach (Button button in miniBoard.Children)
            {
                short row = short.Parse(button.Name[4].ToString());
                short col = short.Parse(button.Name[5].ToString());

                switch (row)
                {
                    case 0:
                        {
                            switch (col)
                            {
                                case 0: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopLeft.png")); break;
                                case 1: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopMiddle.png")); break;
                                default: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oTopRight.png")); break;
                            }
                            break;
                        }

                    case 1:
                        {
                            switch (col)
                            {
                                case 0: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oLeft.png")); break;
                                case 1: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/blank.png")); break;
                                default: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oRight.png")); break;
                            }
                            break;
                        }
                    default:
                        {
                            switch (col)
                            {
                                case 0: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomLeft.png")); break;
                                case 1: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomMiddle.png")); break;
                                default: brush.ImageSource = new BitmapImage(new Uri("ms-appx:///images/oBottomRight.png")); break;
                            }
                            break;
                        }
                }

                button.Background = brush;
                brush = new ImageBrush();
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
                short row = short.Parse(button.Name[4].ToString());
                short col = short.Parse(button.Name[5].ToString());

                if (row == 1 && col == 0)
                    button.Background = brush;
                else if (row == 1 && col == 1)
                    button.Background = brush1;
                else if (row == 1 && col == 2)
                    button.Background = brush2;
                else
                    button.Background = brush3;
            }
        }

        private void UpdateTurnLabels(char player, short boardNum)
        {
            turnText.Text = player + "'s";

            switch (boardNum)
            {
                case 0: locationText.Text = "Must play in Top Left game"; break;
                case 1: locationText.Text = "Must play in Top Middle game"; break;
                case 2: locationText.Text = "Must play in Top Right game"; break;
                case 3: locationText.Text = "Must play in Middle Left game"; break;
                case 4: locationText.Text = "Must play in Middle Middle game"; break;
                case 5: locationText.Text = "Must play in Middle Right game"; break;
                case 6: locationText.Text = "Must play in Bottom Left game"; break;
                case 7: locationText.Text = "Must play in Bottom Middle game"; break;
                case 8: locationText.Text = "Must play in Bottom Right game"; break;
                default: locationText.Text = "Any board"; break;
            }
        }
    }
}