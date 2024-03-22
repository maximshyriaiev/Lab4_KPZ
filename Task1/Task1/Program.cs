namespace Task1
{
    class UserRequest
    {
        public string Issue { get; set; }
        public UserRequest(string issue)
        {
            Issue = issue;
        }
    }

    // Абстрактний клас обробника
    abstract class SupportHandler
    {
        protected SupportHandler successor;

        public void SetSuccessor(SupportHandler successor)
        {
            this.successor = successor;
        }

        public abstract void HandleRequest(UserRequest request);
    }

    // Конкретний обробник
    class Level1Support : SupportHandler
    {
        public override void HandleRequest(UserRequest request)
        {
            if (request.Issue.Equals("technical"))
            {
                Console.WriteLine("Technical support specialist will assist you.");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    // Конкретний обробник
    class Level2Support : SupportHandler
    {
        public override void HandleRequest(UserRequest request)
        {
            if (request.Issue.Equals("billing"))
            {
                Console.WriteLine("Billing support specialist will assist you.");
            }
            else if (successor != null)
            {
                successor.HandleRequest(request);
            }
        }
    }

    // Конкретний обробник
    class Level3Support : SupportHandler
    {
        public override void HandleRequest(UserRequest request)
        {
            if (request.Issue.Equals("general"))
            {
                Console.WriteLine("General support specialist will assist you.");
            }
            else
            {
                Console.WriteLine("No suitable support specialist found.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створення ланцюжка обробників
            SupportHandler level1 = new Level1Support();
            SupportHandler level2 = new Level2Support();
            SupportHandler level3 = new Level3Support();

            // Встановлення послідовності обробників
            level1.SetSuccessor(level2);
            level2.SetSuccessor(level3);

            // Симуляція запиту користувача
            UserRequest request1 = new UserRequest("technical");
            level1.HandleRequest(request1);

            UserRequest request2 = new UserRequest("billing");
            level1.HandleRequest(request2);

            UserRequest request3 = new UserRequest("general");
            level1.HandleRequest(request3);

            UserRequest request4 = new UserRequest("network");
            level1.HandleRequest(request4);
        }
    }
}