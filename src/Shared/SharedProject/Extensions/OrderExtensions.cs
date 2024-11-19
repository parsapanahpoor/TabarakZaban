
namespace SharedProject.Extensions;

public static class OrderExtensions
{
    public static bool IsInOrder(this List<ulong> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return false;

        var orderedNumbers = numbers
            .OrderBy(n => n).ToList();

        if (!orderedNumbers.Contains(1))
            return false;

        for (int i = 1; i < orderedNumbers.Count; i++)
        {
            if (orderedNumbers[i] - orderedNumbers[i - 1] != 1)
                return false;
        }

        return true;
    }
}
