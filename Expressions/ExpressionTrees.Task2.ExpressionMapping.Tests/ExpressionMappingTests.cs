using System.Collections.Generic;
using ExpressionTrees.Task2.ExpressionMapping.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExpressionTrees.Task2.ExpressionMapping.Tests;

[TestClass]
public class ExpressionMappingTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var mapGenerator = new MappingGenerator();

        var fieldMappings = new Dictionary<string, string>
        {
            { "Identifier", "Id" },
            { "FullName", "Name" }
        };

        var mapper = mapGenerator.Generate<Foo, Bar>(fieldMappings);

        var foo = new Foo { Id = 1, Name = "John Doe" };
        var bar = mapper.Map(foo);

        Assert.AreEqual(foo.Id, bar.Identifier);
        Assert.AreEqual(foo.Name, bar.FullName);
    }

    [TestMethod]
    public void TestMappingWithMissingSourceProperty()
    {
        var mapGenerator = new MappingGenerator();

        var fieldMappings = new Dictionary<string, string>
        {
            { "Identifier", "NonExistentProperty" },
            { "FullName", "Name" }
        };

        var mapper = mapGenerator.Generate<Foo, Bar>(fieldMappings);

        var foo = new Foo { Id = 1, Name = "John Doe" };
        var bar = mapper.Map(foo);

        Assert.AreEqual(default, bar.Identifier);
        Assert.AreEqual(foo.Name, bar.FullName);
    }
}