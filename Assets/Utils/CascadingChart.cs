using UnityEngine;
using System.Collections.Generic;

namespace MYK
{
    /// <summary>
    /// Data structure for storing a series of charts that can refer to other charts.
    /// Any of the charts can be accessed by name, which will return a random entry on that chart.
    /// </summary>
    public class CascadingChart
    {
        private Dictionary<string, List<string>> _charts;
        private char _openEscape, _closeEscape;
        
        public CascadingChart(string json, char open = '{', char close = '}')
        {
            _openEscape = open;
            _closeEscape = close;
            _charts = JsonUtility.FromJson<Dictionary<string, List<string>>>(json);

            if (_charts == null)
                throw new System.ArgumentException("Invalid JSON for charts.");
        }

        public string GetResult(string chart)
        {
            if (!_charts.ContainsKey(chart))
                throw new System.ArgumentException(chart + " is not a valid chart.");
            string input = _charts[chart].GetRandom();

            for (int last = 0; true; last++)
            {
                int first = input.IndexOf(_openEscape, last);
                if (first < 0) break;

                last = input.IndexOf(_closeEscape, first + 1);
                if (last < 0) break;

                string middle = input.Substring(first + 1, (last - first) - 1);
                input = input.Substring(0, first) + Resolve(middle) + input.Substring(last + 1);
            }

            return input;
        }

        private string Resolve(string input)
        {
            // Currently, there is only one type of special action - reference another chart
            return GetResult(input);
        }
    }
}