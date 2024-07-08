using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineController : MonoBehaviour
{
    public GameObject[] candyPrefabs; // Префабы конфет
    public Transform[] reels; // Массив трансформов для каждого барабана (4 в данном случае)
    public float spinDuration = 2.0f; // Продолжительность вращения
    public int rows = 3; // Количество строк (вертикальных слотов)
    public float candyHeight = 1.0f; // Высота одной конфеты

    [SerializeField] private Button spinButton;

    private bool isSpinning = false;

    private void Start()
    {
        SetupReels();
        SetupButton();
    }

    private void SetupReels()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            for (int j = 0; j < rows + 1; j++) // rows + 1, чтобы был видимый зазор
            {
                int randomIndex = Random.Range(0, candyPrefabs.Length);
                GameObject candy = Instantiate(candyPrefabs[randomIndex], reels[i]);
                candy.transform.localPosition = Vector3.down * j * candyHeight;
            }
        }
    }

    private void SetupButton()
    {
        if (spinButton != null)
        {
            spinButton.onClick.AddListener(Spin);
        }
        else
        {
            Debug.LogError("Spin Button is not assigned in the inspector.");
        }
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            StartCoroutine(SpinReels());
        }
    }

    private IEnumerator SpinReels()
    {
        isSpinning = true;
        float elapsedTime = 0f;
        Vector3[] initialPositions = new Vector3[reels.Length];

        for (int i = 0; i < reels.Length; i++)
        {
            initialPositions[i] = reels[i].localPosition;
        }

        while (elapsedTime < spinDuration)
        {
            for (int i = 0; i < reels.Length; i++)
            {
                reels[i].localPosition = initialPositions[i] + Vector3.down * (elapsedTime / spinDuration) * rows * candyHeight;
                if (reels[i].localPosition.y <= -rows * candyHeight)
                {
                    reels[i].localPosition += Vector3.up * (rows + 1) * candyHeight;
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Останавливаем ровно посередине
        for (int i = 0; i < reels.Length; i++)
        {
            float offset = reels[i].localPosition.y % candyHeight;
            if (offset != 0)
            {
                reels[i].localPosition += Vector3.down * offset;
            }
        }

        isSpinning = false;
    }
}
