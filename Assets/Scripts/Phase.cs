﻿using Managers;
using UIManagers;

public class Phase
{
	private Game Game;

	public int Number { get; set; }

	public Phase(Game game)
	{
		Game = game;
		Number = 0;
	}

	public void Finish()
	{
		Game.Players.ForEach(p => p.Characters.ForEach(c => c.OnPhaseFinish()));
		Number++;
		UIManager.Instance.UpdateActivePhaseText();
	}
}