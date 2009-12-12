namespace GenerationStudio.Elements
{
    public class ElementError
    {
        public ElementError() {}

        public ElementError(Element owner, string message)
        {
            Owner = owner;
            Message = message;
        }

        public Element Owner { get; set; }
        public string Message { get; set; }
    }
}