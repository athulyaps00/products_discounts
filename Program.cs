
using System;

namespace ShoppingCart
{
    struct Product
    {
        public string name;
        public double price;
        public int quantity;
        public bool GiftWrapped;
    }

    public class Program
    {
        public static void Main()
        {
            // products Catalog  
            Product[] catalog = new Product[]
            {
                new Product { name = "Product A", price = 20, quantity = 0, GiftWrapped = false },
                new Product { name = "Product B", price = 40, quantity = 0, GiftWrapped = false },
                new Product { name = "Product C", price = 50, quantity = 0, GiftWrapped = false }
            };

            // select quantity and gift wrap choice for each product
            for (int i = 0; i < 3; i++)
            {
                Console.Write("Enter the quantity of " + catalog[i].name + ": ");
                catalog[i].quantity = int.Parse(Console.ReadLine());

                Console.Write("Wrap " + catalog[i].name + " as a gift? (1 for Yes, 0 for No): ");
                catalog[i].GiftWrapped = Convert.ToBoolean(int.Parse(Console.ReadLine()));
            }

            // Calculate subtotal and check for applicable discounts
            double subtotal = 0;
            int totalQuantity = 0;
            bool Flat10Discount = false;
            bool Bulk5Discount = false;
            bool Bulk10Discount = false;
            bool Tiered50Discount = false;

            for (int i = 0; i < 3; i++)
            {
                double productTotal = catalog[i].price * catalog[i].quantity;
                subtotal += productTotal;
                totalQuantity += catalog[i].quantity;

                if (catalog[i].quantity > 10)
                    Bulk5Discount = true;

                if (totalQuantity > 20)
                     Bulk10Discount = true;

                if (totalQuantity > 30 && catalog[i].quantity > 15)
                     Tiered50Discount = true;
            }

            if (subtotal > 200)
                 Flat10Discount = true;

            // Apply the best discount
            double discountAmount = 0;
            string discountName = "";

            if ( Tiered50Discount)
            {
                discountAmount = (totalQuantity - 15) * 0.5 * catalog[0].price; // Assuming all products have the same price
                discountName = "tiered_50_discount";
            }
            else if ( Bulk10Discount)
            {
                discountAmount = subtotal * 0.1;
                discountName = "bulk_10_discount";
            }
            else if ( Bulk5Discount)
            {
                discountAmount = catalog[0].price * catalog[0].quantity * 0.05; // Assuming only Product A has the discount
                discountName = "bulk_5_discount";
            }
            else if ( Flat10Discount)
            {
                discountAmount = 10;
                discountName = "flat_10_discount";
            }

            // Calculate shipping fee and gift wrap fee
            int numPackages = totalQuantity / 10;
            int remainingItems = totalQuantity % 10;
            double shippingFee = numPackages * 5;
            double giftWrapFee = 0;

            for (int i = 0; i < 3; i++)
            {
                if (catalog[i].GiftWrapped)
                    giftWrapFee += catalog[i].quantity;
            }

            giftWrapFee *= 1; // Assuming gift wrap fee is $1 per unit

            // Calculate the total
            double total = subtotal - discountAmount + shippingFee + giftWrapFee;

            // Output details
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Product: " + catalog[i].name + ", Quantity: " + catalog[i].quantity + ", Total: " + (catalog[i].price * catalog[i].quantity));
            }

            Console.WriteLine("Subtotal: " + subtotal);
            Console.WriteLine("Discount Applied: " + discountName + ", Amount: " + discountAmount);
            Console.WriteLine("Shipping Fee: " + shippingFee);
            Console.WriteLine("Gift Wrap Fee: " + giftWrapFee);
            Console.WriteLine("Total: " + total);
        }
    }
}
