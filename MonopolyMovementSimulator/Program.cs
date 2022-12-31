using System;
using System.Text.RegularExpressions;

namespace MonopolyMovementSimulator
{
    class Program
    {
        public static List<string> Spaces = PopulateSpaces();
        private static Random _rng = new Random();

        public static List<string> PlacesVisited = new List<string>();
        public static int CurrentSquare = 0;
        public static int DoublesStreak = 0;

        private static int NumberOfMoves = 0;

        static void Main(string[] args)
        {
            for (var x = 0; x < 1000000; x++)
            {
                Cards.InitChanceDeck();
                Cards.InitCChestDeck();
                for (var y = 0; y < 100; y++)
                {
                    Turn();
                }
                CurrentSquare = 0;
                DoublesStreak = 0;
            }

            PresentFinalStats();
            Console.ReadLine();
        }

        static void Turn()
        {
            var die1 = _rng.Next(1, 7);
            var die2 = _rng.Next(1, 7);

            if (die1 == die2)
            {
                DoublesStreak++;
            }
            else
            {
                DoublesStreak = 0;
            }

            if (DoublesStreak == 3) // Go to Jail for 3 doubles in a row
            {
                DoublesStreak = 0;
                CurrentSquare = 10;
                PlacesVisited.Add($"In Jail (via three doubles; {die1 + die2})");
            }
            else
            {
                CurrentSquare += die1 + die2;
                CurrentSquare = CurrentSquare % 40;
                if (CurrentSquare == 30) // 'Go to Jail' space
                {
                    CurrentSquare = 10;
                    PlacesVisited.Add($"In Jail (via Go to Jail; {die1 + die2})");
                }
                else if (CurrentSquare == 7 || CurrentSquare == 22 || CurrentSquare == 36) // 'Chance' space
                {
                    var card = Cards.DrawChanceCard();
                    switch (card)
                    {
                        case ChanceCard.KingsCross:
                            CurrentSquare = 5;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                            break;
                        case ChanceCard.Jail:
                            CurrentSquare = 10;
                            PlacesVisited.Add($"In Jail (via Chance; {die1 + die2})");
                            break;
                        case ChanceCard.Back3:
                            CurrentSquare -= 3;
                            if (CurrentSquare == 33)
                            {
                                var cchestCard = Cards.DrawCChestCard();
                                switch (cchestCard)
                                {
                                    case CChestCard.Jail:
                                        CurrentSquare = 10;
                                        PlacesVisited.Add($"In Jail (via Community Chest via 'Go back three' Chance card; {die1 + die2})");
                                        break;
                                    case CChestCard.Go:
                                        CurrentSquare = 0;
                                        PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Community Chest via 'Go back three' Chance card; {die1 + die2})");
                                        break;
                                    default:
                                        PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Go back three' Chance card; {die1 + die2})");
                                        break;
                                }
                            }
                            else
                            {
                                PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Go back three' Chance card; {die1 + die2})");
                            }
                            break;
                        case ChanceCard.NextStation1:
                            switch (CurrentSquare)
                            {
                                case 7:
                                    CurrentSquare = 15;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                                case 22:
                                    CurrentSquare = 25;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                                case 36:
                                    CurrentSquare = 5;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                            }
                            break;
                        case ChanceCard.NextStation2:
                            switch (CurrentSquare)
                            {
                                case 7:
                                    CurrentSquare = 15;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                                case 22:
                                    CurrentSquare = 25;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                                case 36:
                                    CurrentSquare = 5;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via 'Next station' Chance card; {die1 + die2})");
                                    break;
                            }
                            break;
                        case ChanceCard.NextUtility:
                            switch (CurrentSquare)
                            {
                                case 7:
                                    CurrentSquare = 12;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                                    break;
                                case 22:
                                    CurrentSquare = 28;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                                    break;
                                case 36:
                                    CurrentSquare = 12;
                                    PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                                    break;
                            }
                            break;
                        case ChanceCard.PallMall:
                            CurrentSquare = 11;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                            break;
                        case ChanceCard.Mayfair:
                            CurrentSquare = 39;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                            break;
                        case ChanceCard.Trafalgar:
                            CurrentSquare = 24;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                            break;
                        case ChanceCard.Go:
                            CurrentSquare = 0;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Chance; {die1 + die2})");
                            break;
                        default:
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} ({die1 + die2})");
                            break;
                    }
                }
                else if (CurrentSquare == 2 || CurrentSquare == 17 || CurrentSquare == 33) // 'Community Chest' space
                {
                    var card = Cards.DrawCChestCard();
                    switch (card)
                    {
                        case CChestCard.Jail:
                            CurrentSquare = 10;
                            PlacesVisited.Add($"In Jail (via Community Chest; {die1 + die2})");
                            break;
                        case CChestCard.Go:
                            CurrentSquare = 0;
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} (via Community Chest; {die1 + die2})");
                            break;
                        default:
                            PlacesVisited.Add($"{Spaces[CurrentSquare]} ({die1 + die2})");
                            break;
                    }
                }
                else
                {
                    PlacesVisited.Add($"{Spaces[CurrentSquare]} ({die1 + die2})");
                }
                NumberOfMoves++;
            }

            //Console.WriteLine(PlacesVisited[PlacesVisited.Count - 1]);
        }

        static void PresentFinalStats()
        {
            var stats = new Dictionary<string, int>();
            foreach (var space in Spaces)
            {
                stats.Add(space, 0);
            }
            stats.Add("In Jail", 0);

            foreach (var move in PlacesVisited)
            {
                var match = Regex.Match(move, "(.*?) \\(");
                var place = match.Groups[1].Value;
                stats[place]++;
            }

            var orderedStats = stats.OrderByDescending(x => x.Value);

            Console.WriteLine("Press enter...");
            Console.ReadLine();
            Console.WriteLine("FINAL STATISTICS");
            var n = 1;
            foreach (var space in orderedStats)
            {
                Console.WriteLine($"{n}. {space.Key} - {space.Value} ({space.Value / (double)NumberOfMoves * 100}%)");
                n++;
            }
        }

        static List<string> PopulateSpaces()
        {
            return new List<string>
            {
                "Go",
                "Old Kent Road",
                "Chest 1",
                "Whitechapel Road",
                "Income Tax",
                "King's Cross Station",
                "The Angel, Islington",
                "Chance 1",
                "Euston Road",
                "Pentonville Road",
                "Just Visiting",
                "Pall Mall",
                "Electric Company",
                "Whitehall",
                "Northumberland Avenue",
                "Marylebone Station",
                "Bow Street",
                "Chest 2",
                "Marlborough Street",
                "Vine Street",
                "Free Parking",
                "Strand",
                "Chance 2",
                "Fleet Street",
                "Trafalgar Square",
                "Fenchurch Street Station",
                "Leicester Square",
                "Coventry Street",
                "Water Works",
                "Piccadilly",
                "Go to Jail",
                "Regent Street",
                "Oxford Street",
                "Chest 3",
                "Bond Street",
                "Liverpool Street Station",
                "Chance 3",
                "Park Lane",
                "Super Tax",
                "Mayfair"
            };
        }
    }
}