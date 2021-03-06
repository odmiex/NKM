﻿using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
	public class MainMenu : MonoBehaviour
	{
		[UsedImplicitly]
		public void PlayButtonClick() => SceneManager.LoadScene(Scenes.PreGameOptions);
		[UsedImplicitly]
		public void OptionsButtonClick(){}//TODO: create options
		[UsedImplicitly]
		public void ExitButtonClick() => Application.Quit();
	}
}