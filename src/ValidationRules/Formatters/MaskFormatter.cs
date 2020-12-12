using Plugin.ValidationRules.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.ValidationRules.Formatters
{
    public class MaskFormatter : IValueFormatter<string>
    {
        IDictionary<int, char> _positions;
        string _oldValue;

        public MaskFormatter(string mask)
        {
            Mask = mask;
            SetPositions();
        }

        public string Mask { get; private set; }

        public string Format(string text)
        {
            if (text == _oldValue)
                return text;

            _oldValue = text;

            if (string.IsNullOrWhiteSpace(text) || _positions == null)
                return text;

            if (text.Length > Mask.Length)            
                return text.Remove(text.Length - 1);
            
            foreach (var position in _positions)
            {
                if (text.Length >= position.Key + 1)
                {
                    var value = position.Value.ToString();
                    if (text.Substring(position.Key, 1) != value)
                        text = text.Insert(position.Key, value);
                }
            }

            return text;
        }


        void SetPositions()
        {
            if (string.IsNullOrEmpty(Mask))
            {
                _positions = null;
                return;
            }

            var list = new Dictionary<int, char>();
            for (var i = 0; i < Mask.Length; i++)
                if (Mask[i] != 'X')
                    list.Add(i, Mask[i]);

            _positions = list;
        }

        public void ChangeMask(string mask) => Mask = mask;
    }
}
