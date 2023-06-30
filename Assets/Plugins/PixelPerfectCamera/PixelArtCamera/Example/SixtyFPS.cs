using UnityEngine;

namespace Plugins.PixelPerfectCamera.PixelArtCamera.Example
{
	public class SixtyFPS : MonoBehaviour {
		void Start () {
			Application.targetFrameRate = 60;
		}
	}
}
