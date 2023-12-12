using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day11Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day11_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(374, Day11.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day11.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day11.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day11_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(1030, Day11.Solve2(input, true));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day11.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day11.Solve2(input, false));
		}
	}
}