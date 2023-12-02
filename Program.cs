using System;

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

    static void Main(string[] args) 
    {
        //Puzzle1();
        Puzzle2();
    }
}



