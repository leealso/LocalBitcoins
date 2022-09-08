using System;

namespace LocalBitcoins.Functions.Utilities;

public static class ApplicationSettingsUtility 
{
    public static string Get(string name) 
    {
        return System.Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
    }
}