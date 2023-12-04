using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace AoC2023.Core
{
	public static class Day4
	{
		public class Card
		{
			public Card(string contents)
			{
				this.Parse(contents);
			}
			
			private void Parse(string cardInfo)
			{
				var fragments1 = cardInfo.Split(':');
				var fragments2 = fragments1[1].Split('|');
				var winningNumbersFragments = fragments2[0].Split(' ');
				var cardNumbersFragments = fragments2[1].Split(' ');
				this.WinningNumbers.AddRange(winningNumbersFragments.Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).Select(s => Convert.ToInt32(s)));
				this.NumbersOnCard.AddRange(cardNumbersFragments.Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).Select(s => Convert.ToInt32(s)));
			}


			public void AddCopies(int amount)
			{
				NumberOfCopies += amount;
			}


			public int GetScoreP1()
			{
				return Convert.ToInt32(Math.Pow(2, NumberOfWinningNumbers-1));
			}

			public int NumberOfWinningNumbers => this.NumbersOnCard.Count(n => this.WinningNumbers.Contains(n));
			public List<int> WinningNumbers { get; } = new();
			public List<int> NumbersOnCard { get; } = new();
			public int NumberOfCopies { get; set; }
		}
		
		
		public static int Solve1(List<string> input)
		{
			var cards = input.Select(s => new Card(s)).ToList();
			return cards.Select(c => c.GetScoreP1()).Sum();
		}


		public static int Solve2(List<string> input)
		{
			var cards = input.Select(s => new Card(s)).ToList();
			int index = 0;
			while(index < cards.Count)
			{
				var card = cards[index];
				int numberOfWinningNumbers = card.NumberOfWinningNumbers;
				if(numberOfWinningNumbers > 0)
				{
					for(int copySourceIndex = index + 1; copySourceIndex < Math.Min(cards.Count(), (index + 1) + card.NumberOfWinningNumbers); copySourceIndex++)
					{
						cards[copySourceIndex].AddCopies(card.NumberOfCopies+1);
					}
				}

				index++;
			}
			return cards.Sum(s=>s.NumberOfCopies) + cards.Count;
		}
	}
}
