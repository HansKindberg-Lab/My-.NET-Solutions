using System;
using System.Globalization;
using System.Linq;

namespace Company.WebApplication.Business.Globalization
{
	public class CustomCulture : CultureInfo
	{
		#region Constructors

		public CustomCulture(string displayName, string englishName, string name, string nativeName, string parentName) : base(name)
		{
			this.DisplayName = displayName;
			this.EnglishName = englishName;
			this.NativeName = nativeName;

			var parent = GetCultures(CultureTypes.AllCultures).FirstOrDefault(culture => culture.Name.Equals(parentName, StringComparison.OrdinalIgnoreCase));

			this.Parent = parent ?? throw new ArgumentException($"The parent-name \"{parentName}\" is invalid. The parent-name must be the name of an existing culture.", nameof(parentName));
		}

		#endregion

		#region Properties

		public override Calendar Calendar => this.Parent.Calendar;
		public override CompareInfo CompareInfo => this.Parent.CompareInfo;

		public override DateTimeFormatInfo DateTimeFormat
		{
			get => this.DateTimeFormatChanged ? base.DateTimeFormat : this.Parent.DateTimeFormat;
			set
			{
				base.DateTimeFormat = value;
				this.DateTimeFormatChanged = true;
			}
		}

		protected internal virtual bool DateTimeFormatChanged { get; set; }
		public override string DisplayName { get; }
		public override string EnglishName { get; }
		public override bool IsNeutralCulture => this.Parent.IsNeutralCulture;
		public override int KeyboardLayoutId => this.Parent.KeyboardLayoutId;
		public override int LCID => this.Parent.LCID;
		public override string NativeName { get; }

		public override NumberFormatInfo NumberFormat
		{
			get => this.NumberFormatChanged ? base.NumberFormat : this.Parent.NumberFormat;
			set
			{
				base.NumberFormat = value;
				this.NumberFormatChanged = true;
			}
		}

		protected internal virtual bool NumberFormatChanged { get; set; }
		public override Calendar[] OptionalCalendars => this.Parent.OptionalCalendars;
		public override CultureInfo Parent { get; }
		public override TextInfo TextInfo => this.Parent.TextInfo;
		public override string ThreeLetterISOLanguageName => this.Parent.ThreeLetterISOLanguageName;
		public override string ThreeLetterWindowsLanguageName => this.Parent.ThreeLetterWindowsLanguageName;
		public override string TwoLetterISOLanguageName => this.Parent.TwoLetterISOLanguageName;

		#endregion

		#region Methods

		public override object GetFormat(Type formatType)
		{
			return this.Parent.GetFormat(formatType);
		}

		#endregion
	}
}