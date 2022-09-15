using UnityEngine;
using UnityEngine.UI;

namespace Com.Donut.BattleSystem
{
    public class NameplateBoss : MonoBehaviour
    {
        [SerializeField] private Text fighterName;
        [SerializeField] private Text fighterLevel;

        private Fighter _fighter;

        public void Initialize(Fighter fighter)
        {
            _fighter = fighter;
            fighterName.text = _fighter.Name;
            fighterLevel.text = "Lvl" + _fighter.Level;
        }
    }
}