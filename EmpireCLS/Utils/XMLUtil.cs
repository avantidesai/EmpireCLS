using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Linq;

using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Text.RegularExpressions;


namespace EmpireCLS
{
	public sealed partial class XMLUtil
	{
		private XMLUtil ()
		{
		}

		private static XmlSerializerNamespaces _plainNamespaces = null;

		/// <summary>
		/// Gets an XML a blank namespace so that none are serialized, to the serialized XML
		/// </summary>
		/// <returns></returns>
		private static XmlSerializerNamespaces GetPlainNamespaces ()
		{
			lock (typeof(XMLUtil)) {
				if (_plainNamespaces == null) {
					_plainNamespaces = new XmlSerializerNamespaces ();
					_plainNamespaces.Add (string.Empty, string.Empty);
				}
				return _plainNamespaces;
			}
		}

		public static string SerializePlainXMLToString (object objectToSerialize)
		{
			XmlSerializer ser = XMLUtil.GetSerializer (objectToSerialize.GetType ());
			StringWriter swr = new StringWriter ();
			ser.Serialize (swr, objectToSerialize, GetPlainNamespaces ());
			return swr.ToString ();
		}


		/// <summary>
		/// Additional serializer that accepts additional types to be serialized.
		/// Especially useful in inheritance scenarios
		/// </summary>
		/// <param name="objectToSerialize"></param>
		/// <param name="extraTypes"></param>
		/// <returns></returns>
		public static string SerializePlainXMLToString (object objectToSerialize, Type[] extraTypes)
		{
			XmlSerializer ser = XMLUtil.GetSerializer (objectToSerialize.GetType (), null, extraTypes);
			using (StringWriter swr = new StringWriter ()) {
				ser.Serialize (swr, objectToSerialize, GetPlainNamespaces ());
				return swr.ToString ();
			}
		}

		public static object DeserializePlainXMLToObject (Stream PlainXML, Type TypeToDeserializeTo)
		{
			XmlSerializer ser = XMLUtil.GetSerializer (TypeToDeserializeTo);
			object retObj = ser.Deserialize (PlainXML);
			return retObj;
		}

		public static object DeserializePlainXMLToObject (string PlainXML, Type TypeToDeserializeTo)
		{
			using (System.IO.StringReader sr = new System.IO.StringReader (PlainXML)) {
				XmlSerializer ser = new XmlSerializer (TypeToDeserializeTo);
				object retObj = ser.Deserialize (sr);
				return retObj;
			}
		}

		public static object DeserializePlainXMLToObject (string PlainXML, Type TypeToDeserializeTo, Type[] extraTypes)
		{
			using (System.IO.StringReader sr = new System.IO.StringReader (PlainXML)) {
				XmlSerializer ser = XMLUtil.GetSerializer (TypeToDeserializeTo, null, extraTypes);
				object retObj = ser.Deserialize (sr);
				return retObj;
			}

		}

