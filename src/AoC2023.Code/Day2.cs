using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AoC2023.Core
{
	public class Game
	{
		public List<SetContents> Sets { get; } = new();
		public int GameID { get; set; }

		public override string ToString()
		{
			return string.Format("Game: {0}. Sets: {1}. Power: {2}", GameID, string.Join("| ", Sets.Select(s => s.ToString()).ToArray()), DeterminePower());
		}

		public bool IsPossible(SetContents thresholds)
		{
			return Sets.All(s => thresholds.BlueStoneCount >= s.BlueStoneCount && thresholds.GreenStoneCount >= s.GreenStoneCount && thresholds.RedStoneCount >= s.RedStoneCount);
		}


		public int DeterminePower()
		{
			int maxRed = this.Sets.Select(s=>s.RedStoneCount).Max();
			int maxGreen =  this.Sets.Select(s=>s.GreenStoneCount).Max();
			int maxBlue =  this.Sets.Select(s=>s.BlueStoneCount).Max();
			return maxRed * maxGreen * maxBlue;
		}
	}
	
	public class SetContents
	{
		public int RedStoneCount { get; set; }
		public int GreenStoneCount { get; set; }
		public int BlueStoneCount { get; set; }
		
		public override string ToString()
		{
			return string.Format("Red: {0}, Green: {1}, Blue: {2}", RedStoneCount, GreenStoneCount, BlueStoneCount);
		}
	}
	
	public static class Day2
	{
		public static List<Game> DetermineGames(List<string> input)
		{
			var toReturn = new List<Game>();
			foreach(var s in input)
			{
				var newGame = new Game();

				var fragments1 = s.Split(':');
				var gameFragments = fragments1[0].Split(' ');
				newGame.GameID = Convert.ToInt32(gameFragments[1]);
				var fragments2 = fragments1[1].Split(';');
				foreach(string fragment in fragments2)
				{
					var fragments3 = fragment.Trim().Split(',');
					var newSet = new SetContents();
					foreach(var colorFragment in fragments3)
					{
						var fragments4 = colorFragment.Trim().Split(' ');
						var amount = Convert.ToInt32(fragments4[0]);
						switch(fragments4[1])
						{
							case "red":
								newSet.RedStoneCount = amount;
								break;
							case "green":
								newSet.GreenStoneCount = amount;
								break;
							case "blue":
								newSet.BlueStoneCount = amount;
								break;
						}
					}
					newGame.Sets.Add(newSet);
				}
				toReturn.Add(newGame);
			}

			return toReturn;
		}

		
		public static int Solve1(List<string> input)
		{
			var games = DetermineGames(input);
			var thresholds = new SetContents() { BlueStoneCount = 14, GreenStoneCount = 13, RedStoneCount = 12 };

			int toReturn = 0;
			foreach(var game in games)
			{
				if(game.IsPossible(thresholds))
				{
					toReturn += game.GameID;
				}
			}

			return toReturn;
		}


		public static int Solve2(List<string> input)
		{
			var games = DetermineGames(input);

			var toReturn = 0;
			foreach(var game in games)
			{
				toReturn += game.DeterminePower();
			}
			return toReturn;
		}
	}
}
