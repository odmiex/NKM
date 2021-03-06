﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Hex;
using MyGameObjects.MyGameObject_templates;

namespace MyGameObjects.Abilities.Aqua
{
	public class Purification : Ability
	{
		private const int AbilityRange = 5;

		public Purification()
		{
			Name = "Purification";
			Cooldown = 4;
			CurrentCooldown = 0;
			Type = AbilityType.Normal;
		}
		public override string GetDescription() => $@"{ParentCharacter.Name} rzuca oczyszczający czar na sojusznika, zdejmując z niego wszelkie negatywne efekty.
Zasięg: {AbilityRange} Czas odnowienia: {Cooldown}";

		protected override void CheckIfCanBePrepared()
		{
			base.CheckIfCanBePrepared();
			List<HexCell> cellRange = GetRangeCells();
			cellRange.RemoveNonFriends();
			if (cellRange.Count == 0)
			{
				throw new Exception("Nie ma nikogo w zasięgu umiejętności!");
			}
		}

		public override List<HexCell> GetRangeCells() => ParentCharacter.ParentCell.GetNeighbors(AbilityRange);

		protected override void Use()
		{
			List<HexCell> cellRange = GetRangeCells();
			cellRange.RemoveNonFriends();
			var canUseAbility = Active.Prepare(this, cellRange);
			if (canUseAbility) return;

			MessageLogger.DebugLog("Nie ma nikogo w zasięgu umiejętności!");
			OnFailedUseFinish();
		}
		public override void Use(Character character)
		{
			character.Effects.Where(e => e.Type == EffectType.Negative).ToList().ForEach(e => e.RemoveFromParent());
			OnUseFinish();
		}
	}
}
