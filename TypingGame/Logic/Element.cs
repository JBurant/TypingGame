namespace TypingGame.Logic
{
    public class Element
    {
        public string Text { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Speed { get; set; }
        public ColorRGBA Color { get; }
        public bool IsHit { get; private set; }

        public Element()
        { }

        public Element(string text, ColorRGBA color, decimal speed, decimal x, decimal y)
        {
            Text = text;
            Color = color;
            Speed = speed;
            X = x;
            Y = y;
        }

        public void Move(decimal deltaTime)
        {
            X += deltaTime * Speed;
        }

        public bool TryHit(string letterToRemove)
        {
            if(Text.Length > 0 && Text.Substring(0, 1) == letterToRemove)
            {
                Text = Text.Remove(0, 1);
                IsHit = string.IsNullOrWhiteSpace(Text) ? false : true;
                return true;
            }

            return false;
       }
    }
}
