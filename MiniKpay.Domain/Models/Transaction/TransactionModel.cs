﻿namespace MiniKpay.Domain.Models.Transaction
{
    public class TransactionModel
    {
        public int TransferId { get; set; }

        public string SenderMobileNo { get; set; } = null!;

        public string ReceiverMobileNo { get; set; } = null!;

        public decimal? Amount { get; set; }

        public string? Notes { get; set; }
    }
}