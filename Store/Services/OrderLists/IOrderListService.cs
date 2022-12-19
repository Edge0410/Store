﻿using Store.Models;
using Store.Models.DTOs;

namespace Store.Services.OrderLists
{
    public interface IOrderListService
    {
        Task Create(OrderListRequestDto newList);
    }
}
