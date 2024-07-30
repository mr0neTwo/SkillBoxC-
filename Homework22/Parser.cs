namespace Homework22;

public static class Parser
{
	public static double StringToDouble(string? input)
	{
		if (string.IsNullOrEmpty(input))
		{
			throw new ArgumentNullException(nameof(input));
		}

		int index = 0;
		
		bool hasGapAhead = true;

		while (hasGapAhead && index < input.Length)
		{
			if (input[0] == ' ')
			{
				index++;
			}
			else
			{
				hasGapAhead = false;
			}
		}
		
		if (string.IsNullOrEmpty(input))
		{
			throw new FormatException("Input string is empty.");
		}

		bool isNegative = false;
		
		if (input[index] == '-')
		{
			isNegative = true;
			index++;
		}
		else if (input[index] == '+')
		{
			index++;
		}

		double result = 0.0;
		double fractionalPart = 0.0;
		bool isFractional = false;
		double fractionalDivisor = 10.0;
		bool hasDigits = false;
		
		while (index < input.Length)
		{
			char symbol = input[index];

			if (symbol is >= '0' and <= '9')
			{
				hasDigits = true;
				int digit = symbol - '0';

				if (isFractional)
				{
					fractionalPart += digit / fractionalDivisor;
					fractionalDivisor *= 10.0;
				}
				else
				{
					result = result * 10 + digit;
				}
			}
			else if (symbol == '.')
			{
				if (isFractional)
				{
					throw new FormatException("Input string has multiple decimal points.");
				}

				isFractional = true;
			}
			else if (symbol is 'e' or 'E')
			{
				index++;
				result += fractionalPart;

				int exponent = ParseExponent(input.Substring(index));

				while (exponent != 0)
				{
					if (exponent > 0)
					{
						result *= 10;
						exponent--;
					}
					else
					{
						result /= 10;
						exponent++;
					}
				}

				return result;
			}
			else
			{
				throw new FormatException($"Invalid character '{symbol}' in input string.");
			}

			index++;
		}

		if (!hasDigits)
		{
			throw new FormatException("Input string does not contain any digits.");
		}

		result += fractionalPart;

		return isNegative ? -result : result;
	}

	static int ParseExponent(string exponentPart)
	{
		if (string.IsNullOrEmpty(exponentPart))
		{
			throw new FormatException("Exponent part is empty.");
		}

		bool isNegative = false;
		int index = 0;

		if (exponentPart[index] == '-')
		{
			isNegative = true;
			index++;
		}
		else if (exponentPart[index] == '+')
		{
			index++;
		}

		int exponent = 0;
		bool hasDigits = false;

		while (index < exponentPart.Length)
		{
			char symbol = exponentPart[index];

			if (symbol is >= '0' and <= '9')
			{
				hasDigits = true;
				int digit = symbol - '0';
				exponent = exponent * 10 + digit;
			}
			else
			{
				throw new FormatException($"Invalid character '{symbol}' in exponent part.");
			}

			index++;
		}

		if (!hasDigits)
		{
			throw new FormatException("Exponent part does not contain any digits.");
		}

		return isNegative ? -exponent : exponent;
	}
}
