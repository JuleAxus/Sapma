using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thesphere : MonoBehaviour
{
	 [ExecuteInEditMode]

	public int cubemapSize = 128;
	public bool oneFacePerFrame = false;
	public Camera cam ;
	public RenderTexture rtex ;
	public Color[] colors;
	public Shader cubeshader;
	void Start()
	{
		// render all six faces at startup
		UpdateCubemap(63);
		Randomize();
	}

	void LateUpdate()
	{
		if (oneFacePerFrame)
		{
			var faceToRender = Time.frameCount % 6;
			var faceMask = 1 << faceToRender;
			UpdateCubemap(faceMask);
		}
		else
		{
			UpdateCubemap(63); // all six faces
		}
	}

	void UpdateCubemap(int faceMask)
	{
		if (!cam)
		{
			var go = new GameObject("CubemapCamera", typeof(Camera));
			go.hideFlags = HideFlags.HideAndDontSave;
			go.transform.position = transform.position;
			go.transform.rotation = Quaternion.identity;
			cam = go.GetComponent<Camera>();
			cam.farClipPlane = 100; // don't render very far into cubemap
			cam.enabled = false;
		}

		if (!rtex)
		{
			rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
			rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
			rtex.hideFlags = HideFlags.HideAndDontSave;

			Material mat = new Material(cubeshader);
			GetComponent<Renderer>().material = mat;
			GetComponent<Renderer>().material.SetTexture("_Cube", rtex);
		}

		cam.transform.position = transform.position;
		cam.RenderToCubemap(rtex, faceMask);
	}

	public void Randomize()
	{
		float i = Random.Range(0f, 1f);

		if (i < 0.5f) gameObject.SetActive(false);



		this.transform.localScale = this.transform.localScale * Random.Range(0.75f, 1.25f);
		GetComponent<Renderer>().sharedMaterial.SetColor("_Color0", colors[Random.Range(0, colors.Length)]);
	}

	void OnDisable()
	{
		DestroyImmediate(cam);
		DestroyImmediate(rtex);
	}
}
