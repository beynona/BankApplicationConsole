using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        //При выводе денег
        protected internal event AccountStateHandler Withdrawed;
        //При поступлении денег
        protected internal event AccountStateHandler Added;
        //При открытии счёта
        protected internal event AccountStateHandler Opened;
        //При закрытии счёта
        protected internal event AccountStateHandler Closed;
        //При начислении процентов
        protected internal event AccountStateHandler Calculated;

        static int couner;
        protected int _days; // Время с открытия счёта
        public decimal Sum { get; set; }
        public int Percentage { get; set; }
        public int ID { get; set; }
        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
        }
        private void CallEvents(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
            {
                handler?.Invoke(this, e);
            }
        }
        //Вызов отдельных событий
        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvents(e, Withdrawed);
        }
        protected virtual void OnAdd(AccountEventArgs e)
        {
            CallEvents(e, Added);
        }
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvents(e, Opened);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvents(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvents(e, Calculated);
        }

        // метод снятия со счета, возвращает сколько снято со счета
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum > sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"Сумма {sum} снята со счета {ID}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Недостаточно денег на счете {ID}", 0));
            }
            return result;
        }
        //добавление денег на счёт
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdd(new AccountEventArgs($"На счет поступило {sum}", sum));
        }
        //Открытие счёта
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Открыт новый счет! Id счета: {ID}", Sum));
        }
        //Закрытие счёта
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Счет {ID} закрыт.  Итоговая сумма: {Sum}", Sum));
        }
        protected internal void IncrementDays()
        {
            _days++;
        }
        //Начисление процентов
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Начислены проценты в размере: {increment}", increment));
        }

    }
}
