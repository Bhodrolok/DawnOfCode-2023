using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day01;

/// <summary>
///  Class <c>Solution</c> for solving Problems (Part 1 + Part 2) given in Day 1 of Advent of Code 2023.
///  <para>
///  The newly-improved calibration document consists of lines of text; 
///  each line originally contained a specific calibration value that the Elves now need to recover. 
///  On each line, the calibration value can be found by combining the first digit and the last digit 
///  (in that order) to form a single two-digit number.
///  Given puzzle input, compute the summation of all the calibration values. 
///  Looks like both parts accepts a single string-type input as answer submission.
///  </para>
/// </summary>
class Solution : SolutionBase
{
    // Constructor calls base class `SolutionBase` with (day, year, name)
    public Solution() : base(01, 2023, "Trebuchet?!")
    {
        // you can use the constructor for preparations shared by both part one and two if you 
    }

    /*
     * Given an input string, return it's 'calibration value'
     * which is defined as the combination of the first and last (numerical) digits, in the string
     * this combination will form a single two digit number (as a string!).
     */
    public static string GetCalibrationValue(string line)
    {
        // Adapted from: https://stackoverflow.com/a/4734164 ;
        // \d+ => >=1 digits match, so was returning chained digits instead of literal first occurence
        // \d => [0-9]
        // Scan from Left to Right
        string first_num = Regex.Match(line, @"\d").Value;
        // Scan from Right to Left (cuz don't care about other letters, digit not getting reversed ;) )
        string last_num = Regex.Match(line.ReverseString(), @"\d").Value;
        //return "Calibration value of line: " + line + " is: " + first_num + last_num;
        return first_num + last_num;
    }

    /*
     * Given an input string array, return the sum, as int, of all it's 'calibration values'
     * For each element of the string array (i.e. line), get it's corresponding calibration value
     * and add it to a temporary int array (whose length is equal to length of the input string array)
     * Use useProper as simple flag to determine whether to use it for Part 2's calibration formula ('proper' one) 
     * as rest of the logic would be same
     * Final result obtained by summin' the int array.
     * NB: string[] is an IEnumerable<string> ( from IEnumerable<T> )
     */
    public static int GetSumOfCalibrationValues(string[] lines, bool useProper)
    {
        int[] temp_lines_int = new int[lines.Length];

        foreach (string each_line in lines)
        {
            // Get calibration value of each line
            string each_line_calibration_value = ( useProper ) ? GetProperCalibrationValue(each_line) : GetCalibrationValue(each_line);
            // Store this, as int, to a int array for finally summing 
            // Prob can be shortened by simply updating function return value data type itself {https://stackoverflow.com/a/638587}
            int num_val = int.Parse(each_line_calibration_value);
            // Console.WriteLine("Adding: " + num_val + " to int array!");
            temp_lines_int = temp_lines_int.Append(num_val).ToArray();
        }
        // string firs_line = lines.Last().Trim();
        // return lines.First();
        //return GetCalibrationValue(firs_line);
        return temp_lines_int.Sum();
    }

    protected override string SolvePartOne()
    {
        string[] input_lines = Input.SplitByNewline();
        return GetSumOfCalibrationValues(input_lines, false).ToString();
    }

