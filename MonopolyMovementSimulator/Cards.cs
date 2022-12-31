using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyMovementSimulator
{
    public enum ChanceCard
    {
        KingsCross,
        Jail,
        Back3,
        NextStation1,
        NextStation2,
        NextUtility,
        PallMall,
        Mayfair,
        Trafalgar,
        Go,
        Stay1,
        Stay2,
        Stay3,
        Stay4,
        Stay5,
        Stay6
    }

    public enum CChestCard
    {
        Jail,
        Go,
        Stay1,
        Stay2,
        Stay3,
        Stay4,
        Stay5,
        Stay6,
        Stay7,
        Stay8,
        Stay9,
        Stay10,
        Stay11,
        Stay12,
        Stay13,
        Stay14
    }

    public class Cards
    {
        private static Random _rng = new Random();
        private static List<ChanceCard> ChanceCards = new List<ChanceCard>();
        private static List<CChestCard> CChestCards = new List<CChestCard>();

        public static ChanceCard DrawChanceCard()
        {
            var card = ChanceCards[0];
            ChanceCards.Add(card);
            ChanceCards.RemoveAt(0);
            return card;
        }

        public static CChestCard DrawCChestCard()
        {
            var card = CChestCards[0];
            CChestCards.Add(card);
            CChestCards.RemoveAt(0);
            return card;
        }

        public static void InitChanceDeck()
        {
            var unshuffledDeck = new List<ChanceCard>();
            foreach (var cardType in Enum.GetValues<ChanceCard>())
            {
                unshuffledDeck.Add(cardType);
            }

            var shuffledDeck = new List<ChanceCard>();
            while (unshuffledDeck.Count > 0)
            {
                var index = _rng.Next(0, unshuffledDeck.Count);
                var card = unshuffledDeck[index];
                shuffledDeck.Add(card);
                unshuffledDeck.Remove(card);
            }

            ChanceCards = shuffledDeck;
        }

        public static void InitCChestDeck()
        {
            var unshuffledDeck = new List<CChestCard>();
            foreach (var cardType in Enum.GetValues<CChestCard>())
            {
                unshuffledDeck.Add(cardType);
            }

            var shuffledDeck = new List<CChestCard>();
            while (unshuffledDeck.Count > 0)
            {
                var index = _rng.Next(0, unshuffledDeck.Count);
                var card = unshuffledDeck[index];
                shuffledDeck.Add(card);
                unshuffledDeck.Remove(card);
            }

            CChestCards = shuffledDeck;
        }
    }
}
