using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class MarkoPolo : MonoBehaviour
{
    [SerializeField]
    private Button openButton;
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private TextMeshProUGUI text;

    private CanvasGroup canvasGroup;
    private Coroutine currentCoroutine;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        openButton.onClick.AddListener(Open);
        closeButton.onClick.AddListener(Close);
    }

    private void Open()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        currentCoroutine = StartCoroutine(OpenMarkoPolo());
    }

    private void Close()
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);

        text.text = "";
        currentCoroutine = StartCoroutine(CloseMarkoPolo());
    }

    private IEnumerator OpenMarkoPolo()
    {
        while (canvasGroup.alpha < 1f)
        {
            yield return new WaitForSeconds(0.02f);
            canvasGroup.alpha += 0.04f;
        }

        canvasGroup.blocksRaycasts = true;
        Calculate();
    }

    private IEnumerator CloseMarkoPolo()
    {
        canvasGroup.blocksRaycasts = false;

        while (canvasGroup.alpha > 0)
        {
            yield return new WaitForSeconds(0.02f);
            canvasGroup.alpha -= 0.04f;
        }
    }

    private void Calculate()
    {
        for (int number = 1; number <= 100; number++)
        {
            if (number % 3 == 0 && number % 5 == 0)
            {
                text.text += "MarkoPolo\n";
            }
            else if (number % 3 == 0)
            {
                text.text += "Marko\n";
            }
            else if (number % 5 == 0)
            {
                text.text += "Polo\n";
            }
            else
            {
                text.text += number.ToString() + "\n";
            }
        }
    }
}
