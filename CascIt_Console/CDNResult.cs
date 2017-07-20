namespace CascIt_Console
{
    #region Imports

    using System.Collections.Generic;
    using System.Linq;

    #endregion
    public class CDNResult
    {
        public CDNResult(string line)
        {
            Results = line.Split('|');

            if (Results.Length > 0)
                Region = Results[0];

            if (Results.Length > 1)
                QueryPath = Results[1];

            if (Results.Length > 2)
                RawHosts = Results[2];

            Hosts = RawHosts.Split(' ');
        }

        public string[] Results { get; set; }
        public string Region { get; set; }
        public string QueryPath { get; set; }
        public string RawHosts { get; set; }
        public IEnumerable<string> Hosts { get; set; }

        public IEnumerable<string> Urls
        {
            get
            {
                return Hosts.Where(w => !string.IsNullOrWhiteSpace(w))
                    .Select(s => $"http://{s}/{QueryPath}/{{0}}/{{1}}/{{2}}/{{3}}")
                    .Distinct();
            }
        }
    }
}