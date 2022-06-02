using luxick.Result;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void NewOk()
    {
        var res = new Ok();
        Assert.IsTrue(res.Success);
    }

    [Test]
    public void NewOkHasNoMessages()
    {
        var res = new Ok();
        Assert.IsTrue(res.Messages.Count == 0);
    }

    [Test]
    public void GetMessageReturnsEmptyString()
    {
        var res = new Ok();
        Assert.IsTrue(res.GetMessage() == "");
    }

    [Test]
    public void OkWithMessage()
    {
        var r = new Ok("Hello");
        Assert.IsTrue(r.Messages.Count == 1);
        Assert.IsTrue(r.Messages[0] == "Hello");
    }

    [Test]
    public void OkWithMultipleMessages()
    {
        var r = new Ok("Hello", "World", "This", "Is");
        Assert.IsTrue(r.Messages.Count == 4);
        Assert.IsTrue(r.Messages[2] == "This");
    }

    [Test]
    public void GetMessageWithMultipleStrings()
    {
        var r = new Ok(new[] { "Hello", "World" });
        Assert.IsTrue(r.GetMessage() == "Hello");
        Assert.IsTrue(r.GetFullMessage() == "Hello\nWorld");
    }
}