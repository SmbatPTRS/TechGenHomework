namespace ACA.PriceEngineWrapper;
using ACA.PriceEngine;
using System;
using System.Reflection;
public class OrderCorrecter
{
    Assembly _assembly = Assembly.Load("ACA.PriceEngine");

    

    
    public object Run(PriceInput input)
    {
        
        Type type = _assembly.GetType("ACA.PriceEngine.PriceEngine");
        
        if (type == null)
        {
            Console.WriteLine("Couldn't find PriceEngine");
            return null;
        }
        
        object obj = Activator.CreateInstance(type);

        if (obj == null)
        {
            Console.WriteLine("Couldn't create instance of PriceEngine");
            return null;
        }
        
        MethodInfo? method1 = type.GetMethod("ComputeSubtotal", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method2 = type.GetMethod("CountUnits", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method3 = type.GetMethod("ApplyVolumeDiscount", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method4 = type.GetMethod("ApplyLoyaltyDiscount", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method5 = type.GetMethod("ApplyCoupon", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method6 = type.GetMethod("ApplyVat", BindingFlags.NonPublic | BindingFlags.Instance);
        MethodInfo? method7 = type.GetMethod("RoundMoney", BindingFlags.NonPublic | BindingFlags.Static); 


        object subtotal = method1.Invoke(obj, new object[] { input.Lines });

        object totalUnits = method2.Invoke(obj, new object[] { input.Lines });

        object afterVolume = method3.Invoke(obj, new object[] { subtotal, totalUnits });

        object afterLoyalty = method4.Invoke(obj, new object[] { afterVolume, input.LoyaltyTier });

        object afterCoupon = method5.Invoke(obj, new object[] { afterLoyalty, input.CouponAmount });

        object afterVat = method6.Invoke(obj, new object[] { afterCoupon, input.VatRate });

        object finalResult = method7.Invoke(null, new object[] { afterVat });

        return finalResult;
    }
}