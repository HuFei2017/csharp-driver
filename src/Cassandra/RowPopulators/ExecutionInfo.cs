using System;
using System.Collections.Generic;
using System.Net;

namespace Cassandra
{
    /// <summary>
    ///  Basic information on the execution of a query. <p> This provides the
    ///  following information on the execution of a (successful) query: </p> <ul> <li>The
    ///  list of Cassandra hosts tried in order (usually just one, unless a node has
    ///  been tried but was dead/in error or a timeout provoked a retry (which depends
    ///  on the RetryPolicy)).</li> <li>The consistency level achieved by the query
    ///  (usually the one asked, though some specific RetryPolicy may allow this to be
    ///  different).</li> <li>The query trace recorded by Cassandra if tracing had
    ///  been set for the query.</li> </ul>
    /// </summary>
    public class ExecutionInfo
    {
        public ExecutionInfo()
        {
            AchievedConsistency = ConsistencyLevel.Any;
        }

        /// <summary>
        /// Gets the list of host that were queried before getting a valid response, 
        /// being the last host the one that replied correctly.
        /// </summary>
        public IList<IPAddress> TriedHosts { get; private set; }
        
        /// <summary>
        /// Retrieves the coordinator that responded to the request
        /// </summary>
        public IPAddress QueriedHost
        {
            get
            {
                if (TriedHosts == null)
                {
                    throw new NullReferenceException("Tried host is null");
                }
                return TriedHosts.Count > 0 ? TriedHosts[TriedHosts.Count - 1] : null;
            }
        }

        /// <summary>
        /// Gets the trace for the query execution.
        /// </summary>
        public QueryTrace QueryTrace { get; private set; }

        /// <summary>
        /// Gets the final achieved consistency
        /// </summary>
        public ConsistencyLevel AchievedConsistency { get; private set; }

        internal void SetTriedHosts(List<IPAddress> triedHosts)
        {
            TriedHosts = triedHosts;
        }

        internal void SetQueryTrace(QueryTrace queryTrace)
        {
            QueryTrace = queryTrace;
        }

        internal void SetAchievedConsistency(ConsistencyLevel achievedConsistency)
        {
            AchievedConsistency = achievedConsistency;
        }
    }
}