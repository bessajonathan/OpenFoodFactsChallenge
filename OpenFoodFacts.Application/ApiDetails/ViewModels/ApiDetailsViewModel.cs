using OpenFoodFacts.Domain.Enums;
using System;

namespace OpenFoodFacts.Application.ApiDetails.ViewModels
{
    public class ApiDetailsViewModel
    {
        public ApiDetailsViewModel(EDatabaseStatus writeStatus, EDatabaseStatus readerStatus,DateTime? lasContExecution, int onlineTime,long memoryUsedInBytes)
        {
            WriteStatus = writeStatus;
            ReaderStatus = readerStatus;
            LasContExecution = lasContExecution;
            OnlineTime = onlineTime;
            MemoryUsedInBytes = memoryUsedInBytes;
        }
        public EDatabaseStatus WriteStatus { get; set; }
        public EDatabaseStatus ReaderStatus { get; set; }
        public DateTime? LasContExecution { get; set; }
        public int OnlineTime { get; set; }
        public long MemoryUsedInBytes { get; set; }
    }
}
