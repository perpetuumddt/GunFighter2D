using UnityEngine;

namespace Gunfighter.Plugins.PixelPerfectCamera.PixelArtCamera.Example
{
	public class SixtyFPS : MonoBehaviour {
		void Start () {
			Application.targetFrameRate = 60;
		}
	}
}
