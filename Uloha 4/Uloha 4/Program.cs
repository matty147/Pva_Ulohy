using System;
using System.Collections.Generic;

class Counter<T>
{
	public readonly Dictionary<T, int> items = new Dictionary<T, int>();

	public void Add(T item)
	{
		if (items.TryGetValue(item, out var curCount))
		{
			items[item] = ++curCount;
		}
		else
		{
			items.Add(item, 1);
		}
	}
}


class Program
{

	static bool isValidInput(string input)
	{
		if (int.TryParse(input, out _))
		{
			return true;
		}
		else return false;
	}

	public List<int> RemoveDuplicats(List<int> list)
	{
		int i = 0;
		List<int> distinctElements = new List<int>();
		while (i < list.Count)
		{
			if (!distinctElements.Contains(list[i]))
				distinctElements.Add(list[i]);
			i++;
		}
		return distinctElements;
	}

	private static Counter<int> GetAllSums(List<int> numbers)
	{
		Counter<int> sums = new Counter<int>();

		for (int start = 0; start < numbers.Count; start++)
		{
			int sum = numbers[start];

			for (int end = start + 1; end < numbers.Count; end++)
			{
				sum += numbers[end];
				sums.Add(sum);
			}
		}
		return sums;
	}


	static void Main(string[] args)
	{


		string input = "1,5,2,4,2,2,2,a,a,a,s4";
		string[] trystring = input.Split(',');
		List<int> numbers = new List<int>();
		foreach (string i in trystring)
		{
			if (isValidInput(i))
			{
				numbers.Add(int.Parse(i));
			}

		}

		if (numbers.Count > 2000)
		{
			Console.WriteLine("Too many numbers");
			Console.ReadKey();
			Environment.Exit(1);
		}else if (numbers.Count > 0)
		{
			Console.WriteLine("not enught numbers");
			Console.ReadKey();
			Environment.Exit(1);
		}

		Counter<int> sums = GetAllSums(numbers);

		foreach (var kv in sums.items)
		{
			int pairs = kv.Value * (kv.Value - 1) / 2;
			if (pairs > 0)
			{
				Console.WriteLine($"sum={kv.Key}: {kv.Value} times; {pairs} pairs");	
			}
		}

		Console.ReadKey();
	}
}
