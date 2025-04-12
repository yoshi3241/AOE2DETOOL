using AOE2DETOOL.Definition;
using DotNetEnv;

namespace AOE2DETOOL
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CheckEnv();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static void CheckEnv()
        {
            Env.Load();
            string apiKey = Environment.GetEnvironmentVariable(Constants.KEY_ENV_OPENAI) ?? throw new Exception("OPENAI_API_KEYÇ™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ");
        }
    }
}