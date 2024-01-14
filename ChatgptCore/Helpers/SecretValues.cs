namespace ChatgptCore.Helpers
{
    public static class SecretValues
    {
        public static string? GetValue(string variable)
        {
            string? value = Environment.GetEnvironmentVariable(variable);
            return value;
        }
    }
}
