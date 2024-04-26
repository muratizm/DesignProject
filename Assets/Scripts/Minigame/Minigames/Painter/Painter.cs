using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Painter : MiniGame
{
    private bool isPainting = false;
    private float paintedPercentage = 0f;
    public GameObject brickSprite; // Reference to the brick-like sprite object
    private LineRenderer lineRenderer; // Reference to the line renderer for painting



    public override void StartGame(){
        SceneCoordinator.Instance.UnlockCursor();
        slider = gameObject.transform.Find("Slider").GetComponent<Slider>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        GameManager.Instance.IsGamePaused = true;
        isMiniGameRunning = true;

        timer = new Timer();
        timer.OnTimerTick += ManageSlider;
        timer.OnTimerComplete += () => CheckStatus();
        StartTimer();
    }

    void Update(){
        if (!isMiniGameRunning){return;}

        Paint();
    }


    private void Paint()
    {
        // Paint the brick-like sprite with the line renderer
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log("update paint");

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit something");
            if (hit.collider.gameObject == brickSprite)
            {
                Debug.Log("Hit the brick");
                Vector3 point = hit.point;
                point.y = 0.1f; // Adjusting the y-coordinate to ensure painting on the surface
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);

                // Calculate painted percentage
            }

            else{
                Debug.Log("Hit something else");
                Debug.Log(": " + hit.collider.gameObject.name);
            }
        }
    }

    private void CheckStatus()
    {
        Debug.Log("positioncount " + lineRenderer.positionCount);
        Debug.Log("vertexcount " + brickSprite.GetComponent<MeshFilter>().mesh.vertexCount);
        paintedPercentage = (float)lineRenderer.positionCount / (float)brickSprite.GetComponent<MeshFilter>().mesh.vertexCount;
        Debug.Log("Painted Percentage: " + paintedPercentage);

        if (paintedPercentage >= 0.75f)
        {
            ExitGame(true);
        }
        else
        {
            ExitGame(false);
        }
    }

    public override void ExitGame(bool isWon = false){
        base.ExitGame(isWon);
    }





}


