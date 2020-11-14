using System;
using Application.Common.Interfaces;

namespace Application.Common.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
