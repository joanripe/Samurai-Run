using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{

    public PieceType type;
    private Piece currentPiece;
    
    public void Spawn()
    {
        int amtObj = 0;
        switch (type)
        {
            case PieceType.ramp:
                amtObj = LevelManager.Instance.ramps.Count;
                break;
            case PieceType.longBlock:
                amtObj = LevelManager.Instance.longblocks.Count;
                break;
            case PieceType.jump:
                amtObj = LevelManager.Instance.jumps.Count;
                break;
            case PieceType.slide:
                amtObj = LevelManager.Instance.slides.Count;
                break;
            default:
                break;
        }

        currentPiece = LevelManager.Instance.GetPiece(type, Random.Range(0, amtObj)); //obtener una nueva pieza del pool
        currentPiece.gameObject.SetActive(true);
        currentPiece.transform.SetParent(transform, false);
    }

    public void DeSpawn()
    {
        currentPiece.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
