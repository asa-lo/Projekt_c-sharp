using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projekt_c_sharp.net
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members

        //Holds the current results of cells in active game
        private MarkType[] mResults;

        //True if its player 1's turn (X)
        private bool mPlayer1Turn;

        //True if the game has been won or draw
        private bool mGameEnded;


        #endregion
        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

      
        // Starts a new game and clears all values
        private void NewGame()
        {
            //Create new black array of free cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = MarkType.Free;

            mPlayer1Turn = true;

            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                //Change background, font color and content to defaut value when a new game starts
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
            //Make sure that the game has not ended when a new game is started
            mGameEnded = false;
        }

        //Handle button click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            //Cast the sender to a button
            var button = (Button)sender;

            //Find the buttons position in array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            //Fomrula to indentify column
            var index = column + (row * 3);

            //If cell as value, do nothing
            if (mResults[index] != MarkType.Free)
                return;

            //Set the cell value based on which players turn it is (X or O)

            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;

            //Declare text value
            button.Content = mPlayer1Turn ? "X" : "O";

            //Change O to green

            if (!mPlayer1Turn)
                button.Foreground = Brushes.Red;

            //Toggle the players turn
            mPlayer1Turn ^= true;

            //check for winner
            CheckForWinner();
        }

        //Check for winner
        private void CheckForWinner()
        {
            // Check for horizontal wins
            #region Horizontal wins
     
            //Row 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Foreground = Button1_0.Foreground = Button2_0.Foreground = Brushes.Green;
            }
            

            //row 1
            if (mResults[3] != MarkType.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_1.Foreground = Button1_1.Foreground = Button2_1.Foreground = Brushes.Green;
            }

            //row 2
            if (mResults[6] != MarkType.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_2.Foreground = Button1_2.Foreground = Button2_2.Foreground = Brushes.Green;
            }
            #endregion
            //Check for vertical wins
            #region Vertical wins

            //Column 0
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Foreground = Button0_1.Foreground = Button0_2.Foreground = Brushes.Green;
            }

            //Column 1
            if (mResults[1] != MarkType.Free && (mResults[1] & mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button1_0.Foreground = Button1_1.Foreground = Button1_2.Foreground = Brushes.Green;
            }

            //Column 2
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Foreground = Button2_1.Foreground = Button2_2.Foreground = Brushes.Green;
            }
            #endregion

            // Check for diaganol wins

            #region Diagonal Wins
            //Diagonal 1
            if (mResults[0] != MarkType.Free && (mResults[0] & mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button0_0.Foreground = Button1_1.Foreground = Button2_2.Foreground = Brushes.Green;
            }

            //Diagonal 1
            if (mResults[2] != MarkType.Free && (mResults[2] & mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;

                //Highlight winning cells in green
                Button2_0.Foreground = Button1_1.Foreground = Button0_2.Foreground = Brushes.Green;
            }
            #endregion
            //Turn all font to orange if no winner is declared
            #region No winner

            if (!mResults.Any(f => f == MarkType.Free))
            {

                mGameEnded = true;

                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Foreground = Brushes.Orange;
                });
            }
            #endregion
        }
    }
}
