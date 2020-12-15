using System.Collections.Generic;
using System;

class Day12Part2 {
    private List<Day12Command> data = new List<Day12Command>();
    private int[] waypointCoordinates = new int[2] {10,1}; // 0 (+east,-west), 1 (+north,-south)
    private int[] shipCoordinates = new int[2] {0,0}; // 0 (+east,-west), 1 (+north,-south)
 
    public Day12Part2(string filePath) {
        this.parseFile(filePath);
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines) {
            this.data.Add(new Day12Command(line.Substring(0,1), line.Substring(1)));
        }
    }

    private void executeCommand(Day12Command command) {
        switch (command.instruction) {
            case "N":
                this.waypointCoordinates[1] += command.units;
                break;
            case "S":
                this.waypointCoordinates[1] -= command.units;
                break;
            case "E":
                this.waypointCoordinates[0] += command.units;
                break;
            case "W":
                this.waypointCoordinates[0] -= command.units;
                break;
            case "L":
                for (int i = 0; i < command.units/90; i++) {
                    int tempY = this.waypointCoordinates[1];
                    this.waypointCoordinates[1] = this.waypointCoordinates[0];
                    this.waypointCoordinates[0] = -tempY;
                }
                break;
            case "R":
                for (int i = 0; i < command.units/90; i++) {
                    int tempX = this.waypointCoordinates[0];
                    this.waypointCoordinates[0] = this.waypointCoordinates[1];
                    this.waypointCoordinates[1] = -tempX;
                }
                break;
            case "F":
                this.shipCoordinates[0] += (command.units * this.waypointCoordinates[0]);
                this.shipCoordinates[1] += (command.units * this.waypointCoordinates[1]);
                break;
        }
    }

    private void runPartTwo() {
        for (int i = 0; i < this.data.Count; i++) {
            this.executeCommand(this.data[i]);
            // Console.WriteLine(this.data[i].instruction+","+this.data[i].units);
            // Console.WriteLine("waypoint:"+this.waypointCoordinates[0]+","+this.waypointCoordinates[1]);
            // Console.WriteLine("ship    :"+this.shipCoordinates[0]+","+this.shipCoordinates[1]);
            // Console.WriteLine("-------------");
        }

        Console.WriteLine("part two: "+(Math.Abs(this.shipCoordinates[0]) + Math.Abs(this.shipCoordinates[1])));
    }
}