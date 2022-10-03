using System;
using System.Collections.Generic;

namespace LocalBitcoins.Functions.Extensions;

public static class DateTimeExtensions
{
    public static IEnumerable<DateTime> To(this DateTime startDate, DateTime endDate, int step = 1) 
    {  
        for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(step)) 
            yield return date;  
    } 
}