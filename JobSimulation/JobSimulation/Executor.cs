namespace JobSimulation
{
public delegate void JobExecutor(Job job);

public static class Executors
{
    public static void SafeExecutor(Job job)
    {

        int x = 0;
        for (int i = 0; i < 100; i++)
        {
            x++;
        }

        try
        {
            if (job.Name.Contains("fail-safe", StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Job '{job.Name}' failed intentionally (fast-safe)");
            }
        }
        catch(Exception e)
        {
            throw new InvalidOperationException($"Job '{job.Name}' was wrapped by safe exception", e);
        }
        
    }
    public static void RetryExecutor(Job job)
    {
        if (job.Name.Contains("fail-retry", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Job '{job.Name}' failed intentionally (fail-retry)");
        }
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            try
            {
                Console.WriteLine($"RetryExecutor: attempt {attempt} for job '{job.Name}'");
                
                if (attempt <= job.RetryFailuresBeforeSuccess)
                {
                    throw new Exception($"Transient failure for job '{job.Name}', attempt {attempt}.");
                }

                Console.WriteLine($"RetryExecutor: job '{job.Name}' succeeded on attempt {attempt}");
                return;
            }
            catch (Exception ex)
            {
                if (attempt == 3)
                {
                    throw;
                }

                Console.WriteLine($"RetryExecutor: attempt {attempt} failed for job '{job.Name}', retrying");
            }
        }
    }

    public static void FastExecutor(Job job)
    {
        if (job.Name.Contains("fail-fast", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception("$\"Job '{job.Name}' failed intentionally (fail-fast)");
        }

        Console.WriteLine($"Job '{job.Name}' is executed by fast execution");
    }
    
    
}
    
    
    
    
    
}