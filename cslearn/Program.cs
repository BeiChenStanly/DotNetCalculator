namespace cslearn
{
    #region 注释
    //单行注释
    /// <summary>
    /// 三行注释
    /// </summary>
    class C_1
    {
        public int i;
    }
    #endregion

    #region 异常捕获
    class C_2
    {
        void F_1()
        {
            try
            {
                Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("输入错误");
            }
        }
    }
    #endregion

    #region 枚举
    enum E_genral { Famale, Male, Other }
    class C_3
    {
        public E_genral genral;
    }
    #endregion

    #region 数组
    class C_4
    {
        int[] i_1 = new int[] { 1, 2, 3, 4 };//一维数组
        int[] i_2 = { 1, 2, 3, 4 };//不需要new的书写方式
        int[,] i_3 = new int[3, 3] { { 1, 2, 3 }, //二维数组
                                     { 4, 5, 6 },
                                     { 7, 8, 9 } };
        int[][] i_4 = new int[3][] { new int[] { 1, 2, 3 },//交错数组
                                     new int[] { 4, 5 },
                                     new int[] { 6, 7, 8, 9 } };
    }
    #endregion

    #region 函数
    class C_5
    {
        public int Plas(int a, int b)//函数定义
        {
            return a + b;//返回值
        }


        public static void ChangeValue(ref int p, int v)//用ref传入的值可以在函数内部修改
        {
            //p = v;
            //ref可以不在其中赋值
        }
        public static void ChangeValue2(out int p, int v)//用out传入的值可以在函数外部不初始化
        {
            p = v;//out必须在其中赋值
        }


        public static void Speak(string name, E_genral genral = E_genral.Male, params string[] args)//不定长参数用params，可选参数用默认值，但是在此种情况中，三个参数都有，必须提供性别枚举的可选参数，且不定长参数必须放在后面
        {
            switch (genral)//switch语句与枚举联用
            {
                case E_genral.Famale:
                    foreach (string arg in args)//foreach循环用来遍历数组
                    {
                        Console.WriteLine($"{name}女士说：{arg}");
                    }
                    break;
                case E_genral.Male:
                    foreach (string arg in args)
                    {
                        Console.WriteLine($"{name}男士说：{arg}");
                    }
                    break;
                case E_genral.Other:
                    Console.WriteLine("性别错误");
                    break;
                default:
                    break;
            }
        }

        public static void Print(string arg) { Console.WriteLine(arg); }//函数的重载
        public static void Print(int arg) { Console.WriteLine(arg); }
        public static void Print(float arg) { Console.WriteLine(arg); }


        public static void Count(int x)//递归
        {
            if (x >= 10) { return; }
            Console.WriteLine(x);
            Count(++x);
        }
    }
    #endregion

    #region 结构体
    struct S_1
    {
        int age;
        E_genral genral;
        public S_1(int age, E_genral genral)//声明构造函数
        {
            this.age = age;//构造函数中的赋值
            this.genral = genral;
        }
        public void speak()
        {
            Console.WriteLine($"我今年{age}岁了");
        }
    }
    #endregion

    #region 排序
    class PaiXu
    {
        #region 冒泡排序
        public static int[] MaoPaoPaiXu(ref int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                    }
                }
            }
            return arr;
        }
        #endregion

        #region 选择排序
        public static int[] XuanZePaiXu(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int index = 0;
                for (int j = 1; j < arr.Length - i; j++)
                {
                    if (arr[index] < arr[j])
                    {
                        index = j;
                    }
                }
                if (index != arr.Length - 1 - i)
                {
                    int temp = arr[index];
                    arr[index] = arr[arr.Length - 1 - i];
                    arr[arr.Length - 1 - i] = temp;
                }
            }
            return arr;
        }
        #endregion
    }
    #endregion

    #region 属性
    class Person
    {
        private string name;
        private int age;
        private int money;
        private E_genral genral;
        public Person(string name, int age, int money, E_genral genral)//构造函数
        {
            this.name = name;
            this.age = age;
            this.money = money;
            this.genral = genral;
        }
        public string Name
        {
            get { return name; }
            private set { name = value; }//名字不能改qwq
        }
        public int Age
        {
            get { return age; }
            set { if (age > 0) age = value; else age = 0; }//逻辑处理：年龄不能为负
        }
        public int Money
        {
            get { return (money - 100) / 2; }//加密
            set { money = value * 2 + 100; }
        }
        public E_genral Genral
        {
            get { return genral; }//没有set，只能得不能改
        }
        public int Height//自动属性，可以节约代码量，甚至不用申明变量
        {
            get;
            set;
        }
    }
    #endregion

    #region 索引器
    class Student
    {
        public string name;
        public Student(string name)//构造函数
        {
            this.name = name;
        }
    }
    class Class
    {
        Student[] students;
        public Class(Student[] students)//构造函数
        {
            this.students = students;
        }
        public Student this[int i]//索引器
        {
            get
            {
                if (students.Length - 1 < i)//如果溢出了
                {
                    return new Student("OverFlow");
                }
                return students[i];
            }
            set
            {
                if (students == null)//如果students未赋值
                {
                    students = new Student[1] { value };
                }
                if (students.Length - 1 >= i)//如果没有溢出
                {
                    students[i] = value;
                }
                else if (students.Length - 1 < i)//如果溢出了
                {
                    Student[] mid = new Student[i + 1];//中间商，扩展长度
                    for (int n = 0; n < students.Length; n++)//把原来的搬到中间商里
                    {
                        mid[n] = students[n];
                    }
                    for (int m = students.Length; m < i; m++)//把中间可能的空位用default补齐
                    {
                        mid[m] = new Student("Default");
                    }
                    mid[i] = value;
                    students = mid;
                }
            }
        }
    }
    #endregion

    #region 拓展方法
    public static class Extend
    {
        public static void Sord(this int[] value)//默认引用传递，不需要ref
        {
            for (int i = 0; i < value.Length - 1; i++)
            {
                for (int j = 0; j < value.Length - 1 - i; j++)
                {
                    if (value[j] > value[j + 1])
                    {
                        int temp = value[j];
                        value[j] = value[j + 1];
                        value[j + 1] = temp;
                    }
                }
            }
        }
    }
    #endregion

    #region 运算符重载
    class Vector2
    {
        public int x;
        public int y;
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public static Vector2 operator +(Vector2 a, Vector2 b)//运算符重载
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator +(Vector2 a, int value)//一个运算符可以有多个重载
        {
            return new Vector2(a.x + value, a.y + value);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static bool operator ==(Vector2 a, Vector2 b)//成对的条件运算符，如==和!=，必须一起实现
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Vector2 a, Vector2 b)//成对的条件运算符，如==和!=，必须一起实现
        {
            return a.x != b.x || a.y != b.y;
        }
    }
    #endregion

    #region 继承
    class Teacher
    {
        public string name;
        protected int number;//protected，外部不可以访问，子类可以继承//工号
        private E_genral genral;
        public Teacher(string name, int number, E_genral genral)
        {
            this.name = name;
            this.number = number;
            this.genral = genral;
        }
    }
    class TeachingTeacher : Teacher
    {
        public string subject;
        public TeachingTeacher(string name, int number, E_genral genral, string subject) : base(name, number, genral)
        {
            this.subject = subject;
        }
        public void IntroduceSubject()
        {
            Console.WriteLine("I teach " + subject);
        }
    }
    class ChineseTeacher : TeachingTeacher//可以继承父类的父类里的东西
    {
        public ChineseTeacher(string name, int number, E_genral genral, string subject) : base(name, number, genral, subject)
        {
        }
        public void Skill()
        {
            Console.WriteLine("门泊东吴万里船");
        }
    }
    #endregion

    #region 里氏替换原则
    class GameObject
    {
        public string name;
        public GameObject(string name)
        {
            this.name = name;
        }
    }
    class Player : GameObject
    {
        public Player(string name) : base(name) { }
        public void PlayerAtk() { Console.WriteLine("玩家攻击"); }
    }
    class Boss : GameObject
    {
        public Boss(string name) : base(name) { }
        public void BossAtk() { Console.WriteLine("Boss攻击"); }
    }
    #endregion

    #region 密封类
    sealed class SealedClass { }
    //class Son:SealedClass{}     //这样会报错，因为密封类不能被继承
    #endregion

    #region vob&抽象类&接口（多态
    abstract class Graphics//抽象类不能被实例化
    {
        public abstract double Area(double a, double b);//抽象方法必须在子类实现，父类不能有方法体,必须写在抽象类中
        public virtual double Area(double a, double b, double c)
        {
            Console.WriteLine("无法用三个参数计算,故默认返回0");
            return 0;
        }
        public virtual double Perimeter(double a, double b)
        {
            Console.WriteLine("无法用两个参数计算,故默认返回0");
            return 0;
        }
        public virtual double Perimeter(double a, double b, double c)
        {
            Console.WriteLine("无法用三个参数计算,故默认返回0");
            return 0;
        }
    }
    class Rectangle : Graphics
    {
        /// <summary>
        /// 给定长和宽，求矩形面积
        /// </summary>
        /// <param name="a">长</param>
        /// <param name="b">宽</param>
        /// <returns>面积</returns>
        public override double Area(double a, double b)
        {
            return a * b;
        }
        /// <summary>
        /// 给定长和宽，求矩形周长
        /// </summary>
        /// <param name="a">长</param>
        /// <param name="b">宽</param>
        /// <returns>周长</returns>
        public override double Perimeter(double a, double b)
        {
            return 2 * (a + b);
        }
    }
    class Triangle : Graphics
    {
        /// <summary>
        /// 给定底和高，求三角形面积
        /// </summary>
        /// <param name="a">底</param>
        /// <param name="b">高</param>
        /// <returns>面积</returns>
        public override double Area(double a, double b)
        {
            return a * b / 2;
        }
        /// <summary>
        /// 给定三条边，求三角形面积
        /// </summary>
        /// <param name="a">一边</param>
        /// <param name="b">另一边</param>
        /// <param name="c">第三边</param>
        /// <returns>面积</returns>
        public override double Area(double a, double b, double c)
        {
            double p = a + b + c;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        /// <summary>
        /// 给定三条边，求三角形周长
        /// </summary>
        /// <param name="a">一边</param>
        /// <param name="b">第二边</param>
        /// <param name="c">第三边</param>
        /// <returns>周长</returns>
        public override double Perimeter(double a, double b, double c)
        {
            return a + b + c;
        }
    }


    interface IMove
    {
        void Move();//接口内不写方法体
    }
    interface IFly : IMove//接口可以继承接口
    {
        void Fly();
    }
    interface IWalk : IMove
    {
        void Walk();
    }
    class Animal : IMove
    {
        public void Move()
        {
            Console.WriteLine("动物移动");
        }
    }
    class Human : Animal, IWalk
    {
        public void Walk()
        {
            Console.WriteLine("人行走");
        }
    }
    class Bird : Animal, IFly
    {
        public void Fly()
        {
            Console.WriteLine("鸟飞");
        }
    }
    class Plane : IFly
    {
        public void Move()
        {
            Console.WriteLine("飞机移动");
        }
        public void Fly()
        {
            Console.WriteLine("飞机飞");
        }
    }
    #endregion

    #region 泛型
    class MyArrayList<T>
    {
        public T[] array;
        public int count;
        public MyArrayList() { }
        public MyArrayList(T[] values)
        {
            array = values;
            count = array.Length;
        }
        public void Add(T item)
        {
            T[] array2 = new T[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = array[i];
            }
            array2[array.Length] = item;
            array = array2;
            count = array.Length;
        }
        public T this[int i]
        {
            get
            {
                if (array.Length - 1 < i)//如果溢出了
                {
                    throw new IndexOutOfRangeException();
                }
                return array[i];
            }
            set
            {
                if (array == null)//如果students未赋值
                {
                    array = new T[1] { value };
                }
                if (array.Length - 1 >= i)//如果没有溢出
                {
                    array[i] = value;
                }
                else if (array.Length - 1 < i)//如果溢出了
                {
                    T[] mid = new T[i + 1];//中间商，扩展长度
                    for (int n = 0; n < array.Length; n++)//把原来的搬到中间商里
                    {
                        mid[n] = array[n];
                    }
                    for (int m = array.Length; m < i; m++)//把中间可能的空位用default补齐
                    {
                        mid[m] = default(T);
                    }
                    mid[i] = value;
                    array = mid;
                }
                count = array.Length;
            }
        }
    }
    #endregion

    #region 泛型约束
    class Test1
    {

    }
    class Test2 : Test1
    {

    }
    interface I_Test
    {

    }
    class Test3 : I_Test
    {

    }
    interface I_Test2 : I_Test
    {

    }
    class Ys1<T> where T : struct//struct值类型；class引用类型；new()此类型必须是有一个public的无参构造函数的非抽象类型（即可以在外部用new()创建新对象）
    {
        public T Value { get; set; }
        public Ys1(T value) { Value = value; }
    }
    class Ys2<T> where T : Test1//后面加类名，只能是这个类及其子类
    {

    }
    class Ys3<T> where T : I_Test//后面加接口，必须是这个接口及其子类、子接口
    {

    }
    class Ys4<T, U> where T : U//T必须是U或者U的子类
    {

    }

    class Ys5<T> where T : class, new()//对同一个泛型类型组合使用多个约束规则
    {

    }

    class Ys6<T, K> where T : class, new() where K : struct
    {

    }
    #endregion

    #region MyLinkedList

    class MyLinkedNude<T>
    {
        public T value;
        public MyLinkedNude<T> nextNude;

        public MyLinkedNude(T value)
        {
            this.value = value;
        }
    }
    class MyLinkedList<T>
    {
        public MyLinkedNude<T> head;
        public MyLinkedNude<T> tail;

        public void Add(T item)
        {
            MyLinkedNude<T> nude = new MyLinkedNude<T>(item);
            if (head == null)
            {
                head = nude;
                tail = nude;
            }
            else
            {
                tail.nextNude = nude;
                tail = nude;
            }
        }

        public void Remove(T item)
        {
            if (head == null) return;
            if (head.value.Equals(item))
            {
                head = head.nextNude;
                if (head == null)
                {
                    tail = null;
                }
                return;
            }
            MyLinkedNude<T> nude = head;
            while (nude.nextNude != null)
            {
                if (nude.nextNude.value.Equals(item))
                {
                    if (nude.nextNude.Equals(tail))
                    {
                        nude.nextNude = nude.nextNude.nextNude;
                        tail = nude;
                        break;
                    }
                    nude.nextNude = nude.nextNude.nextNude;
                    break;
                }
                nude = nude.nextNude;
            }
        }
    }
    #endregion

    #region 自定义类的排序
    class Item : IComparable<Item>
    {
        public int value;

        public int CompareTo(Item? other)
        {
            if (other.value > value) return -1;
            else return 1;
        }

        public Item(int value) { this.value = value; }
    }
    class Boom
    {
        public int damage;
        public Boom(int damage) { this.damage = damage; }
    }
    #endregion
    class Program
    {
        static void Main(string[] args)
        {
            #region 枚举
            C_1 c_1 = new C_1();
            c_1.i = 1;
            Console.WriteLine(c_1.i);
            C_3 c_3 = new C_3();
            c_3.genral = E_genral.Famale;
            Console.WriteLine(c_3.genral.ToString());
            #endregion

            #region 函数
            int a = 10;
            C_5.ChangeValue(ref a, 100);//ref的用法：传入前必须赋值
            Console.WriteLine(a);
            int b;
            C_5.ChangeValue2(out b, 10);//out的用法：传入前可以不赋值
            Console.WriteLine(b);

            C_5.Print(99);//函数的重载
            C_5.Print("字符串");
            C_5.Print(99.0f);

            C_5.Count(1);//递归函数
            #endregion

            #region 结构体
            S_1 s_1 = new S_1(18, E_genral.Famale);
            s_1.speak();
            #endregion

            #region 冒泡排序
            Console.WriteLine("=======冒泡排序=======");
            int[] i_1 = new int[] { 5, 2, 7, 3, 4, 9, 1, 6, 8 };
            PaiXu.MaoPaoPaiXu(ref i_1);
            for (int i = 0; i < i_1.Length; i++)
            {
                Console.WriteLine(i_1[i]);
            }
            #endregion

            #region 选择排序
            Console.WriteLine("==========选择排序==========");
            int[] i_2 = PaiXu.XuanZePaiXu(new int[] { 5, 2, 7, 3, 4, 9, 1, 6, 8 });
            for (int i = 0; i < i_2.Length; i++)
            {
                Console.WriteLine(i_2[i]);
            }
            #endregion

            #region 索引器
            Console.WriteLine("=======索引器=======");
            Class cl1 = new Class(new Student[] { new Student("小明"), new Student("小红") });
            Student st3 = new Student("小亮");
            cl1[6] = st3;
            for (int n = 0; n < 7; n++)
            {
                Console.WriteLine(cl1[n].name);
            }
            Console.WriteLine(cl1[10].name);
            #endregion

            #region 拓展方法
            Console.WriteLine("=======拓展方法=======");
            int[] i_3 = new int[] { 1, 4, 2, 8, 5, 7, 3, 6, 9, 0 };
            i_3.Sord();//默认引用传递，不需要ref
            for (int i = 0; i < i_3.Length; i++)
            {
                Console.WriteLine(i_3[i]);
            }
            #endregion

            #region 运算符重载
            Console.WriteLine("=======运算符重载=======");
            Vector2 v1 = new Vector2(1, 9);
            Vector2 v2 = new Vector2(9, 1);
            Vector2 v3 = v1 + v2;
            Console.WriteLine(v3.x.ToString() + " " + v3.y.ToString());
            Vector2 v4 = v3 + 90;
            Console.WriteLine(v4.x.ToString() + " " + v4.y.ToString());
            #endregion

            #region 继承
            Console.WriteLine("=======继承=======");
            ChineseTeacher ct1 = new ChineseTeacher("LGY", 1, E_genral.Famale, "语文");
            ct1.IntroduceSubject();
            ct1.Skill();
            #endregion

            #region 里氏替换原则
            Console.WriteLine("=======里氏替换原则=======");
            GameObject p1 = new Player("player");
            GameObject b1 = new Boss("Boss");
            GameObject[] gos = new GameObject[] { new Player("player"), new Boss("Boss") };
            if (p1 is Player)
            {
                (p1 as Player).PlayerAtk();//两种写法
            }
            if (b1 is Boss)
            {
                Boss b2 = b1 as Boss;//两种写法
                b2.BossAtk();
            }
            foreach (GameObject go in gos)
            {
                Console.WriteLine(go.name);
            }

            #endregion

            #region 万物之父
            Console.WriteLine("=======万物之父=======");
            Object o_1 = 10;
            Object o_2 = "10";
            Object[] ojs = new Object[] { 10, "100", new Vector2(10, 100) };

            string s_2 = o_2.ToString();//string类型两种写法
            string s_3 = o_2 as string;

            int i_5 = (int)o_1;//值类型的强转

            Vector2 v_6 = ojs[2] as Vector2;//引用类型的转换

            for (int i = 0; i < ojs.Length; i++)
            {
                if (ojs[i] is int) Console.WriteLine((int)ojs[i] + " 是int");
                else if (ojs[i] is string) Console.WriteLine((string)ojs[i] + " 是string");
                else if (ojs[i] is Vector2) Console.WriteLine("x:" + (ojs[i] as Vector2).x.ToString() + ",y:" + (ojs[i] as Vector2).y.ToString());
            }
            #endregion

            #region vob&抽象类&接口
            Console.WriteLine("=======vob=======");
            Graphics triangle1 = new Triangle();//多态是为了让具有同一个父类的多个子类在实现父类的方法时有不同的表现
            Graphics rectangle1 = new Rectangle();
            Graphics[] graphics = new Graphics[] { triangle1, rectangle1 };
            foreach (Graphics g in graphics)
            {
                if (g is Triangle)
                {
                    Triangle g1 = g as Triangle;
                    if (g1.Area(10, 10) != 0) { Console.WriteLine(g1.Area(10, 10)); }
                    if (g1.Area(10, 10, 10) != 0) { Console.WriteLine(g1.Area(10, 10, 10)); }
                    if (g1.Perimeter(10, 10) != 0) { Console.WriteLine(g1.Perimeter(10, 10)); }
                    if (g1.Perimeter(10, 10, 10) != 0) { Console.WriteLine(g1.Perimeter(10, 10, 10)); }
                }
                else if (g is Rectangle)
                {
                    Rectangle g1 = g as Rectangle;
                    if (g1.Area(10, 10) != 0) { Console.WriteLine(g1.Area(10, 10)); }
                    if (g1.Area(10, 10, 10) != 0) { Console.WriteLine(g1.Area(10, 10, 10)); }
                    if (g1.Perimeter(10, 10) != 0) { Console.WriteLine(g1.Perimeter(10, 10)); }
                    if (g1.Perimeter(10, 10, 10) != 0) { Console.WriteLine(g1.Perimeter(10, 10, 10)); }
                }
            }
            Console.WriteLine("=======抽象类=======");
            Graphics triangle2 = new Triangle();
            Console.WriteLine(triangle2.Area(100, 100));

            Console.WriteLine("=======接口=======");
            IFly[] iflies = new IFly[2] { new Bird(), new Plane() };//接口遵循里氏替换原则
            foreach (IFly ifly in iflies)
            {
                ifly.Fly();
                ifly.Move();
            }
            #endregion

            #region 泛型
            Console.WriteLine("======泛型======");
            MyArrayList<int> list = new MyArrayList<int>([1, 2]);
            list.Add(3);
            list[3] = 5;
            list[4] = 4;
            list[7] = 5;
            for (int i = 0; i < list.count; i++)
            {
                Console.WriteLine(list[i]);
            }
            MyArrayList<Student> list2 = new MyArrayList<Student>(new Student[] { new Student("小明") });
            list2.Add(new Student("小红"));
            for (int i = 0; i < list2.count; i++)
            {
                Console.WriteLine(list2[i].name);
            }
            #endregion

            #region 泛型约束
            Console.WriteLine("======泛型约束======");
            //Yueshu<Student> y1 = new Yueshu<Student>(new Student("小敏"));//会报错，因为约束了只能用值类型
            Ys1<int> y2 = new Ys1<int>(1);//约束了只能用值类型

            //Ys2<int> y3 = new Ys2<int>();//会报错，因为int不是Test1的子类
            Ys2<Test2> y4 = new Ys2<Test2>();//可以是Test2，因为它是Test1的子类
            Ys2<Test1> y5 = new Ys2<Test1>();//可以是Test1本身

            Ys3<I_Test> y6 = new Ys3<I_Test>();//可以是I_Test本身
            Ys3<I_Test2> y7 = new Ys3<I_Test2>();//可以是I_Test的子接口
            Ys3<Test3> y8 = new Ys3<Test3>();//可以是I_Test的子类

            Ys4<int, object> y9 = new Ys4<int, object>();//int是object的子类，故没有问题
            #endregion

            #region List
            Console.WriteLine("======List======");
            List<int> ints1 = new List<int>() { 1, 2, 3 };

            ints1.Add(1);//增
            List<int> ints2 = new List<int>() { 1, 2, 3, 1 };
            ints1.AddRange(ints2);//一次添加多个
            ints1.Insert(1, 2);//在索引为1的位置插入一个2

            ints1.Remove(1);//删1这个元素
            ints1.RemoveAt(0);//删掉索引为0的元素（第一个）
            ints1.Clear();//清空所有元素

            ints1 = ints2;
            Console.WriteLine(ints1[0]);//访问指定位置的元素
            if (ints1.Contains(1))
            {
                Console.WriteLine("存在1");
            }
            Console.WriteLine(ints1.IndexOf(1));//从前往后获取1的位置
            Console.WriteLine(ints1.LastIndexOf(1));//从后往前找到1的位置

            ints1[1] = 1;//修改

            for (int i = 0; i < ints1.Count; i++)
            {
                Console.WriteLine(ints1[i]);//遍历
            }
            foreach (int i in ints1)
            {
                Console.WriteLine(i);
            }
            #endregion

            #region Dictionary
            Console.WriteLine();
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            dictionary.Add(0, "0");//增
            dictionary.Add(1, "1");
            dictionary.Add(2, "2");

            dictionary.Remove(2);//删，若不存在也不会报错，只是没反应
            dictionary.Clear();//清空

            dictionary.Add(0, "0");
            dictionary.Add(1, "1");
            if (dictionary.ContainsKey(0))
            {
                Console.WriteLine("存在键为0的键值对");
            }
            if (dictionary.ContainsValue("1"))
            {
                Console.WriteLine("存在值为1的键值对");
            }

            dictionary[0] = "000";
            dictionary[1] = "111";//改

            foreach (int item in dictionary.Keys)
            {
                Console.WriteLine(item);
                Console.WriteLine(dictionary[item]);
            }
            foreach (string item in dictionary.Values)
            {
                Console.WriteLine(item);
            }
            foreach (KeyValuePair<int, string> item in dictionary)
            {
                Console.WriteLine($"键：{item.Key} 值：{item.Value}");
            }
            #endregion

            #region MyLinkedList
            MyLinkedList<int> myLinkedList = new MyLinkedList<int>();
            myLinkedList.Add(0);
            myLinkedList.Add(1);
            myLinkedList.Add(2);
            myLinkedList.Remove(0);
            myLinkedList.Remove(2);
            myLinkedList.Remove(1);
            #endregion

            #region 自定义类的List排序
            Console.WriteLine("List排序");
            List<Item> itemList = new List<Item>() { new Item(2), new Item(4), new Item(5), new Item(1), new Item(3) };
            foreach (Item item in itemList) { Console.WriteLine(item.value); }
            itemList.Sort();
            foreach (Item item in itemList) { Console.WriteLine(item.value); }

            Console.WriteLine("List委托排序");
            List<Boom> booms = new List<Boom>() { new Boom(3), new Boom(1), new Boom(4), new Boom(2) };
            foreach (Boom bo in booms) { Console.WriteLine(bo.damage); }
            //booms.Sort(CompareBooms);
            /*booms.Sort((Boom b1,Boom b2) =>
            {
                if (b1.damage > b2.damage) return 1;
                else return -1;
            });*/
            booms.Sort((a, b) => { return a.damage > b.damage ? 1 : -1; });
            foreach (Boom bo in booms) { Console.WriteLine(bo.damage); }
            /*static int CompareBooms(Boom b1,Boom b2)
            {
                if (b1.damage > b2.damage) return 1;
                else return -1;
            }*/
            #endregion

            #region 多线程
            Console.WriteLine("多线程");
            bool isRunning = true;
            Thread t = new Thread(() =>
            {
                while (isRunning)
                {
                    Console.WriteLine("Hello");
                }
            });
            t.Start();
            t.IsBackground = true;
            /*Console.ReadKey();
            isRunning = false;
            Console.ReadKey();*/
            #endregion
        }
    }
}