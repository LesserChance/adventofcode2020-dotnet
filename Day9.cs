using System.Collections.Generic;
using System;
class Day9
{
    private int preambleSize = 25;
    private long partOneResult;
    private List<long> data = new List<long>();
    private List<long> validSums = new List<long>();

    public Day9(string filePath)
    {
        this.parseFile(filePath);
        this.runPartOne();
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            this.data.Add(Int64.Parse(line));
        }
    }

    private void generateValidSumList(int fromIndex) {
        this.validSums = new List<long>();
        for (int i = fromIndex - preambleSize; i < fromIndex; i++)
        {
            for (int j = i + 1; j < fromIndex; j++)
            {
                this.validSums.Add(this.data[i]+this.data[j]);
            }
        }
    }

    private void runPartOne() {
        for (int i = preambleSize; i < this.data.Count; i++)
        {
            this.generateValidSumList(i);
            if (!this.validSums.Contains(this.data[i])) {
                this.partOneResult = this.data[i];
                break;
            }
        }

        Console.WriteLine("part one: " + this.partOneResult);
    }

    private void runPartTwo() {
        long result = -1;
        for (int i = 0; i < this.data.Count && result == -1; i++)
        {
            long sum = 0;
            long min = this.data[i];
            long max = this.data[i];

            for (int j = i; j < this.data.Count && sum < this.partOneResult; j++)
            {
                sum += this.data[j];
                min = Math.Min(min, this.data[j]);
                max = Math.Max(max, this.data[j]);
            }

            if (sum == this.partOneResult) {
                result = min + max;
            }
        }
        Console.WriteLine("part two: " + result);
    }
    
}