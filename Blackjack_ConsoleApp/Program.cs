using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Program
    {
        // this method returns a random card from the deck
        static KeyValuePair<string, int> GetCard(int hand)
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

        static int PlayerGame()
        {

            Console.WriteLine("*** Player Round ***");

            int playerHand_init = 0;    // the player starts with a hand value of zero
            int playerHand;   // the value of the players hand
            bool stopGame = false;  // in the false state, the game will continue

            List<String> userAllowedActions_hit = new List<String>();   // allowable hit actions
            List<String> userAllowedActions_stay = new List<String>();  // allowable stay actions

            // add the allowable user inputs for Hit
            userAllowedActions_hit.Add("Hit");
            userAllowedActions_hit.Add("hit");
            userAllowedActions_hit.Add("HIT");
            userAllowedActions_hit.Add("H");
            userAllowedActions_hit.Add("h");

            // add the allowable user inputs for Stay
            userAllowedActions_stay.Add("Stay");
            userAllowedActions_stay.Add("stay");
            userAllowedActions_stay.Add("STAY");
            userAllowedActions_stay.Add("S");
            userAllowedActions_stay.Add("s");


            var playerHand_Card1 = GetCard(playerHand_init);    // the players first card
            var playerHand_Card2 = GetCard(playerHand_init);    // the players second card
            //KeyValuePair<string, int> card = GetCard(15);

            Console.WriteLine("Player Card 1: " + playerHand_Card1.Key);
            Console.WriteLine("Player Card 2: " + playerHand_Card2.Key);

            // players hand after receiving the initial two cards
            playerHand = playerHand_Card1.Value + playerHand_Card2.Value;
            
            //playerHand = 21;  // for testing
            Console.WriteLine("Your hand: " + playerHand);
            
            // the player automatically wins
            if (playerHand == 21)
            {
                Console.WriteLine("Automatic WIN!");
                //return playerHand;
            }
            else
            {
                // while the game has not been stopped
                while (!stopGame)
                {

                    // the player automatically wins
                    if (playerHand == 21)
                    {
                        Console.WriteLine("Automatic WIN!");
                        stopGame = true;
                        return playerHand;
                    }

                    Console.WriteLine("Hit or Stay?");
                    string userAction = Console.ReadLine();   // user input
                    Console.WriteLine("\n");

                    // if the user has chosen to Hit
                    if (userAllowedActions_hit.Contains(userAction))
                    {
                        var playerHand_NewCard = GetCard(playerHand);
                        Console.WriteLine("New Card: " + playerHand_NewCard.Key);
                        playerHand += playerHand_NewCard.Value;
                        Console.WriteLine("Your Hand: " + playerHand);
                        if (playerHand > 21)
                        {
                            Console.WriteLine("Bust!");
                            stopGame = true;   // stop the game
                        }
                    }

                    // if the user has chosen to Stay
                    else if (userAllowedActions_stay.Contains(userAction))
                    {
                        Console.WriteLine("Your final hand: " + playerHand);
                        stopGame = true;   // stop the game
                    }

                    else
                    {
                        Console.WriteLine("You didn't pick an available action. Try again");
                    }

                }
            }

            

            Console.WriteLine("\n");

            return playerHand;
        }

        static int DealerGame(int playerHand)
        {
            Console.WriteLine("*** Dealer Round ***");

            int dealerHand_init = 0;    // the player starts with a hand value of zero
            int dealerHand;   // the value of the players hand

            var dealerHand_Card1 = GetCard(dealerHand_init);    // the dealers first card
            var dealerHand_Card2 = GetCard(dealerHand_init);    // the dealers second card

            Console.WriteLine("Dealer Card 1: " + dealerHand_Card1.Key);
            Console.WriteLine("Dealer Card 2: " + dealerHand_Card2.Key);

            // players hand after receiving the initial two cards
            dealerHand = dealerHand_Card1.Value + dealerHand_Card2.Value;

            Console.WriteLine("Dealers hand: " + dealerHand);

            if (playerHand < 21)
            {
                while ((dealerHand < playerHand) && (dealerHand < 21))
                {
                    var dealerHand_NewCard = GetCard(dealerHand);
                    Console.WriteLine("New Card: " + dealerHand_NewCard.Key);
                    dealerHand += dealerHand_NewCard.Value;
                    Console.WriteLine("Dealer Hand: " + dealerHand);
                    if (dealerHand > 21)
                    {
                        Console.WriteLine("Dealer Bust!");
                        break;   // stop the game
                    }
                }
            }
            

            Console.WriteLine("\n");

            return dealerHand;
        }

        static void Play()
        {
            int playerHand = PlayerGame();   // the players round
            int dealerHand = DealerGame(playerHand);

            if (playerHand == 21)    // if the player has not scored a Blackjack
                dealerHand = 0;    //the dealers round

            Console.WriteLine("Players final hand: " + playerHand);
            Console.WriteLine("Dealers final hand: " + dealerHand);

            if ((playerHand <= 21) && (dealerHand <= 21))
            {
                if (playerHand > dealerHand)
                {
                    Console.WriteLine("The player wins");
                }
                else if (playerHand < dealerHand)
                {
                    Console.WriteLine("The dealer wins");
                }
                else
                {
                    Console.WriteLine("Its a draw");
                }
            }

            else if ((playerHand <= 21) && (dealerHand > 21))
            {
                Console.WriteLine("The player wins");
            }

            else if ((dealerHand <= 21) && (playerHand > 21))
            {
                Console.WriteLine("The dealer wins");
            }

            else
            {
                Console.WriteLine("Its a draw");
            }

        }

        static void Main(string[] args)
        {
            bool stopGame = false;

            while (!(stopGame))
            {
                Console.WriteLine("Would you like to play? (yes or no): ");

                string userChoice = Console.ReadLine();

                if (userChoice == "yes")
                    Play();
                else
                    stopGame = true;
                    Environment.Exit(0);
            }

            
            
        }
    }
}
