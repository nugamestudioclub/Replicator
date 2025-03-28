using System;
using UnityEngine;

namespace Numbering
{
    public class SymbolNumber
    {
        private double _value;
        private string _symbol;

        private static readonly string[] Symbols = { "", "k", "M", "B", "T", "P", "E" };

        public double Value
        {
            get => _value;
            set => SetAndFormatValue(value);
        }

        public string Symbol => _symbol;

        public SymbolNumber(double value)
        {
            SetAndFormatValue(value);
        }

        private void SetAndFormatValue(double newValue)
        {
            _value = newValue;
            UpdateSymbol();
        }

        private void UpdateSymbol()
        {
            if (_value == 0)
            {
                _symbol = Symbols[0];
                return;
            }

            int exponent = (int)Math.Floor(Math.Log10(Math.Abs(_value)) / 3) * 3;
            int symbolIndex = exponent / 3;

            if (symbolIndex < 0)
            {
                symbolIndex = 0;
            }
            else if (symbolIndex >= Symbols.Length)
            {
                symbolIndex = Symbols.Length - 1;
            }

            _symbol = Symbols[symbolIndex];
            _value /= Math.Pow(10, symbolIndex * 3);
        }

        public override string ToString()
        {
            if (_value == 0)
            {
                return "0" + Symbol;
            }

            return $"{_value:F0}{Symbol}";
        }

        // Arithmetic operators updated to use the property Value
        public static SymbolNumber operator +(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.Value + b.Value);
        }

        public static SymbolNumber operator -(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.Value - b.Value);
        }

        public static SymbolNumber operator *(SymbolNumber a, SymbolNumber b)
        {
            return new SymbolNumber(a.Value * b.Value);
        }

        public static SymbolNumber operator /(SymbolNumber a, SymbolNumber b)
        {
            if (b.Value == 0)
                throw new DivideByZeroException("Cannot divide by zero.");

            return new SymbolNumber(a.Value / b.Value);
        }
    }
}
