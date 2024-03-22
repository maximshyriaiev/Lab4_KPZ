namespace Task5
{
    // Клас, що представляє текстовий документ
    public class TextDocument
    {
        private string content;

        public TextDocument(string initialContent)
        {
            content = initialContent;
        }

        // Метод для зміни вмісту документу
        public void SetContent(string newContent)
        {
            content = newContent;
        }

        // Метод для отримання поточного вмісту документу
        public string GetContent()
        {
            return content;
        }
    }

    // Клас, що відповідає за зберігання стану документа
    public class Caretaker
    {
        private Stack<TextDocument> history = new Stack<TextDocument>();

        // Метод для збереження стану документа
        public void Save(TextDocument document)
        {
            history.Push(new TextDocument(document.GetContent()));
        }

        // Метод для відновлення попереднього стану документа
        public void Undo(TextDocument document)
        {
            if (history.Count > 0)
            {
                var lastState = history.Pop();
                document.SetContent(lastState.GetContent());
            }
            else
            {
                Console.WriteLine("Cannot undo. History is empty.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextDocument document = new TextDocument("Initial content");
            Caretaker caretaker = new Caretaker();

            // Виконуємо зміни в документі
            document.SetContent("Updated content");
            caretaker.Save(document);

            // Виконуємо ще одну зміну та зберігаємо стан
            document.SetContent("New content");
            caretaker.Save(document);

            // Показуємо поточний стан документа
            Console.WriteLine("Current content:");
            Console.WriteLine(document.GetContent());

            // Відновлюємо попередній стан документа
            caretaker.Undo(document);

            // Показуємо оновлений стан документа
            Console.WriteLine("\nAfter undo:");
            Console.WriteLine(document.GetContent());
        }
    }
}