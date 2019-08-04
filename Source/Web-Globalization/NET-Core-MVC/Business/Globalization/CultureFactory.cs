﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Company.WebApplication.Business.Globalization.Configuration;

namespace Company.WebApplication.Business.Globalization
{
	public class CultureFactory : ICultureFactory
	{
		#region Constructors

		public CultureFactory(IDictionary<string, CustomCultureOptions> customCultures)
		{
			this.CustomCultures = customCultures ?? throw new ArgumentNullException(nameof(customCultures));
		}

		#endregion

		#region Properties

		protected internal virtual IDictionary<string, CustomCultureOptions> CustomCultures { get; }

		#endregion

		#region Methods

		public virtual CultureInfo Create(string name)
		{
			return this.Create(name, false);
		}

		public virtual CultureInfo Create(string name, bool readOnly)
		{
			var culture = this.CustomCultures.TryGetValue(name, out var options) ? new CustomCulture(options.BaseName, options.DisplayName, options.EnglishName, name, options.NativeName, this.Create(this.GetParentName(name))) : new CultureInfo(name);

			return readOnly ? CultureInfo.ReadOnly(culture) : culture;
		}

		protected internal virtual string GetParentName(string name)
		{
			const char separator = '-';

			var parts = (name ?? string.Empty).Split(separator).ToArray();

			var take = parts.Length > 0 ? parts.Length - 1 : 0;

			return string.Join(separator, parts.Take(take));
		}

		#endregion
	}
}