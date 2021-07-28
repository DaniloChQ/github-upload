namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            Servidor s = new Servidor("localhost", 9090);
            s.Start();
        }
    }
}


