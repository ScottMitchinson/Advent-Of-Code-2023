using System;
using System.Numerics;
using System.Text.RegularExpressions;

class AdventOfCode
{
    // I originally went for the replace approach, but 'eightwo' was pain.
    // I found this idea for an approach, it was kinda gross but it works.
    // There is definitely a nicer way to do this, but I forget C# everytime I leave, so.
    static Dictionary<string, string> Numbers = new Dictionary<string, string>() {
        {"one", "o1e"},
        {"two", "t2o"},
        {"three", "th3ee"},
        {"four", "f4r"},
        {"five", "f5e"},
        {"six", "s6x"},
        {"seven", "s7n"},
        {"eight", "ei8ht"},
        {"nine", "n9e"},
    };

    static void Puzzle1()
    {
        string[] FileLines = File.ReadAllLines("AdventOfCode2023/inputs/input1.txt");
        
        // Replace Strings to make them easier
        for (int Index = 0; Index < FileLines.Length; Index++)
        {
            foreach (var KV in Numbers)
            {
                FileLines[Index] = FileLines[Index].Replace(KV.Key, KV.Value);
            }
        }

        // Now Find Values
        int Answer = 0;
        foreach (string Line in FileLines)
        {
            string First = "";
            string Latest = "";

            foreach (char Character in Line)
            {
                if(char.IsDigit(Character))
                {
                    if(First.Length == 0)
                    {
                        First = Character.ToString();
                    }

                    Latest = Character.ToString();
                }
            }

            Answer += Int32.Parse(First + Latest);
        }

        // Give Answer
        Console.WriteLine(Answer);
    }

    static void Puzzle2()
    {
        string[] FileLines = File.ReadAllLines("AdventOfCode2023/inputs/input2.txt");

        int Answer = 0;

        for (int Index = 0; Index < FileLines.Length; Index++)
        {
            int GameIndex = Index + 1;

            string Game = FileLines[Index].Split(":")[1];
            string[] Turns = Game.Split(";");

            int RequiredBlue = 0;
            int RequiredRed = 0;
            int RequiredGreen = 0;
            
            foreach (string Turn in Turns)
            {
                string[] Cubes = Turn.Split(",");

                foreach (string Cube in Cubes)
                {
                    string[] Details = Cube.Split(" ");
                    if(Details[2] == "blue" && Int32.Parse(Details[1]) > RequiredBlue)
                    {
                        RequiredBlue = Int32.Parse(Details[1]);
                    }
                    else if (Details[2] == "green" && Int32.Parse(Details[1]) > RequiredGreen)
                    {
                        RequiredGreen = Int32.Parse(Details[1]);
                    }
                    else if(Details[2] == "red" && Int32.Parse(Details[1]) > RequiredRed)
                    {
                        RequiredRed = Int32.Parse(Details[1]);
                    }
                }
            }

            Answer += RequiredBlue * RequiredGreen * RequiredRed;
        }

        Console.WriteLine(Answer);
    }

    static List<char> GetSurroundingCharacters(int ColumnIndex, int RowIndex, string[] FileLines)
    {
        string CurrentRow = FileLines[RowIndex];

        // This makes some gross assumptions about the length of every line. It also
        // duplicates values. But for our purpose this is fine.
        int LeftIndex   = Math.Clamp(ColumnIndex - 1, 0, CurrentRow.Length - 1);
        int RightIndex  = Math.Clamp(ColumnIndex + 1, 0, CurrentRow.Length - 1);
        int TopIndex    = Math.Clamp(RowIndex - 1, 0, FileLines.Length - 1);
        int BottomIndex = Math.Clamp(RowIndex + 1, 0, FileLines.Length - 1);

        List<char> SurroundingCharacters = new List<char>();
        SurroundingCharacters.Add(FileLines[TopIndex][LeftIndex]);      // TopLeft
        SurroundingCharacters.Add(FileLines[TopIndex][ColumnIndex]);    // TopMiddle
        SurroundingCharacters.Add(FileLines[TopIndex][RightIndex]);     // TopRight
        SurroundingCharacters.Add(FileLines[BottomIndex][LeftIndex]);   // BottomLeft
        SurroundingCharacters.Add(FileLines[BottomIndex][ColumnIndex]); // BottomMiddle
        SurroundingCharacters.Add(FileLines[BottomIndex][RightIndex]);  // BottomRight
        SurroundingCharacters.Add(FileLines[RowIndex][LeftIndex]);      // Left
        SurroundingCharacters.Add(FileLines[RowIndex][RightIndex]);     // Right

        return SurroundingCharacters;
    }

    static void Puzzle3()
    {
        string[] FileLines = File.ReadAllLines("AdventOfCode2023/inputs/input3.txt");

        List<int> Numbers = new List<int>();
        string CurrentNumber = "";
        bool bIsPartValid = false;

        // This is the regex I was attempting to use at the start
        // it did not work. Not sure why though off the top of my head
        Regex SymbolRegex = new Regex("[*/@&$=#-+%]");

        Action TryCacheNumber = () =>
        {
            if(CurrentNumber.Length > 0)
            {
                if(bIsPartValid)
                {
                    Numbers.Add(Int32.Parse(CurrentNumber));
                }

                CurrentNumber = "";
                bIsPartValid = false; 
            }
        };

        for(int RowIndex = 0; RowIndex < FileLines.Length; RowIndex++)
        {
            string CurrentRow = FileLines[RowIndex];
            
            for(int ColumnIndex = 0; ColumnIndex < FileLines[RowIndex].Length; ColumnIndex++)
            {
                if(Char.IsDigit(CurrentRow[ColumnIndex]))
                {
                    CurrentNumber += (CurrentRow[ColumnIndex]);

                    List<char> SurroundingCharacters = GetSurroundingCharacters(ColumnIndex, RowIndex, FileLines);
                    foreach(char Character in SurroundingCharacters)
                    {
                        // Char.IsSymbol does not match '*'???? Using a Regex instead then
                        //if(Character != '.' && SymbolRegex.IsMatch(Character.ToString()))
                        if(Character != '.' && !Char.IsDigit(Character))
                        {
                            bIsPartValid = true;
                        }
                    }

                    continue;
                }

                TryCacheNumber();
            }

            // End of the Line, cache whatever we have
            TryCacheNumber();
        }

        int Answer =  0;
        foreach (int Number in Numbers)
        {
            Answer += Number;
        }

        Console.WriteLine(Answer);
    }

    static void Main(string[] args) 
    {
        //Puzzle1();
        //Puzzle2();
        Puzzle3();
    }
}



