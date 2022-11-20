//   Copyright 2022 DataHint BV
//   Copyright 2022 Ben Motmans
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System.ComponentModel;
using System.Reflection;
using System.Web;

namespace Vizor.Extensions;

public static class TypeExtensions
{
	public static string ToReadableTypeName(this PropertyInfo prop)
	{
		var ctx = new NullabilityInfoContext().Create(prop);
		bool isNullable = ctx.ReadState != NullabilityState.NotNull;

		Type propType = prop.PropertyType;
		if (isNullable)
		{
			var tmp = Nullable.GetUnderlyingType(propType);
			if (tmp != null)
				propType = tmp;
		}

		return propType.ToReadableTypeName() + (isNullable ? "?" : "");
	}

	public static string ToReadableTypeName(this Type type)
	{
		string arrayPostfix = string.Empty;
		string nullablePostfix = string.Empty;

		// array support

		if (type.IsArray)
		{
			for (int i = 0; i < type.GetArrayRank(); ++i)
				arrayPostfix += "[]";
		}

		// generic type support
		if (type.IsGenericType)
		{
			// special case for nullable types
			if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				nullablePostfix = "?";
			}
			else
			{
				var typeParams = string.Join(",", type.GenericTypeArguments.Select(t => t.ToReadableTypeName()));
				var tickIndex = type.Name.IndexOf('`');
				return type.Name[0..tickIndex] + "<" + typeParams + ">" + arrayPostfix;
			}
		}

		// enum support
		if (type.IsEnum)
		{
			return type.Name + arrayPostfix + nullablePostfix;
		}

		switch (Type.GetTypeCode(type))
		{
			case TypeCode.Boolean:
				return "bool" + arrayPostfix + nullablePostfix;
			case TypeCode.Char:
				return "char" + arrayPostfix + nullablePostfix;
			case TypeCode.SByte:
				return "sbyte" + arrayPostfix + nullablePostfix;
			case TypeCode.Byte:
				return "byte" + arrayPostfix + nullablePostfix;
			case TypeCode.Int16:
				return "short" + arrayPostfix + nullablePostfix;
			case TypeCode.UInt16:
				return "ushort" + arrayPostfix + nullablePostfix;
			case TypeCode.Int32:
				return "int" + arrayPostfix + nullablePostfix;
			case TypeCode.UInt32:
				return "uint" + arrayPostfix + nullablePostfix;
			case TypeCode.Int64:
				return "long" + arrayPostfix + nullablePostfix;
			case TypeCode.UInt64:
				return "ulong" + arrayPostfix + nullablePostfix;
			case TypeCode.Single:
				return "float" + arrayPostfix + nullablePostfix;
			case TypeCode.Double:
				return "double" + arrayPostfix + nullablePostfix;
			case TypeCode.Decimal:
				return "decimal" + arrayPostfix + nullablePostfix;
			case TypeCode.DateTime:
				return "DateTime" + arrayPostfix + nullablePostfix;
			case TypeCode.String:
				return "string" + arrayPostfix + nullablePostfix;
		};

		return type.Name + arrayPostfix + nullablePostfix;
	}

	public static bool HasAttribute<T>(this PropertyInfo prop) where T : Attribute
	{
		return prop.GetCustomAttribute<T>() != null;
	}

	public static MarkupString? GetDescription(this Type type)
	{
		var desc = type.GetCustomAttribute<DescriptionAttribute>();
		return GetDescription(desc);
	}

	public static MarkupString? GetDescription(this PropertyInfo prop)
	{
		var desc = prop.GetCustomAttribute<DescriptionAttribute>();
		return GetDescription(desc);
	}

	public static MarkupString? GetDescription<TEnum>(this TEnum e) where TEnum : struct, Enum
	{
		var desc = e.GetType().GetField(e.ToString())?.GetCustomAttribute<DescriptionAttribute>();
		return GetDescription(desc);
	}

	private static MarkupString? GetDescription(DescriptionAttribute? desc)
	{
		if (desc == null)
			return null;

		return new MarkupString(desc.Description);
	}
}
