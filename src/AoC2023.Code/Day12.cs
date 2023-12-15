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

			
			public long CalculateAmountArrangements(bool puzzle2)
			{
				long toReturn = 0;
				for(int i = 0; i < Records.Count; i++)
				{
					var r = Records[i];
					var lengths = Counts[i];
					if(puzzle2)
					{
						r = r + "?" + r + "?" + r + "?" + r + "?" + r;
						var originalLengths = Counts[i].ToArray();
						lengths.AddRange(originalLengths);
						lengths.AddRange(originalLengths);
						lengths.AddRange(originalLengths);
						lengths.AddRange(originalLengths);
					}
					//Console.WriteLine("Record: '{0}' with {1}", r, string.Join(',', lengths.ToArray()));
					var numberOfArrangements = ArrangeRow(r, lengths, new Dictionary<string, long>());
					toReturn += numberOfArrangements;
					//Console.WriteLine("Number of arrangements found: {0}", numberOfArrangements);
				}

				return toReturn;
			}


			private long ArrangeRow(string r, List<int> lengths, Dictionary<string, long> cache)
			{
				var cacheKey = r + "|" + string.Join(",", lengths.ToArray());
				if(cache.TryGetValue(cacheKey, out var result))
				{
					// already did this setup
					return result;
				}
				// stop recursion checks
				if(lengths.Count <= 0)
				{
					// check if we still have possible damaged springs in r. If not, we're done
					if(r.Length>0 && r.Any(c => c == '#'))
					{
						// possible damaged springs left, not valid
						return 0;
					}
					// valid, because no more damaged springs left (we can transform ? to .)
					return 1;
				}

				if(r.Length <= 0)
				{
					// still lengths left but no more record, invalid
					return 0;
				}

				var currentLength = lengths[0];
				if(currentLength > r.Length)
				{
					// won't fit
					return 0;
				}
				
				long toReturn = 0;
				// check if we can place a block at the start of r. If we see a ?, we do two things: *and* we see it as a . and move up one char and go into the recursion with that
				// *and* we see it as a # and check if we can place our current block of damaged springs there. If so we skip that part + 1 (trailing .) and go into the recursion with that. 
				switch(r[0])
				{
					case '.':
						// move to next char
						toReturn += ArrangeRow(r.Substring(1), lengths, cache);
						break;
					case '?':
						// to do 2 things here gives us the different arrangements
						// see it as a .:
						toReturn += ArrangeRow(r.Substring(1), lengths, cache);
						// see it as a #:
						// check if the current set of damaged springs can be placed here
						if(CanPlaceBlock(r, currentLength))
						{
							toReturn += ArrangeRow(r.Substring(currentLength+ ((r.Length<=currentLength) ? 0 : 1)), lengths.GetRange(1, lengths.Count - 1), cache);
						}
						break;
					case '#':
						// check if the current set of damaged springs can be placed here
						if(CanPlaceBlock(r, currentLength))
						{
							toReturn += ArrangeRow(r.Substring(currentLength+ ((r.Length<=currentLength) ? 0 : 1)), lengths.GetRange(1, lengths.Count - 1), cache);
						}
						break;
				}
				cache[cacheKey] = toReturn;
				return toReturn;
			}


			public bool CanPlaceBlock(string r, int length)
			{
				var toCheck = r[..length];
				bool atEnd = r.Length == length;
				return !toCheck.Contains('.') && (atEnd ? true : r[length] != '#');
			}
			
			
			public List<List<int>> Counts { get; } = new();
			public List<string> Records { get; } = new();
		}
		
		public static long Solve1(List<string> input)
		{
			var resolver = new RecordSolver(input);
			return resolver.CalculateAmountArrangements(false);
		}


		public static long Solve2(List<string> input)
		{
			var resolver = new RecordSolver(input);
			return resolver.CalculateAmountArrangements(true);
		}
	}
}
