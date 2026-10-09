using System;
using System.Collections.Generic;
using NUnit.Framework;
using DividerApp;

namespace DividerApp.Tests
{
    [TestFixture]
    public class StringDividerTests
    {
        // 001 – Строка, длина которой кратна длине блока
        [Test]
        public void Divider_EvenLength()
        {
            string input = "HelloWorld";
            int blockLength = 5;
            List<string> expected = new List<string> { "Hello", "World" };
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 002 – Строка, длина которой не кратна длине блока
        [Test]
        public void Divider_OddLength()
        {
            string input = "HelloWorld!";
            int blockLength = 4;
            List<string> expected = new List<string> { "Hell", "oWor", "ld!\0" };
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 003 – Длина строки меньше длины блока
        [Test]
        public void Divider_SingleBlock()
        {
            string input = "Hi";
            int blockLength = 5;
            List<string> expected = new List<string> { "Hi\0\0\0" };
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 004 – Длина строки точно равна длине блока
        [Test]
        public void Divider_ExactBlock()
        {
            string input = "Hello";
            int blockLength = 5;
            List<string> expected = new List<string> { "Hello" };
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 005 – Пустая строка
        [Test]
        public void Divider_EmptyString()
        {
            string input = "";
            int blockLength = 3;
            List<string> expected = new List<string>();
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 006 – null вместо строки
        [Test]
        public void Divider_NullString()
        {
            string input = null;
            int blockLength = 3;
            var ex = Assert.Throws<ArgumentNullException>(() => StringDivider.Divider(input, blockLength));
            Assert.That(ex.ParamName, Is.EqualTo("str"));
        }

        // 007 – blockLength = 0
        [Test]
        public void Divider_ZeroBlockLength()
        {
            string input = "Hello";
            int blockLength = 0;
            var ex = Assert.Throws<ArgumentException>(() => StringDivider.Divider(input, blockLength));
            Assert.That(ex.ParamName, Is.EqualTo("blockLength"));
        }

        // 008 – blockLength < 0
        [Test]
        public void Divider_NegativeBlockLength()
        {
            string input = "Hello";
            int blockLength = -2;
            var ex = Assert.Throws<ArgumentException>(() => StringDivider.Divider(input, blockLength));
            Assert.That(ex.ParamName, Is.EqualTo("blockLength"));
        }

        // 009 – Тест с тестовым двойником
        [Test]
        public void Divider_WithFileStub()
        {
            FileReaderStub stub = new FileReaderStub("HelloWorld");
            string input = stub.ReadAllText("test.txt");
            int blockLength = 5;
            List<string> expected = new List<string> { "Hello", "World" };
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expected));
        }

        // 010-014 – Параметризованный тест
        [TestCase("HelloWorld", 5, new[] { "Hello", "World" })]
        [TestCase("abcdef", 2, new[] { "ab", "cd", "ef" })]
        [TestCase("abc", 2, new[] { "ab", "c\0" })]
        [TestCase("Test", 4, new[] { "Test" })]
        [TestCase("x", 3, new[] { "x\0\0" })]
        public void Divider_MultipleData(string input, int blockLength, string[] expected)
        {
            List<string> expectedList = new List<string>(expected);
            List<string> result = StringDivider.Divider(input, blockLength);
            Assert.That(result, Is.EqualTo(expectedList));
        }
    }
}