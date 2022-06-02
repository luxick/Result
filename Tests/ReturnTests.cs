using System.Globalization;
using luxick.Result;
using NUnit.Framework.Internal;

namespace Tests;

public class ReturnTests
{
    private Result GetValue(string input)
    {
        if (input == "invalid") return new Error("Invalid value given");
        return new Ok();
    }

    private Result GetValue2(int input)
    {
        return input == 0
            ? new Error("Zero not supported")
            : new Ok();
    }

    private Result<int> ConvertToInt(string input)
    {
        if (input == "") return new Error<int>("no Input");
        try
        {
            return new Ok<int>(Convert.ToInt32(input));
        }
        catch (Exception e)
        {
            return new Error<int>(e);
        }
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetValueOk()
    {
        var result = GetValue("Ok, value");
        Assert.IsTrue(result is Ok);
    }

    [Test]
    public void GetValueError()
    {
        var result = GetValue("invalid");
        Assert.IsTrue(result is Error);
    }

    [Test]
    public void GetValue2Ok()
    {
        var result = GetValue2(1);
        Assert.IsTrue(result is Ok);
    }

    [Test]
    public void GetValue2Error()
    {
        var result = GetValue2(0);
        Assert.IsTrue(result is Error e);
        Assert.IsTrue(result.GetMessage() == "Zero not supported");
    }

    [Test]
    public void ConvertToIntOk()
    {
        var result = ConvertToInt("12");
        Assert.IsTrue(result is Ok<int>);
        Assert.IsTrue(result.Value == 12);
    }

    [Test]
    public void ConvertToIntEmptyString()
    {
        var r = ConvertToInt("");
        Assert.IsFalse(r.Success);
        Assert.IsTrue(r.GetMessage() == "no Input");
    }

    [Test]
    public void ConvertToIntInvalidInput()
    {
        var r = ConvertToInt("Something invalid");
        Assert.IsFalse(r.Success);
        Assert.IsTrue(r.GetMessage() == nameof(FormatException));
    }
}