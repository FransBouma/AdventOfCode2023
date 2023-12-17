using System;
using System.Diagnostics;
using System.IO;
using AoC2023.Core;
using NUnit.Framework;

namespace AoC2023.Tests
{
	public class Day14Tests
	{
		[SetUp]
		public void Setup()
		{
		}


		[Test]
		public void Puzzle1_ExampleInput()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day14_example.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(136, Day14.Solve1(input));
		}
		

		[Test]
		public void Puzzle1_Solver()
		{
			Stopwatch sw = new();
			sw.Start();
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day14.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day14.Solve1(input));
			sw.Stop();
			Console.WriteLine("Total time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
		}

		
		[Test]
		public void Puzzle2_ExampleInput2()
		{
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day14_example2.txt");
			Assert.IsTrue(input.Count>0);
			Assert.AreEqual(8, Day14.Solve2(input));
		}
		
		
		[Test]
		public void Puzzle2_Solver()
		{
			Stopwatch sw = new();
			sw.Start();
			var input = InputReader.GetInputAsStringList("..\\..\\..\\PuzzleInputs\\day14.txt");
			Assert.IsTrue(input.Count>0);
			Console.WriteLine(Day14.Solve2(input));
			sw.Stop();
			Console.WriteLine("Total time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
		}
	}
}