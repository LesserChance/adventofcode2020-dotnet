using System.Collections.Generic;
using System.Linq;
using System;
public class Location
{
    public static char Floor   { get { return '.'; } }
    public static char Available   { get { return 'L'; } }
    public static char Occupied   { get { return '#'; } }
}

class Day11
{
    private List<char[]> data = new List<char[]>();
    private int rowSize = 0;

    public Day11(string filePath) {
        this.parseFile(filePath);
        this.runPartOne();
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        this.rowSize = lines[0].Length;
        foreach (string line in lines) {
            this.data.Add(line.ToCharArray());
        }
    }

    private void printState(List<char[]> data) {
        for (int i = 0; i < data.Count; i++) {
            Console.Write(i + ":");
            for (int j = 0; j < data[i].Length; j++) {
                Console.Write(data[i][j]);
            }
            Console.WriteLine("");
        }
        Console.WriteLine("---------");
    }

    private string getRoundID(List<char[]> data) {
        return data.Aggregate("", (acc, row) => acc + string.Join("",row.Select(seat => string.Join("",seat))));
    }

    private bool positionExists(int x, int y) {
        return x < this.data.Count && y < this.rowSize && x >=  0 && y >= 0;
    }

    private char calculateNextRound(List<char[]> data, int x, int y, int distance, int abandonAt) {
        if (data[x][y] == Location.Floor) {
            return Location.Floor;
        }

        int stepSize = 1;
        int[] foundDirections = new int[8] {0,0,0,0,0,0,0,0};
        int[] occupiedDirections = new int[8] {0,0,0,0,0,0,0,0};

        while (stepSize <= distance) {
            int curDirection = 0;

            int[] xSteps = {x-stepSize, x, x+stepSize};
            int[] ySteps = {y-stepSize, y, y+stepSize};

            for (int i = 0; i < xSteps.Length; i++) {
                for (int j = 0; j < ySteps.Length; j++) {
                    if (!(x == xSteps[i] && y == ySteps[j])) {
                        if (this.positionExists(xSteps[i], ySteps[j])) {
                            if (data[xSteps[i]][ySteps[j]] != Location.Floor && foundDirections[curDirection] == 0) {
                                foundDirections[curDirection] = 1;
                                if (data[xSteps[i]][ySteps[j]] == Location.Occupied) {
                                    occupiedDirections[curDirection] = 1;
                                }
                            }
                        }
                        curDirection++;
                    }
                }
            }
            stepSize++;
        }

        int occupied = occupiedDirections.Aggregate(0, (acc, seat) => acc + seat);

        if (data[x][y] == Location.Available && occupied == 0) {
            return Location.Occupied;
        }
        
        if (data[x][y] == Location.Occupied && occupied >= abandonAt) {
            return Location.Available;
        }

        return data[x][y];
    }

    private List<char[]> nextRound(List<char[]> data, int distance = 1, int abandonAt = 4) {
        List<char[]> result = new List<char[]>();

        for (int i = 0; i < data.Count; i++) {
            result.Insert(i, new char[data[i].Length]);
            for (int j = 0; j < data[i].Length; j++) {
                result[i][j] = this.calculateNextRound(data, i, j, distance, abandonAt);
            }
        }
        return result;
    }

    private void runPartOne() {
        string lastRound = this.getRoundID(this.data);

        List<char[]> nextRound = this.nextRound(this.data, 1, 4);
        string nextRoundId = this.getRoundID(nextRound);

        while (nextRoundId != lastRound) {
            lastRound = this.getRoundID(nextRound);
            nextRound = this.nextRound(nextRound, 1, 4);
            nextRoundId = this.getRoundID(nextRound);
        }

        int occupiedCount = nextRoundId.Length - nextRoundId.Replace(Location.Occupied.ToString(), "").Length;
        Console.WriteLine("part one: " + occupiedCount);
    }

    private void runPartTwo() {
        string lastRound = this.getRoundID(this.data);

        List<char[]> nextRound = this.nextRound(this.data, 8, 5);
        string nextRoundId = this.getRoundID(nextRound);

        while (nextRoundId != lastRound) {
            lastRound = this.getRoundID(nextRound);
            nextRound = this.nextRound(nextRound, 8, 5);
            nextRoundId = this.getRoundID(nextRound);
        }

        int occupiedCount = nextRoundId.Length - nextRoundId.Replace(Location.Occupied.ToString(), "").Length;
        Console.WriteLine("part two: " + occupiedCount);
    }
    
}