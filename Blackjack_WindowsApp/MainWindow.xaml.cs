using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blackjack_WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int playerHand;
        int dealerHand;

        public MainWindow()
        {
            InitializeComponent();

            ((INotifyCollectionChanged)playerHand_Cards.Items).CollectionChanged += playerHand_Cards_CollectionChanged;

            ((INotifyCollectionChanged)dealerHand_Cards.Items).CollectionChanged += dealerHand_Cards_CollectionChanged;
        }

        private void HandsUpdate()
        {
            playerHand_Sum.Content = playerHand;
            dealerHand_Sum.Content = dealerHand;

            if (playerHand == 21)
            {
                StopGame();
            }

            if (playerHand > 21)
            {
                StopGame();
            }
        }

        private void playerHand_Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                HandsUpdate();
            }
        }

        private void dealerHand_Cards_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                HandsUpdate();
            }
        }

        private void playGame()
        {
           

            ResetGame();

            var playerHand_Card1 = GetCard(playerHand);    // the players first card
            var playerHand_Card2 = GetCard(playerHand);    // the players second card

            var dealerHand_Card1 = GetCard(dealerHand);    // the dealers first card
            var dealerHand_Card2 = GetCard(dealerHand);    // the dealers second card

            playerHand_Cards.Items.Add(playerHand_Card1);
            playerHand_Cards.Items.Add(playerHand_Card2);

            dealerHand_Cards.Items.Add(dealerHand_Card1);
            dealerHand_Cards.Items.Add(dealerHand_Card2);

            playerHand = playerHand_Card1.Value + playerHand_Card2.Value;
            dealerHand = dealerHand_Card1.Value + dealerHand_Card2.Value;

            HandsUpdate();
        }

        private void startGame(object sender, RoutedEventArgs e)
        {
            playGame();

        }

        private void CompareFinalHands()
        {
            if (playerHand > 21)
            {
                MessageBox.Show("Player Bust");
            }

            if ((playerHand <= 21) && (dealerHand <= 21))
            {
                if (playerHand > dealerHand)
                {
                    MessageBox.Show("The Player Wins");
                }
                else if (playerHand < dealerHand)
                {
                    MessageBox.Show("The Dealer Wins");
                }
                else
                {
                    MessageBox.Show("Its a draw");
                }
            }

            else if ((playerHand <= 21) && (dealerHand > 21))
            {
                MessageBox.Show("The Player Wins");
            }

            else if ((dealerHand <= 21) && (playerHand > 21))
            {
                MessageBox.Show("The Dealer Wins");
            }

            else
            {
                MessageBox.Show("Its a draw");
            }

            MessageBoxResult result = MessageBox.Show($"Final Score - Player: {playerHand}, Dealer: {dealerHand}. \n Would you like to play again?", "Final Score", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                playGame();
            }
            else
            {
                return;
            }

            

        }

        private void StopGame()
        {
            btn_Hit.IsEnabled = false;
            btn_Stay.IsEnabled = false;
            btn_play.Content = "Re-play";

            CompareFinalHands();
        }

        private void ResetGame()
        {
            // reset the player and dealers hands to a value of 0
            playerHand = 0;
            dealerHand = 0;

            playerHand_Cards.Items.Clear();
            dealerHand_Cards.Items.Clear();

            btn_Hit.IsEnabled = true;
            btn_Stay.IsEnabled = true;
        }


        // this method returns a random card from the deck
        private KeyValuePair<string, int> GetCard(int hand)
        {
            Random random = new Random();
            Dictionary<string, int> deck = new Dictionary<string, int>();

            deck.Add("2", 2);
            deck.Add("3", 3);
            deck.Add("4", 4);
            deck.Add("5", 5);
            deck.Add("6", 6);
            deck.Add("7", 7);
            deck.Add("8", 8);
            deck.Add("9", 9);
            deck.Add("10", 10);
            deck.Add("Jack", 10);
            deck.Add("Queen", 10);
            deck.Add("King", 10);

            if (hand <= 10)
                deck.Add("Ace", 11);
            else
                deck.Add("Ace", 1);

            int index = random.Next(deck.Count);

            KeyValuePair<string, int> pair = deck.ElementAt(index);

            return new KeyValuePair<string, int>(pair.Key, pair.Value);
        }

        private void dealerHit()
        {
            var newCard = GetCard(dealerHand);
            dealerHand += newCard.Value;
            dealerHand_Cards.Items.Add(newCard);
        }

        private void playerHit(object sender, RoutedEventArgs e)
        {
            var newCard = GetCard(playerHand);
            playerHand += newCard.Value;
            playerHand_Cards.Items.Add(newCard);
        }

        private void runDealerGame()
        {
            while ((dealerHand < playerHand) && (dealerHand < 21))
            {
                dealerHit();
            }

            if (dealerHand > 21)
            {
                MessageBox.Show("Dealer Busts!");
            }

            StopGame();
        }

        private void playerStay(object sender, RoutedEventArgs e)
        {
            btn_Hit.IsEnabled = false;
            runDealerGame();
        }

    }
}
