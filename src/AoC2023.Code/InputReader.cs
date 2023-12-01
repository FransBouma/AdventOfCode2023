using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC2023.Core
{
	public static class InputReader
	{
		public static List<int> GetInputAsIntList(string pathFilename, bool commaSeparated = false)
		{
			if(commaSeparated)
			{
				var lines = GetInputAsStringList(pathFilename);
				var toReturn = new List<int>();
				foreach(var l in lines)
				{
					if(string.IsNullOrWhiteSpace(l))
					{
						continue;
					}
					toReturn.AddRange(l.Split(',').Select(f=>int.Parse(f)));
				}

				return toReturn;
			}
			return File.ReadLines(pathFilename).Select(l => int.Parse(l)).ToList();
		}

		
		public static List<string> GetInputAsStringList(string pathFilename)
		{
			return new List<string>(File.ReadLines(pathFilename));
		}


		public static int[,] GetInputAs2DIntArrayList(string pathFilename)
		{
			var lines = GetInputAsStringList(pathFilename);
			int[,] toReturn = new int[lines[0].Length, lines.Count];
			for(var y = 0; y < lines.Count; y++)
			{
				for(var x = 0; x < lines[y].Length; x++)
				{
					toReturn[x, y] = Convert.ToInt32(lines[y][x]) - 48;		// 48 is Ascii of 0 
				}
			}
			return toReturn;
		}
	}
}