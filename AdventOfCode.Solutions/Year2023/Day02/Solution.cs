namespace AdventOfCode.Solutions.Year2023.Day02;

/// <summary>
///  Class <c>Solution</c> for solving Problems (Part 1 + Part 2) given in Day 2 of Advent of Code 2023.
///  <para>
///  The Elf would first like to know which games would have been possible 
///  if the bag contained only: 12 red cubes, 13 green cubes, and 14 blue cubes?
///  i.e.
///  Determine which games would have been possible 
///  if the bag had been loaded with only: 12 red cubes, 13 green cubes, and 14 blue cubes. 
///  What is the sum of the IDs of those games?
///  Format of input:
///  Each game is listed with its ID number (like the 11 in Game 11: ...) 
///  followed by a semicolon-separated list of: subsets of cubes that were revealed from the bag (like 3 red, 5 green, 4 blue).
///  </para>
/// </summary>
class Solution : SolutionBase
{
    public Solution() : base(02, 2023, "Cube Conundrum") { }

    /*
     * Given an input game, determine if it was possible to have been played 
     * if the bag only had 12 Red, 13 Green, and 14 Blue cubes.
     * Returns true if it was indeed possible and false if it wasn't!
     * Sample Input => Expected Output (given in problem description)
     * Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green => True
     * Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue => True
     * Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red => False { 20 Red }
     * Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red => False { 14 Red + 15 Blue}
     * Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green => True
     * NB: expects the input 'line' as string! Need to parse it properly!
     */
    public static bool GetPossibilityOfGame(string game_played)
    {
        // Dictionary<string, int>
        return false;
    }

    /*
     * Given a string representing the recorded information of a game that was played
     * extract the ID number, as an int, of the game. Makes parsing elswhere easier & more readable!
     * Same Input => Expected Output
     * Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green => 5
     * Game 84: 4 red, 2 blue, 2 green; 8 red, 10 blue; 1 green, 15 red, 8 blue => 84
     * NB: Assumption that text is strictly of format as above...
     * NB: Not sure if the double splittin' is efficient tbf
     */
    public static int GetGameID(string game_played)
    {
        string before_colon = game_played.Split(':')[0];
        // Console.WriteLine($"For game: {game_played}\nGame ID is: {GetGameID(before_colon.Split(' ')[1]}");
        // Quick reference on str-int conversion: {https://stackoverflow.com/a/638587}
        return int.Parse(before_colon.Split(' ')[1]);
    }
    
    /*
     * Given a collection of games played, return the total number of games that were actually possible.
     * Obtained by summing up the possible Game's ID numbers, returns value of Integer data type
     * Thankfully the IDs are chronological and actually correspond to the order in which they were played
     * i.e. Game 42 was played before Game 11
     */
    public static int GetSumOfAllPossibleGames(string[] games_played)
    {
        int[] valid_game_IDs = new int[games_played.Length];

        foreach(string each_game in games_played)
        {
            // If the game played was indeed possible
            if(GetPossibilityOfGame(each_game))
            {
                // Extract game ID and update the tracker
                valid_game_IDs = valid_game_IDs.Append(GetGameID(each_game)).ToArray();
            }
        }

        return valid_game_IDs.Sum();
    }

    protected override string SolvePartOne()
    {
        // Load records of all games played into a string array
        // So each line should be like: [ Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue;  ]
        string[] input_games_played = Input.SplitByNewline();
        string first_game = input_games_played.First();
        return GetSumOfAllPossibleGames(input_games_played).ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
