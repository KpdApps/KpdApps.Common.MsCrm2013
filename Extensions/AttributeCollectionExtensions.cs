using System;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace KpdApps.Common.MsCrm2013.Extensions
{
	public static class AttributeCollectionExtensions
	{
		public static object GetValue(this AttributeCollection properties, string name)
		{
			if (!properties.Contains(name))
				return null;

			object crmValue = properties[name];
			if (crmValue == null)
				return null;

			Type fieldType = crmValue.GetType();
			Type[] endTypes = { typeof(DateTime), typeof(int), typeof(double) };

			EntityReference reference = crmValue as EntityReference;
			if (reference != null)
				return reference.Id;

			if (fieldType == typeof(OptionSetValue))
			{
				OptionSetValue field = (OptionSetValue)crmValue;
				return field.Value;
			}

			if (fieldType == typeof(Money))
			{
				Money field = (Money)crmValue;
				return field.Value;
			}

			if (endTypes.Contains(fieldType))
				return crmValue;

			if (fieldType == typeof(AliasedValue))
				return ((AliasedValue)crmValue).Value;

			return crmValue;
		}

		public static string GetStringValue(this AttributeCollection properties, string name)
		{
			if (!properties.Contains(name))
				return null;

			object property = properties[name];
			return property?.ToString();
		}
	}
}