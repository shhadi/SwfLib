﻿using System.IO;
using NUnit.Framework;
using SwfLib.Avm2;

namespace SwfLib.Tests.Avm2 {
    [TestFixture]
    public class AbcReadWriteTests {

        [Test]
        public void ReadWriteConstantPoolTest() {
            var pool = new AsConstantPool {
                Doubles = new[] { double.NaN, 1.2, 5.3 },
                Integers = new[] { 0, -123, 456 },
                Multinames = new AsMultiname[0],
                NamespaceSets = new AsNamespaceSet[0],
                Namespaces = new AsNamespace[0],
                Strings = new[] { "", "abc", "def" },
                UnsignedIntegers = new uint[] { 0, 1, uint.MaxValue }
            };
            var mem = new MemoryStream();
            var writer = new AbcWriter(new SwfStreamWriter(mem));
            writer.WriteConstantPool(pool);
            mem.Seek(0, SeekOrigin.Begin);
            var reader = new AbcReader(new SwfStreamReader(mem));
            var res = reader.ReadConstantPool();

            Assert.AreEqual(pool.Doubles.Length, res.Doubles.Length);
            for (var i = 0; i < pool.Doubles.Length; i++) {
                Assert.AreEqual(pool.Doubles[i], res.Doubles[i]);
            }

            Assert.AreEqual(pool.Integers.Length, res.Integers.Length);
            for (var i = 0; i < pool.Integers.Length; i++) {
                Assert.AreEqual(pool.Integers[i], res.Integers[i]);
            }

            //Assert.AreEqual(pool.Multinames.Length, res.Multinames.Length);
            //Assert.AreEqual(pool.NamespaceSets.Length, res.NamespaceSets.Length);
            //Assert.AreEqual(pool.Namespaces.Length, res.Namespaces.Length);

            Assert.AreEqual(pool.Strings.Length, res.Strings.Length);
            for (var i = 0; i < pool.Strings.Length; i++) {
                Assert.AreEqual(pool.Strings[i], res.Strings[i]);
            }

            Assert.AreEqual(pool.UnsignedIntegers.Length, res.UnsignedIntegers.Length);
            for (var i = 0; i < pool.UnsignedIntegers.Length; i++) {
                Assert.AreEqual(pool.UnsignedIntegers[i], res.UnsignedIntegers[i]);
            }
        }
    }
}