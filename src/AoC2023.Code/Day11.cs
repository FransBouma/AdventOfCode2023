using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day11
	{
		public class Universe
		{
			public Universe(List<string> input, bool isPuzzle2=false, bool isExample=false)
			{
				int galaxyCounter = 0;
				List<int> rowsWithoutGalaxies = new();
				HashSet<int> columnsWithGalaxies = new();
				GalaxyMap = new char[input.Count][];
				Dictionary<int, Point> originalCoords = new();
				for(int y = 0; y < input.Count; y++)
				{
					var line = input[y];
					GalaxyMap[y] = line.ToCharArray();
					bool foundGalaxyOnRow = false;
					for(int x = 0; x < line.Length; x++)
					{
						if(line[x] == '#')
						{
							foundGalaxyOnRow = true;
							columnsWithGalaxies.Add(x);
							galaxyCounter++;
							originalCoords[galaxyCounter] = new Point(x, y);
						}
					}
					if(!foundGalaxyOnRow)
					{
						rowsWithoutGalaxies.Add(y);
					}
				}

				List<int> columnsWithoutGalaxies =  new();
				for(int i = 0; i < GalaxyMap[0].Length; i++)
				{
					if(!columnsWithGalaxies.Contains(i))
					{
						columnsWithoutGalaxies.Add(i);
					}
				}
				
				// expand coordinates
				int extraRowColsToInsert = 1;
				if(isPuzzle2)
				{
					extraRowColsToInsert = isExample ? (10-1) : (1000000-1);
				}

				foreach(var kvp in originalCoords)
				{
					var coordinate = kvp.Value;
					var numberOfInsertedRows = rowsWithoutGalaxies.Count(r => r < coordinate.Y);
					var numberOfInsertedColumns = columnsWithoutGalaxies.Count(c => c< coordinate.X);
					coordinate.X += (extraRowColsToInsert * numberOfInsertedColumns);
					coordinate.Y += (extraRowColsToInsert * numberOfInsertedRows);
					GalaxyLocations[kvp.Key] = coordinate;
				}
			}

			public long CalculateShortestPath()
			{
				// create pairs
				var pairs = new List<Pair<int, int>>();
				for(int i = 1; i <= GalaxyLocations.Count; i++)
				{
					for(int j = i + 1; j <= GalaxyLocations.Count; j++)
					{
						pairs.Add(new Pair<int, int>(i, j));
					}
				}

				long toReturn = 0;
				foreach(var p in pairs)
				{
					// shortest path is deltaX + deltaY;
					var g1Coordinates = GalaxyLocations[p.Value1];
					var g2Coordinates = GalaxyLocations[p.Value2];
					toReturn += Math.Abs(g1Coordinates.X - g2Coordinates.X) + Math.Abs(g1Coordinates.Y - g2Coordinates.Y);
				}
				return toReturn;
			}
			

			public Dictionary<int, Point> GalaxyLocations { get; } = new(); 
			public char[][] GalaxyMap { get; }
		}
		
		public static long Solve1(List<string> input)
		{
			var u = new Universe(input);
			return u.CalculateShortestPath();
		}


		public static long Solve2(List<string> input, bool isExample)
		{
			var u = new Universe(input, true, isExample);
			return u.CalculateShortestPath();
		}
	}
}
