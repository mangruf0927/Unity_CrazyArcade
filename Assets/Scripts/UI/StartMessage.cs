using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    public GameObject[] stageImageArray;
    public GameObject[] startImageArray;


    

    private void Start()
    {
        StartCoroutine(ShowStartMessage());
    }

    IEnumerator ShowStartMessage()
    {
        for(int i = 0; i < stageImageArray.Length; i++)
        {
            stageImageArray[i].SetActive(true);

            Vector2 targetPosition = new Vector2(stageImageArray[i].transform.position.x, stageImageArray[i].transform.position.y - 95f);
            StartCoroutine(MoveImage(stageImageArray[i], targetPosition, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        for(int i = 0; i < startImageArray.Length; i++)
        {
            startImageArray[i].SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < startImageArray.Length; i++)
        {
            Vector2 stageTargetPosition = new Vector2(stageImageArray[i].transform.position.x, stageImageArray[i].transform.position.y + 195f);
            Vector2 startTargetPosition = new Vector2(startImageArray[i].transform.position.x, startImageArray[i].transform.position.y - 200f);

            StartCoroutine(MoveImage(stageImageArray[i], stageTargetPosition, 0.3f, false));
            StartCoroutine(MoveImage(startImageArray[i], startTargetPosition, 0.3f, false));

            yield return new WaitForSeconds(0.3f);
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
