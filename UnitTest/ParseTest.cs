using NUnit.Framework;
using Utils.StaticUtils;

namespace UtilsEditor.Tests {
	public static class ParseTest {
		[Test]
		public static void TestTryParseFloat() {
			Assert.AreEqual(0, 0);
			Assert.IsTrue(Parse.TryFloat("0", out var t));
			Assert.AreEqual(0, t);
			Assert.IsTrue(Parse.TryFloat("1", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat("1.0", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat("1,0", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat($"{1f:0}", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat($"{1f:0.0}", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat("1.5", out t));
			Assert.AreEqual(1.5f, t);
			Assert.IsTrue(Parse.TryFloat("1,5", out t));
			Assert.AreEqual(1.5f, t);
			Assert.IsTrue(Parse.TryFloat($"{1.4f:0}", out t));
			Assert.AreEqual(1, t);
			Assert.IsTrue(Parse.TryFloat($"{1.5f:0.0}", out t));
			Assert.AreEqual(1.5f, t);
		}
	}
}