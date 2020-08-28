using PaystubJsonApp.Models.ReapirOrders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PaystubJsonApp.Exceptions
{
    public class UnknownWorkItemException : Exception
    {
        public WorkItem WorkItem { get; private set; }
        public UnknownWorkItemException( ) { }

        public UnknownWorkItemException( WorkItem workItem ) =>
            WorkItem = workItem;

        public UnknownWorkItemException( string message ) : base(message) { }

        public UnknownWorkItemException(
            string message,
            WorkItem workItem
        ) : base(message) =>
            WorkItem = workItem;

        public UnknownWorkItemException(
            string message,
            Exception innerException
        ) : base(message, innerException) { }

        public UnknownWorkItemException(
            string message,
            WorkItem workItem,
            Exception innerException
        ) : base(message, innerException) =>
            WorkItem = workItem;

        protected UnknownWorkItemException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}
