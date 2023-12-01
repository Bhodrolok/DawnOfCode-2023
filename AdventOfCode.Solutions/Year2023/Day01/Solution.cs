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

    // Given an input string, return it's 'calibration value'
    // which is defined as the combination of the first and last (numerical) digits, in the string
    // this combination will form a single two digit number (as a string!)
    public static string GetCalibrationValue(string line)
    {
        // Adapted from: https://stackoverflow.com/a/4734164 ;
        // \d+ => >=1 digits match, so was returning chained digits instead of literal first occurence
        string first_num = Regex.Match(line, @"\d").Value;
        string last_num = Regex.Match(line.ReverseString(), @"\d").Value;
        //return "Calibration value of line: " + line + " is: " + first_num + last_num;
        return first_num + last_num;
    }

    protected override string SolvePartOne()
    {
        string[] lines = Input.SplitByNewline();
        
        int[] temp_lines_int = new int[lines.Length];

        foreach( string each_line in lines )
        {
            // Get calibration value of each line
            string each_line_calibration_value = GetCalibrationValue(each_line);
            // Store this, as int, to a int array for finally summing 
            int num_val = int.Parse(each_line_calibration_value);
            // Console.WriteLine("Adding: " + num_val + " to int array!");
            temp_lines_int = temp_lines_int.Append(num_val).ToArray();
        }
        // string firs_line = lines.Last().Trim();
        // return lines.First();
        //return GetCalibrationValue(firs_line);
        return temp_lines_int.Sum().ToString();
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
