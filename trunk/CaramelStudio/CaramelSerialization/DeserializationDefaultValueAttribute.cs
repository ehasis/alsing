using System;

namespace Paieon.Common
{
	/// <summary>
	/// Holds Deserialization Default Value for fields that were added due to a type defenition and
	/// need to have default values even if older versions are deserialized
	/// </summary>
	[AttributeUsage(AttributeTargets.Field, AllowMultiple=true, Inherited=true)]
	public sealed class DeserializationDefaultValueAttribute:Attribute
	{
		private object defaultValue;
		/// <summary>
		/// construct a new DeserializationDefaultValueAttribute with a default field value
		/// </summary>
		/// <param name="defaultValue">the default field value</param>
		public DeserializationDefaultValueAttribute(object defaultValue)
		{
			this.defaultValue = defaultValue;
		}
		
		/// <summary>
		/// Gets the default field value
		/// </summary>
		public object DefaultValue
		{
			get
			{
				return defaultValue;
			}
		}
	}
}
