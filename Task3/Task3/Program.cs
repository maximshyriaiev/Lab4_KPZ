namespace Task3
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Composer
    {
        // Абстрактний клас вузла розмітки
        public abstract class LightNode
        {
            public abstract string OuterHTML { get; }
            public abstract string InnerHTML { get; }
        }

        // Клас для текстового вузла розмітки
        public class LightTextNode : LightNode
        {
            private string text;

            public LightTextNode(string text)
            {
                this.text = text;
            }

            public override string OuterHTML => text;
            public override string InnerHTML => text;
        }

        // Перелік типів відображення елементу
        public enum DisplayType
        {
            Block,
            Inline
        }

        // Перелік типів закриття елементу
        public enum ClosingType
        {
            SingleTag,
            ClosingTag
        }

        // Клас для елемента розмітки
        public class LightElementNode : LightNode
        {
            public string TagName { get; }
            public DisplayType Display { get; }
            public ClosingType Closing { get; }
            public List<string> Classes { get; }
            public List<LightNode> Children { get; }
            private Dictionary<string, List<Action>> eventListeners = new Dictionary<string, List<Action>>();

            public LightElementNode(string tagName, DisplayType display, ClosingType closing, List<string> classes, List<LightNode> children)
            {
                TagName = tagName;
                Display = display;
                Closing = closing;
                Classes = classes;
                Children = children;
            }

            // Додавання EventListener
            public void AddEventListener(string eventType, Action action)
            {
                if (!eventListeners.ContainsKey(eventType))
                {
                    eventListeners[eventType] = new List<Action>();
                }
                eventListeners[eventType].Add(action);
            }

            // Виклик події
            public void DispatchEvent(string eventType)
            {
                if (eventListeners.ContainsKey(eventType))
                {
                    foreach (var listener in eventListeners[eventType])
                    {
                        listener();
                    }
                }
            }

            public override string OuterHTML
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append($"<{TagName}");

                    foreach (var cls in Classes)
                    {
                        sb.Append($" class=\"{cls}\"");
                    }

                    if (Closing == ClosingType.SingleTag)
                    {
                        sb.Append(" />");
                    }
                    else
                    {
                        sb.Append(">");

                        foreach (var child in Children)
                        {
                            sb.Append(child.OuterHTML);
                        }

                        sb.Append($"</{TagName}>");
                    }

                    return sb.ToString();
                }
            }

            public override string InnerHTML
            {
                get
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var child in Children)
                    {
                        sb.Append(child.OuterHTML);
                    }

                    return sb.ToString();
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.OutputEncoding = System.Text.Encoding.Default;

                // Створення елементів розмітки
                var paragraph = new LightElementNode("p", DisplayType.Block, ClosingType.ClosingTag, new List<string> { "paragraph" }, new List<LightNode>
            {
                new LightTextNode("Це перший абзац."),
                new LightTextNode("Це другий абзац.")
            });

                // Додавання EventListener для click
                paragraph.AddEventListener("click", () =>
                {
                    Console.WriteLine("Paragraph clicked!");
                });

                var listItems = new List<LightNode>
            {
                new LightTextNode("Пункт 1"),
                new LightTextNode("Пункт 2"),
                new LightTextNode("Пункт 3")
            };

                var unorderedList = new LightElementNode("ul", DisplayType.Block, ClosingType.ClosingTag, new List<string> { "list" }, listItems);

                // Додавання EventListener для mouseover
                unorderedList.AddEventListener("mouseover", () =>
                {
                    Console.WriteLine("Mouse over list!");
                });

                // Виведення елементів розмітки
                Console.WriteLine("Зовнішній HTML:");
                Console.WriteLine(paragraph.OuterHTML);

                Console.WriteLine("\nВнутрішній HTML:");
                Console.WriteLine(paragraph.InnerHTML);

                Console.WriteLine("\nСписок:");
                Console.WriteLine(unorderedList.OuterHTML);

                // Виклик події click
                paragraph.DispatchEvent("click");

                // Виклик події mouseover
                unorderedList.DispatchEvent("mouseover");
            }
        }
    }

}