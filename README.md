# SerializerComparer

## Environment
- .NETCore 3.1.0

## Purpose
Compare any type of object and output as json.

２つのクラスのデータ差分をJson形式で表示します。

## How to Use
``` C#
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

``` 

``` C#

    var options = new System.Text.Json.JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };
    var serializerCompareOptions = new SerializerComparerOptions()
    {
        FilePath = "test.html",
        AsHtml = true
    };

    SerializerComparer<TestClass>.Compare(TestA, TestB, options, serializerCompareOptions);

```
## 結果
test.html
<span>{&para;<br>  "a": "</span><del style="background:#ffe6e6;">aiue</del><ins style="background:#e6ffe6;">kakikukek</ins><span>o",&para;<br>  "b": </span><del style="background:#ffe6e6;">0</del><ins style="background:#e6ffe6;">1</ins><span>,&para;<br>  "c": [&para;<br>    "</span><del style="background:#ffe6e6;">asdfg</del><ins style="background:#e6ffe6;">qwert</ins><span>",&para;<br>    "hjk"&para;<br>  ]&para;<br>}</span>

