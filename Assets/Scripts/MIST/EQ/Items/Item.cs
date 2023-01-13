using UnityEngine;

namespace MIST.EQ.Items
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private string _name;
        [SerializeField] private int _price;

        public int Id { get { return _id; } }
        public Sprite Sprite { get { return _sprite; } }
        public Color Color { get { return _color; } }
        public string Name { get { return _name; } }
        public int Price { get { return _price; } }
    }
}

