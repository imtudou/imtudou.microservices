namespace Imtudou.Core.Logs.Serilog
{
    public class SerilogFactory
    {
        private static readonly object _dblock = new object();
        private static readonly object _filelock = new object();
        private static Serilog2Database serilog2Database;
        private static Serilog2File serilog2File;

        public static Serilog2Database DatabaseLogManager()
        {
            if (serilog2Database == null) // Double checked locking, singleton
            {
                lock (_dblock) // thread safe, singleton
                {
                    if (serilog2Database == null)
                        serilog2Database = new Serilog2Database();
                }
            }
            return serilog2Database;
        }

        public static Serilog2Database DatabaseLogManager(string connectionString)
        {
            if (serilog2Database == null) // Double checked locking, singleton
            {
                lock (_dblock) // thread safe, singleton
                {
                    if (serilog2Database == null)
                        serilog2Database = new Serilog2Database(connectionString);
                }
            }
            return serilog2Database;
        }

        public static Serilog2File FileLogManager()
        {
            if (serilog2File == null)
            {
                lock (_filelock)
                {
                    if (serilog2File == null)
                        serilog2File = new Serilog2File();
                }
            }
            return serilog2File;
        }

        public static Serilog2File FileLogManager(string path)
        {
            if (serilog2File == null)
            {
                lock (_filelock)
                {
                    if (serilog2File == null)
                        serilog2File = new Serilog2File(path);
                }
            }
            return serilog2File;
        }

    }
}
