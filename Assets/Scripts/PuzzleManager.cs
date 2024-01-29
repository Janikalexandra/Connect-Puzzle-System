using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeField] private GameObject fullRaven;
    [SerializeField] private GameObject[] ravenPieces;
    public bool ravenPiece1Connected;
    public bool ravenPiece2Connected;
    public bool ravenPiece3Connected;

    public bool isPuzzleFinished;

    // Update is called once per frame
    void Update()
    {
        if(!isPuzzleFinished && ravenPiece1Connected && ravenPiece2Connected && ravenPiece3Connected)
        {
            isPuzzleFinished = true;
            ShowFullPuzzle();
        }
    }

    public void ShowFullPuzzle()
    {
        fullRaven.SetActive(true);
        ravenPieces[0].SetActive(false);
        ravenPieces[1].SetActive(false);
        ravenPieces[2].SetActive(false);
    }
}
