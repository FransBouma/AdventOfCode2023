using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.Algorithmia.GeneralDataStructures;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day10
	{
		// 4 connector points: north (0), east (1), south (2), west (3)
		// 6 different elements: | (0), - (1), L (2), J (3), 7 (4), F (5)
		// element-connector points:
		// 0 has n+s
		// 1 has e+w
		// 2 has n+e
		// 3 has n+w
		// 4 has s+w
		// 5 has s+e
		// 
		// connectors that can connect to each other: (connecting A with B: connector on A can connect to opposite connector of B, so A north can connect to B south etc.)
		// so store per element the two connector points they have.  		

		public class PipeDefinition
		{
			public enum Direction
			{
				North,
				East, 
				South, 
				West
			}

			private Dictionary<char, List<Direction>> _connectorsPerElement = new();
			
			public PipeDefinition(List<string> input)
			{
				AddElementConnectors('|', Direction.North, Direction.South);
				AddElementConnectors('-', Direction.East, Direction.West);
				AddElementConnectors('L', Direction.North, Direction.East);
				AddElementConnectors('J', Direction.North, Direction.West);
				AddElementConnectors('7', Direction.South, Direction.West);
				AddElementConnectors('F', Direction.South, Direction.East);
				_connectorsPerElement['.'] = new List<Direction>();
				
				Ground = new char[input.Count][];
				GroundSeen = new char[input.Count][];
				for(int i=0;i<input.Count;i++)
				{
					Ground[i] = input[i].ToCharArray();
					var xS = input[i].IndexOf('S');
					if(xS >= 0)
					{
						StartLocation = new Point(xS, i);
					}
					GroundSeen[i] = new String('.', input[0].Length).ToCharArray();
				}
				
				// replace S with its real element
				var canConnectTo = new List<Direction>();
				// can connect to north? Which means can tile above us navigate to south?
				if(CanNavigateIntoDirection(this.StartLocation.X, this.StartLocation.Y - 1, Direction.South))
				{
					canConnectTo.Add(Direction.North);
				}
				// can connect to east? Which means can tile right of us navigate to west?
				if(CanNavigateIntoDirection(this.StartLocation.X+1, this.StartLocation.Y, Direction.West))
				{
					canConnectTo.Add(Direction.East);
				}
				// can connect to south? Which means can tile below of us navigate to north?
				if(CanNavigateIntoDirection(this.StartLocation.X, this.StartLocation.Y+1, Direction.North))
				{
					canConnectTo.Add(Direction.South);
				}
				// can connect to west? Which means can tile left of us navigate to east?
				if(CanNavigateIntoDirection(this.StartLocation.X-1, this.StartLocation.Y, Direction.East))
				{
					canConnectTo.Add(Direction.West);
				}
				// find tile
				foreach(var kvp in _connectorsPerElement)
				{
					if(kvp.Value.SetEqual(canConnectTo))
					{
						Console.WriteLine("S is a {0}", kvp.Key);
						Ground[this.StartLocation.Y][this.StartLocation.X] = kvp.Key;
						break;
					}
				}
			}

			
			public bool CanNavigateIntoDirection(int x, int y, Direction d)
			{
				if(x < 0 || x >= Ground[0].Length || y < 0 || y >= Ground.Length)
				{
					// out of bounds
					return false;
				}

				switch(d)
				{
					case Direction.North:
						if(y == 0)
						{
							return false;
						}
						break;
					case Direction.East:
						if(x == Ground[0].Length - 1)
						{
							return false;
						}
						break;
					case Direction.South:
						if(y == Ground.Length - 1)
						{
							return false;
						}
						break;
					case Direction.West:
						if(x == 0)
						{
							return false;
						}
						break;
				}
				// the tile to go to is inside the array
				var currentTile = Ground[y][x];
				return _connectorsPerElement[currentTile].Contains(d);
			}


			private void AddElementConnectors(char element, Direction c1, Direction c2)
			{
				_connectorsPerElement[element] = new List<Direction>() { c1, c2 };
			}


			public long CalculateMaxLength()
			{
				var route1Pos = new Point(this.StartLocation.X, this.StartLocation.Y);
				var route2Pos = new Point(this.StartLocation.X, this.StartLocation.Y);
				long lengthRoute = 0;
				var startTile = Ground[this.StartLocation.Y][this.StartLocation.X];
				var connectorsStartTile = _connectorsPerElement[startTile];
				var fromDirectionRoute1 = connectorsStartTile[0];
				var fromDirectionRoute2 = connectorsStartTile[1];
				do
				{
					(fromDirectionRoute1, route1Pos) = Step(fromDirectionRoute1, route1Pos);
					(fromDirectionRoute2, route2Pos) = Step(fromDirectionRoute2, route2Pos);
					lengthRoute++;
				} while(route1Pos != route2Pos);
				return lengthRoute;
			}


			private (Direction, Point) Step(Direction direction, Point currentLocation)
			{
				GroundSeen[currentLocation.Y][currentLocation.X] = Ground[currentLocation.Y][currentLocation.X];
				// can only go into 1 direction, the opposite of the direction we came from.
				var deltaX = 0;
				var deltaY = 0;
				switch(direction)
				{
					case Direction.North:
						deltaY = -1;
						break;
					case Direction.East:
						deltaX = 1;
						break;
					case Direction.South:
						deltaY = 1;
						break;
					case Direction.West:
						deltaX = -1;
						break;
				}

				var nextLocation = new Point(currentLocation.X + deltaX, currentLocation.Y + deltaY);
				var nextTile = Ground[nextLocation.Y][nextLocation.X];
				var nextTileConnectors = _connectorsPerElement[nextTile];
				var oppositeDirection = DetermineOppositeDirection(direction);
				var nextDirection = nextTileConnectors.First(c => c != oppositeDirection);

				GroundSeen[nextLocation.Y][nextLocation.X] = Ground[nextLocation.Y][nextLocation.X];

				return (nextDirection, nextLocation);
			}


			private Direction DetermineOppositeDirection(Direction d)
			{
				switch(d)
				{
					case Direction.North:
						return Direction.South;
					case Direction.East:
						return Direction.West;
					case Direction.South:
						return Direction.North;
					case Direction.West:
						return Direction.East;
				}

				return Direction.North;
			}


			public long CalculateNumberEnclosed()
			{
				// do the calculate max length to get the pipe filled into our seen array
				var maxLength = CalculateMaxLength();
				// traverse from the outer ring of locations to all reachable locations and mark them with 'O'
				for(int y = 0; y < GroundSeen.Length; y++)
				{
					if(y == 0 || y == GroundSeen.Length - 1)
					{
						for(int x = 0; x < GroundSeen[y].Length; x++)
						{
							SpreadMarking(x, y);
						}
					}
					else
					{
						// only start / end pixels
						SpreadMarking(0, y);
						SpreadMarking(GroundSeen[y].Length - 1, y);
					}
				}
				
				// do the 'is point contained in polygon' operation. For every '.' found, we trace a line horizontally to the right, and count the vertical edges we cross
				// this is done by calculating the '|', F[-]J and L[-]7 patterns, as these form vertical edges we could cross. Is the total even the point is outside the polygon, otherwise in it.
				// When it's inside, we add it to the number we need to return.
				var toReturn = 0;
				for(int y = 0; y < GroundSeen.Length; y++)
				{
					for(int x = 0; x < GroundSeen[0].Length; x++)
					{
						if(GroundSeen[y][x] != '.')
						{
							continue;
						}

						var numberOfEdgesCrossed = CalculateNumberOfEdgesCrossed(x, y);
						if(numberOfEdgesCrossed % 2 == 0)
						{
							this.GroundSeen[y][x] = 'O';
						}
						else
						{
							this.GroundSeen[y][x] = 'I';
							toReturn++;
						}
					}
				}
				return toReturn;
			}


			private int CalculateNumberOfEdgesCrossed(int x, int y)
			{
				int toReturn = 0;

				var laX = x+1;
				while(laX < GroundSeen[0].Length)
				{
					var tile = GroundSeen[y][laX];
					switch(tile)
					{
						case '|':
							toReturn++;
							break;
						case 'F':
							if(ScanEdgePart(ref laX, y, 'J'))
							{
								toReturn++;
							}
							break;
						case 'L':
							if(ScanEdgePart(ref laX, y, '7'))
							{
								toReturn++;
							}
							break;
					}
					laX++;
				}

				return toReturn;
			}


			private bool ScanEdgePart(ref int laX, int y, char stopCharForValidEdge)
			{
				bool toReturn = false;

				laX++;
				while(laX < GroundSeen[0].Length)
				{
					var tile = GroundSeen[y][laX];
					if(tile == stopCharForValidEdge)
					{
						toReturn = true;
						break;
					}

					if(tile != '-')
					{
						// invalid char for edge
						break;
					}
					laX++;
				}
				
				return toReturn;
			}


			private void SpreadMarking(int x, int y)
			{
				if(x < 0 || x >= GroundSeen[0].Length || y < 0 || y >= GroundSeen.Length)
				{
					// out of bounds, done
					return;
				}
				if(GroundSeen[y][x] == 'O' || GroundSeen[y][x] != '.')
				{
					// done
					return;
				}
				if(GroundSeen[y][x] == '.')
				{
					GroundSeen[y][x] = 'O';
				}
				SpreadMarking(x + 1, y);		// east
				SpreadMarking(x, y+1);			// south
				SpreadMarking(x-1, y);			// west
				SpreadMarking(x, y-1);			// north
				// try squeezing
				
			}


			public char[][] Ground { get; set; }
			public char[][] GroundSeen { get; set; }
			public Point StartLocation { get; set; }
		}
		
		public static long Solve1(List<string> input)
		{
			var g = new PipeDefinition(input);
			Console.WriteLine("Start position: {0}, {1}", g.StartLocation.X, g.StartLocation.Y);
			return g.CalculateMaxLength();
		}


		public static long Solve2(List<string> input)
		{
			var g = new PipeDefinition(input);
			return g.CalculateNumberEnclosed();
		}
	}
}
