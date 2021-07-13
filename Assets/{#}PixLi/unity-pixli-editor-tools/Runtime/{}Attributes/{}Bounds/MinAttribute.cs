using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PixLi
{
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public class MinAttribute : MultiSupportPropertyAttribute
	{
		private readonly float _floatMin;
		private readonly int _intMin;

		private bool _floatValue;

		public MinAttribute(float value)
		{
			this._floatMin = value;

			this._floatValue = true;
		}

		public MinAttribute(float value, bool drawMain) : base(drawMain)
		{
			this._floatMin = value;

			this._floatValue = true;
		}

		public MinAttribute(int value)
		{
			this._intMin = value;

			this._floatValue = false;
		}

		public MinAttribute(int value, bool drawMain) : base(drawMain)
		{
			this._intMin = value;

			this._floatValue = false;
		}

#if UNITY_EDITOR
		public override void EndDrawInInspector(Rect rect, SerializedProperty serializedProperty, GUIContent label)
		{
			if (this._floatValue)
				serializedProperty.floatValue = Mathf.Clamp(serializedProperty.floatValue, this._floatMin, float.MaxValue);
			else
				serializedProperty.intValue = Mathf.Clamp(serializedProperty.intValue, this._intMin, int.MaxValue);
		}
#endif
	}

#if UNITY_EDITOR
	[CustomPropertyDrawer(typeof(MinAttribute))]
	[CanEditMultipleObjects]
	public class MinAttributeDrawer : MultiSupportPropertyAttributeDrawer
	{
	}
#endif
}