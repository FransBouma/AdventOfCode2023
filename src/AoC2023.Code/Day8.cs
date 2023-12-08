using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day8
	{
		public class Parser
		{
			public Parser(List<string> input)
			{
				Parse(input);
			}


			private void Parse(List<string> input)
			{
				this.Commands = input[0];

				for(int i = 2; i < input.Count; i++)
				{
					var fragments = input[i].Split('=');
					var stepFragments = fragments[1].Replace("(", "").Replace(")", "").Split(',');
					Steps.Add(fragments[0].Trim(), new Pair<string, string>(stepFragments[0].Trim(), stepFragments[1].Trim()));
				}
			}


			public long Traverse()
			{
				int rip = 0;
				var current = "AAA";
				while(current != "ZZZ")
				{
					var command = this.Commands[rip % this.Commands.Length];
					var potentialSteps = this.Steps.GetValue(current);
					current = command == 'L' ? potentialSteps.Value1 : potentialSteps.Value2;
					rip++;
				}
				return rip;
			}
			
			public long Traverse2()
			{
				var currents = this.Steps.Keys.Where(k => k.EndsWith('A')).ToList();
				long[] rips= new long[currents.Count];
				// now per current calculate the # of steps to find the end. Then find the least common multiple of the values. 
				while(!currents.All(c=>c.EndsWith('Z')))
				{
					for(var index = 0; index < currents.Count; index++)
					{
						var c = currents[index];
						if(c.EndsWith('Z'))
						{
							// ended
							continue;
						}
						var command = this.Commands[(int)(rips[index] % (long)this.Commands.Length)];
						var potentialSteps = this.Steps.GetValue(c);
						currents[index] = command == 'L' ? potentialSteps.Value1 : potentialSteps.Value2;
						rips[index] += 1;
					}
				}

				return LCM(rips.ToArray());
			}
			
			// I knew I had to find the least common multiple, just not how to calculate it, so this is from SO: https://stackoverflow.com/a/29717490/44991
			static long LCM(long[] numbers)
			{
				return numbers.Aggregate(LCM);
			}
			static long LCM(long a, long b)
			{
				return Math.Abs(a * b) / GCD(a, b);
			}
			static long GCD(long a, long b)
			{
				return b == 0 ? a : GCD(b, a % b);
			}
			
			public string Commands { get; set; }
			public Dictionary<string, Pair<string, string>> Steps { get; } = new();
		}
		
		
		public static long Solve1(List<string> input)
		{
			var p = new Parser(input);
			return p.Traverse();
		}


		public static long Solve2(List<string> input)
		{
			var p = new Parser(input);
			return p.Traverse2();
		}
	}
}
