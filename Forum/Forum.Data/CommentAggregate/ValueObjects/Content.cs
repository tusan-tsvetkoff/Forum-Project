using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Data.CommentAggregate.ValueObjects;

public class Content : ValueObject
{
    public string Text { get; private set; }

    private Content(string text)
    {
        Text = text;
    }

    public static Content Create(string text)
    {
        return new Content(text);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Text;
    }

#pragma warning disable CS8618
    private Content()
    {

    }
#pragma warning restore CS8618
}
