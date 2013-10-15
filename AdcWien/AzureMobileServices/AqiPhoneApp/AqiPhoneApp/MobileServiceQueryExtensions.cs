using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace AqiPhoneApp
{
    // Adapted from: http://blogs.msdn.com/b/kevinash/archive/2013/02/13/retrieving_2d00_more_2d00_data_2d00_from_2d00_azure_2d00_mobile_2d00_services_2d00_using_2d00_paging.aspx
    public static class MobileServiceQueryExtensions
    {
        public async static Task<List<T>> LoadAllAsync<T>(this IMobileServiceTable<T> table, int bufferSize = 1000)
        {
            var query = table.IncludeTotalCount();
            var results = await query.ToEnumerableAsync();
            long count = ((ITotalCountProvider)results).TotalCount;
            if (results != null && count > 0)
            {
                var updates = new List<T>();
                while (updates.Count < count)
                {

                    var next = await query.Skip(updates.Count).Take(bufferSize).ToListAsync();
                    updates.AddRange(next);
                }
                return updates;
            }

            return null;
        }
    }
}
