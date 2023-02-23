using CodeBase.Agent;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private AgentPresenter agentPresenter;

        private void Update()
        {
            Pick();
        }

        private void Pick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000))
                {
                    if (hit.transform.CompareTag(Constance.Agent))
                        agentPresenter.SelectAgent(hit.transform.GetComponent<AgentInfo>());
                    else
                        agentPresenter.SelectAgent(null);
                }
                else
                    agentPresenter.SelectAgent(null);
            }
        }
    }
}