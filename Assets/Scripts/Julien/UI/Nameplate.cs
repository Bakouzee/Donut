using UnityEngine;
using UnityEngine.UI;

namespace Com.Donut.BattleSystem
{
    public class Nameplate : MonoBehaviour
    {
        [SerializeField] private Text life;
        [SerializeField] private Text power;

        private Fighter _fighter;

        public void Initialize(FighterData fighterData)
        {
            _fighter = fighterData.fighter;
            life.text = _fighter.CurrentHealth.ToString();
            power.text = fighterData.fighter.Power.ToString();
        }

        public void UpdateNameplate()
        {
            life.text = _fighter.CurrentHealth.ToString();
            power.text = _fighter.Power.ToString();

            if (_fighter.CurrentHealth <= 0)
            {
                life.text = "0";
            }

            if (_fighter.Power <= 0)
            {
                power.text = "0";
            }
        }
    }
}