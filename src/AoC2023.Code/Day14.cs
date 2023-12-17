using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day14
	{
		public class Platform
		{
			public Platform(List<string> input)
			{
				Contents = new char[input.Count][];
				for(var index = 0; index < input.Count; index++)
				{
					var s = input[index];
					Contents[index] = s.ToCharArray();
				}
			}


			public long CalculateTotalLoad()
			{
				var contentsCopy = Contents.Select(a => a.ToArray()).ToArray();
				
				for(int x = 0; x < Contents[0].Length; x++)
				{
					SlideColumn(contentsCopy, x);
				}

				// Console.WriteLine("Sliding result");
				// foreach(var s in contentsCopy)
				// {
				// 	Console.WriteLine(new string(s));
				// }

				long toReturn = 0;
				for(int i = contentsCopy.Length-1; i >= 0; i--)
				{
					toReturn += (contentsCopy.Length-i) * (contentsCopy[i].Count(c => c == 'O'));
				}
				return toReturn;
			}


			private void SlideColumn(char[][] contentsCopy, int x)
			{
				int destination = 0;
				int source = 1;
				while(destination < contentsCopy.Length && source<contentsCopy.Length)
				{
					if(contentsCopy[destination][x] != '.')
					{
						// move down
						destination++;
						source++;
						continue;
					}
					// now find the first O. If we run into a #, we place destination on the line below #
					while(source < contentsCopy.Length)
					{
						if(contentsCopy[source][x] == 'O')
						{
							contentsCopy[destination][x] = 'O';
							contentsCopy[source][x] = '.';
							break;
						}
						if(contentsCopy[source][x] == '#')
						{
							destination = source + 1;
							source = destination + 1;
							break;
						}
						source++;
					}
				}
			}


			public char[][] Contents { get; }
		}
		
		
		public static long Solve1(List<string> input)
		{
			var p = new Platform(input);
			return p.CalculateTotalLoad();
		}


		public static long Solve2(List<string> input)
		{
			return -1;
		}
	}
}
