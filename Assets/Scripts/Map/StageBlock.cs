using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageBlock : MonoBehaviour
{
    // 블록 맵에 설치
    public delegate void BlockInstallHandler(ObjectTypeEnums blockType, Vector2 position);
    public event BlockInstallHandler OnBlockInstall;

    // 박스 파괴
    public delegate void BlockDestructionHandler(Vector2 position);
    public event BlockDestructionHandler OnBlockDestruction; 

    public List<GameObject> ObjectList;
    public List<GameObject> BoxList;
    public List<MovableBox> MovableBoxeList;

    private Dictionary<Vector2, GameObject> boxDictionary = new Dictionary<Vector2, GameObject>();

    private void Start() 
    {
        foreach(GameObject obj in ObjectList)
        {
            OnBlockInstall?.Invoke(ObjectTypeEnums.Object, obj.transform.position);
        }    

        foreach(GameObject box in BoxList)
        {
            OnBlockInstall?.Invoke(ObjectTypeEnums.Box, box.transform.position);
            boxDictionary[box.transform.position] = box; // 위치를 키로 하고 박스를 값으로 추가
        }

        foreach(MovableBox box in MovableBoxeList)
        {
            OnBlockInstall?.Invoke(ObjectTypeEnums.Box, box.transform.position);
            boxDictionary[box.transform.position] = box.gameObject;

            box.OnChangePos += (oldPos, newPos) => UpdateBox(box, oldPos, newPos);
        }
    }
    
    public void UpdateBox(MovableBox box, Vector2 oldPos, Vector2 newPos)
    {
        // 기존 위치에서 Dictionary 항목 제거
        if (boxDictionary.ContainsKey(oldPos))
        {
            boxDictionary.Remove(oldPos);
        }

        // 새로운 위치로 Dictionary에 추가
        boxDictionary[newPos] = box.gameObject;
    }

    public void RemoveBox(Vector2 pos)
    {
        if (!boxDictionary.TryGetValue(pos, out GameObject box)) return;
        
        boxDictionary.Remove(pos);
        BoxList.Remove(box);
        OnBlockDestruction?.Invoke(pos);

        // 애니메이션 실행
        Animator animator = box.GetComponent<Animator>();
        animator.Play("Pop");
        if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.99f) return;

        // 박스 파괴
        Destroy(box, 0.2f);
    }
}
