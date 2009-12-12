using System;
using System.Runtime.Serialization;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Paieon.Common
{
	/// <summary>
	/// The VersionSupportingBinaryFormatter is a binary formatter that supports de-serialization
	/// of types that their type defenition has changed since the serialzation of the instances.
	/// During de-serialization it behaves as follows:
	/// Fields that exist in the new type definition and in the old are de-serialized as usual.
	/// Fields that exist in the old type definition but not in the new are disregarded.
	/// Fields that exist in the new type definition but not in the old are initialized 
	/// with the default CLR value (null for reference types and defaults for value types).
	/// 
	/// Although it would be logical to inherit from BinaryFormatter, unfortunately BinaryFormatter 
	/// is a sealed class so the VersionSupportingBinaryFormatter implements the IFormatter and 
	/// uses a composite BinaryFormatter
	/// </summary>
	public class VersionSupportingBinaryFormatter : IFormatter
	{
		//the composed binary formatter
		private BinaryFormatter formatter;

		/// <summary>
		/// Construct a new VersionSupportingBinaryFormatter instance.
		/// </summary>
		public VersionSupportingBinaryFormatter()
		{
			formatter = new BinaryFormatter();
			SurrogateSelector surrogateSelector = new SurrogateSelector();
			formatter.SurrogateSelector = surrogateSelector;
			VersionSupportingSurrogateConnectingBinder  deserializationSurrogateConnectingBinder
				= new VersionSupportingSurrogateConnectingBinder(surrogateSelector);
			formatter.Binder = deserializationSurrogateConnectingBinder;
		}

		#region IFormatter implementations 
		/// <summary>
		/// Deserializes the data on the provided stream and reconstitutes the graph of objects.
		/// </summary>
		/// <param name="serializationStream">The stream containing the data to deserialize</param>
		/// <returns></returns>
		public object Deserialize(Stream serializationStream)
		{
			return formatter.Deserialize(serializationStream);
		}
		
		/// <summary>
		/// Serializes an object, or graph of objects with the given root to the provided stream.
		/// </summary>
		/// <param name="serializationStream">The stream where the formatter puts the serialized data. 
		/// This stream can reference a variety of backing stores (such as files, network, memory, and so on). </param>
		/// <param name="graph">The object, or root of the object graph, to serialize. All child objects of this root 
		/// object are automatically serialized</param>
		public void Serialize(Stream serializationStream, object graph)
		{
			formatter.Serialize(serializationStream, graph);
		}
		
		/// <summary>
		/// Gets or sets the SerializationBinder that performs type lookups during deserialization.
		/// </summary>
		public SerializationBinder Binder 
		{
			get
			{
				return formatter.Binder; 
			}
			set
			{
				formatter.Binder = value;
			}
		}
		/// <summary>
		/// Gets or sets the StreamingContext used for serialization and deserialization.
		/// </summary>
		public StreamingContext Context 
		{
			get
			{
				return formatter.Context;
			}
			set
			{
				formatter.Context = value;
			}
		}
		/// <summary>
		/// Gets or sets the SurrogateSelector used by the current formatter.
		/// </summary>
		public ISurrogateSelector  SurrogateSelector  
		{
			get
			{
				return formatter.SurrogateSelector ;
			}
			set
			{
				formatter.SurrogateSelector  = value;
			}
		}


		#endregion

	}

	/// <summary>
	/// The VersionSupportingSurrogateConnectingBinder  is a "work around" class to trick a Formatter.
	/// what we realy would like to do is to add the VersionSupportingSerializationSurrogate to a formatter 
	/// surrogateSelector such that it would be called for each and every type that is being Deserialized, however
	/// this is not possible, BUT every time a type is being Deserialized the formatter calls it's SerializationBinder
	/// to get the currect type to use for deserialization, so we simply return the type that we received and "on the way"
	/// we add a new VersionSupportingSerializationSurrogate to the surrogateSelector for the current type... (-;
	/// </summary>
	sealed class VersionSupportingSurrogateConnectingBinder : SerializationBinder 
	{
		private SurrogateSelector surrogateSelector;
		/// <summary>
		/// construct a new VersionSupportingSurrogateConnectingBinder
		/// </summary>
		/// <param name="surrogateSelector">the surrogate Selector that this binder will add surrogates 
		/// for each type this binder is called for</param>
		public VersionSupportingSurrogateConnectingBinder (SurrogateSelector surrogateSelector)
		{
			this.surrogateSelector = surrogateSelector;
		}
		/// <summary>
		/// controls the binding of a serialized object to a type.
		/// </summary>
		/// <param name="assemblyName">Specifies the Assembly name of the serialized object</param>
		/// <param name="typeName">Specifies the Type name of the serialized object.</param>
		/// <returns>The type of the object the formatter creates a new instance of</returns>
		public override Type BindToType(string assemblyName, string typeName) 
		{
			Type typeToDeserialize = null;

			// To return the exact same type as given
			typeToDeserialize = Type.GetType(String.Format("{0}, {1}", 
				typeName, assemblyName));
			//take this opertunity and connect to this type the VersionSupportingSerializationSurrogate
			try
			{
				surrogateSelector.AddSurrogate(typeToDeserialize, new StreamingContext(StreamingContextStates.All), 
					new VersionSupportingSerializationSurrogate());
			}
				//an ArgumentException will be thrown if the surrogate has already been added to the surrogateSelector
				//which is OK by us.
			catch(ArgumentException arg)
			{}
			return typeToDeserialize;
		}
	}

	/// <summary>
	/// The VersionSupportingSerializationSurrogate is surrogate that serializa nad de-serialize 
	/// using reflection on the current loaded type, thus:
	/// Fields that exist in the new type definition and in the old are de-serialized as usual.
	/// Fields that exist in the old type definition but not in the new are disregarded.
	/// Fields that exist in the new type definition but not in the old are initialized 
	/// with the default CLR value (null for reference types and defaults for value types).
	/// </summary>
	sealed class VersionSupportingSerializationSurrogate : ISerializationSurrogate 
	{

		/// <summary>
		/// Populates the provided SerializationInfo with the data needed to serialize the object.
		/// </summary>
		/// <param name="obj">The object to serialize</param>
		/// <param name="info">The SerializationInfo to populate with data</param>
		/// <param name="context">The destination (see StreamingContext) for this serialization.</param>
		public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context) 
		{
			Type t = obj.GetType();
			foreach(FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic ))
			{
				info.AddValue(fieldInfo.Name, fieldInfo.GetValue(obj));
			}
		}

		/// <summary>
		/// Populates the object using the information in the SerializationInfo
		/// </summary>
		/// <param name="obj">The object to populate.</param>
		/// <param name="info">The information to populate the object.</param>
		/// <param name="context">The source from which the object is deserialized.</param>
		/// <param name="selector">The surrogate selector where the search for a compatible surrogate begins.</param>
		/// <returns>The populated deserialized object.</returns>
		public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context,
									ISurrogateSelector selector) 
		{
			Type t = obj.GetType();
			//iterate only the fields Declared in the current type (not the super classes) 
			foreach(FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly 
							|BindingFlags.Static  | BindingFlags.Public | BindingFlags.NonPublic ))
			{
				try
				{
					fieldInfo.SetValue(obj, info.GetValue(fieldInfo.Name, fieldInfo.FieldType));
				}
				catch(SerializationException ex)
				{
					handleSerializationException(ex, fieldInfo, obj, info, t);
				}
			}
			//fill the super classes members
			obj = setBaseClassesMembers(obj, info, obj.GetType());
			return obj;   // Formatters ignore this return value
		}

		// a private recursive helper function for placing base class members of all base classes
		private Object setBaseClassesMembers(Object obj, SerializationInfo info, Type t)
		{
			t = t.BaseType;
			if(t==null || t == typeof(Object))
				return obj;
			//iterate and place values in all private static and non static fields.
			foreach(FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly 
				|BindingFlags.Static  | BindingFlags.Public | BindingFlags.NonPublic ))
			{
				try
				{
					//check if the field is public or not
					if(fieldInfo.Attributes == FieldAttributes.Public)
					{
						fieldInfo.SetValue(obj, info.GetValue(fieldInfo.Name, fieldInfo.FieldType));
					}
					else
					{
						//a "hack" for placing private and protected member values from the serialization info in to our object,
						//attach the class name and with a + in the beginning and...magic.
						fieldInfo.SetValue(obj, info.GetValue(t.Name + "+" + fieldInfo.Name, fieldInfo.FieldType));
					}
				}
				catch(SerializationException ex)
				{
					handleSerializationException(ex, fieldInfo, obj, info, t);
				}
			}
			//Recursively call this function with it's base type
			return setBaseClassesMembers(obj, info, t.BaseType);
		}
		
		//a private helper function to handle serialization exceptions that denotes missing fields, 
		//checks if the DeserializationDefaultValueAttribute or the DeserializationOldNameAttribute
		//were defined over the new field and if so handle the field.
		private void handleSerializationException(SerializationException ex, FieldInfo fieldInfo, Object obj
													, SerializationInfo info, Type t)
		{
			//this is ugly...for some reason they did not defined a MemberNotFoundSerializationException
			//so to make sure this is indeed this exception and not an actual error we need to check the message
			//and compare strings.
			if(!ex.Message.EndsWith("not found."))
				throw ex;
			//check to see if the custume attribute DeserializationDefaultValueAttribute was placed on this new field
			//and if so set it's default value in the field
			Object[] fieldAttributes = fieldInfo.GetCustomAttributes(true);
			foreach(Object attribute in fieldAttributes)
			{
				//see if this is a DeserializationDefaultValueAttribute
				if(attribute.GetType() == typeof(DeserializationDefaultValueAttribute))
				{
					//set the Default Value in the field
					fieldInfo.SetValue(obj, ((DeserializationDefaultValueAttribute)attribute).DefaultValue);
				}
				//see if this is a DeserializationOldNameAttribute
				if(attribute.GetType() == typeof(DeserializationOldNameAttribute))
				{
					object storedValue;
					//get the old name field value
					//check if this is a private field and if so append the class name to access the field
					if(fieldInfo.Attributes == FieldAttributes.Public)
						storedValue =  info.GetValue(((DeserializationOldNameAttribute)attribute).OldName, fieldInfo.FieldType);
					else
						storedValue = info.GetValue(t.Name + "+" + ((DeserializationOldNameAttribute)attribute).OldName
							, fieldInfo.FieldType);
					//set the old field name value to the new field name.
					fieldInfo.SetValue(obj, storedValue);
				}
			}
		}
	
	}


}
