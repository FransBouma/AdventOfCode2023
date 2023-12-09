using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day9
	{
		public class SequenceSolver
		{
			public SequenceSolver(List<string> input)
			{
				foreach(var s in input)
				{
					Sequences.Add(s.Split(' ').Select(v=>Convert.ToInt64(v)).ToList());
				}
			}


			public long CalculateValue(bool puzzle2 = false)
			{
				long toReturn = 0;
				foreach(var sequence in Sequences)
				{
					toReturn += DetermineNextValue(sequence, puzzle2);
				}
				return toReturn;
			}


			private long DetermineNextValue(List<long> sequence, bool puzzle2)
			{
				var current = sequence.ToArray();
				var next = new long[current.Length-1];
				bool done = false;
				var lastRowValues = new List<long>(); 
				while(!done)
				{
					bool allZero = true;
					for(int i = 0; i < next.Length; i++)
					{
						next[i] = current[i + 1] - current[i];
						allZero &= next[i] == 0;
					}
					lastRowValues.Add(puzzle2 ? current[0] : current[next.Length]);
					if(!allZero)
					{
						Array.Resize(ref current, current.Length-1);
						Array.Clear(current);
						Array.Copy(next, current, next.Length);
						Array.Clear(next);
						Array.Resize(ref next, next.Length-1);
					}
					done = allZero;
				}
				if(puzzle2)
				{
					lastRowValues.Reverse();
					long toReturn = 0;
					for(int i = 0; i < lastRowValues.Count; i++)
					{
						toReturn = lastRowValues[i]-toReturn;
					}

					return toReturn;
				}
				return lastRowValues.Sum();
			}


			public List<List<long>> Sequences { get; } = new();
		}
		
		
		public static long Solve1(List<string> input)
		{
			var solver = new SequenceSolver(input);
			return solver.CalculateValue();
		}


		public static long Solve2(List<string> input)
		{
			var solver = new SequenceSolver(input);
			return solver.CalculateValue(true);
		}
	}
}
