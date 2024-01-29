using System.Collections;
using UnityEngine;

public class PuzzleInteractable : MonoBehaviour
{
    [Header("Puzzle Piece Settings")]
    [SerializeField] private Transform snapPoint;
    public bool isDragging;

    [Header("Drag & Drop Input Actions")]
    PuzzleActions puzzleActions;
    //[SerializeField] private InputAction press;
    //[SerializeField] private InputAction screenPos;

    [Header("Camera")]
    [SerializeField] Camera cam;   
    private Vector3 curScreenPos;

    [Header("References")]
    [SerializeField] private PuzzleManager puzzleManager;

    // Read only - Convert mouse screen position to world position
    private Vector3 WorldPos
    {
        get
        {
            // Takes only the Z value of the mouses world position and stores it in float z
            float z = cam.WorldToScreenPoint(transform.position).z;
            
            // Adds the mouse world position z value to curScreenPos position z & then we return that value
            return cam.ScreenToWorldPoint(curScreenPos + new Vector3(0,0,z));
        }
    }

    // Read only boolean to check if THIS object is clicked
    private bool isClickedOn
    {
        get
        {   // Do a raycast from current mouse position
            Ray ray = cam.ScreenPointToRay(curScreenPos);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {   // Return true if we hit THIS object
                return hit.transform == transform;
            }
            // If we didn't hit anything
            return false;
        }
    }

    private void Awake()
    {
        puzzleManager = GetComponentInParent<PuzzleManager>();
        // Enable Input Actions in THIS order
        puzzleActions = new PuzzleActions();
        puzzleActions.ConnectPuzzle.Enable();
        //screenPos.Enable();
        //press.Enable();

        // When mouse is moving we store the Vector2 value to curScreenPos
        puzzleActions.ConnectPuzzle.ScreenPos.performed += context => 
        { curScreenPos = context.ReadValue<Vector2>(); };

        // When press is performed we start the coroutine IF isClickedOn is TRUE
        puzzleActions.ConnectPuzzle.Press.performed += _ => 
        { if(isClickedOn) StartCoroutine(Drag()); };

        // Checking distance IF you have moved an object and then setting it to false
        puzzleActions.ConnectPuzzle.Press.canceled += _ => 
        { if(isDragging) CheckDistance(); isDragging = false; };

    }

    private IEnumerator Drag()
    {
        isDragging = true;

        // Creating offset so the object doesn't snap to where we click
        Vector3 offset = transform.position - WorldPos;

        //Grab
        while(isDragging)
        {
            //Drag
            transform.position = WorldPos + offset;
            yield return null;
        }
        //Drop
    }

    public void CheckDistance()
    {
        float distance = Vector3.Distance(snapPoint.transform.position, transform.position);
        if(distance < 0.2f)
        {
            CheckForSnapPoint(true);
            // Move THIS game object to its unique snap point position
            this.gameObject.transform.position = snapPoint.transform.position;
        }
        else
        {
            CheckForSnapPoint(false);
        }
        Debug.Log(distance);
    }

    public void CheckForSnapPoint(bool isConnected)
    {
        if (snapPoint.name == "Snap Point 1")
        {
            puzzleManager.ravenPiece1Connected = isConnected;
            //gameObject.GetComponentInParent<RavenPuzzle>().SetPieceTrue(1);
        }
        else if (snapPoint.name == "Snap Point 2")
        {
            puzzleManager.ravenPiece2Connected = isConnected;
            //gameObject.GetComponentInParent<RavenPuzzle>().SetPieceTrue(2);
        }
        else if (snapPoint.name == "Snap Point 3")
        {
            puzzleManager.ravenPiece3Connected = isConnected;
            //gameObject.GetComponentInParent<RavenPuzzle>().SetPieceTrue(2);
        }
    }
}
