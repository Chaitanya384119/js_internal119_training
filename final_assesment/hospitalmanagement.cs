using System;

// Delegate for billing strategy
public delegate double BillingStrategy(double amount);

// Custom EventArgs
public class HospitalEventArgs : EventArgs
{
    public string Message;

    public HospitalEventArgs(string message)
    {
        Message = message;
    }
}

// ================= Patient Hierarchy =================

public abstract class Patient
{
    public int PatientId;
    public string Name;
    public int Age;
    public string ContactNumber;
    public string Symptoms;

    public abstract double CalculateBaseBill();

    public virtual void DisplayDetails()
    {
        Console.WriteLine("\n--- Patient Details ---");
        Console.WriteLine("ID       : " + PatientId);
        Console.WriteLine("Name     : " + Name);
        Console.WriteLine("Age      : " + Age);
        Console.WriteLine("Contact  : " + ContactNumber);
        Console.WriteLine("Symptoms : " + Symptoms);
    }
}

// Patient Types
public class GeneralPatient : Patient
{
    public override double CalculateBaseBill() => 2000;
}

public class EmergencyPatient : Patient
{
    public override double CalculateBaseBill() => 5000;
}

public class InsurancePatient : Patient
{
    public override double CalculateBaseBill() => 3000;
}

public class ICUPatient : Patient
{
    public override double CalculateBaseBill() => 8000;
}

public class DiagnosticPatient : Patient
{
    public override double CalculateBaseBill() => 1500;
}

// ================= Hospital Class =================

public class Hospital
{
    public event EventHandler<HospitalEventArgs> PatientAdmitted;
    public event EventHandler<HospitalEventArgs> BillGenerated;

    public void AdmitPatient(Patient patient)
    {
        PatientAdmitted?.Invoke(this,
            new HospitalEventArgs("Patient " + patient.Name + " admitted."));
    }

    public double ApplyBilling(Patient patient, BillingStrategy strategy)
    {
        return strategy(patient.CalculateBaseBill());
    }

    public void GenerateBill(Patient patient, double amount)
    {
        BillGenerated?.Invoke(this,
            new HospitalEventArgs("Final Bill Amount: Rs." + amount));
    }
}

// ================= Program =================

class Program
{
    static void Main()
    {
        Hospital hospital = new Hospital();

        hospital.PatientAdmitted += ReceptionNotification;
        hospital.BillGenerated += AccountsNotification;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Hospital Management System ===");
            Console.WriteLine("1. Admit New Patient");
            Console.WriteLine("2. Exit");
            Console.Write("Select Option: ");

            int option;
            if (!int.TryParse(Console.ReadLine(), out option))
                continue;

            if (option == 1)
                AdmitPatientFlow(hospital);
            else if (option == 2)
                return;

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    // ================= Admission Flow =================

    static void AdmitPatientFlow(Hospital hospital)
    {
        Patient patient;

        Console.Write("Enter Patient ID: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Enter Patient Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine());

        Console.Write("Enter Contact Number: ");
        string contact = Console.ReadLine();

        Console.Write("Enter Symptoms: ");
        string symptoms = Console.ReadLine();

        Console.WriteLine("\nSelect Service Type");
        Console.WriteLine("1. General");
        Console.WriteLine("2. Emergency");
        Console.WriteLine("3. Insurance");
        Console.WriteLine("4. ICU");
        Console.WriteLine("5. Diagnostic");
        Console.Write("Choice: ");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1: patient = new GeneralPatient(); break;
            case 2: patient = new EmergencyPatient(); break;
            case 3: patient = new InsurancePatient(); break;
            case 4: patient = new ICUPatient(); break;
            case 5: patient = new DiagnosticPatient(); break;
            default:
                Console.WriteLine("Invalid service type");
                return;
        }

        patient.PatientId = id;
        patient.Name = name;
        patient.Age = age;
        patient.ContactNumber = contact;
        patient.Symptoms = symptoms;

        hospital.AdmitPatient(patient);

        BillingStrategy billing =
            (patient is InsurancePatient) ? InsuranceBilling : EnhancedBilling;

        double finalBill = hospital.ApplyBilling(patient, billing);

        patient.DisplayDetails();
        hospital.GenerateBill(patient, finalBill);
    }

    // ================= Billing Strategies =================

    static double InsuranceBilling(double amount)
    {
        return amount * 0.5; // 50% covered by insurance
    }

    static double EnhancedBilling(double amount)
    {
        double serviceCharge = 500;
        double tax = amount * 0.18;
        return amount + serviceCharge + tax;
    }

    // ================= Event Handlers =================

    static void ReceptionNotification(object sender, HospitalEventArgs e)
    {
        Console.WriteLine("[Reception] " + e.Message);
    }

    static void AccountsNotification(object sender, HospitalEventArgs e)
    {
        Console.WriteLine("[Accounts] " + e.Message);
    }
}
