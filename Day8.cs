using System.Collections;
using System.Collections.Generic;
using System;
class Instruction
{
    public Instruction(string _instruction, string _param)
    {
        instruction = _instruction;
        param = int.Parse(_param);
    }

    public string instruction { get; set; }
    public int param { get; set; }
    public void swapNopJmp(){
        switch (this.instruction) {
            case "jmp":
                this.instruction = "nop";
                break;
            case "nop":
                this.instruction = "jmp";
                break;
        }
    }
}

class Day8
{
    private List<Instruction> instructionList;
    private List<int> executedInstructions;
    private int accumulator;
    private int instructionLine;

    public Day8(string filePath)
    {
        this.parseFile(filePath);
        this.runPartOne();
        this.runPartTwo();
    }

    private void parseFile(string filePath) {
        string[] lines = System.IO.File.ReadAllLines(filePath);

        this.instructionList  = new List<Instruction>();

        foreach (string line in lines)
        {
            string[] instruction = line.Split(" ");
            this.instructionList.Add(new Instruction(instruction[0], instruction[1]));
        }
    }

    private void acc(int param) {
        this.accumulator += param;
        this.instructionLine++;
    }
    private void jmp(int param) {
        this.instructionLine += param;
    }
    private void nop(int param) {
        this.instructionLine++;
    }

    private void executeNextInstruction() {
        if (this.executedInstructions.Contains(this.instructionLine)) {
            return;
        }
        if (this.instructionLine >= this.instructionList.Count) {
            return;
        }

        Instruction _instruction = this.instructionList[this.instructionLine];
        this.executedInstructions.Add(this.instructionLine);
        
        switch (_instruction.instruction) {
            case "acc":
                this.acc(_instruction.param);
                break;
            case "jmp":
                this.jmp(_instruction.param);
                break;
            case "nop":
                this.nop(_instruction.param);
                break;
        }

        this.executeNextInstruction();
    }

    private void executeProgram() {
        this.accumulator = 0;
        this.instructionLine = 0;
        this.executedInstructions = new List<int>();
        this.executeNextInstruction();
    }

    private void runPartOne() {
        this.executeProgram();
        Console.WriteLine("part one: " + this.accumulator.ToString());
    }

    private void runPartTwo() {
        for (int i = 0; i < this.instructionList.Count; i++)
        {
            this.instructionList[i].swapNopJmp();
            this.executeProgram();

            if(this.instructionLine == this.instructionList.Count) {
                // success!
                break;
            } 
            this.instructionList[i].swapNopJmp();
        }

        Console.WriteLine("part two: " + this.accumulator.ToString());
    }
    
}