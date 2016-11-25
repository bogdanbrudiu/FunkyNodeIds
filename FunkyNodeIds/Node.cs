namespace FunkyNodeIds
{
    public class Node
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public static Node Parse(string pair) {
            return new Node() {
                Name = pair.Split('/')[0],
                Number = int.Parse(pair.Split('/')[1])
            };
            
        } 
    }
}
