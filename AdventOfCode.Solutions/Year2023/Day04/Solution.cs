using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2023.Day04;

class Solution : SolutionBase
{
    public Solution() : base(04, 2023, "Scratchcards") { }


    public static int GetCardNumber(string scatchcard)
    {
        string card_num = Regex.Match(scatchcard, @"\d+\:").Value;
        return int.Parse(card_num.Remove(card_num.Length-1));
    }

    /*
     * First match makes the card worth one point
     * and each match after the first doubles the point value of that card!
     * 0 => 0
     * 1 => 1
     * 2 => 2
     * 3 => 4
     */
    public static int QuickMafs(int n)
    {
        if (n <= 0)
        {
            return 0;
        }
        return (int) Math.Pow(2, n-1);
    }

    /*
     * Given an input string representing a scratchcard of the format: Game ID_Number: Winning_Numbers_List | Numbers_Got_List
     * Return the total points that the scratchcard is worth.
     * The points are dependent on the numbers in the list of the numbers that I have that match with the winning numbers list.
     * The first match makes the card worth one point and each match after the first doubles the point value of that card.
     */
    protected static int GetCardPoint(string scratchcard)
    {
        int card_number = GetCardNumber(scratchcard);
        // Console.WriteLine($"Working on Card #{card_number}");

        // Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
        // Winning Numbers | Potentially Winning Numbers that I have
        string scratchcard_2 = scratchcard.Substring(scratchcard.IndexOf(':')+":".Length);

        // Console.WriteLine("At this point, scratch card is: " + scratchcard_2);
        string[] temp1 = scratchcard_2.Split('|');
        // Guess split got some good tricks up it's sleeve...
        string[] winning_numbers = temp1[0].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string actual_numbers = temp1[1].Trim();

        /*
        Regex rx = new Regex(@"(?is)(?:.*?\b(" + string.Join("|", winning_numbers) + @")\b)+");
        Match m = rx.Match(actual_numbers);
        */
        HashSet<int> matching_numbers = [];
        foreach (string winning_number in winning_numbers)
        {
            // Check for occurences of the digit/number amongst my string i.e. numbers that I have
            //Console.WriteLine($"Checking for a match for the winning number: {winning_number}");
            if (Regex.IsMatch(actual_numbers, @"\b" + Regex.Escape(winning_number) + @"\b"))
            {
                matching_numbers.Add(int.Parse(winning_number));
            }
        }
        //matching_numbers.ToList<int>().ForEach(x => Console.WriteLine($"I found a matched winning number, it is: {x}"));
        int card_match_counter = matching_numbers.Count;
        // Console.WriteLine($"So I got a total of: {card_match_counter} matching numbers!");
        // int card_match_counter = m.Groups[1].Captures.Count;
        // Console.WriteLine($"Thus, {card_number} is worth: {QuickMafs(card_match_counter)} points!");
        return QuickMafs(card_match_counter);
    }

    /*
     * Given an string array representing a collection(pile) of scratchcards
     * Return the total points that they are worth alltogether
     */
    protected static int GetSumOfAllPoints(string[] all_cards)
    {
        int[] result = new int[all_cards.Length];
        // => = []
        foreach(string each_card in all_cards)
        {
            // Get point(s) for each card from the pile
            Console.WriteLine($"Checking for points from scratchard: {GetCardNumber(each_card)}");
            int card_point = GetCardPoint(each_card);
            Console.WriteLine($"This card is worth {card_point} points!\n");
            result = result.Append(card_point).ToArray();
        }
        // Console.WriteLine("Array line : " + string.Join(",", result
        return result.Sum();
    }

    protected override string SolvePartOne()
    {
        string[] all_scratchcards = Input.SplitByNewline();
        // string first_card = all_scratchcards.First();
        //Array.Resize(ref all_scratchcards, 2);
        // Array.Resize(ref all_scratchcards, 5);
        return GetSumOfAllPoints(all_scratchcards).ToString();
        // return "XDDD";
    }

    protected override string SolvePartTwo()
    {
        return "";
    }
}
