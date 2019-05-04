using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { set; get; }

    public bool SHOW_COLLIDER = true; //$$

    // spawn del nivel
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int INITIAL_TRANSITION_SEGMENTS = 2;
    private const int MAX_SEGMENTS_ON_SCREEN = 100;
    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;


    //lista de piezas

    public List<Piece> ramps = new List<Piece>();
    public List<Piece> longblocks = new List<Piece>();
    public List<Piece> jumps = new List<Piece>();
    public List<Piece> slides = new List<Piece>();
    [HideInInspector] public List<Piece> pieces = new List<Piece>(); // todas las piezas en el pool

    //lista de segmentos
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();
    [HideInInspector] public List<Segment> segments = new List<Segment>();

    //Gameplay
    private bool isMoving = false;

 

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            if (i < INITIAL_TRANSITION_SEGMENTS)
            {
                SpawnTransition();
            }
            else
            {
                GenerateSegment();
            }
           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
        }

        if (amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].DeSpawn();
            amountOfActiveSegments--;
        }
    }

    private void GenerateSegment()
    {
        SpawnSegment();

        if (Random.Range(0f,1f) < (continousSegments * 0.1f))
        {
            //Spawnear segmento de transicion
            continousSegments = 0;
            SpawnTransition();
        }
        else
        {
            continousSegments++;
        }
        
    }

    private void SpawnSegment()
    {
        List<Segment> posibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, posibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();

    }

    private void SpawnTransition()
    {
        List<Segment> posibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, posibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.lenght;
        amountOfActiveSegments++;
        s.Spawn();
    }
    
    public Segment GetSegment (int _id, bool _transition)
    {
        Segment s = null;
        s = segments.Find(x => x.SegId == _id && x.transition == _transition && !x.gameObject.activeSelf);

        if (s == null)
        {
            GameObject go = Instantiate((_transition) ? availableTransitions[_id].gameObject : availableSegments[_id].gameObject) as GameObject;
            s = go.GetComponent<Segment>();

            s.SegId = _id;
            s.transition = _transition;

            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }

        return s;
    }
    public Piece GetPiece(PieceType _pt, int visualIndex)
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

}
