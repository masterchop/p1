﻿using UnityEngine;
using System.Collections;

namespace P1
{
		public class SliderDragger : MonoBehaviour
		{

				public float moveIncrement;
				private float moveAmountX;
				private float deltaX;
				public float moveLimit = 1;
				private Vector3 origPos;
				private bool isThisHit = false;
				private int sliderInt;

				private	float snappedXint;
				private float snappedXpos;

				public GameObject HandleVisMesh;
				public GameObject HandleVisGRP;

				
			
			void OnMouseDown ()
				{
						isThisHit = true;
						moveAmountX = moveIncrement;
						origPos = Input.mousePosition;
						SliderManager.Instance.SliderBarHandleMesh.renderer.material = SliderManager.Instance.SliderHandleActive;
				}
	
				void Update ()
				{
						if (Input.GetMouseButton (0) && isThisHit == true) {
								deltaX = origPos.x - Input.mousePosition.x;
								moveAmountX = moveIncrement * ((Mathf.Abs (deltaX) * SliderManager.Instance.SliderSpeed));

								if (Input.mousePosition.x > origPos.x && transform.localPosition.x < moveLimit) {
										Debug.Log ("moveAmountX = " + moveAmountX);
										this.transform.Translate (moveAmountX, 0f, 0f);
								}
								if (Input.mousePosition.x < origPos.x && transform.localPosition.x > 0) {
										this.transform.Translate (-moveAmountX, 0f, 0f);
								}
								float sliderValue = SliderManager.Instance.MaxLimit * this.transform.localPosition.x;
								sliderInt = (int)sliderValue;
								SliderManager.Instance.TextSliderValue.text = sliderInt.ToString ();
						}
						origPos = Input.mousePosition;

						Vector3 pos = rigidbody.position;
						pos.x = Mathf.Clamp(pos.x, 0.0f, 1.0f);
						rigidbody.position = pos;

					 	if (rigidbody.position.x < 1.0f) {
							HandleVisGRP.transform.position = rigidbody.position;
						}
		}
		void FixedUpdate(){
					float sliderValue = SliderManager.Instance.MaxLimit * this.transform.localPosition.x;
					sliderInt = (int)sliderValue;
					SliderManager.Instance.TextSliderValue.text = sliderInt.ToString ();

		//					rigidbody.Sleep ();
		//					GetComponent<Collider> ().enabled = false;
				}

				void OnMouseUp ()
				{
						isThisHit = false;
						SliderManager.Instance.SliderBarHandleMesh.renderer.material = SliderManager.Instance.SliderHandle;

						snappedXint = (Mathf.Round (sliderInt / SliderManager.Instance.Interval)) * SliderManager.Instance.Interval;
						Debug.Log ("snappedXint = " + snappedXint);
						snappedXpos = (1f / SliderManager.Instance.MaxLimit) * snappedXint;
						Debug.Log ("snappedXpos = " + snappedXpos);

						this.transform.localPosition = new Vector3 (snappedXpos, this.transform.localPosition.y, this.transform.localPosition.z);
						SliderManager.Instance.TextSliderValue.text = snappedXint.ToString ();

				}
				void OnTriggerEnter(){
					SliderManager.Instance.SliderBarHandleMesh.renderer.material = SliderManager.Instance.SliderHandleActive;
				}
				void OnTriggerExit(){
					SliderManager.Instance.SliderBarHandleMesh.renderer.material = SliderManager.Instance.SliderHandle;
				}
	}
}