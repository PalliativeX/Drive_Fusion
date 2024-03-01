using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Localization
{
	[Serializable]
	public struct LocaleId : IEquatable<LocaleId>
	{
		[SerializeField] private string _value;

		public static LocaleId None => new LocaleId("");
		
		public string Value => _value;
		
		public LocaleId(string value) => _value = value;

		public bool Equals(LocaleId other) => _value == other._value;

		public override bool Equals(object obj) => obj is LocaleId id && Equals(id);

		public override int GetHashCode() => _value.GetHashCode();

		public static explicit operator LocaleId(string value) => new LocaleId(value);

		public static implicit operator string(LocaleId id) => id._value;

		public static bool operator ==(LocaleId a, LocaleId b) => a._value == b._value;

		public static bool operator !=(LocaleId a, LocaleId b) => a._value != b._value;

		public override string ToString() => _value;
		
		private IEnumerable GetAllItemOptions()
		{
			var locales = Resources.Load<LocaleStorage>("Configs/LocaleStorage");

			List<string> localeKeys = new List<string>();
			foreach (var (key, locale) in locales.Locales) 
				localeKeys.Add(key);

			return localeKeys;
		}
	}
}