using System.Text.Json;
using luxick.Result;

namespace Tests;

public class SerializeTests
{
    private class TestClass
    {
        public string Text { get; set; } = "";

        public string Something { get; set; } = "";

        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void DeserializeOk()
    {
        var r = new Ok("Alles Suppi");
        var data = JsonSerializer.Serialize(r);
        var parsed = Result.FromJson(data);
        Assert.IsTrue(parsed is Ok);
    }

    [Test]
    public void DeserializeOkWithMessage()
    {
        var r = new Ok("Alles Suppi");
        var data = JsonSerializer.Serialize(r);
        var parsed = Result.FromJson(data);
        Assert.IsTrue(parsed is Ok);
        Assert.IsTrue(parsed.GetMessage() == "Alles Suppi");
    }

    [Test]
    public void DeserializeErrorWithMessage()
    {
        var data = JsonSerializer.Serialize(new Error("Fehler", "Ein Fehler ist aufgetreten"));
        var result = Result.FromJson(data);
        Assert.IsTrue(result is Error);
        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.GetFullMessage() == "Fehler\nEin Fehler ist aufgetreten");
    }

    [Test]
    public void DeserializeGenericOkAsNonGeneric()
    {
        var input = new Ok<string>("Some Data");
        var data = JsonSerializer.Serialize(input);
        // Deserializing via non-generic class will nontheless yield the Success Property
        var result = Result.FromJson(data);
        
        Assert.IsTrue(result is Ok);
        Assert.IsFalse(result is Ok<string>);
    }

    [Test]
    public void DeserializeGenericOk()
    {
        var input = new Ok<string>("Some Data");
        var data = JsonSerializer.Serialize(input);
        var result = Result<string>.FromJson(data);
        Assert.IsTrue(result is Ok<string>);
    }

    [Test]
    public void DeserializeCustomClass()
    {
        var data = new TestClass
        {
            Text = "Hello",
            Something = "AAAAAAAAA"
        };
        var json = JsonSerializer.Serialize(new Ok<TestClass>(data));

        var result = Result<TestClass>.FromJson(json);
        Assert.IsTrue(result is Ok<TestClass>);
        
        if (result is not Ok<TestClass> ok) return;
        Assert.IsTrue(ok.Value.Text == "Hello");
        Assert.IsTrue(ok.Value.Something == "AAAAAAAAA");
    }

    [Test]
    public void DeserializeGenericError()
    {
        var json = JsonSerializer.Serialize(new Error<TestClass>("Cloud not create data"));
        var result = Result<TestClass>.FromJson(json);
        
        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result is Error<TestClass>);

    }

    [Test]
    public void DeserializeLessSpecificErrorShouldWork()
    {
        var genericResult = new Error<TestClass>("Some thing wrong");
        var json = JsonSerializer.Serialize(genericResult);
        Result result = new Ok();
        Assert.DoesNotThrow(() => result = Result.FromJson(json));
        Assert.IsTrue(result is Error);
        Assert.IsFalse(result is Error<TestClass>);
    }
    
    [Test]
    public void DeserializeLessSpecificOkShouldWork()
    {
        var genericResult = new Ok<int>(42);
        var json = JsonSerializer.Serialize(genericResult);
        Result result = new Error();
        Assert.DoesNotThrow(() => result = Result.FromJson(json));
        Assert.IsTrue(result is Ok);
        Assert.IsFalse(result is Ok<int>);
    }
    
    
    [Test]
    public void DeserializeMoreSpecificErrorShouldWork()
    {
        var result = new Error("not ok");
        var json = JsonSerializer.Serialize(result);
        Assert.DoesNotThrow(() => Result<int>.FromJson(json));
    }
    
    [Test]
    public void DeserializeMoreSpecificOkShouldWork()
    {
        var result = new Ok();
        var json = JsonSerializer.Serialize(result);
        Result r = new Error<int>("nothing here yet");
        Assert.DoesNotThrow(() => r = Result<int>.FromJson(json));
        Assert.IsTrue(r is Ok<int> ok && ok.Value == default);
    }
}