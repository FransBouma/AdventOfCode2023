using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day13
	{
		public class Patterns
		{
			public Patterns(List<string> input)
			{
				List<string> patternContent = new();
				foreach(var s in input)
				{
					if(string.IsNullOrWhiteSpace(s))
					{
						PatternContents.Add(patternContent);
						patternContent = new List<string>();
					}
					else
					{
						patternContent.Add(s);
					}
				}
				PatternContents.Add(patternContent);
			}


			public long CalculateValue()
			{
				long toReturn = 0;
				foreach(var p in PatternContents)
				{
					toReturn += CalculateHorizontalReflectionValue(p) * 100;
					toReturn += CalculateVerticalReflectionValue(p);
				}
				return toReturn;
			}


			private long CalculateVerticalReflectionValue(List<string> p)
			{
				// rotate 90 degrees to the right, starting at the bottom left corner
				var rotatedP = new List<string>();
				var copiedP = p.ToList();
				copiedP.Reverse();
				for(int i = 0; i < p[0].Length; i++)
				{
					rotatedP.Add(string.Join("", (from s in copiedP select s[i]).ToArray()));
				}
				
				return CalculateHorizontalReflectionValue(rotatedP);
			}


			private long CalculateHorizontalReflectionValue(List<string> p)
			{
				int rowBeforeReflectionLine = 0;
				for(int i = 0; i < p.Count-1; i++)
				{
					if(p[i + 1] == p[i])
					{
						// check reflection line
						bool reflectionOk = true;
						int delta = 1;
						while(reflectionOk)
						{
							if(i - delta < 0 || i + delta + 1 >= p.Count)
							{
								// done
								break;
							}

							reflectionOk &= (p[i - delta] == p[i + delta + 1]);
							delta++;
						}
						if(reflectionOk)
						{
							rowBeforeReflectionLine = i+1;
							break;
						}
					}
				}

				return rowBeforeReflectionLine;
			}

			public List<List<string>> PatternContents { get; } = new();
		}
		
		
		public static long Solve1(List<string> input)
		{
			var p = new Patterns(input);
			return p.CalculateValue();
		}


		public static long Solve2(List<string> input)
		{
			return -1;
		}
	}
}
