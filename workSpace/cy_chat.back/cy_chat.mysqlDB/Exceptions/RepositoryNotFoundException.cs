using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cy_chat.mysqlDB.Exceptions
{
    public class RepositoryNotFoundException : Exception
    {
        public RepositoryNotFoundException(string repositoryName, string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(repositoryName)) throw new ArgumentException($"{nameof(repositoryName)} cannot be null or empty.", nameof(repositoryName));
            this.RepositoryName = repositoryName;
        }

        public string RepositoryName { get; private set; }
    }
}
