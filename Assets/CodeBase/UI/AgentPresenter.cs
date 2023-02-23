using UnityEngine;
using TMPro;
using CodeBase.Agent;

namespace CodeBase.UI
{
    public class AgentPresenter : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI agentName;
        [SerializeField]
        private TextMeshProUGUI agentHealth;

        private AgentInfo info;

        public void SelectAgent(AgentInfo info)
        {
            if (info != null)
            {
                if (this.info != null && this.info.agentName == info.agentName)
                {
                    EventsUnsub();
                    this.info.SelectOff();
                    ClearInfo();

                    return;
                }
                else if (this.info != null)
                {
                    EventsUnsub();
                    this.info.SelectOff();
                }

                this.info = info;
                EventsSub();
                info.SelectOn();
                SetAllInfo();
            }
            else
            {
                if (this.info != null)
                {
                    EventsUnsub();
                    this.info.SelectOff();
                }

                ClearInfo();
            }
        }

        private void EventsUnsub()
        {
            info.OnHealthUpdate -= SetAllInfo;
            info.GetComponent<AgentHealth>().OnDeath -= ClearInfo;
        }

        private void EventsSub()
        {
            info.OnHealthUpdate += SetAllInfo;
            info.GetComponent<AgentHealth>().OnDeath += ClearInfo;
        }

        private void SetAllInfo()
        {
            agentName.text = info.agentName;
            agentHealth.text = info.health;
        }

        private void ClearInfo()
        {
            info = null;

            agentName.text = "Name: -";
            agentHealth.text = "Health: -";
        }
    }
}