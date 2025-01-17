﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class PID {
	public Parameters Gain;

	private float Error;
	private float Integrator;
	private float Differentiator;
	private float LastError;

	public PID(float P, float I, float D) {
		Gain = new Parameters(P, I, D);
	}

	public PID(Parameters gain) {
		Gain = gain;
	}

	public float Solve(float current, float target, float delta) {
		Error = target-current;
		
		Integrator += Error*delta;

		Differentiator = (Error-LastError)/delta;

		LastError = Error;

		return current + Error*Gain.P + Integrator*Gain.I + Differentiator*Gain.D;
	}

	public void Reset(float error = 0f, float integrator = 0f, float differentiator = 0f, float lastError = 0f) {
		Error = error;
		Integrator = integrator;
		Differentiator = differentiator;
		LastError = lastError;
	}

	[System.Serializable]
	public class Parameters {
		public float P;
		public float I;
		public float D;
		public Parameters(float p, float i, float d) {
			P = p;
			I = i;
			D = d;
		}
		#if UNITY_EDITOR
		public void Inspector(string name) {
			float width = EditorGUIUtility.currentViewWidth;
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(name, GUILayout.Width(0.4f*width));
			EditorGUILayout.LabelField("P", GUILayout.Width(25f));
			P = EditorGUILayout.FloatField(P, GUILayout.Width(0.2f*width-0.6f*75f));
			EditorGUILayout.LabelField("I", GUILayout.Width(25f));
			I = EditorGUILayout.FloatField(I, GUILayout.Width(0.2f*width-0.6f*75f));
			EditorGUILayout.LabelField("D", GUILayout.Width(25f));
			D = EditorGUILayout.FloatField(D, GUILayout.Width(0.2f*width-0.6f*75f));
			EditorGUILayout.EndHorizontal();
		}
		#endif
	}
}