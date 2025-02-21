using System;

namespace Numbering
{
    public class SymbolNumber
    {
        private double value;
        private string symbol;

        private static readonly string[] symbols = { "", "k", "M", "B", "T", "P", "E" };

        public SymbolNumber(double value)
        {
            this.value = value;
            UpdateSymbol();
        }

        private void UpdateSymbol()
        {
            int exponent = (int)Math.Floor(Math.Log10(Math.Abs(value)) / 3) * 3;
            int symbolIndex = exponent / 3;
            if (symbolIndex >= symbols.Length) symbolIndex = symbols.Length - 1;
            symbol = symbols[symbolIndex];

            // Normalize value to within range
            value /= Math.Pow(10, exponent);
        }

        public override string ToString()
        {
            return $"{value:F3}{symbol}";
        }

        public static SymbolNumber operator +(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.value * Math.Pow(10, a.GetExponent()) + b.value * Math.Pow(10, b.GetExponent()));
        }

        public static SymbolNumber operator -(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.value * Math.Pow(10, a.GetExponent()) - b.value * Math.Pow(10, b.GetExponent()));
        }

        public static SymbolNumber operator *(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.value * b.value * Math.Pow(10, a.GetExponent() + b.GetExponent()));
        }

        public static SymbolNumber operator /(SymbolNumber a, SymbolNumber b)
        {
            if (b.value == 0)
                throw new DivideByZeroException("Cannot divide by zero.");
            return new SymbolNumber((a.value * Math.Pow(10, a.GetExponent())) / (b.value * Math.Pow(10, b.GetExponent())));
        }

        private int GetExponent()
        {
            // Calculate exponent to determine the actual value
            return (int)Math.Floor(Math.Log10(Math.Abs(value)) / 3) * 3;
        }
    }
}
