namespace web_api.Extensions.Process
{
    public class ProcessHelper
    {
        private readonly string processId;
        public ProcessHelper()
        {
            if (string.IsNullOrEmpty(processId))
            {
                processId = Guid.NewGuid().ToString();
            }
        }

        public string getProcessId()
        {
            return processId;
        }
    }
}