    /* Given an input string, return it's PROPER 'calibration value'
     * on top of the previous definition (first/last digits), stuff like: 'one', 'two', also count as valid 'digits' 
     * so `4nineeightseven2` would be `42` but
     * but `zoneight234` would be `14` and not `24`
     * for computing the calibration value.
     * Idea so far?
     * 1. Check regex against list of matches i.e. ['one','two', 'three', .... 'nine'] or the regular digit i.e. \d
     * 2. If valid word is found/matched first, before the \d, check if it is valid from dictionary (key: word, value: int version)
     * 3. If digit found instead first, just use/return that
     */
    public static string GetProperCalibrationValue(string line)
    {
        // Adapted from: https://stackoverflow.com/a/4734164 ;
        // \d+ => >=1 digits match, so was returning chained digits instead of literal first occurence
        // also look for words (with ONLY letters no digits)
        // Apparently not possible to directly match word in string from list/dict of valid words directly in regex??
        // Regex help: https://stackoverflow.com/a/74067813
        // https://stackoverflow.com/a/11416239

        // bool does_str_contain_digit = line.Any(char.IsDigit);     // https://stackoverflow.com/a/18251942

        string result = "";
        // Keep a reference hash-table of valid words and their numerical value mappings (in string, not int!)
        // incl. actual digits like 1, 2, themselves too!
        Dictionary<string, string> valid_digits_all = new Dictionary<string, string>
        {
            { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" },{ "seven", "7" },
            { "eight", "8" }, { "nine", "9" }, 
            { "1", "1" }, { "2", "2" }, { "3", "3" },{ "4", "4" }, { "5", "5" }, { "6", "6" }, { "7", "7" }, { "8", "8" }, { "9","9" },
            { "0","0" }
        };

        // Keys of above table for quick checkin'
        Dictionary<string, string>.KeyCollection valid_words_all = valid_digits_all.Keys;

        // Flag to check if the line contains: NO Valid Letter Words (don't care about digit)
        // If found true, use the OG GetCalibrationValue() (cuz {assumption} that the line would atleast have a digit in it then,
        // either by themselves or mixed with gibberish
        bool does_str_contain_valid_letter = false;
        Dictionary<string, string> valid_digits_only_letters = new Dictionary<string, string>
        {
            { "one", "1" }, { "two", "2" }, { "three", "3" }, { "four", "4" }, { "five", "5" }, { "six", "6" },{ "seven", "7" },
            { "eight", "8" }, { "nine", "9" }
        };

        // Keep a reference table of valid words themselves i.e. only 'one', 'two' ... 'nine' including the literal digits too '1', '2', ... '9'!
        // Dictionary<string, string>.KeyCollection valid_words_only_letters = valid_digits_only_letters.Keys;
        foreach (KeyValuePair<string, string> each_valid_letter_digit in valid_digits_only_letters)
        {
            if (line.Contains(each_valid_letter_digit.Key))  
            {
                // Found a valid letter digit like 'one', 'nine' in the input string, mark flag as true and GTFO
                does_str_contain_valid_letter = true;
                break;
            }
        }
        if (does_str_contain_valid_letter == false)
        {
            // No valid letter digits found, in string, use OG GetCalibrationValue() as only concerned with digits
            Console.WriteLine($"No valid LETTER digit found in {line}!!");
            result = GetCalibrationValue(line);
            Console.WriteLine($"So the calibration value of {line} is: {result}");
            return result;
        }
        // Else, confirmed that atleast one LETTER digit is present in the input string

        // First, scan line forward i.e. from Left to Right,
        // use tracking index equal to length (cuz line[length] is null, only legit till line[length-1])
        int minIndex = line.Length;
        string first_valid_digit = null;

        foreach (string word in valid_words_all)
        {
            int index = line.IndexOf(word);
            if (index != -1 && index < minIndex)
            {
                minIndex = index;
                first_valid_digit = word;
            }
        }

        if (first_valid_digit != null)
        {
            Console.WriteLine($"First valid 'digit' for {line} is: {first_valid_digit}");
        }
        else
        {
            Console.WriteLine($"No valid 'digit' found for {line} #FIRST.");
            first_valid_digit = "0";
        }
        
        // Now, scan the word backwards i.e. Right to Left
        string second_valid_digit = null;
        // Lot of hustlin cuz of this, prev max_windex was 1....
        int max_index = -1;
        foreach (string word in valid_words_all)
        {
            int index = line.LastIndexOf(word);
            if (index != -1 && index > max_index)
            {
                max_index = index;
                second_valid_digit = word;
            }
        }

        if (second_valid_digit != null)
        {
            Console.WriteLine($"Second valid word for {line} is: {second_valid_digit}");
        }
        else
        {
            Console.WriteLine($"No valid words found in {line} #SECOND.");
            second_valid_digit = "0";
        }
        result = valid_digits_all[first_valid_digit] + valid_digits_all[second_valid_digit];
        return result;
    }

    protected override string SolvePartTwo()
    {
        // Load all input lines into a string array
        string[] input_lines = Input.SplitByNewline();
        return GetSumOfCalibrationValues(input_lines, true).ToString();
    }
}
