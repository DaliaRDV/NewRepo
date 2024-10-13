using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI_2024_III_PI_EXAMENI_PROGRA_2021130021
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numFacturas = 3;
            const int numDetalles = 3;

             
            int[] numeroFactura = new int[numFacturas];
            DateTime[] fechaFactura = new DateTime[numFacturas];
            string[] nombreCliente = new string[numFacturas];
            decimal[] subTotalFactura = new decimal[numFacturas];
            decimal[] ISVFactura = new decimal[numFacturas];
            decimal[] descuentoFactura = new decimal[numFacturas];
            decimal[] totalPagar = new decimal[numFacturas];

             
            int[,] linea = new int[numFacturas, numDetalles];
            string[,] producto = new string[numFacturas, numDetalles];
            int[,] cantidad = new int[numFacturas, numDetalles];
            decimal[,] precio = new decimal[numFacturas, numDetalles];
            decimal[,] descuentoDetalle = new decimal[numFacturas, numDetalles];
            decimal[,] ISVDetalle = new decimal[numFacturas, numDetalles];
            decimal[,] subTotalDetalle = new decimal[numFacturas, numDetalles];

            for (int i = 0; i < numFacturas; i++)
            {
                numeroFactura[i] = i + 1;   

                Console.Write("Ingrese la fecha de la factura (yyyy-mm-dd): ");
                fechaFactura[i] = DateTime.Parse(Console.ReadLine());   

                Console.Write("Ingrese el nombre del cliente: ");
                nombreCliente[i] = Console.ReadLine();    

                for (int j = 0; j < numDetalles; j++)
                {
                    linea[i, j] = j + 1;

                    Console.Write("Ingrese el producto: ");
                    producto[i, j] = Console.ReadLine();

                    Console.Write("Ingrese la cantidad: ");
                    cantidad[i, j] = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese el precio: ");
                    precio[i, j] = decimal.Parse(Console.ReadLine());

                    Console.Write("Ingrese el descuento (ejemplo 0.10 para 10%): ");
                    descuentoDetalle[i, j] = decimal.Parse(Console.ReadLine());

                    Console.Write("Ingrese el ISV (ejemplo 0.15 para 15%): ");
                    ISVDetalle[i, j] = decimal.Parse(Console.ReadLine());

                    subTotalDetalle[i, j] = CalcularSubTotalDetalle(cantidad[i, j], precio[i, j], descuentoDetalle[i, j], ISVDetalle[i, j]);
                }

                subTotalFactura[i] = 0;  
                ISVFactura[i] = 0;    
                descuentoFactura[i] = 0;    

                for (int j = 0; j < numDetalles; j++)
                {
                    subTotalFactura[i] += subTotalDetalle[i, j];
                    descuentoFactura[i] += cantidad[i, j] * precio[i, j] * descuentoDetalle[i, j];   
                    ISVFactura[i] += cantidad[i, j] * precio[i, j] * ISVDetalle[i, j];  
                }

                totalPagar[i] = subTotalFactura[i] - descuentoFactura[i] + ISVFactura[i];

                ImprimirFactura(i, numeroFactura[i], fechaFactura[i], nombreCliente[i], subTotalFactura[i], descuentoFactura[i], ISVFactura[i], totalPagar[i], producto, cantidad, precio, subTotalDetalle);
            }
        }

        static decimal CalcularSubTotalDetalle(int cantidad, decimal precio, decimal descuento, decimal isv)
        {
            decimal subTotal = cantidad * precio;
            decimal descuentoCalculado = subTotal * descuento;
            decimal isvCalculado = subTotal * isv;
            return subTotal - descuentoCalculado + isvCalculado;
        }

        static void ImprimirFactura(int index, int numeroFactura, DateTime fechaFactura, string nombreCliente, decimal subTotalFactura, decimal descuentoFactura, decimal ISVFactura, decimal totalPagar, string[,] producto, int[,] cantidad, decimal[,] precio, decimal[,] subTotalDetalle)
        {
            Console.WriteLine($"\nFactura No: {numeroFactura}");
            Console.WriteLine($"Fecha: {fechaFactura.ToShortDateString()}");
            Console.WriteLine($"Cliente: {nombreCliente}");
            Console.WriteLine("Detalles:");

            for (int j = 0; j < producto.GetLength(1); j++)
            {
                Console.WriteLine($"Producto: {producto[index, j]}, Cantidad: {cantidad[index, j]}, Precio: {precio[index, j]}, SubTotal: {subTotalDetalle[index, j]}");
            }

            Console.WriteLine($"SubTotal Factura: {subTotalFactura}");
            Console.WriteLine($"Descuento Factura: {descuentoFactura}");
            Console.WriteLine($"ISV Factura: {ISVFactura}");
            Console.WriteLine($"Total a Pagar: {totalPagar}\n");
        }
    }
}
