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
        private const char OPEN_ESCAPE = '{';
        private const char CLOSE_ESCAPE = '}';

        [System.Serializable] private class ChartSet
        {
            public string version;
            public List<Chart> charts;
        }

        [System.Serializable] private class Chart
        {
            public string name;
            public List<string> items;
        }

        private Dictionary<string, List<string>> _charts;
        
        
        public CascadingChart(string json)
        {
            var set = JsonUtility.FromJson<ChartSet>(json);
            if (set == null || set.charts == null || set.charts.Count == 0)
                throw new System.ArgumentException("Invalid JSON for charts.");

            _charts = new Dictionary<string, List<string>>();
            for(int i = 0; i < set.charts.Count; i++)
            {
                _charts.Add(set.charts[i].name, set.charts[i].items);
            }
        }

        public string GetResult(string chart)
        {
            if (!_charts.ContainsKey(chart))
                throw new System.ArgumentException(chart + " is not a valid chart.");
            string input = _charts[chart].GetRandom();

            for (int last = 0; last < input.Length;)
            {
                int first = input.IndexOf(OPEN_ESCAPE, last);
                if (first < 0) break;

                last = input.IndexOf(CLOSE_ESCAPE, first + 1);
                if (last < 0) break;

                string middle = input.Substring(first + 1, (last - first) - 1);
                input = input.Substring(0, first) + Resolve(middle) + input.Substring(last + 1);
                last = first + middle.Length;
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