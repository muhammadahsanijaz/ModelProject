using System;
using UnityEngine;

namespace MoonKart
{
	public class ResourcePathAttribute : PropertyAttribute
	{
		// PUBLIC MEMBERS

		public readonly Type Type;

		// CONSTRUCTOR

		public ResourcePathAttribute(Type type = null)
		{
			Type = type != null ? type : typeof(GameObject);
		}
	}

	[Serializable]
	public struct ResourcePath
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private string _path;

		// OPERATORS

		public static implicit operator string(ResourcePath path) { return path._path; }
		public static implicit operator ResourcePath(string str) { return new ResourcePath() { _path = str }; }
	}

	[Serializable]
	public struct SpritePath
	{
		// PRIVATE MEMBERS

		[SerializeField]
		private string _path;

		// OPERATORS

		public static implicit operator string(SpritePath path) { return path._path; }
		public static implicit operator SpritePath(string str) { return new SpritePath() { _path = str }; }
	}
}
