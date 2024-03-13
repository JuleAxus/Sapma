using UnityEngine;

namespace Assets.Scripts.Cam.Effects {
	[ExecuteInEditMode]
	[RequireComponent(typeof(Camera))]
	[AddComponentMenu("Image Effects/Custom/Posterize")]
	public class Posterize : MonoBehaviour {
		private Material m_material;
		public Shader shader;
		public int[] factors;
		public int redComponent = 8;
		public int greenComponent = 8;
		public int blueComponent = 8;



		private Material material {
			get {
				if (m_material == null) {
				
					m_material = new Material(shader) {hideFlags = HideFlags.DontSave};
				}

				return m_material;
			}
		}

		private void Start() {
			
		}

        public void Awake()
        {
			ChangePos(Random.Range(0, factors.Length));
        }

		[ContextMenu("Set Posterize")]
		public void Randomize()
		{
			ChangePos(Random.Range(0, factors.Length));
		}

		public void ChangePos(int f)
        {
			redComponent =  64 /factors[f];
			greenComponent = 64 / factors[f];
			blueComponent = 64/ factors[f];
		}

        public void OnRenderImage(RenderTexture src, RenderTexture dest) {
			if (material) {
				material.SetInt("_Red", redComponent);
				material.SetInt("_Green", greenComponent);
				material.SetInt("_Blue", blueComponent);
				Graphics.Blit(src, dest, material);
			}
		}

		private void OnDisable() {
			if (m_material)
				DestroyImmediate(m_material);
		}
	}
}