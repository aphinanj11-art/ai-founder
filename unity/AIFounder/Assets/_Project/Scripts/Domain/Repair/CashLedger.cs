namespace AIFounder.Domain.Repair
{
    public sealed class CashLedger
    {
        public CashLedger(int startingCash)
        {
            Cash = startingCash;
        }

        public int Cash { get; private set; }

        public bool TrySpend(int amount)
        {
            if (amount < 0 || Cash < amount)
            {
                return false;
            }

            Cash -= amount;
            return true;
        }

        public void AddRevenue(int amount)
        {
            if (amount > 0)
            {
                Cash += amount;
            }
        }
    }
}
