using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using SD.Tools.BCLExtensions.CollectionsRelated;

namespace AoC2023.Core
{
	public static class Day5
	{
		public class SeedTranslations
		{
			public class SeedRange
			{
				public long Start { get; set; }
				public long Length { get; set; }
			}
			
			public class RangeMap
			{
				public RangeMap Map(RangeMap toMap)
				{
					var toReturn = new RangeMap();
					toReturn.SourceStart = toMap.SourceStart;
					if(toMap.DestinationStart >= SourceStart && toMap.DestinationStart < SourceStart + Length)
					{
						// start falls into this range
						toReturn.DestinationStart = Map(toMap.DestinationStart);
						if((toMap.DestinationStart + toMap.Length) <= (SourceStart + Length))
						{
							// end falls into this range
							toReturn.Length = toMap.Length;
						}
						else
						{
							// end falls outside this range
							toReturn.Length = (SourceStart + Length) - toMap.DestinationStart;
						}
					}
					// destination start falls outside this range, so we return an empty range;
					return toReturn;
				}


				public long Map(long toMap)
				{
					if(toMap < SourceStart || toMap >= SourceStart + Length)
					{
						return toMap;
					}
					return DestinationStart + (toMap - SourceStart);
				}
				
				
				public long SourceStart { get; set; }
				public long DestinationStart { get; set;}
				public long Length { get; set;}
			}
			
			public SeedTranslations(List<string> input)
			{
				for(int index = 0;index<input.Count;index++)
				{
					string line = input[index];
					if(string.IsNullOrEmpty(line))
					{
						continue;
					}
					if(line.StartsWith("seeds:"))
					{
						Seeds.AddRange(line.Split(':')[1].Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => Convert.ToInt64(s)));
						for(int i = 0; i < Seeds.Count; i+=2)
						{
							this.SeedRanges.Add(new SeedRange(){Start = Seeds[i], Length = Seeds[i+1]});
						}
					}
					if(line.StartsWith("seed-to"))
					{
						index = ParseRanges(input, index, this.SeedToSoil);
					}
					if(line.StartsWith("soil-to"))
					{
						index = ParseRanges(input, index, this.SoilToFertilizer);
					}
					if(line.StartsWith("fertilizer-to"))
					{
						index = ParseRanges(input, index, this.FertilizerToWater);
					}
					if(line.StartsWith("water-to"))
					{
						index = ParseRanges(input, index, this.WaterToLight);
					}
					if(line.StartsWith("light-to"))
					{
						index = ParseRanges(input, index, this.LightToTemperature);
					}
					if(line.StartsWith("temperature-to"))
					{
						index = ParseRanges(input, index, this.TemperatureToHumidity);
					}
					if(line.StartsWith("humidity-to"))
					{
						index = ParseRanges(input, index, this.HumidityToLocation);
					}
				}
			}

			private int ParseRanges(List<string> input, int startIndex, List<RangeMap> destination)
			{
				int index = startIndex+1;
				while(index < input.Count)
				{
					string line = input[index];
					if(string.IsNullOrEmpty(line))
					{
						break;
					}
					var values = line.Split(' ').Select(s => Convert.ToInt64(s)).ToList();
					destination.Add(new RangeMap() { SourceStart=values[1], DestinationStart = values[0], Length = values[2]});
					index++;
				}
				return index;
			}


			public List<RangeMap> Flatten(List<RangeMap> start)
			{
				var toReturn = new List<RangeMap>();
				toReturn.AddRange(start);
				toReturn = Flatten(toReturn, SeedToSoil);
				toReturn = Flatten(toReturn, SoilToFertilizer);
				toReturn = Flatten(toReturn, FertilizerToWater);
				toReturn = Flatten(toReturn, WaterToLight);
				toReturn = Flatten(toReturn, LightToTemperature);
				toReturn = Flatten(toReturn, TemperatureToHumidity);
				toReturn = Flatten(toReturn, HumidityToLocation);
				return toReturn;
			}


			public List<RangeMap> Flatten(List<RangeMap> current, List<RangeMap> toMap)
			{
				var toReturn = new List<RangeMap>();
				foreach(var r in current)
				{
					var stillToMap = new RangeMap() { SourceStart = r.SourceStart, DestinationStart = r.DestinationStart, Length = r.Length};
					foreach(var t in toMap.OrderBy(x=>x.SourceStart))
					{
						var result = t.Map(stillToMap);
						if(result.Length > 0)
						{
							toReturn.Add(result);
							stillToMap.SourceStart += result.Length;
							stillToMap.DestinationStart += result.Length;
							stillToMap.Length -= result.Length;
						}

						if(stillToMap.Length <= 0)
						{
							break;
						}
					}

					if(stillToMap.Length > 0)
					{
						// leftovers. Source maps to itself if destination isn't found
						toReturn.Add(stillToMap);
					}
				}

				return toReturn;
			}
            

			public long GetMinimalSeedLocation(bool useSeedRanges)
			{
				var seedTo = new List<RangeMap>();
				if(useSeedRanges)
				{
					foreach(var seedRange in SeedRanges)
					{
						seedTo.Add(new RangeMap() { DestinationStart = seedRange.Start, SourceStart = seedRange.Start, Length = seedRange.Length});
					}					
				}
				else
				{
					// add seeds as single value ranges
					foreach(var s in Seeds)
					{
						seedTo.Add(new RangeMap() { DestinationStart = s, SourceStart = s, Length = 1 });
					}
				}

				var flattenResult = Flatten(seedTo);
				return flattenResult.Min(r => r.DestinationStart);
			}
			
			public List<long> Seeds { get; } = new();
			public List<SeedRange> SeedRanges { get; } = new();
			public List<RangeMap> SeedToSoil { get; } = new();
			public List<RangeMap> SoilToFertilizer { get; } = new();
			public List<RangeMap> FertilizerToWater { get; } = new();
			public List<RangeMap> WaterToLight { get; } = new();
			public List<RangeMap> LightToTemperature { get; } = new();
			public List<RangeMap> TemperatureToHumidity { get; } = new();
			public List<RangeMap> HumidityToLocation { get; } = new();
		}
		
		
		public static long Solve1(List<string> input)
		{
			var translator = new SeedTranslations(input);
			return translator.GetMinimalSeedLocation(false);
		}


		public static long Solve2(List<string> input)
		{
			var translator = new SeedTranslations(input);
			return translator.GetMinimalSeedLocation(true);
		}
	}
}
