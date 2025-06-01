namespace Calculator
{
    public enum Type
    {
        Function,
        Operator
    }
    public class Operation
    {
        public int argc;
        public Type type;
        public int priority;
        public Func<decimal[], decimal> conduct;
        public Operation(int argc, Type type, int priority, Func<decimal[], decimal> conduct)
        {
            this.argc = argc;
            this.type = type;
            this.priority = priority;
            this.conduct = conduct;
        }
    }
    public static class Calculator
    {
        private static Dictionary<string, Operation> Operations = new Dictionary<string, Operation>(){
            {"sqrt",new Operation(1,Type.Function,3,
            (argArray)=>{
                if (argArray[0] < 0)
                {
                    throw new ArgumentException("Cannot compute square root of a negative number.");
                }
                return (decimal)Math.Sqrt((double)argArray[0]); })},
            {"sin",new Operation(1,Type.Function,3,
            (argArray)=>(decimal)Math.Sin((double)argArray[0]))},
            {"cos",new Operation(1,Type.Function,3,
            (argArray)=>(decimal)Math.Cos((double)argArray[0]))},
            {"tan",new Operation(1,Type.Function,3,
            (argArray)=>(decimal)Math.Tan((double)argArray[0]))},
            {"ln",new Operation(1,Type.Function,3,
            (argArray)=>{
                if (argArray[0] <= 0)
                    {
                        throw new ArgumentException("Cannot compute natural logarithm of a non-positive number.");
                    }
                return (decimal)Math.Log((double)argArray[0]);
            })},
            {"exp",new Operation(1,Type.Function,3,
            (argArray)=>(decimal)Math.Exp((double)argArray[0]))},
            {"log",new Operation(2,Type.Function,3,//log(value,base)
            (argArray)=>(decimal)(Math.Log((double)argArray[0]) / Math.Log((double)argArray[1])))},
            {"max",new Operation(-1,Type.Function,3,
            (argArray)=>argArray.Max())},//TODO
            {"min",new Operation(-1,Type.Function,3,
            (argArray)=>argArray.Min())},
            {"+",new Operation(2,Type.Operator,0,
            (argArray)=>argArray[0] + argArray[1])},
            {"-",new Operation(2,Type.Operator,0,
            (argArray)=>argArray[0] - argArray[1])},
            {"*",new Operation(2,Type.Operator,1,
            (argArray)=>argArray[0] * argArray[1])},
            {"/",new Operation(2,Type.Operator,1,
            (argArray)=>{
                if (argArray[1] == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                return argArray[0] / argArray[1];
                })},
            {"^",new Operation(2,Type.Operator,2,
            (argArray)=>(decimal)Math.Pow((double)argArray[0], (double)argArray[1]))}
        };
        private static decimal ConductOperation(string operation, params decimal[] argArray)
        {
            if (!Operations.ContainsKey(operation))
            {
                throw new ArgumentException($"Invalid operation: {operation}");
            }
            if (Operations[operation].argc != argArray.Length && Operations[operation].argc != -1)
            {
                throw new ArgumentException($"Invalid arg count {argArray.Length} for {operation},expecting {Operations[operation].argc}");
            }
            return Operations[operation].conduct(argArray);
        }
        private static decimal CalculateHead(ref Stack<decimal> numbers, ref Stack<string> operators, ref Stack<int> argLengths)
        {
            string ope = operators.Pop();
            if (Operations[ope].type == Type.Function && Operations[ope].argc != argLengths.Peek() && Operations[ope].argc != -1)
            {
                throw new ArgumentException($"Invalid arg count {argLengths.Peek()} for {ope}, expecting{Operations[ope]}");
            }
            Stack<decimal> args = new Stack<decimal>();
            for (int i = 0; i < (Operations[ope].type == Type.Function ? argLengths.Peek() : Operations[ope].argc); ++i)
            {
                args.Push(numbers.Pop());
            }
            if (Operations[ope].type == Type.Function)
                argLengths.Pop();
            return ConductOperation(ope, args.ToArray());
        }

        private static int GetPriority(string ope)
        {
            if (ope == "(") return -1;
            if (!Operations.ContainsKey(ope))
            {
                throw new ArgumentException("Unknown priority for " + ope);
            }
            return Operations[ope].priority;
        }
        public static decimal Calculate(string s)
        {
            Stack<decimal> numbers = new Stack<decimal>();
            Stack<string> operators = new Stack<string>();
            Stack<int> argCountStack = new Stack<int>();
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
                else if (s[i] == '-' && "(,".Contains(tokens[tokens.Count - 1]))
                {
                    tokens.Add("-1");
                    tokens.Add("*");
                    ++i;
                }
                else if ("+-*/^(),".Contains(s[i]))
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
            foreach (string token in tokens)
            {
                if (token == ",")
                {
                    while (operators.Peek() != "(")
                    {
                        numbers.Push(CalculateHead(ref numbers, ref operators, ref argCountStack));
                    }
                    argCountStack.Push(argCountStack.Pop() + 1);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    while (operators.Peek() != "(")
                    {
                        numbers.Push(CalculateHead(ref numbers, ref operators, ref argCountStack));
                    }
                    operators.Pop();
                    if (operators.Count > 0 && Operations.ContainsKey(operators.Peek()) && Operations[operators.Peek()].type == Type.Function)
                    {
                        numbers.Push(CalculateHead(ref numbers, ref operators, ref argCountStack));
                    }
                }
                else if (Operations.ContainsKey(token))
                {
                    if (Operations[token].type == Type.Operator)
                    {
                        while (GetPriority(token) <= GetPriority(operators.Peek()) &&
                        !(token == "^" && GetPriority(token) == GetPriority(operators.Peek())))
                        {
                            numbers.Push(CalculateHead(ref numbers, ref operators, ref argCountStack));
                        }
                        operators.Push(token);
                    }
                    else if (Operations[token].type == Type.Function)
                    {
                        while (GetPriority(token) <= GetPriority(operators.Peek()) &&
                        !(token == "^" && GetPriority(token) == GetPriority(operators.Peek())))
                        {
                            numbers.Push(CalculateHead(ref numbers, ref operators, ref argCountStack));
                        }
                        operators.Push(token);
                        argCountStack.Push(1);
                    }
                }
                else
                {
                    numbers.Push(decimal.Parse(token));
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