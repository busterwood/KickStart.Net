﻿using System.IO;
using System.Text;

namespace KickStart.Net.Extensions
{
    public static class StreamExtensions
    {
        public static Stream ToStream(this string input, Encoding @default = null)
        {
            @default = @default ?? Encoding.Default;
            var stream = new MemoryStream(@default.GetBytes(input));
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static Stream ToStream(this byte[] inputs)
        {
            var stream = new MemoryStream(inputs);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static string StreamToString(this Stream stream, Encoding @default = null)
        {
            @default = @default ?? Encoding.Default;
            using (var reader = new StreamReader(stream, @default))
            {
                return reader.ReadToEnd();
            }
        }
    }
}