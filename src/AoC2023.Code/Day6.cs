using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day6
	{
		public class Races
		{
			public Races(List<string> input, bool puzzle2)
			{
				if(puzzle2)
				{
					RaceData.Add(new Pair<long, long>(Convert.ToInt64(input[0].Split(':')[1].Replace(" ", "")), Convert.ToInt64(input[1].Split(':')[1].Replace(" ", ""))));
				}
				else
				{
					var times = input[0].Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Convert.ToInt32(s)).ToList();
					var distances = input[1].Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Convert.ToInt32(s)).ToList();
					for(int i = 0; i < times.Count; i++)
					{
						RaceData.Add(new Pair<long, long>(times[i], distances[i]));
					}
				}
			}
			
			public List<Pair<long, long>> RaceData { get; } = new();
		}

		
		public static long Solve1(List<string> input)
		{
			var races = new Races(input, false);
			return CalculateResult(races);
		}


		public static long Solve2(List<string> input)
		{
			var races = new Races(input, true);
			return CalculateResult(races);
		}
		
		
		private static long CalculateResult(Races races)
		{
			var numberOfWaysToWin = 1;
			foreach(var p in races.RaceData)
			{
				var amountWon = 0;
				for(int t = 0; t < p.Value1; t++)
				{
					var speed = t;
					var distance = speed * (p.Value1 - t);
					if(distance > p.Value2)
					{
						amountWon++;
					}
				}

				numberOfWaysToWin *= amountWon;
			}

			return numberOfWaysToWin;
		}
	}
}
