using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Warehouse.Models;

namespace Warehouse.ModelTests
{
	[TestFixture]
	class SuccessTests
	{
		[Test]
		public void TestCanMakeEmptySuccess()
		{
			Success success = new Success();

			Assert.IsNotNull(success);
			Assert.AreEqual("", success.ResultComment);
		}

		[Test]
		public void TestCanMakeTrueSuccess()
		{
			Success success = new Success(true);

			Assert.IsNotNull(success);
			Assert.IsTrue(success.Result);
		}

		[Test]
		public void TestCanMakeDefaultFalseSuccess()
		{
			Success success = new Success(false);

			Assert.IsNotNull(success);
			Assert.IsFalse(success.Result);
			Assert.AreEqual("", success.ResultComment);
		}

		[Test]
		public void TestCanMakeCustomFailure()
		{
			string message = "Something went terribly sideways";
			Success success = new Success(false, message);

			Assert.IsNotNull(success);
			Assert.IsFalse(success.Result);
			Assert.AreEqual(message, success.ResultComment);
		}
	}
}
