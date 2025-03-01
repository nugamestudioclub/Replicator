using System;
using UnityEngine;

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
            if (value == 0)
            {
                // Handle the special case where the value is 0.
                symbol = symbols[0];
                return;
            }

            // Determine the exponent in groups of 3
            int exponent = (int)Math.Floor(Math.Log10(Math.Abs(value)) / 3) * 3;

            // Calculate symbol index
            int symbolIndex = exponent / 3;

            // Ensure the symbol index is within bounds
            if (symbolIndex < 0)
            {
                symbolIndex = 0;
            }
            else if (symbolIndex >= symbols.Length)
            {
                symbolIndex = symbols.Length - 1;
            }

            // Set the symbol
            symbol = symbols[symbolIndex];

            // Adjust the value to be in the appropriate range
            value /= Math.Pow(10, symbolIndex * 3);
        }


        public override string ToString()
        {
            int digits = Mathf.CeilToInt((float)value / 10f);
            if (value == 0)
            {
                return "0" + symbol; // Special case for zero
            }

            // Determine the scale factor needed to isolate the most significant digits
            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(value))) - digits + 1);

            // Round the value to the nearest integer at this scale
            double roundedValue = Math.Round(value / scale) * scale;

            // Format the rounded value with no fractional part
            return $"{roundedValue:F0}{symbol}";
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
