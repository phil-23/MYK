using UnityEngine;
using System.Collections.Generic;

namespace MYK
{
    [CreateAssetMenu(fileName = "ChartAsset", menuName = "Character/ChartAsset")]
    public class ChartAsset : ScriptableObject
    {
        [SerializeField] private string resourceFile;
        [SerializeField] private string primaryChart;

        private CascadingChart _chart;

        public string GetResult(string chart = null)
        {
            var foo = new List<string>(new string[] { "a", "b", "c" });
            string result = JsonUtility.ToJson(foo);
            Debug.Log(result);

            if (_chart == null)
                LoadChart();

            return _chart.GetResult(chart ?? primaryChart);
        }

        private void LoadChart()
        {
            var textAsset = Resources.Load<TextAsset>(resourceFile);
            _chart = new CascadingChart(textAsset.text);
        }
    }
}