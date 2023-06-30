using UnityEngine;

namespace Plugins.PixelPerfectCamera.PixelArtCamera.Example
{
	public class RandomRotate : MonoBehaviour {

		// Use this for initialization
		void Start () {
		
		}
	
		// Update is called once per frame
		void Update () {
			transform.Rotate(230.43f * Time.deltaTime * Random.value, 150.52f * Time.deltaTime * Random.value, 40.34f * Time.deltaTime * Random.value);	
		}
	}
}