		#if PCL
		

#else
		public static T Deserialize<T> (string filePath) where T : class
		{
			using (TextReader rdr = new StreamReader (filePath)) {
				XmlSerializer ser = XMLUtil.GetSerializer (typeof(T));			
				return ser.Deserialize (rdr) as T;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectToSerialize"></param>
		/// <param name="filePath"></param>
		public static void SerializeToFile (object objectToSerialize, string filePath)
		{
			using (TextWriter wtr = new StreamWriter (filePath, false)) {
				XmlSerializer ser = XMLUtil.GetSerializer (objectToSerialize.GetType ());
				ser.Serialize (wtr, objectToSerialize);
			}
		}

		#endif

		/// <summary>
		/// deserialize an element into a type instance
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <param name="rootAttribute"></param>
		/// <returns></returns>
		/// <remarks>
		/// 6/14/12 JMO, added rootAttribute to handle the root element being named differently than the type name
		/// </remarks>
		public static T Deserialize<T> (XElement xml, XmlRootAttribute rootAttribute = null) where T : class
		{
			#if MONO
			using(XmlReader rdr = xml.CreateReader())
			#else
			using (XmlReader rdr = xml.CreateReader (ReaderOptions.None))
			#endif
			{
				return Deserialize<T> (rdr, rootAttribute);
			}
		}

		/// <summary>
		/// deserialize an element into a type instance
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <param name="rootAttribute"></param>
		/// <returns></returns>
		/// <remarks>
		/// 6/14/12 JMO, added rootAttribute to handle the root element being named differently than the type name
		/// </remarks>
		public static T Deserialize<T> (XmlReader rdr, XmlRootAttribute rootAttribute = null) where T : class
		{
			XmlSerializer ser = XMLUtil.GetSerializer (typeof(T), rootAttribute);
			return ser.Deserialize (rdr) as T;
		}

		/// <summary>
		/// singleton collection of serializers
		/// </summary>
		#if PCL
		private static readonly Dictionary<string, XmlSerializer> _serializers = 
		new Dictionary<string, XmlSerializer>();
		#else
		private static readonly System.Collections.Concurrent.ConcurrentDictionary<string, XmlSerializer> _serializers = 
			new System.Collections.Concurrent.ConcurrentDictionary<string, XmlSerializer> ();
		#endif

		/// <summary>
		/// single thread safe collection of all serializers.  cuts down on the dynamic assemly creation at runtime.
		/// </summary>
		/// <param name="typeToSerialize"></param>
		/// <param name="rootAttribute"></param>
		/// <returns></returns>
		private static XmlSerializer GetSerializer (Type typeToSerialize, XmlRootAttribute rootAttribute = null, params Type[] extraTypes)
		{
			string key = string.Format ("{0}:{1}:{2}", typeToSerialize.FullName, rootAttribute != null ? rootAttribute.ElementName : "", string.Join (",", (from e in extraTypes
			                                                                                                                                                select e.FullName).ToArray ()));

			Func<XmlSerializer> strategy = () => {
				return rootAttribute == null
					? extraTypes == null ? new XmlSerializer (typeToSerialize) : new XmlSerializer (typeToSerialize, extraTypes)
						: new XmlSerializer (typeToSerialize, rootAttribute);
			};

			#if PCL
			lock (_serializers)
			{
			if (!_serializers.ContainsKey(key))
			_serializers.Add(key, strategy());
			return _serializers[key];
			}
			#else
			return _serializers.GetOrAdd (key, (k) => strategy ());
			#endif
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <param name="extraTypes"></param>
		/// <returns></returns>
		public static T Deserialize<T> (XElement xml, Type[] extraTypes) where T : class
		{
			#if MONO
			using(XmlReader rdr = xml.CreateReader())
			#else
			using (XmlReader rdr = xml.CreateReader (ReaderOptions.None))
			#endif
			{
				XmlSerializer ser = XMLUtil.GetSerializer (typeof(T), null, extraTypes);
				return ser.Deserialize (rdr) as T;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="xml"></param>
		/// <param name="extraTypes"></param>
		/// <returns></returns>
		public static T Deserialize<T> (Stream stm, Type[] extraTypes) where T : class
		{
			using (XmlReader rdr = XmlReader.Create (stm)) {
				// 1/24/2013 JMO, changed so that the "params" arg is not null when this function is called with null
				XmlSerializer ser = extraTypes == null ? XMLUtil.GetSerializer (typeof(T), null) : XMLUtil.GetSerializer (typeof(T), null, extraTypes);
				return ser.Deserialize (rdr) as T;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="objectToSerialize"></param>
		/// <param name="rootAttribute"></param>
		/// <returns></returns>
		public static XElement SerializeToXElement (object objectToSerialize, XmlRootAttribute rootAttribute = null)
		{
			XDocument doc = new XDocument ();

			using (XmlWriter wtr = doc.CreateWriter ()) {
				XmlSerializer ser = XMLUtil.GetSerializer (objectToSerialize.GetType (), rootAttribute);
				ser.Serialize (wtr, objectToSerialize);
			}

			return doc.Descendants ().First ();
		}

		/// <summary>
		/// serialize an object into an existing writer
		/// </summary>
		/// <param name="objectToSerialize"></param>
		/// <param name="writer"></param>
		/// <param name="rootAttribute"></param>
		public static void SerializeToWriter (object objectToSerialize, XmlWriter writer, XmlRootAttribute rootAttribute = null)
		{
			XmlSerializer ser = XMLUtil.GetSerializer (objectToSerialize.GetType (), rootAttribute);
			ser.Serialize (writer, objectToSerialize);
		}

		public static string RemoveXMLTags (string xmlString)
		{
			Regex regex = new Regex ("<[/a-zA-Z:]*>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
			return regex.Replace (xmlString, string.Empty);
		}
	}
}
