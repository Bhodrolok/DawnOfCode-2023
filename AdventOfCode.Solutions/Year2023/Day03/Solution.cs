namespace AdventOfCode.Solutions.Year2023.Day03;

class Solution : SolutionBase
{
    public Solution() : base(03, 2023, "Gear Ratios") { }

    static int GetSumOfAllPartNums(string[] engine_schematic)
    {
        int[] part_nums_all = new int[engine_schematic.Length];

        foreach(string each_line in engine_schematic)
        {
            // For each line, get all the part numbers
            int[] part_num_line = GetPartNumForLine(each_line);
            // And add them to the tracker
            // Could also sum them up here and have tracker instead store sum of all part numbers (in each line)?
            // part_nums_all = part_nums_all.Append(part_num_line);

        }

        return part_nums_all.Sum();
    }

    static int[] GetPartNumForLine(string line)
    {
        // Prob not best in terms of sys resource/memory usage?
        int[] result = new int[line.Length];
        return result;
    }

    protected override string SolvePartOne()
    {
        // Load records entire engine schematic into a string array
        // So each line should be a part of a visual rep. of the machine
        // Compare one line with the next immediate line
        // do stuff
        // continue till last line i.e. Length of the arr or [Length-1]
        // KINDA like FLipping a mattress? surely it can be faster to do this by 'rolling' from both start and last elements till the medium, no?
        // 
        string[] engine_schematic = Input.SplitByNewline();
        // string first_game = engine_schematic.First();
        // Array.Resize(ref engine_schematic, 3);
        //return GetSumOfAllPossibleGames(engine_schematic).ToString();
        return engine_schematic.Length.ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
