using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guessing_Game
// This application allows one - two players to play the game "Guess the number" 
{
    class Program
    {
        // Set the variables to the default values as given in the brief
        static int numplayer = 2;
        static int numattempt = 3;
        static int maxsecretnumber = 20;

        static void Main(string[] args)
        {
            int mainselection, secretnum, settingsoption;
            bool correct = false;

            Console.WriteLine("Welcome to the Guessing Game");
            Console.WriteLine("");

            //Display Main Menu and have user select an option
            mainselection = MainMenu();

            while (mainselection != 3) // while still playing (not exit)
            {
                while (mainselection == 2) // settings
                {
                    //Display settings menu and have user select an option
                    settingsoption = SettingsMenu();

                    if (settingsoption == 1) //change number of guesses
                    {
                        Console.Clear();
                        numattempt = ChangeNumberAttempts();
                        //after user enters how many attempts/guesses they would like in each game, display this new number in a gramatically correct sentence.
                        //must be a number greater than 0 according to error checking in the functions, so not needed as an if statement.
                        if (numattempt > 1)
                        {
                            Console.WriteLine($"You will have {numattempt} guesses.");
                        }
                        else
                        {
                            Console.WriteLine($"You will have {numattempt} guess.");
                        }
                        ReturnToSettingsMenu();
                    }
                    else if (settingsoption == 2) //change max value
                    {
                        Console.Clear();
                        // changing the max value can increase or decrase the range of numbers
                        // The allowable input are numbers 1-100 (inclusive)
                        // 0 IS AN OPTION FOR THE SECRET NUMBER ALWAYS! (the minumum value)
                        maxsecretnumber = ChangeMaxSecretNumber();
                        Console.WriteLine($"The maximum secret number is now {maxsecretnumber}");
                        ReturnToSettingsMenu();


                    }
                    else if (settingsoption == 3) //reset settings
                    {
                        //Return all settings to default settings as outlined in the brief
                        numplayer = 2;
                        numattempt = 3;
                        maxsecretnumber = 20;
                        Console.Clear();
                        Console.WriteLine("The settings have been reset.");
                        ReturnToSettingsMenu();
                    }
                    else // back to home
                    {
                        Console.Clear();
                        mainselection = MainMenu();
                    }
                }

                while (mainselection == 1) // play game
                {
                    Console.Clear();
                    numplayer = ChangePlayerMode();

                    if (numplayer == 1) //single player
                    {
                        //https://stackoverflow.com/questions/2706500/how-do-i-generate-a-random-int-number
                        //used to research generating a random number. Referenced solution from April 24, 2010.
                        Random r = new Random();
                        secretnum = r.Next(0, (maxsecretnumber + 1));

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Welcome to SINGLE-PLAYER!");
                        Console.ForegroundColor = ConsoleColor.White;

                        correct = PlayGame(numattempt, secretnum);
                        if (!correct)
                        {
                            Console.WriteLine("");
                            Console.WriteLine($"Sorry, you have used all of your guesses. The number was {secretnum}.");
                            Console.WriteLine("Better luck next time!");
                        }
                    }
                    else if (numplayer == 2) //2 players as per assignment
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Welcome to MULTI-PLAYER!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Please decide who will be Player ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("1 ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("and ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("2");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("When you are ready pass the device to ");
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Player 1");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Hello Player 1!");
                        Console.ForegroundColor = ConsoleColor.White;

                        //have player 1 enter the secret number
                        secretnum = GetNum();

                        Console.Clear();

                        Console.Write("Thank you ");

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("Player 1! ");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Your number has been stored.");
                        Console.Write("Please pass the device to ");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Player 2.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Press enter to continue");
                        Console.ReadLine();
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Hello Player 2!");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Your partner has entered a secret number between 0-{maxsecretnumber}.");

                        correct = PlayGame(numattempt, secretnum);

                        if (!correct)
                        {
                            Console.WriteLine("");
                            Console.WriteLine($"Sorry, you have used all of your guesses. The number was {secretnum}.");
                            Console.WriteLine("Better luck next time!");
                        }

                    }
                    Console.WriteLine();
                    Console.WriteLine("Press enter to go back to the Main Menu.");
                    Console.WriteLine("Please note any changes to settings made for this round will continue to the next game.");
                    Console.WriteLine("If you would like to make changes you must go back into the settings menu.");
                    Console.ReadLine();
                    mainselection = MainMenu();
                }
            }
            Console.Clear();
            Console.WriteLine("Thank you for playing. Goodbye!");
        }

        private static void ReturnToSettingsMenu()
        {
            Console.WriteLine("Press enter to go back to settings.");
            Console.ReadLine();
            Console.Clear();
        }

        private static bool PlayGame(int numattempt, int secretnum)
        {
            bool correct = false;
            string message = "";
            int guess;
            int i;
            //grammar check
            if (numattempt > 1)
            {
                Console.WriteLine($"You will have {numattempt} attempts to guess a random number from 0-{maxsecretnumber}");
            }
            else
            {
                Console.WriteLine($"You will have {numattempt} attempt to guess a random number from 0-{maxsecretnumber}");
            }
            Console.WriteLine("");

            i = 0; // set count, delcared at top of method with other variables
            //while the player has not guessed correctly, and the player has guessed less times than allowed
            while (!correct && i < numattempt)
            {
                if (i == 0)
                {
                    message = $"Please enter guess number {i + 1}:";
                }
                else if ((i >= 1) && i != (numattempt - 1))
                {
                    message = $"You have {numattempt - i} guesses left. Please enter guess number {i + 1}:";
                }
                else if (i == (numattempt - 1)) //grammar, only one guess left
                {
                    message = $"You have {numattempt - i} guess left. Please enter guess number {i + 1}:";
                }
                guess = EnterGuess(message);

                correct = CheckGuess(guess, secretnum, i);
                i++;
            }
            return correct;
        }

        public static bool CheckGuess(int guess, int secretnum, int count)
        {
            bool correct = false;

            if (guess > secretnum)
            {
                Console.Write("That guess was too ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("high.");
                Console.ForegroundColor = ConsoleColor.White;
                correct = false;
            }
            else if (guess < secretnum)
            {
                Console.Write("That guess was too ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("low.");
                Console.ForegroundColor = ConsoleColor.White;
                correct = false;
            }
            else if (guess == secretnum)
            {
                //if statement for grammar
                if (count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("CONGRATS! ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("The number was ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{secretnum}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($". You guessed this in {count + 1} attempt.");
                }
                else if (count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("CONGRATS! ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("The number was ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{secretnum}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($". You guessed this in {count + 1} attempts.");
                }
                correct = true;
            }
            return correct;
        }
        public static int EnterGuess(string message)
        {
            int guess;

            Console.WriteLine(message);
            guess = CheckWholeNumber($"That is not a valid option. Please enter a number from 0 - {maxsecretnumber}", 0, maxsecretnumber);
            return guess;
        }
        private static int GetNum()
        {
            int secretnum;
            string message = $"Please enter a whole number from 0 - {maxsecretnumber}:";

            Console.WriteLine(message);
            secretnum = CheckWholeNumber(message, 0, maxsecretnumber);

            return secretnum;
        }

        private static int ChangePlayerMode()
        {
            int num;
            string message = "You must choose 1 or 2.";

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("**************************");
            Console.WriteLine("         Game Menu        ");
            Console.WriteLine("**************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Single Player");
            Console.WriteLine("2. Multi-Player (2 people)");

            num = CheckWholeNumber(message, 1, 2);

            return num;
        }
        private static int ChangeMaxSecretNumber()
        {
            int maxnum;
            string message = "You must enter a positive whole number";

            Console.WriteLine("What would you like the highest number to guess to be? (Enter 1-100, Default 20)");
            maxnum = CheckWholeNumber(message, 1, 100);
            return maxnum;
        }

        private static int ChangeNumberAttempts()
        {
            int num;
            string message = $"You must enter a number greater than 0 and less than 10";

            Console.WriteLine($"How many attempts would you like? (1-10)");
            num = CheckWholeNumber(message, 1, 10);
            return num;
        }

        private static int SettingsMenu()
        {
            int option;
            string message = "Please choose a number from 1-4.";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("***********************************************");
            Console.WriteLine("                 SETTINGS MENU                 ");
            Console.WriteLine("***********************************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Change number of guesses (default 3)");
            Console.WriteLine("2. Change maximum of secret number (default 20)");
            Console.WriteLine("3. Reset Settings");
            Console.WriteLine("4. Back");

            option = CheckWholeNumber(message, 1, 4);

            return option;
        }


        private static int MainMenu()
        {
            int option;
            string message = "You must choose a number from 1-3";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("*****************************");
            Console.WriteLine("          MAIN MENU          ");
            Console.WriteLine("*****************************");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please make a selection:");
            Console.WriteLine("1. Play");
            Console.WriteLine("2. Settings");
            Console.WriteLine("3. Exit");
            option = CheckWholeNumber(message, 1, 3);

            return option;
        }

        public static int CheckWholeNumber(string message, int min, int max)
        {
            int number;
            bool success;

            success = int.TryParse(Console.ReadLine(), out number);

            while (!success || (number < min || number > max))
            {
                Console.WriteLine($"That is not a valid input. {message}");
                success = int.TryParse(Console.ReadLine(), out number);
            }

            return number;
        }
    }
}
