using System.Globalization;

public static class NumberToWordsConverter
{
    private static readonly string[] unitsMap = {
        "zero", "one", "two", "three", "four", "five", "six",
        "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen",
        "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
    };

    private static readonly string[] tensMap = {
        "zero", "ten", "twenty", "thirty", "forty", "fifty",
        "sixty", "seventy", "eighty", "ninety"
    };

    public static string Convert(decimal amount)
    {
        var intPart = (int)Math.Floor(amount);
        var decimalPart = (int)((amount - intPart) * 100);

        string words = $"{ConvertToWords(intPart)} rupees";

        if (decimalPart > 0)
            words += $" and {ConvertToWords(decimalPart)} paisa";

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words + " only");
    }

    private static string ConvertToWords(int number)
    {
        if (number == 0)
            return unitsMap[0];

        if (number < 0)
            return "minus " + ConvertToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += ConvertToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += ConvertToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += ConvertToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words.Trim();
    }
}
