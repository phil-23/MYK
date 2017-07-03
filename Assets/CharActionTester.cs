using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MYK
{
    public class CharActionTester : MonoBehaviour
    {
        [System.Serializable]
        public class AnimAction
        {
            public string name;
            public bool toggle = false;
            public float duration = 5f;            
        }

        [SerializeField] private List<AnimAction> actions;
        [SerializeField] private float timeBetweenActions = 1f;
        [SerializeField] private Text statusField;
        [SerializeField] private ScriptableObject nameGen;


        private Animator _anim;
        private string _lastToggle;
        
        private void Start ()
        {
            _anim = GetComponent<Animator>();
            PlayAction();
        }

        private void PlayAction()
        {
            AnimAction act = actions[Random.Range(0, actions.Count)];
            statusField.text = (nameGen as INameGenerator).Generate();
            if (act.toggle)
            {
                _anim.SetBool(act.name, true);
                _lastToggle = act.name;
            }
            else
            {
                _anim.SetTrigger(act.name);
            }

            Invoke("FinishAction", act.duration);
        }

        private void FinishAction()
        {
            if(_lastToggle != null)
            {
                _anim.SetBool(_lastToggle, false);
            }

            Invoke("PlayAction", timeBetweenActions);
        }
	}
}