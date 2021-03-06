﻿using MyGameObjects.MyGameObject_templates;

namespace MyGameObjects.Effects
{
	// ReSharper disable once InconsistentNaming
	public class HPDrain : Effect
	{
		private readonly Character _characterThatAttacks;
		private readonly Damage _damagePerTick;

		public HPDrain(Character characterThatAttacks, Damage damagePerTick, int cooldown, Character parentCharacter, string name = null) : base(cooldown, parentCharacter, name)
		{
			Name = name ?? "HP Drain";
			_damagePerTick = damagePerTick;
			_characterThatAttacks = characterThatAttacks;
			Type = EffectType.Negative;
			Character.VoidDelegate tryToActivateEffect = () =>
			{
				_characterThatAttacks.Attack(ParentCharacter, _damagePerTick);
				if (ParentCharacter.IsAlive)
					ParentCharacter.Heal(_characterThatAttacks, _damagePerTick.Value);
			};
			ParentCharacter.JustBeforeFirstAction += tryToActivateEffect;
			OnRemove += () => ParentCharacter.JustBeforeFirstAction -= tryToActivateEffect;
		}
		public override string GetDescription()
		{
			return string.Format(
@"Zadaje {0} obrażeń co fazę, oraz leczy za tą samą ilość bohatera, który nałożył ten efekt (<b>{1}</b>).
Czas do zakończenia efektu: {2}",
						 _damagePerTick, _characterThatAttacks.Name, CurrentCooldown);
		}
	}
}
