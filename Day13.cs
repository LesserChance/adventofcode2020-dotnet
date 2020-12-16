using System.Collections.Generic;
using System;
class Day13 {
    private int earliestTimestamp;
    private string[] busIds;

    public Day13(string filePath) {
        parseFile(filePath);
        runPartOne();
        runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        earliestTimestamp = Int32.Parse(lines[0]);
        busIds = lines[1].Split(",");
    }

    private void runPartOne() {
        int bestArrival = -1;
        string bestBus = "";
        for (int i = 0; i < busIds.Length; i++) {
            if (busIds[i] != "x") {
                int busTime = Int32.Parse(busIds[i]);
                int factor = (int)Math.Ceiling((double) earliestTimestamp / busTime);
                int arrival = (factor * busTime);

                if (bestArrival == -1 || arrival < bestArrival) {
                    bestArrival = arrival;
                    bestBus = busIds[i];
                }
            }
        }

        Console.WriteLine("part one: " + (Int32.Parse(bestBus) * (bestArrival - earliestTimestamp)));
    }

    private void runPartTwo() {
        List<(int offset, int value)> busOffsets = new List<(int, int)>();
        for (int i = 0; i < busIds.Length; i++) {
            if (busIds[i] != "x") {
                busOffsets.Add((i, Int32.Parse(busIds[i])));
            }
        }

        busOffsets.Sort((a, b) => a.value - b.value);
        busOffsets.Reverse();

        long multiple = busOffsets[0].value;
        int curBus = 0;
        long baseNumber = busOffsets[0].value - busOffsets[0].offset;

        while (curBus < busOffsets.Count - 1) {
            (int offset, int value) nextBus = busOffsets[curBus + 1];
            
            if ((baseNumber + nextBus.offset) % nextBus.value == 0) {
                multiple *= nextBus.value;
                curBus++;
            }
            baseNumber += multiple;
        }
        
        Console.WriteLine("part two: " + (baseNumber-multiple));
    }
    
}