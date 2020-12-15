using System.Collections.Generic;
using System;

class Day12Command {
    public Day12Command(string _instruction, string _units = "") {
        instruction = _instruction;
        units = int.Parse(_units);
    }

    public string instruction { get; set; }
    public int units { get; set; }

}

class Day12 {
    private List<Day12Command> data = new List<Day12Command>();
    private int[] coordinates = new int[2] {0,0}; // 0 (+east,-west), 1 (+north,-south)
    private uint direction = 0; // 0 = East, 1 = South, 2 = West, 3 = North

    public Day12(string filePath) {
        this.parseFile(filePath);
        this.runPartOne();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines) {
            this.data.Add(new Day12Command(line.Substring(0,1), line.Substring(1)));
        }
    }

    private void executeCommand(Day12Command command) {
        string instruction = command.instruction;

        if (instruction == "F") {
            switch (this.direction) {
                case 0:
                    instruction = "E";
                    break;
                case 1:
                    instruction = "S";
                    break;
                case 2:
                    instruction = "W";
                    break;
                case 3:
                    instruction = "N";
                    break;
            }
        }

        switch (instruction) {
            case "N":
                this.coordinates[1] += command.units;
                break;
            case "S":
                this.coordinates[1] -= command.units;
                break;
            case "E":
                this.coordinates[0] += command.units;
                break;
            case "W":
                this.coordinates[0] -= command.units;
                break;
            case "L":
                this.direction = (this.direction - (uint)(command.units/90)) % 4;
                break;
            case "R":
                this.direction = (this.direction + (uint)(command.units/90)) % 4;
                break;
        }
    }

    private void runPartOne() {
        for (int i = 0; i < this.data.Count; i++) {
            this.executeCommand(this.data[i]);
            // Console.WriteLine(this.data[i].instruction+","+this.data[i].units);
            // Console.WriteLine(this.coordinates[0]+","+this.coordinates[1]);
        }

        Console.WriteLine("part one: "+(Math.Abs(this.coordinates[0]) + Math.Abs(this.coordinates[1])));
    }
    
}