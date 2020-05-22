using Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Books.Lambda.Models
{
	public class Message
	{
		public Book Book { get; set; }
		public Operation Operation { get; set; }
	}
}
