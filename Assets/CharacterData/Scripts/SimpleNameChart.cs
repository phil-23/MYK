using UnityEngine;
using System.Collections.Generic;

namespace MYK
{
    [CreateAssetMenu(fileName = "SimpleNameChart", menuName = "Character/Name/SimpleChart")]
    public class SimpleNameChart : ScriptableObject, INameGenerator 
	{
        [SerializeField] private List<string> names;

        public string Generate()
        {
            return names.GetRandom();
        }
	}
}