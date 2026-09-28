using System;
using System.Collections.Generic;
using System.Threading;

namespace XDproject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PathFinder> finders = new List<PathFinder>();

            File file = new File(".txt");
            finders.Add(new PathFinder(" 1 ", new FileLogWritter(file)));
            finders.Add(new PathFinder(" 4 ", new ConsoleLogWritter()));
            finders.Add(new PathFinder(" 8 ", new SecureLogWritter(new FileLogWritter(file), DayOfWeek.Monday)));
            finders.Add(new PathFinder(" 8 ", new SecureLogWritter(new ConsoleLogWritter(), DayOfWeek.Monday)));
            finders.Add(new PathFinder(" Пасхалко ", new ConsoleLogWritter(new SecureLogWritter(new FileLogWritter(file), DayOfWeek.Monday))));

            for (int i = 0; i < finders.Count; i++)
            {
                finders[i].Find();
            }
        }
    }

    interface ILogger
    {
        void WriteLog(string message);
    }

    class PathFinder
    {
        private string _message;
        private ILogger _logger;

        public PathFinder(string message, ILogger logger)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(message);
            }

            _logger = logger ?? throw new NullReferenceException();
            _message = message;
        }

        public void Find()
        {
            _logger.WriteLog(_message);
        }
    }

    class FileLogWritter : ILogger
    {
        private ILogger _logger;
        private File _file;

        public FileLogWritter(File file, ILogger logger = null)
        {
            _file = file ?? throw new NullReferenceException();
            _logger = logger;
        }

        public void WriteLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException();
            }

            _file.WriteAllText(message);

            if (_logger != null)
            {
                _logger.WriteLog(message);
            }
        }
    }

    class ConsoleLogWritter : ILogger
    {
        private ILogger _logger;

        public ConsoleLogWritter(ILogger logger = null)
        {
            _logger = logger;
        }

        public void WriteLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException();
            }

            Console.WriteLine(message);

            if (_logger != null)
            {
                _logger.WriteLog(message);
            }
        }
    }

    class SecureLogWritter : ILogger
    {
        private ILogger _logger;
        private DayOfWeek _currentDay;

        public SecureLogWritter(ILogger logger, DayOfWeek day)
        {
            _logger = logger ?? throw new NullReferenceException();
            _currentDay = day;
        }

        public void WriteLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException();
            }

            if (DateTime.Now.DayOfWeek == _currentDay)
            {
                _logger.WriteLog(message);
            }
        }
    }

    class File
    {
        private string _name;

        public File(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException();
            }

            _name = name;
        }

        public void WriteAllText(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException();
            }

            Console.WriteLine(_name + " " + message);
        }
    }
}