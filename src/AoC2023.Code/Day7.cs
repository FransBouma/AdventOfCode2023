using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day7
	{
		// from low to high
		public enum HandType
		{
			None = 0,
			HighCard,
			OnePair,
			TwoPair,
			ThreeOfAKind,
			FullHouse,
			FourOfAKind,
			FiveOfAKind,
		}
		
		public class Hand
		{
			private List<char> _cardTypes1 = new() { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
			private List<char> _cardTypes2 = new() { 'J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
			
			public Hand(string input, bool puzzle2)
			{
				var fragments = input.Split(' ');
				Cards = fragments[0].Trim();
				Bid = Convert.ToInt32(fragments[1]);

				DetermineHandType(puzzle2);
			}


			// returns -1 if this has lower score than toCompareWith, 0 if equal and 1 if higher score than toCompareWith
			public int Compare(Hand toCompareWith, bool puzzle2)
			{
				if(this.TypeOfHand != toCompareWith.TypeOfHand)
				{
					return ((int)this.TypeOfHand).CompareTo((int)toCompareWith.TypeOfHand);
				}

				List<char> cardTypes = puzzle2 ? _cardTypes2 : _cardTypes1;
				for(int i = 0; i < Cards.Length; i++)
				{
					var cardValueThis = cardTypes.IndexOf(this.Cards[i]);
					var cardValueToCompareWith = cardTypes.IndexOf(toCompareWith.Cards[i]);
					if(cardValueThis == cardValueToCompareWith)
					{
						continue;
					}

					return cardValueThis.CompareTo(cardValueToCompareWith);
				}
				return 0;
			}
			
            
			private void DetermineHandType(bool puzzle2)
			{
				bool containsJoker = this.Cards.Contains('J');
				TypeOfHand = HandType.None;
				var grouped = (from c in this.Cards
							   group c by c
							   into g
							   select new { Card = g.Key, Amount = g.Count() }).OrderByDescending(g=>g.Amount).ToList();
				switch(grouped.Count)
				{
					case 1:
						TypeOfHand = HandType.FiveOfAKind;
						break;
					case 2:		// or 4 + 1, or 3+2
						TypeOfHand = grouped[0].Amount == 4 ? HandType.FourOfAKind : HandType.FullHouse;
						break;
					case 3:		// or 3 + 1 + 1, or 2 + 2 + 1
						TypeOfHand = grouped[0].Amount == 3 ? HandType.ThreeOfAKind : HandType.TwoPair;
						break;
					case 4:		// 2 + 1 + 1 + 1
						TypeOfHand = HandType.OnePair;
						break;
					case 5:
						TypeOfHand = HandType.HighCard;
						break;
				}

				if(!containsJoker || !puzzle2)
				{
					return;
				}
				// promote jokers
				switch(this.TypeOfHand)
				{
					case HandType.HighCard:
						// always becomes 1 pair
						TypeOfHand = HandType.OnePair;
						break;
					case HandType.OnePair:
						// Always 3 of a kind
						TypeOfHand = HandType.ThreeOfAKind;
						break;
					case HandType.TwoPair:
						// if one of the 2 groups is a joker group, it becomes 4 of a kind, otherwise it becomes full house (the joker joins one of the pairs)
						if(grouped[grouped.Count - 1].Card == 'J')
						{
							TypeOfHand = HandType.FullHouse;
						}
						else
						{
							TypeOfHand = HandType.FourOfAKind;
						}
						break;
					case HandType.ThreeOfAKind:
						// always 4 of a kind (or full house, but 4 of a kind is higher)
						TypeOfHand = HandType.FourOfAKind;
						break;
					case HandType.FullHouse:
					case HandType.FourOfAKind:
						// always 5 of a kind
						TypeOfHand = HandType.FiveOfAKind;
						break;
					case HandType.FiveOfAKind:
						// no promotion needed
						break;
				}
			}

			public HandType TypeOfHand { get; set; }
			public string Cards { get; set; }
			public int Bid { get; set; }
		}
		
		public static long Solve1(List<string> input)
		{
			return CalculateValue(input, false);
		}


		public static long Solve2(List<string> input)
		{
			return CalculateValue(input, true);
		}


		private static long CalculateValue(List<string> input, bool puzzle2)
		{
			var hands = input.Select(s => new Hand(s, puzzle2)).ToList();
			hands.Sort((h1, h2) => h1.Compare(h2, puzzle2));
			var toReturn = 0;
			for(int i = 0; i < hands.Count; i++)
			{
				toReturn += ((i + 1) * hands[i].Bid);
			}
			return toReturn;
		}
	}
}
