﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Открыт новый депозитный счет!Id счета: {this.ID }", this.Sum));
        }
        public override void Put(decimal sum)
        {
            if (_days % 30 == 0)
            {
                base.Put(sum);
            }
            else
            {
                base.OnAdd(new AccountEventArgs($"На счёт можно положить только спустя 30-и дневного периода", 0));
            }
        }
        public override decimal Withdraw(decimal sum)
        {
            if (_days % 30 == 0)
            {
                return base.Withdraw(sum);
            }
            else
            {
                base.OnAdd(new AccountEventArgs($"Со счёта можно снять только спустя 30-и дневного периода", 0));
            }
            return 0;
        }
        protected internal override void Calculate()
        {
            if (_days % 30 == 0)
            {
                base.Calculate();
            }
        }
    }

}
