namespace MISA.WebFresher052023.API.Caclulator
{
    public class Calculator
    {
        public long Add(int x, int y)
        {
            return x + (long)y;
        }

        public long Sub(int x, int y)
        {
            return x - (long)y;
        }

        public long Mul(int x, int y)
        {
            return x * (long)y;
        }

        public double Div(int x, int y)
        {

            if( y == 0)
            {
                throw new  Exception("Không được chia cho 0");
            }
            return x / (double)y;

        }
    }
}
