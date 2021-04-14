using System;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading;

namespace SerializerComparer.Test
{
    public class CompareTest
    {
        public class TestClass
        {
            public string a { get; set; }
            public int b { get; set; }
            public List<string> c { get; set; }
        }

        public static int Main()
        {

            TestClass TestA = new TestClass(){
                a="aiueo",
                b=0,
                c = new List<string>()
                {
                    "asdfg",
                    "hjk"
                }
            };

            TestClass TestB = new TestClass()
            {
                a = "kakikukeko",
                b = 1,
                c = new List<string>()
                {
                    "qwert",
                    "hjk"
                }
            };


            var options = new System.Text.Json.JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var serializerCompareOptions = new SerializerComparerOptions()
            {
                filepath = "test.html",
                asHtml = true
            };

            SerializerComparer<TestClass>.Compare(TestA, TestB, options, serializerCompareOptions);

            return 0;
        }
    }
}
