using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get; }

    // spawn del nivel


    //lista de piezas

    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    public List<Piece> pieces = new List<Piece>(); // todas las piezas en el pool

    public Piece GetPiece (PieceType _pt, int visualIndex)
    {
        Piece p = pieces.Find(x => x.type == _pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if (p == null)
        {
            GameObject go = null;
            if (_pt == PieceType.ramp)
            {
                go = ramps[visualIndex].gameObject;
            }
            else if (_pt == PieceType.longBlock)
            {
                go = longblocks[visualIndex].gameObject;
            }
            else if (_pt == PieceType.jump)
            {
                go = jumps[visualIndex].gameObject;
            }
            else if (_pt == PieceType.slide)
            {
                go = slides[visualIndex].gameObject;
            }

            go = Instantiate(go);
            p = go.GetComponent<Piece>();
            pieces.Add(p);
        }

        return p;
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
