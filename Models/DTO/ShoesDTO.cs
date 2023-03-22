namespace PRN211_ShoesStore.Models.DTO
{
    public class ShoesDTO
    {
        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public decimal price { get; set; }

        public byte[] image { get; set; }

        public long quantity { get; set; }

        public ShoesDTO()
        {

        }

        public ShoesDTO(int id, string name, string description, decimal price, byte[] image, long quantity)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.image = image;
            this.quantity = quantity;
        }
    }
}
