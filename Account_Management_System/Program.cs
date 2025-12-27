using System;
using System.Collections.Generic;

public class Account
{
    public string Name { get; set; }
    public double Balance { get; set; }

    public Account(string name = "Unnamed Account", double balance = 0.0)
    {
        this.Name = name;
        this.Balance = balance;
    }

    public virtual bool Deposit(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        this.Balance += amount;
        return true;
    }

    public virtual bool Withdraw(double amount)
    {
        if (amount < 0 || this.Balance - amount < 0)
        {
            return false;
        }

        this.Balance -= amount;
        return true;
    }

    public override string ToString()
    {
        return $"[Account: {this.Name}: {this.Balance:C}]";
    }
}

public class SavingsAccount : Account
{
    public double InterestRate { get; set; }

    public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance)
    {
        this.InterestRate = interestRate;
    }

    public override string ToString()
    {
        return $"[Savings Account: {this.Name}: {this.Balance:C}, Interest Rate: {this.InterestRate}%]";
    }
}

public class CheckingAccount : Account
{
    private readonly double withdrawalFee = 1.50;  

    public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
        : base(name, balance)
    {
    }

    public override bool Withdraw(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        double total = amount + this.withdrawalFee;

        if (this.Balance - total >= 0)
        {
            this.Balance -= total;
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"[Checking Account: {this.Name}: {this.Balance:C}]";
    }
}

public class TrustAccount : Account
{
    public double InterestRate { get; set; }
     int withdrawalCount;
     int maxWithdrawals = 3;              
      double maxWithdrawalPercentage = 0.20; 
      double bonusThreshold = 5000.0;       
     double bonusAmount = 50.0;         

    public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 0.0)
        : base(name, balance)
    {
        this.InterestRate = interestRate;
        this.withdrawalCount = 0;
    }

    public override bool Deposit(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        double bonus = 0.0;
        if (amount >= this.bonusThreshold)
        {
            bonus = this.bonusAmount;
        }

        this.Balance += amount + bonus;
        return true;
    }

    public override bool Withdraw(double amount)
    {
        if (amount < 0)
        {
            return false;
        }

        if (this.withdrawalCount >= this.maxWithdrawals)
        {
            return false;
        }

        if (amount > this.Balance * this.maxWithdrawalPercentage)
        {
            return false;
        }

        if (this.Balance - amount >= 0)
        {
            this.Balance -= amount;
            this.withdrawalCount++;
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        return $"[Trust Account: {this.Name}: {this.Balance:C}, Interest Rate: {this.InterestRate}%, Withdrawals: {this.withdrawalCount}/{this.maxWithdrawals}]";
    }
}

public static class AccountUtil
{
    public static void Deposit(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Accounts =================================");
        for (int i = 0; i < accounts.Count; i++)
        {
            Account acc = accounts[i];
            bool success = acc.Deposit(amount);
            if (success)
            {
                Console.WriteLine("Deposited " + amount + " to " + acc);
            }
            else
            {
                Console.WriteLine("Failed Deposit of " + amount + " to " + acc);
            }
        }
    }

    public static void Withdraw(List<Account> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
        for (int i = 0; i < accounts.Count; i++)
        {
            Account acc = accounts[i];
            bool success = acc.Withdraw(amount);
            if (success)
            {
                Console.WriteLine("Withdrew " + amount + " from " + acc);
            }
            else
            {
                Console.WriteLine("Failed Withdrawal of " + amount + " from " + acc);
            }
        }
    }

    public static void DepositSavings(List<SavingsAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Savings Accounts =================================");
        for (int i = 0; i < accounts.Count; i++)
        {
            SavingsAccount acc = accounts[i];
            bool success = acc.Deposit(amount);
            if (success)
            {
                Console.WriteLine("Deposited " + amount + " to " + acc);
            }
            else
            {
                Console.WriteLine("Failed Deposit of " + amount + " to " + acc);
            }
        }
    }

