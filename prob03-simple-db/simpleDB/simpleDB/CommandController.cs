using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace simpleDB
{
    public class CommandController
    {
        private Dictionary<string, IDataCommand> _dataCommands = new Dictionary<string, IDataCommand>();

        public CommandController()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<Type> dataCommands = assembly.GetTypes().Where(type => type != typeof(IDataCommand)
                && typeof(IDataCommand).IsAssignableFrom(type)).ToList<Type>();
            dataCommands.ForEach(dataCommand => _dataCommands.Add(dataCommand.Name.ToUpper(), (IDataCommand)Activator.CreateInstance(dataCommand)));

            //ready memory for operations
            _dataCommands["RESETALL"].Operation(null);
        }

        public string ExecuteCommandString(string commandString)
        {
            List<string> args = commandString.Split().ToList();
            string formattedCommand = args[0].ToUpper();
            args.RemoveAt(0);
            int? output;

            try
            {
                output = _dataCommands[formattedCommand].Operation(args);
            }
            catch
            {
                return "UNKNOWN COMMAND";
            }

            if (output.HasValue)
                return output.Value.ToString();
            else
                return _dataCommands[formattedCommand].Message;
        }
    }
}
