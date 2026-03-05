using System.Diagnostics;
using System.Text;

static void RunBenchmarks(int[] tests, bool complex, StreamWriter writer)
{
    Console.WriteLine("Count | Concat(ms) | SB(ms) | Length(Concat) | Length(SB)");

    foreach (int count in tests)
    {
        
        var sw1 = Stopwatch.StartNew();
        string s1 = complex ? GenerateStringConcatComplex(count) : GenerateStringConcat(count);
        sw1.Stop();

        
        var sw2 = Stopwatch.StartNew();
        string s2 = complex ? GenerateStringSBComplex(count) : GenerateStringSB(count);
        sw2.Stop();

        Console.WriteLine(
            $"{count,5} | {sw1.ElapsedMilliseconds,10} | {sw2.ElapsedMilliseconds,5} | {s1.Length,13} | {s2.Length,10}"
        );

        string mode = complex ? "complex" : "simple";
        writer.WriteLine($"{mode},{count},Concat,{sw1.ElapsedMilliseconds},{s1.Length}");
        writer.WriteLine($"{mode},{count},StringBuilder,{sw2.ElapsedMilliseconds},{s2.Length}");

        Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: {mode} count={count} Concat={sw1.ElapsedMilliseconds}ms SB={sw2.ElapsedMilliseconds}ms");
        Debug.WriteLine($"DEBUG: {mode} count={count} done");
    }
}

static string GenerateStringConcat(int count)
{
    Debug.WriteLine($"DEBUG: GenerateStringConcat start count={count}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringConcat start count={count}");

    string result = "";
    for (int i = 1; i <= count; i++)
    {
        result += $"Iteration: {i} ";
    }

    Debug.WriteLine($"DEBUG: GenerateStringConcat end length={result.Length}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringConcat end length={result.Length}");
    return result;
}

static string GenerateStringSB(int count)
{
    Debug.WriteLine($"DEBUG: GenerateStringSB start count={count}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringSB start count={count}");

    var sb = new StringBuilder();
    for (int i = 1; i <= count; i++)
    {
        sb.Append("Iteration: ");
        sb.Append(i);
        sb.Append(' ');
    }

    string result = sb.ToString();
    Debug.WriteLine($"DEBUG: GenerateStringSB end length={result.Length}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringSB end length={result.Length}");
    return result;
}

static string GenerateStringConcatComplex(int count)
{
    Debug.WriteLine($"DEBUG: GenerateStringConcatComplex start count={count}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringConcatComplex start count={count}");

    string result = "";
    var rnd = new Random(12345); 
    for (int i = 1; i <= count; i++)
    {
        int randomNumber = rnd.Next(0, 1_000_000);
        char randomChar = (char)rnd.Next('A', 'Z' + 1);
        result += $"Iteration: {i}, RandomNumber: {randomNumber}, RandomChar: {randomChar} ";
    }

    Debug.WriteLine($"DEBUG: GenerateStringConcatComplex end length={result.Length}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringConcatComplex end length={result.Length}");
    return result;
}

static string GenerateStringSBComplex(int count)
{
    Debug.WriteLine($"DEBUG: GenerateStringSBComplex start count={count}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringSBComplex start count={count}");

    var sb = new StringBuilder();
    var rnd = new Random(12345); 
    for (int i = 1; i <= count; i++)
    {
        int randomNumber = rnd.Next(0, 1_000_000);
        char randomChar = (char)rnd.Next('A', 'Z' + 1);

        sb.Append("Iteration: ");
        sb.Append(i);
        sb.Append(", RandomNumber: ");
        sb.Append(randomNumber);
        sb.Append(", RandomChar: ");
        sb.Append(randomChar);
        sb.Append(' ');
    }

    string result = sb.ToString();
    Debug.WriteLine($"DEBUG: GenerateStringSBComplex end length={result.Length}");
    Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: GenerateStringSBComplex end length={result.Length}");
    return result;
}

Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
Trace.AutoFlush = true;

Debug.WriteLine("DEBUG: Program started"); 
Trace.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TRACE: Program started"); 

int[] tests = { 1000, 10000, 100000 };

string csvPath = "results.csv";
using var writer = new StreamWriter(csvPath, append: false, Encoding.UTF8);
writer.WriteLine("Mode,Count,Method,ElapsedMs,Length");

Console.WriteLine("=== SIMPLE STRING ===");
RunBenchmarks(tests, complex: false, writer);

Console.WriteLine();
Console.WriteLine("=== COMPLEX STRING (random number + random char) ===");
RunBenchmarks(tests, complex: true, writer);

Console.WriteLine();
Console.WriteLine($"Results saved to: {csvPath}");
Trace.WriteLine($"[{DateTime.Now:HH:mm:ss}] TRACE: Results saved to {csvPath}");

Debug.WriteLine("DEBUG: Program finished");
Trace.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TRACE: Program finished");
Trace.Flush();