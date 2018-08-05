namespace LH.CommandLine
{
    public class ArgsReader
    {
        private readonly string[] _args;

        private int _index;

        public ArgsReader(string[] args)
        {
            _args = args;
            _index = 0;
        }

        public string Current
        {
            get
            {
                if (_index < _args.Length)
                {
                    return _args[_index];
                }

                return string.Empty;
            }
        }

        public void MoveNext()
        {
            _index++;
        }
    }
}
