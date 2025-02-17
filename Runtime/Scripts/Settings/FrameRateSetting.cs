﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Toolkits
{
	[AddComponentMenu ("Toolkit/Settings/FrameRateSetting")]
	public class FrameRateSetting : MonoBehaviour
	{
		public int targetFrameRate = 60;

		void Awake ()
		{
			Application.targetFrameRate = targetFrameRate;
		}
	}
}
