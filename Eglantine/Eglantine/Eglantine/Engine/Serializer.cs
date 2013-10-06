using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Eglantine
{
	public static class Serializer
	{
		public static void Serialize<T> (string fileName, T objectToSerialize)
		{
			using(FileStream filestream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
			{
				try 
				{
					BinaryFormatter binFormatter = new BinaryFormatter();
					binFormatter.Serialize (filestream, objectToSerialize);
				}
				catch(Exception e)
				{
					Console.WriteLine ("Serialization error!\n" + e.ToString ());
				}
			}
		}

		public static T Deserialize<T> (string fileName)
		{
			using (FileStream filestream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
			{
				try 
				{
					BinaryFormatter binFormatter = new BinaryFormatter();
					return (T)binFormatter.Deserialize (filestream);
				}
				catch(Exception e)
				{
					Console.WriteLine ("Deserialization error!\n" + e.ToString ());
				}
				return default(T);
			}
		}
	}
}

