using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Globalization;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Data;
using System.Data.SqlTypes;
using System.Numerics;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Threading;
using System.Threading.Tasks;

namespace one
{

    public class myformatternow
    {

        static int num = 4000;
        List<Task> tasks = new List<Task>();
        static BinaryFormatter f = new BinaryFormatter();
        static MemoryStream s = new MemoryStream() ;

        [IterationSetup]
        public void GlobalSetup()
        {
            //s = new MemoryStream();
            for (int i = 0; i < num; i++)
            {
                tasks.Add(new Task(serialization));
            }
        }

        public void serialization()
        {
            MemoryStream s = new MemoryStream();
            object[][] objects = SerializableObjects().ToArray();
            foreach (object[] obj in objects)
            {
                f.Serialize(s, obj[0]);
            }
            
        }

        [Benchmark]
        public async Task serialize()
        {
            for (int i = 0; i < num; i++)
            {
                tasks[i].Start();
            }
            await Task.WhenAll(tasks);
        }

        [IterationCleanup]
        public void GlobalCleanup()
        {
            tasks.Clear();
            s = new MemoryStream();
            f = new BinaryFormatter();
        }

        private static IEnumerable<object[]> SerializableObjects()
        {
            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };
            

            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };
            
            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            var dictionary = new Dictionary<int, string>() { { 1, "test" }, { 2, "another test" } };
            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            var dotnetUri = new Uri("https://dot.net");
            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };

            
            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };

            
            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };
            
            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };

            
            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

            // copy of above characters

            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };


            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };

            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };


            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };


            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };

            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };


            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };


            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };

            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };


            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };


            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };

            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };


            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };


            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };

            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };


            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };


            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };

            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };


            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };


            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };

            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };


            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };


            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };

            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };


            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

            // Primitive types
            yield return new object[] { byte.MinValue };
            yield return new object[] { byte.MaxValue };
            yield return new object[] { sbyte.MinValue };
            yield return new object[] { sbyte.MaxValue };
            yield return new object[] { short.MinValue };
            yield return new object[] { short.MaxValue };
            yield return new object[] { ushort.MinValue };
            yield return new object[] { ushort.MaxValue };
            yield return new object[] { int.MinValue };
            yield return new object[] { int.MaxValue };
            yield return new object[] { uint.MinValue };
            yield return new object[] { uint.MaxValue };
            yield return new object[] { long.MinValue };
            yield return new object[] { long.MaxValue };
            yield return new object[] { ulong.MinValue };
            yield return new object[] { ulong.MaxValue };
            yield return new object[] { char.MinValue };
            yield return new object[] { char.MaxValue };
            yield return new object[] { float.MinValue };
            yield return new object[] { float.MaxValue };
            yield return new object[] { double.MinValue };
            yield return new object[] { double.MaxValue };
            yield return new object[] { decimal.MinValue };
            yield return new object[] { decimal.MaxValue };
            yield return new object[] { decimal.MinusOne };
            yield return new object[] { true };
            yield return new object[] { false };
            yield return new object[] { "" };
            yield return new object[] { "c" };


            yield return new object[] { IntPtr.Zero };
            yield return new object[] { UIntPtr.Zero };
            yield return new object[] { new DateTime(1990, 11, 23) };
            yield return new object[] { new DateTimeOffset(1990, 11, 23, 03, 30, 00, 00, TimeSpan.FromMinutes(30)) };

            yield return new object[] { new List<int>() { 1, 2, 3, 4, 5 } };

            yield return new object[] { dictionary };

            yield return new object[] { Tuple.Create(1, "2") };

            yield return new object[] { dotnetUri };

            yield return new object[] { new WeakReference(dotnetUri, true) };

            yield return new object[] { new StringBuilder() };
            yield return new object[] { new StringBuilder("starting", 0, 5, 10) };


            yield return new object[] { new SqlGuid() };
            yield return new object[] { new SqlGuid(new byte[] { 74, 17, 188, 26, 104, 117, 191, 64, 132, 93, 95, 0, 182, 136, 150, 121 }) };

            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID) };
            yield return new object[] { new SqlString("abc", CultureInfo.InvariantCulture.LCID, SqlCompareOptions.BinarySort) };


            yield return new object[] { new BigInteger() };
            yield return new object[] { new BigInteger(10324176) };
            yield return new object[] { new BigInteger(new byte[] { 1, 2, 3, 4, 5 }) };

            // Arrays of primitive types
            yield return new object[] { Enumerable.Range(0, 256).Select(i => (byte)i).ToArray() };
            yield return new object[] { new int[] { } };
            yield return new object[] { new int[] { 1 } };
            yield return new object[] { new int[] { 1, 2, 3, 4, 5 } };
            yield return new object[] { new char[] { 'a', 'b', 'c', 'd', 'e' } };
            yield return new object[] { new string[] { } };
            yield return new object[] { new string[] { "hello", "world" } };
            yield return new object[] { new short[] { short.MaxValue } };
            yield return new object[] { new long[] { long.MaxValue } };
            yield return new object[] { new ushort[] { ushort.MaxValue } };
            yield return new object[] { new uint[] { uint.MaxValue } };
            yield return new object[] { new ulong[] { ulong.MaxValue } };
            yield return new object[] { new bool[] { true, false } };
            yield return new object[] { new double[] { 1.2 } };
            yield return new object[] { new float[] { 1.2f, 3.4f } };


            yield return new object[] { new List<int>(Enumerable.Range(0, 123).ToArray()) };
            yield return new object[] { new List<int>(new int[] { 5, 7, 9 }) };
            yield return new object[] { new List<Point>() };
            yield return new object[] { new List<Point>(new Point[] { new Point(1, 2), new Point(4, 3) }) };

            yield return new object[] { new Dictionary<int, int>() };
            yield return new object[] { new Dictionary<int, int>(4) };
            yield return new object[] { new Dictionary<string, string>() { { "a", "1" }, { "b", "2" } } };

        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<myformatternow>(new MainConfig());
        }
    }

}