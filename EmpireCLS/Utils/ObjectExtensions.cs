using System;
using System.Reflection;
using System.Linq.Expressions;

namespace EmpireCLS
{

	public static class ObjectExtensions
	{
		public static bool IsEmpty (this object obj)
		{
			// start by checking for null

			if (obj == null)
				return true;

			bool results = false;

			switch (obj.GetType ().ToString ().ToLower ()) {
			// check for empty string
			case "system.string":
				return ((string)obj).Trim () == "";
			}


			return results;
		}

		public static bool IsEmptyOrDummy (this object obj)
		{
			// start by checking for null

			if (obj == null)
				return true;

			bool results = false;

			switch (obj.GetType ().ToString ().ToLower ()) {
			// check for empty string
			case "system.string":
				{
					if (((string)obj).Trim () == "" || ((string)obj).Trim ().ToUpper () == GlobalVars.DummyString)
						return true;
					else
						return false;
					
				}

			case "system.datetime":
				{
					DateTime objDateTime = (DateTime)obj;
					if (objDateTime == GlobalVars.DummyDateTime)
						return true;
					break;
				}
			}


			return results;
		}

		public static string SetDummyData (this string obj)
		{
			if (obj.IsEmpty ())
				return GlobalVars.DummyString;
			else
				return obj;

		}

		public static string ClearDummyData (this string obj)
		{
			if (obj.IsEmptyOrDummy ())
				return "";
			else
				return obj;

		}

		public static DateTime? SetDummyData (this DateTime? obj)
		{
			if (obj.IsEmpty ())
				return GlobalVars.DummyDateTime;
			else
				return obj;
		}

       
		/// <summary>
		/// This extension method is used to find the starting point of an integer in a string.  This is 
		/// useful when multiple strings are used for the same purpose, but have different ending characters to indicate
		/// placement.  For example: SpecialBilling1, SpecialBilling2, .... SpecialBilling20
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static int NumericsBegin (this string obj)
		{ 
			string objValue = obj.Trim ();
			int curPos = 0;
			int result;
			foreach (char x in objValue.ToCharArray()) {
				if (int.TryParse (x.ToString (), out result)) {
					return curPos;
				}
				curPos++;
			}

			return -1;  //return -1 if not found
		}
		/*public static void SetDummyData3<T, TProperty>(this T arg, Expression<Func<T, TProperty>> propertySelector)
		{
	//		TProperty value = GlobalVars.DummyString;

			TProperty currentValue = propertySelector.Compile()(arg);
			//EqualityComparer<TProperty> comparer = EqualityComparer<TProperty>.Default;
			//if (!comparer.Equals(currentValue, default(TProperty)))
			//	return;
			if (currentValue.IsEmpty ()) {
				PropertyInfo property = (PropertyInfo)((MemberExpression)propertySelector.Body).Member;
				property.SetValue (arg, GlobalVars.DummyString);
			}
		} 

		public static void SetDummyData2(this object obj, string fieldName)
		{

			//string fieldName = obj.GetType().ToString();
			FieldInfo fieldInfo = obj.GetType().GetField(fieldName, 
			                                             BindingFlags.Public | 
			                                             BindingFlags.NonPublic |
			                                             BindingFlags.Instance |
			                                             BindingFlags.FlattenHierarchy);

			//var expr = (MemberExpression) obj.Body;
			//var prop = (PropertyInfo) expr.Member;
			//prop.SetValue(outObj, input, null);

			if (obj.IsEmpty ()) {
				obj = GlobalVars.DummyString;
			//	return obj;
			}

			//return obj;
		}*/
	}

}

