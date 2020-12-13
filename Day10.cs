using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
class DayTen
{
    private List<int> data = new List<int>();

    public DayTen(string filePath)
    {
        this.parseFile(filePath);
        this.runPartOne();
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            this.data.Add(int.Parse(line));
        }

        this.data.Sort();
    }

    private void runPartOne() {
        int[] diffs = {0,0,0,1};

        diffs[this.data[0]]++;
        for (int i = 0; i < this.data.Count - 1; i++)
        {
            diffs[this.data[i+1] - this.data[i]]++;
        }

        Console.WriteLine("part one: " + (diffs[1] * diffs[3]));
    }
    private List<List<int>> generatePaths(List<int> startPath, List<List<int>> paths){
        List<List<int>> result = new List<List<int>>();

        for (int i = 0; i < paths.Count; i++)
        {
            List<int> newPath = new List<int>();
            newPath.AddRange(startPath);
            newPath.AddRange(paths[i]);
            result.Add(newPath);
        }

        return result;
    }

    private List<List<int>> getPaths(List<int> data, int fromIndex){
        List<int> path = new List<int>();
        List<List<int>> paths = new List<List<int>>();

        if (fromIndex == data.Count - 1) {
            return new List<List<int>>{new List<int>{data[fromIndex]}};
        }

        path.Add(data[fromIndex]);
        
        for (int i = 1; i <= 3; i++)
        {
            if (fromIndex+i < data.Count && data[fromIndex+i] - data[fromIndex] <=3) {
                List<List<int>> childPaths = this.getPaths(data, fromIndex+i);
                paths.AddRange(this.generatePaths(path, childPaths));
            }
        }

        return paths;
    }

    private void printPaths(List<int> data) {
        List<List<int>> paths = new List<List<int>>();
        paths = this.getPaths(data, 0);

        for (int i = 0; i < paths.Count; i++)
        {
            Console.Write(i + ":");
            for (int j = 0; j < paths[i].Count; j++)
            {
                Console.Write(paths[i][j] + ",");
            }
            Console.WriteLine("----");
        }
    }

    private void runPartTwo() {
        // add a starting 0
        List<int> newData = new List<int>();
        newData.Add(0);
        newData.AddRange(this.data);

        long[] childCount = new long[newData.Count];
        childCount[newData.Count - 1] = 1;

        for (int i = newData.Count - 2; i >= 0; i--)
        {
            long value = 0;
            
            value += (i+1 < newData.Count && newData[i + 1] - newData[i] <= 3) ? childCount[i + 1] : 0;
            value += (i+2 < newData.Count && newData[i + 2] - newData[i] <= 3) ? childCount[i + 2] : 0;
            value += (i+3 < newData.Count && newData[i + 3] - newData[i] <= 3) ? childCount[i + 3] : 0;

            childCount[i] = value;
        }

        // this.printPaths(newData);
        Console.WriteLine("part two: " + childCount[0]);
    }
    
}