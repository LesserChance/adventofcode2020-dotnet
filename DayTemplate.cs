using System;
class DayTemplate
{
    public DayTemplate(string filePath)
    {
        this.parseFile(filePath);
        this.runPartOne();
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        foreach (string line in lines)
        {
            
        }
    }

    private void runPartOne() {
        Console.WriteLine("part one: ");
    }

    private void runPartTwo() {
        Console.WriteLine("part two: ");
    }
    
}