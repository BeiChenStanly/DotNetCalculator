namespace Calculator
{
    static class Calculator
    {
        public static decimal ConductOperation(decimal left, string operation, decimal right = 0)
        {
            decimal result = 0;
            if (!isOperator(operation) && !isFunction(operation))
            {
                throw new ArgumentException($"Invalid operation: {operation}");
            }
            switch (operation)
            {
                case "+":
                    result = left + right;
                    break;
                case "-":
                    result = left - right;
                    break;
                case "*":
                    result = left * right;
                    break;
                case "/":
                    if (right == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    result = left / right;
                    break;
                case "^":
                    result = (decimal)Math.Pow((double)left, (double)right);
                    break;
                case "sqrt":
                    if (left < 0)
                    {
                        throw new ArgumentException("Cannot compute square root of a negative number.");
                    }
                    result = (decimal)Math.Sqrt((double)left);
                    break;
                case "sin":
                    result = (decimal)Math.Sin((double)left);
                    break;
                case "cos":
                    result = (decimal)Math.Cos((double)left);
                    break;
                case "tan":
                    result = (decimal)Math.Tan((double)left);
                    break;
                case "ln":
                    if (left <= 0)
                    {
                        throw new ArgumentException("Cannot compute natural logarithm of a non-positive number.");
                    }
                    result = (decimal)Math.Log((double)left);
                    break;
            }
            return result;
        }

        private static bool isOperator(string s)
        {
            return s == "+" || s == "-" || s == "*" || s == "/" || s == "^";
        }

        private static bool isFunction(string s)
        {
            return s == "sqrt" || s == "ln" || s == "sin" || s == "cos" || s == "tan";
        }

        private static decimal CalculateHead(ref Stack<decimal> numbers, ref Stack<string> operators)
        {
            string ope = operators.Pop();
            decimal result = 0;
            if (isOperator(ope))
            {
                decimal r = numbers.Pop();
                decimal l = numbers.Pop();
                result = ConductOperation(l, ope, r);
            }
            else if (isFunction(ope))
            {
                decimal x = numbers.Pop();
                result = ConductOperation(x, ope);
            }
            return result;
        }

        private static int GetPriority(string ope)
        {
            if (isFunction(ope))
            {
                return 3;
            }
            switch (ope)
            {
                case "+":
                case "-":
                    return 0;
                case "*":
                case "/":
                    return 1;
                case "^":
                    return 2;
                case "(":
                    return -1;
                default:
                    throw new ArgumentException("Unknown priority for " + ope);
            }
        }

        private static List<string> DivideIntoTokens(string s)
        {
            List<string> tokens = ["("];
            int i = 0;
            while (i < s.Length)
            {
                if (char.IsWhiteSpace(s[i])) i++;
                else if (char.IsDigit(s[i]))
                {
                    string temp = "";
                    while (i < s.Length && (char.IsDigit(s[i]) || s[i] == '.'))
                    {
                        temp += s[i++];
                    }
                    tokens.Add(temp);
                }
                else if (s[i] == '-' && tokens[tokens.Count - 1] == "(")
                {
                    tokens.Add("-1");
                    tokens.Add("*");
                    ++i;
                }
                else if (isOperator("" + s[i]) || s[i] == ')' || s[i] == '(')
                {
                    tokens.Add("" + s[i++]);
                }
                else if (char.IsLetter(s[i]))
                {
                    string temp = "";
                    while (i < s.Length && char.IsLetter(s[i]))
                    {
                        temp += s[i++];
                    }
                    tokens.Add(temp);
                }
                else
                {
                    throw new ArgumentException("unknown:" + s[i]);
                }
            }
            tokens.Add(")");
            return tokens;
        }

        public static decimal Calculate(string s)
        {
            Stack<decimal> numbers = new Stack<decimal>();
            Stack<string> operators = new Stack<string>();
            List<string> tokens = DivideIntoTokens(s);
            for (int i = 0; i < tokens.Count; ++i)
            {
                if (tokens[i] == "(")
                {
                    operators.Push(tokens[i]);
                }
                else if (tokens[i] == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        numbers.Push(CalculateHead(ref numbers, ref operators));
                    }
                    operators.Pop();
                }
                else if (isOperator(tokens[i]) || isFunction(tokens[i]))
                {
                    while (GetPriority(tokens[i]) <= GetPriority(operators.Peek()) &&
                    !(tokens[i] == "^" && GetPriority(tokens[i]) == GetPriority(operators.Peek())))
                    {
                        numbers.Push(CalculateHead(ref numbers, ref operators));
                    }
                    operators.Push(tokens[i]);
                }
                else
                {
                    numbers.Push(decimal.Parse(tokens[i]));
                }
            }
            return numbers.Peek();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the DotNet Calculator!");
            Console.WriteLine("It supports +, -, *, /, ^ and sqrt() operations.");
            string? input;
            while (true)
            {
                Console.Write("Enter an expression:");
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.Contains("exit") || input.Contains("quit"))
                {
                    break;
                }
                try
                {
                    Console.WriteLine(input + '=' + Calculator.Calculate(input));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}