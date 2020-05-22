using System;

namespace Books.Exceptions
{
	public class NotFoundException : Exception
	{
		public NotFoundException(string key)
			: base($"Item with key = {key} not found.")
		{
		}
	}
}
