using System;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day10Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(4, Day10.Solve1(input));
		}

		[Test]
		public void Puzzle1_Example2Input()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example2.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(8, Day10.Solve1(input));
		}

		[Test]
		public void Puzzle1_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day10.Solve1(input));
		}
		
		
		[Test]
		public void Puzzle2_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example3.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(4, Day10.Solve2(input));
		}

		
		[Test]
		public void Puzzle2_Example2Input()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example4.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(8, Day10.Solve2(input));
		}

		
		[Test]
		public void Puzzle2_Example3Input()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10_example5.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(10, Day10.Solve2(input));
		}

		
		[Test]
		public void Puzzle2_Solver()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day10.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day10.Solve2(input));
		}
	}
}