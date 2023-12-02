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
        // Keeping track of cubes found in the game
        // Couldn't use this for part one, might help for part two copium 
        Dictionary<string, int> cube_tracker = new Dictionary<string, int>
        {
            { "red", 0 }, { "green", 0 }, { "blue", 0 }
        };

        // Parsing sets of cubes revealed from bag & updating the cube tracker
        // Extract the 'right' substring after the ':'
        int str_len = game_played.Length;
        // -2 for len cuz last char is just 
        string game_super_set = game_played.Substring(game_played.IndexOf(':') + ":".Length).Trim();
        // Console.WriteLine($"Un-parsed sets= {game_set}");

        // Logic:
        // Superset = Game = final eval if possible or not = Collection of Sets = Example: '5 blue, 12 green, 12 red; 11 green, 3 red; 14 green, 3 blue, 18 red'
        // Set = Collection of Game Pairs = Example: '7 blue, 6 green, 3 red' or '8 blue, 1 red'
        // Game Pair = Collection of color of cube and it's number (revealed at once by the elf) = Example: '7 blue' or '1 red'

        bool game_superset_flag = true;

        string[] game_subsets = game_super_set.Split(';');
        foreach(string each_game_set in game_subsets)
        {
            // Flag to check if game set was possible!
            bool game_set_flag = true;
            // Console.WriteLine($"Game set: {each_game_set.Trim()}");
            // Need to match with 'key' in tracker and update value
            // cube_tracker[cube_color] += num_cube_revealed for each '
            string[] all_game_pairs = each_game_set.Split(",");
            foreach (string each_game_pair in all_game_pairs)
            {
                // Flag to check if game pair was possible, by default, says it is false i.e. impossible game pair!

                string cleaned_game_pair = each_game_pair.Trim();
                // Console.WriteLine($"Game pair: {cleaned_game_pair}");
                // At this point, something like: 6 green or 8 blue etc.
                //string[] cube_color_num = cleaned_game_pair.Split(" ");
                string cube_color = cleaned_game_pair.Split(' ')[1];
                int num_cube_revealed = int.Parse(cleaned_game_pair.Split(' ')[0]);
                // Console.WriteLine($"Cube color: {cube_color} || Revealed at once: {num_cube_revealed}");
                cube_tracker[cube_color] += num_cube_revealed;
                // OR SHOULD IT BE LIKE
                // GetGamePairPossibility(cube_color, num_cube_revealed);
                bool game_pair_flag = GetGamePairPossibility(cube_color, num_cube_revealed);
                // Console.WriteLine($"\t\tWas this game pair: {cube_color} | {num_cube_revealed} possible? = {game_pair_flag}");
                if (!game_pair_flag)
                {
                    game_set_flag = false;
                    break;
                }
            }
            Console.WriteLine($"\n\tWas this game sub-set: {each_game_set.Trim()} possible? ==> {game_set_flag}");
            if ( !game_set_flag ) {
                game_superset_flag = false;
                break;
            }
            // Console.WriteLine($"\nWas this set of games played: {game_set.Trim()}\npossible? ===> {game_superset_flag}\n");

        }
        //cube_tracker.Select(i => $"\nFor Key {i.Key}, the Value is {i.Value}").ToList().ForEach(Console.WriteLine);

        // Final check (This approach was not correct cuz the cubes, after being revealed, were put BACK into the bag
        // 
        /*
        if ( (cube_tracker["red"] <= 12) & (cube_tracker["green"] <= 13) & (cube_tracker["blue"] <= 14))
        {
            result_flag = true;
        }
        */

        // Console.WriteLine($"FINALLY, Was this game possible (overall): {game_superset_flag}\n");
        return game_superset_flag;
    }

    /*
     * Given an input colored cube & number of times it was shown at once (as part of a set)
     * determine if it was indeed possible. Returns true if it was possible, false if it wasn't.
     * The color and the maximum number of times it could theoretically have been shown at once are as follows:
     * Red = 12
     * Green = 13
     * Blue = 14
     * So for instance, (red, 1) would return true but (blue, 15) would return false
     */
    public static bool GetGamePairPossibility(string cube_color, int number_revealed_at_once)
    {
        bool result;
        // List<string> valid_colors = ["red", "green", "blue"];
        // var a = valid_colors[0];

        switch (cube_color)
        {
            case "red":
                result = (number_revealed_at_once <= 12) ? true : false;
                break;
            case "green":
                result = ( number_revealed_at_once <= 13 ) ? true : false;
                break;
            case "blue":
                result = (number_revealed_at_once <= 14) ? true : false;
                break;
            default:
                result = false;
                break;
        }
        // Console.WriteLine($"Game pair: {number_revealed_at_once}-{cube_color} possible??: {result}");
        return result;
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
        // string first_game = input_games_played.First();
        // Array.Resize(ref input_games_played, 3);
        return GetSumOfAllPossibleGames(input_games_played).ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
