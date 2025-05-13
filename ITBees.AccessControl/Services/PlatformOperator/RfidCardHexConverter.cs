using System.Text.RegularExpressions;

namespace ITBees.AccessControl.Services.PlatformOperator;

public static class RfidCardHexConverter
{
    public static string GetHexFormat(string cardId)
    {
        if (IsDecimal(cardId))
        {
            return NormalizeUid(DecimalToHex(cardId));
        }

        return cardId;
    }
    
    public static bool IsHex(string input)
    {
        if (string.IsNullOrEmpty(input)) return false;
        
        var match = Regex.Match(input, @"^[A-Fa-f0-9]{8,16}[xy]?$");
        return match.Success;
    }

    public static bool IsDecimal(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsDigit(c))
                return false;
        }
        return true;
    }
    
    static string NormalizeUid(string uid)
    {
        return uid.PadLeft(14, '0');
    }

    public static string DecimalToHex(string decimalString)
    {
        if (ulong.TryParse(decimalString, out ulong dec))
        {
            return dec.ToString("X");
        }
        else
        {
            throw new FormatException("Invalid decimal string format.");
        }
    }
}