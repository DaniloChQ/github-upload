namespace Cliente
{
    class Program
    {
        static void Main(string[] args)
        {
            Cliente c = new Cliente("localhost", 9090);

            c.Start();
            while (true)
            {
                c.mostrar();
            }

        }
    }
}

