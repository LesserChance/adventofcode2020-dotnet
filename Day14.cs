using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System;
class Day14 {
    private int maxMemory = -1;
    private List<(string mask, List<(int memLoc, int memVal)> instructions)> data = new List<(string, List<(int, int)>)>();

    public Day14(string filePath) {
        parseFile(filePath);
        runPartOne();
        runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        int maskIndex = -1;

        List<(int memLoc, int memVal)> instructions = new List<(int memLoc, int memVal)>();
        foreach (string line in lines) {
            if (line.Contains("mask")) {
                maskIndex++;
                Match match = Regex.Match(line, @"mask = (.*)");
                this.data.Add((match.Groups[1].Value, new List<(int memLoc, int memVal)>()));
            } else {
                Match match = Regex.Match(line, @"mem\[(\d+)\] = (\d+)");
                int memLoc = Int32.Parse(match.Groups[1].Value);
                if (memLoc > maxMemory) {
                    maxMemory = memLoc;
                }
                this.data[maskIndex].instructions.Add((memLoc, Int32.Parse(match.Groups[2].Value)));
            }
        }
    }

    private long getValWithMask(string mask, long value) {
        char[] valueString = Convert.ToString(value, toBase: 2).PadLeft(mask.Length, '0').ToCharArray();
        char[] m = mask.ToCharArray();
        for (int i = 0; i < m.Length; i++) {
            if (m[i] == '0') { valueString[i] = '0'; }
            if (m[i] == '1') { valueString[i] = '1'; }
        }
        
        return Convert.ToInt64(string.Join("", valueString), fromBase: 2);
    }

    private void runPartOne() {
        long[] memory = new long[maxMemory + 1];

        for (int i = 0; i < this.data.Count; i++) {
            string mask = this.data[i].mask;
            
            for (int j = 0; j < this.data[i].instructions.Count; j++) {
                memory[this.data[i].instructions[j].memLoc] = getValWithMask(mask, this.data[i].instructions[j].memVal);
            }
        }

        long result = (long) memory.Aggregate((long) 0, (acc, val) => (acc + val));
        Console.WriteLine("part one: " + result);
    }

    private string getXMask(char[] loc, char[] binVal) {
        char[] result = new char[loc.Length];

        int binI = 0;
        for (int i = 0; i < result.Length; i++) {
            result[i] = 'X';
            if (loc[i] == 'X') {
                result[i] = binVal[binI];
                binI++;
            }
        }

        return string.Join("",result);
    }

    private List<long> getLocsWithMask(string mask, int loc) {
        char[] locMask = Convert.ToString(loc, toBase: 2).PadLeft(mask.Length, '0').ToCharArray();
        char[] m = mask.ToCharArray();
        char[] newBaseLoc = new char[locMask.Length];

        int xCount = 0;
        for (int i = 0; i < m.Length; i++) {
            if (m[i] == '1') {
                locMask[i] = '1';
            }
            newBaseLoc[i] = locMask[i];
            if (m[i] == 'X') { 
                locMask[i] = 'X';
                xCount++;
            }
        }

        long newBase = Convert.ToInt64(string.Join("", newBaseLoc), fromBase: 2);

        List<long> locs = new List<long>();
        for (int i = 0; i < Math.Pow(2, xCount); i++) {
            char[] binVal = Convert.ToString(i, toBase: 2).PadLeft(xCount, '0').ToCharArray();
            string valMask = getXMask(locMask, binVal);

            locs.Add(getValWithMask(valMask, newBase));
        }

        return locs;
    }

    private void runPartTwo() {
        Dictionary<long, long> memory = new Dictionary<long, long>();

        for (int i = 0; i < this.data.Count; i++) {
            string mask = this.data[i].mask;
            
            for (int j = 0; j < this.data[i].instructions.Count; j++) {
                List<long> locs = getLocsWithMask(mask, this.data[i].instructions[j].memLoc);

                for (int k = 0; k < locs.Count; k++) {
                    memory[locs[k]] = this.data[i].instructions[j].memVal;
                }
            }
        }

        long result = 0;
        foreach(KeyValuePair<long, long> entry in memory) {
             result += entry.Value;
        }

        Console.WriteLine("part two: " + result);
    }
    
}