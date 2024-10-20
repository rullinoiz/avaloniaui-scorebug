using System;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using Avalonia.Media.Imaging;
using IronSoftware.Drawing;
using Newtonsoft.Json;
using scoreboard2.Common.Extensions;

namespace scoreboard2.Converters;

public class AvaloniaBitmapJsonConverter : JsonConverter<Bitmap>
{
    public override void WriteJson(JsonWriter writer, Bitmap? value, JsonSerializer serializer)
    {
        if (value is null) return;

        using var stream = new MemoryStream();
        value.Save(stream);
        
        using var image = new MemoryStream();
        using (var bmp = AnyBitmap.FromStream(stream))
        {
            bmp.ExportStream(image, AnyBitmap.ImageFormat.Png);
        }
        
        using var compressedStream = new MemoryStream();
        using (var gzip = new GZipStream(compressedStream, CompressionLevel.SmallestSize, false))
        {
            image.WriteTo(gzip);
        }

        var base64String = compressedStream.ConvertToBase64();
        writer.WriteValue(base64String);
    }

    public override Bitmap? ReadJson(JsonReader reader, Type objectType, Bitmap? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        if (reader.Value is not string encodedString) return null;

        var decodedBytes = Convert.FromBase64String(encodedString);
        using var stream = new MemoryStream(decodedBytes);
        using var decompressedStream = new MemoryStream();
        using (var gzip = new GZipStream(stream, CompressionMode.Decompress))
        {
            gzip.CopyTo(decompressedStream);
        }

        decompressedStream.Seek(0, SeekOrigin.Begin);
        return new Bitmap(decompressedStream);
    }
}