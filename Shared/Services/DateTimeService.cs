using Application.Interfaces;
using DevExpress.Xpo;
using System;

namespace Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;



    }

}