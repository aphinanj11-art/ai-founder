namespace AIFounder.Domain.Repair
{
    public sealed class RepairOutcome
    {
        public RepairOutcome(string jobTitle, string methodName, int revenue, int repairCost, int cashBefore, int cashAfter, string explanation)
        {
            JobTitle = jobTitle;
            MethodName = methodName;
            Revenue = revenue;
            RepairCost = repairCost;
            CashBefore = cashBefore;
            CashAfter = cashAfter;
            Explanation = explanation ?? string.Empty;
        }

        public string JobTitle { get; }
        public string MethodName { get; }
        public int Revenue { get; }
        public int RepairCost { get; }
        public int Profit => Revenue - RepairCost;
        public int CashBefore { get; }
        public int CashAfter { get; }
        public string Explanation { get; }
    }
}
