// See https://aka.ms/new-console-template for more information
using Core;
using System.Diagnostics;


    // Buat instance Stopwatch
    Stopwatch stopwatch = new Stopwatch();

    // Mulai Stopwatch
    stopwatch.Start();

    // Mulai proses
    string foo = "bar";
    foo.EvaluteString();

    // Hentikan Stopwatch
    stopwatch.Stop();

    // Jika ingin waktu yang lebih presisi, bisa menggunakan ElapsedTicks atau Elapsed
    //Console.WriteLine($"Waktu yang dibutuhkan: {stopwatch.ElapsedTicks} ticks");
    Console.WriteLine($"Waktu yang dibutuhkan: {stopwatch.Elapsed.TotalMilliseconds} ms");
