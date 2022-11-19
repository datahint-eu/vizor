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

using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace Vizor.Extensions;

public static class RazorExtensions
{
	private static Random rnd = new Random();
	private const string IdCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

	public static string? ToConditionalAttribute(this bool b) => b == true ? "true" : null;

	public static string ToTrueFalse(this bool b) => b == true ? "true" : "false";

	public static string? ToConditionalAttributeInverse(this bool? b) => b == false ? "true" : null;

	public static string RandomId(int len = 8)
	{
		var result = new char[len];

		for (int i = 0; i < len; ++i)
		{
			result[i] = IdCharacters[rnd.Next(IdCharacters.Length)];
		}

		return "id_" + new string(result);
	}

	public static string GetCombinedContent(RenderFragment? childContent, string? content, string? contentSurround = null)
	{
		var sb = new StringBuilder();

		if (childContent != null)
		{
#pragma warning disable BL0006 // don't complain about using internal Blazor API, it's the only way I currently know how to achieve this
			RenderTreeBuilder rb = new RenderTreeBuilder();
			childContent.Invoke(rb);

			var frames = rb.GetFrames();
			if (frames.Count != 1 && frames.Array[0].FrameType != Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrameType.Markup)
				throw new InvalidOperationException("MarkupContent expected");

			sb.AppendLine(frames.Array[0].MarkupContent);
#pragma warning restore BL0006
		}

		if (content != null)
		{
			if (contentSurround != null)
			{
				sb.Append('<');
				sb.Append(contentSurround);
				sb.Append('>');
			}

			sb.Append(content);

			if (contentSurround != null)
			{
				sb.Append('<');
				sb.Append('/');
				sb.Append(contentSurround);
				sb.Append('>');
			}
		}

		return sb.ToString();
	}
}
