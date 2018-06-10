﻿using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace SongLoaderPlugin
{
	public static class NoteHitVolumeChanger
	{
		public static bool PrefabFound { get; private set; }
		private static NoteCutSoundEffect _noteCutSoundEffect;
		private static float _normalVolume;
		private static float _normalMissVolume;

		//Code snippet comes from Taz's NoteHitVolume plugin:
		//https://github.com/taz030485/NoteHitVolume/blob/master/NoteHitVolume/NoteHitVolume.cs
		public static void SetVolume(float hitVolume, float missVolume)
		{
			hitVolume = Mathf.Clamp01(hitVolume);
			missVolume = Mathf.Clamp01(missVolume);
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var pooled = false;
			if (_noteCutSoundEffect == null)
			{
				var noteCutSoundEffectManager = Resources.FindObjectsOfTypeAll<NoteCutSoundEffectManager>().FirstOrDefault();
				_noteCutSoundEffect =
					ReflectionUtil.GetPrivateField<NoteCutSoundEffect>(noteCutSoundEffectManager, "_noteCutSoundEffectPrefab");
				pooled = true;
				PrefabFound = true;
			}

			if (_normalVolume == 0)
			{
				_normalVolume = ReflectionUtil.GetPrivateField<float>(_noteCutSoundEffect, "_goodCutVolume");
				_normalMissVolume = ReflectionUtil.GetPrivateField<float>(_noteCutSoundEffect, "_badCutVolume");
			}

			var newGoodVolume = _normalVolume * hitVolume;
			var newBadVolume = _normalMissVolume * missVolume;
			ReflectionUtil.SetPrivateField(_noteCutSoundEffect, "_goodCutVolume", newGoodVolume);
			ReflectionUtil.SetPrivateField(_noteCutSoundEffect, "_badCutVolume", newBadVolume);

			if (pooled)
			{
				var pool = Resources.FindObjectsOfTypeAll<NoteCutSoundEffect>();
				foreach (var effect in pool)
				{
					if (effect.name.Contains("Clone"))
					{
						ReflectionUtil.SetPrivateField(effect, "_goodCutVolume", newGoodVolume);
						ReflectionUtil.SetPrivateField(_noteCutSoundEffect, "_badCutVolume", newBadVolume);
					}
				}
			}
			
			stopwatch.Stop();
			Console.WriteLine("SetVolume took " + stopwatch.ElapsedMilliseconds + " milliseconds");
		}
	}
}