    public static void WithdrawSavings(List<SavingsAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Savings Accounts ==============================");
        for (int i = 0; i < accounts.Count; i++)
        {
            SavingsAccount acc = accounts[i];
            bool success = acc.Withdraw(amount);
            if (success)
            {
                Console.WriteLine("Withdrew " + amount + " from " + acc);
            }
            else
            {
                Console.WriteLine("Failed Withdrawal of " + amount + " from " + acc);
            }
        }
    }

    public static void DepositChecking(List<CheckingAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Checking Accounts =================================");
        for (int i = 0; i < accounts.Count; i++)
        {
            CheckingAccount acc = accounts[i];
            bool success = acc.Deposit(amount);
            if (success)
            {
                Console.WriteLine("Deposited " + amount + " to " + acc);
            }
            else
            {
                Console.WriteLine("Failed Deposit of " + amount + " to " + acc);
            }
        }
    }

    public static void WithdrawChecking(List<CheckingAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Checking Accounts ==============================");
        for (int i = 0; i < accounts.Count; i++)
        {
            CheckingAccount acc = accounts[i];
            bool success = acc.Withdraw(amount);
            if (success)
            {
                Console.WriteLine("Withdrew " + amount + " from " + acc);
            }
            else
            {
                Console.WriteLine("Failed Withdrawal of " + amount + " from " + acc);
            }
        }
    }

    public static void DepositTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Depositing to Trust Accounts =================================");
        for (int i = 0; i < accounts.Count; i++)
        {
            TrustAccount acc = accounts[i];
            bool success = acc.Deposit(amount);
            if (success)
            {
                Console.WriteLine("Deposited " + amount + " to " + acc);
            }
            else
            {
                Console.WriteLine("Failed Deposit of " + amount + " to " + acc);
            }
        }
    }

    public static void WithdrawTrust(List<TrustAccount> accounts, double amount)
    {
        Console.WriteLine("\n=== Withdrawing from Trust Accounts ==============================");
        for (int i = 0; i < accounts.Count; i++)
        {
            TrustAccount acc = accounts[i];
            bool success = acc.Withdraw(amount);
            if (success)
            {
                Console.WriteLine("Withdrew " + amount + " from " + acc);
            }
            else
            {
                Console.WriteLine("Failed Withdrawal of " + amount + " from " + acc);
            }
        }
    }
}

class Program
{
    static void Main()
    {
        List<Account> accounts = new List<Account>();
        accounts.Add(new Account());
        accounts.Add(new Account("Larry"));
        accounts.Add(new Account("Moe", 2000));
        accounts.Add(new Account("Curly", 5000));

        AccountUtil.Deposit(accounts, 1000);
        AccountUtil.Withdraw(accounts, 2000);

        List<SavingsAccount> savAccounts = new List<SavingsAccount>();
        savAccounts.Add(new SavingsAccount());
        savAccounts.Add(new SavingsAccount("Superman"));
        savAccounts.Add(new SavingsAccount("Batman", 2000));
        savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

        AccountUtil.DepositSavings(savAccounts, 1000);
        AccountUtil.WithdrawSavings(savAccounts, 2000);

        List<CheckingAccount> checAccounts = new List<CheckingAccount>();
        checAccounts.Add(new CheckingAccount());
        checAccounts.Add(new CheckingAccount("Larry2"));
        checAccounts.Add(new CheckingAccount("Moe2", 2000));
        checAccounts.Add(new CheckingAccount("Curly2", 5000));

        AccountUtil.DepositChecking(checAccounts, 1000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);
        AccountUtil.WithdrawChecking(checAccounts, 2000);

        List<TrustAccount> trustAccounts = new List<TrustAccount>();
        trustAccounts.Add(new TrustAccount());
        trustAccounts.Add(new TrustAccount("Superman2"));
        trustAccounts.Add(new TrustAccount("Batman2", 2000));
        trustAccounts.Add(new TrustAccount("Wonderwoman2", 10000, 5.0));

        AccountUtil.DepositTrust(trustAccounts, 1000);
        AccountUtil.DepositTrust(trustAccounts, 6000);
        AccountUtil.WithdrawTrust(trustAccounts, 2000);
        AccountUtil.WithdrawTrust(trustAccounts, 3000);
        AccountUtil.WithdrawTrust(trustAccounts, 500);

        Console.WriteLine();
       
    }
}