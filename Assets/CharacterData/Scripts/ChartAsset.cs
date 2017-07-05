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