using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day12
	{
		public class RecordSolver
		{
			public RecordSolver(List<string> input)
			{
				foreach(var s in input)
				{
					var fragments = s.Split(' ');
					Records.Add(fragments[0]);
					Counts.Add(fragments[1].Split(',').Select(s=>Convert.ToInt32(s)).ToList());
				}
			}

			
			public long CalculateAmountArrangements()
			{
				long toReturn = 0;
				for(int i = 0; i < Records.Count; i++)
				{
					var r = Records[i];
					var counts = Counts[i];
					var fragments = new List<string>();
					// build the fragments to match. 
					foreach(var c in counts)
					{
						fragments.Add(new string('#', c));
					}

					//Console.WriteLine("Record: '{0}' with {1}", r, string.Join(',', counts.ToArray()));
					var numberOfArrangements = ArrangeRow(r, fragments, string.Empty);
					toReturn += numberOfArrangements;
					//Console.WriteLine("Number of arrangements found: {0}", numberOfArrangements);
				}

				return toReturn;
			}


			private long ArrangeRow(string r, List<string> fragments, string currentArrangement)
			{
				int rIndex = 0;
				if(fragments.Count <= 0)
				{
					if(r.Contains('#'))
					{
						return 0;
					}
					Console.WriteLine("\tArrangement found: {0}", currentArrangement);
					return 1;
				}
				if(r.Length <= fragments.Sum(s => s.Length))
				{
					// not a valid arrangement possible
					return 0;
				}
				long toReturn = 0;
				while(rIndex < r.Length) // crIndex can stop if the fragments we still have to do won't fit
				{
					var currentBlock = fragments[0];
					if(CanPlaceBlock(r, currentBlock, rIndex))
					{
						var newFragments = new List<string>();
						for(int i = 1; i < fragments.Count; i++)
						{
							newFragments.Add(fragments[i]);
						}

						toReturn += ArrangeRow(r.Substring(currentBlock.Length+1), newFragments, currentArrangement + new string('.', rIndex) + currentBlock + '.');
					}
					rIndex++;
				}

				return toReturn;
			}


			public bool CanPlaceBlock(string r, string s, int rIndex)
			{
				for(int i = 0; i < s.Length; i++)
				{
					if(rIndex + i >= r.Length)
					{
						// overflow
						return false;
					}
					if(r[rIndex + i] == '.')
					{
						// can't place here
						return false;
					}

					if(rIndex > 0 && r[rIndex - 1] == '#')
					{
						// can't place here because it would create a larger block
						return false;
					}
				}
				// check if we're at the end, or followed by a '.'. In case we're not, we can't place it here
				var followCharOffset = rIndex + s.Length;
				if(followCharOffset >= r.Length)
				{
					// at the end
					return true;
				}
				return r[followCharOffset] != '#';
			}
			
			
			public List<List<int>> Counts { get; } = new();
			public List<string> Records { get; } = new();
		}
		
		public static long Solve1(List<string> input)
		{
			var resolver = new RecordSolver(input);
			return resolver.CalculateAmountArrangements();
		}


		public static long Solve2(List<string> input)
		{
			return -1;
		}
	}
}
