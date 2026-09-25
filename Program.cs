using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Хуильник
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PathFinder> finders = new List<PathFinder>();

            File file = new File(".txt");
            finders.Add(new PathFinder(" 1 ", new FileLogWritter(file)));
            finders.Add(new PathFinder(" 4 ", new ConsoleLogWritter()));
            finders.Add(new PathFinder(" 8 ", new SecureLogWritter(new FileLogWritter(file))));
            finders.Add(new PathFinder(" 8 ", new SecureLogWritter(new ConsoleLogWritter())));
            finders.Add(new PathFinder(" Пасхалко ", new SecureLogWritter(new ConsoleLogWritter(new SecureLogWritter(new FileLogWritter(file))))));

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

    abstract class LogWritter : ILogger 
    {
        protected ILogger Logger;

        public LogWritter(ILogger logger = null)
        {
            Logger = logger;
        }

        public virtual void WriteLog(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException();
            }

            if (Logger != null)
            {
                Logger.WriteLog(message);
            }
        }
    }

    class FileLogWritter : LogWritter
    {
        private File _file;

        public FileLogWritter(File file, ILogger logger = null) : base(logger) 
        {           
            _file = file ?? throw new NullReferenceException();
        }

        public override void WriteLog(string message)
        {
            _file.WriteAllText(message);
        }
    }

    class ConsoleLogWritter : LogWritter
    {     
        public ConsoleLogWritter(ILogger logger = null) : base(logger)
        {

        }

        public override void WriteLog(string message)
        {
            Console.WriteLine(message);
        }
    }

    class SecureLogWritter : LogWritter
    {
        public SecureLogWritter(ILogger logger) : base (logger) 
        {
            if (logger == null)
            {
                throw new ArgumentNullException();
            }
        }

        public override void WriteLog(string message)
        {         
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
            {
                Logger.WriteLog(message);
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
