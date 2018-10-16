// Copyright (c) 2018 Jon P Smith, GitHub: JonPSmith, web: http://www.thereformedprogrammer.net/
// Licensed under MIT license. See License.txt in the project root for license information.

namespace DataLayer
{
    public enum OrderStatuses : byte
    {
        NotSet = 0,
        Created = 1,
        Rejected = 2,
        CancelledByUser = 3,
        OutOfStock = 4,
        Dispatched = 10, 
    }
}