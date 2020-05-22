using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(IEnumerable<string> errors, string objectName)
			: base($"Item {objectName} is invalid. Errors: { string.Join(";", errors) } ")
		{ }
		public ValidationException(string message) : base(message)
		{ }
	}
}
