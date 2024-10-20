using System;
using System.Diagnostics.Contracts;
using System.IO;

namespace scoreboard2.Common.Extensions;

/// <summary>
/// Taken from https://stackoverflow.com/a/60110009
/// </summary>
public static class StreamExtensions
{
    public static string ConvertToBase64(this Stream stream)
    {
        if (stream is MemoryStream memoryStream)
        {
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        var bytes = new byte[(int)stream.Length];

        stream.Seek(0, SeekOrigin.Begin);
        _ = stream.Read(bytes, 0, (int)stream.Length);

        return Convert.ToBase64String(bytes);
    }
}