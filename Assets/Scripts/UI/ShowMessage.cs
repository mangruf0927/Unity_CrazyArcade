using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessage : MonoBehaviour
{
    [Header("STAGE START 이미지")]
    public GameObject[] stageGameObjectArray;
    public GameObject[] startGameObjectArray;

    [Header("STAGE CLEAR 이미지")]
    public GameObject[] endStageGameObjectArray;
    public GameObject[] clearGameObjectArray;

    [Header("LOSE 이미지")]
    public GameObject[] loseGameObjectArray;
    public Image[] loseImageArray;
    public Sprite[] blinkSpriteArray;
    private Sprite[] originalSprite;

    private void Awake()
    {
        originalSprite = new Sprite[loseImageArray.Length];

        for (int i = 0; i < loseImageArray.Length; i++)
        {
            originalSprite[i] = loseImageArray[i].sprite;
        }
    }
    
    
    public IEnumerator ShowStartMessage()
    {
        for(int i = 0; i < stageGameObjectArray.Length; i++)
        {
            stageGameObjectArray[i].SetActive(true);

            Vector2 targetPosition = new Vector2(stageGameObjectArray[i].transform.position.x, stageGameObjectArray[i].transform.position.y - 95f);
            StartCoroutine(MoveImage(stageGameObjectArray[i], targetPosition, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i < startGameObjectArray.Length; i++)
        {
            startGameObjectArray[i].SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < startGameObjectArray.Length; i++)
        {
            Vector2 stageTargetPosition = new Vector2(stageGameObjectArray[i].transform.position.x, stageGameObjectArray[i].transform.position.y + 195f);
            Vector2 startTargetPosition = new Vector2(startGameObjectArray[i].transform.position.x, startGameObjectArray[i].transform.position.y - 200f);

            StartCoroutine(MoveImage(stageGameObjectArray[i], stageTargetPosition, 0.3f, false));
            StartCoroutine(MoveImage(startGameObjectArray[i], startTargetPosition, 0.3f, false));

            yield return new WaitForSeconds(0.3f);
        }

    }

    public IEnumerator ShowClearMessage()
    {
        for(int i = 0; i < endStageGameObjectArray.Length; i++)
        {
            endStageGameObjectArray[i].SetActive(true);

            Vector2 targetPosition = new Vector2(endStageGameObjectArray[i].transform.position.x, endStageGameObjectArray[i].transform.position.y - 95f);
            StartCoroutine(MoveImage(endStageGameObjectArray[i], targetPosition, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i < clearGameObjectArray.Length; i++)
        {
            clearGameObjectArray[i].SetActive(true);
        }
    }

    public IEnumerator ShowLoseMessage()
    {
        for(int i = 0; i < loseImageArray.Length; i++)
        {
            loseGameObjectArray[i].SetActive(true);

            Vector2 targetPosition = new Vector2(loseImageArray[i].transform.position.x, loseImageArray[i].transform.position.y - 95f);
            StartCoroutine(MoveImage(loseGameObjectArray[i], targetPosition, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(BlinkImage());
    }

    IEnumerator BlinkImage()
    {
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < loseImageArray.Length; j++)
            {
                // 빈 스프라이트로 변경 (혹은 null로 설정하여 깜빡임 효과)
                loseImageArray[j].sprite = blinkSpriteArray[j];
            }

            yield return new WaitForSeconds(0.05f);

            for(int j = 0; j < loseImageArray.Length; j++)
            {
                // 원본 이미지로 복구
                loseImageArray[j].sprite = originalSprite[j];
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator MoveImage(GameObject image, Vector2 targetPos, float duration, bool isActive = true)
    {
        Vector2 startPosition = image.transform.position;

        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            image.transform.position = Vector2.Lerp(startPosition, targetPos, elapsedTime/duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        image.transform.position = targetPos;

        image.SetActive(isActive);
    }
}
