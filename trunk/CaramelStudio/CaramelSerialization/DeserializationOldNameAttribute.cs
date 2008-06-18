using System;

namespace Paieon.Common
{
	/// <summary>
	/// Holds Deserialization used old name for a field that his name was changed
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true, Inherited=true)]
	public sealed class DeserializationOldNameAttribute:Attribute
	{
		private string oldName;
		/// <summary>
		/// construct a new DeserializationOldNameAttribute containing the old field name
		/// </summary>
		/// <param name="oldName">the old field name</param>
		public DeserializationOldNameAttribute(string oldName)
		{
			this.oldName = oldName;
		}
		
		/// <summary>
		/// Gets the old field name
		/// </summary>
		public string OldName
		{
			get
			{
				return oldName;
			}
		}
	}
}
