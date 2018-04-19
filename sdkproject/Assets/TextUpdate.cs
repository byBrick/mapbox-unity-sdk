using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Events;

public class TextUpdate : MonoBehaviour {

	[Serializable]
	public class TextEvent : UnityEvent<string>
	{

	}

	[SerializeField]
	private TextEvent _textEvent;

	public void ValueUpdate(float value)
	{
		_textEvent.Invoke(value.ToString(CultureInfo.InvariantCulture));
	}
}
