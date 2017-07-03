using UnityEngine;
using System.Collections.Generic;

namespace MYK
{
    [CreateAssetMenu(fileName = "CompoundNameChart", menuName = "Character/Name/CompoundChart")]
    public class CompoundNameChart : ScriptableObject, INameGenerator
    {
        [System.Serializable]
        public class Entry
        {
            public List<ScriptableObject> generators;
        }

        [SerializeField] private List<Entry> combinations;
        [SerializeField] private string prefixFlag;

        public string Generate()
        {
            var list = combinations.GetRandom().generators;
            if(list == null || list.Count < 1)
            {
                throw new System.NullReferenceException("Empty list.");
            }

            string result = (list[0] as INameGenerator).Generate();
            for(int i = 1; i < list.Count; i++)
            {
                string part = (list[i] as INameGenerator).Generate();
                if(part.StartsWith(prefixFlag))
                {
                    result = part.Substring(prefixFlag.Length) + " " + result;
                }
                else
                {
                    result = result + " " + part;
                }
            }

            return result;
        }
    }
}