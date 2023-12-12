using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrabPickups : MonoBehaviour {

	private AudioSource pickupSoundSource;

	void Awake() {
		pickupSoundSource = DontDestroy.instance.GetComponents<AudioSource>()[1];
	}

    private void Update()
    {
		var position = transform.position;
		if (position.y < -5)
        {
			LevelGenerator.Level = 0;
			GameObject.Find("WhisperSource").GetComponent<AudioSource>().Stop();
			SceneManager.LoadScene("GameOver");
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.gameObject.tag == "Pickup") {
			LevelGenerator.Level += 1;
			pickupSoundSource.Play();
			SceneManager.LoadScene("Play");
		}
	}
}
