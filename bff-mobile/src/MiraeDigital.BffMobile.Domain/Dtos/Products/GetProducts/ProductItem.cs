namespace MiraeDigital.BffMobile.Domain.Dtos.Products.GetProducts
{
    public class ProductItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long ProviderId { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceDiscount { get; set; }
        public string ImagePath { get; set; }
        public string PortalRoute { get; set; }
        public long AgreementTermTypeId { get; set; }

        public ProductItem() { }
        public ProductItem(long id, string name, string description, long providerId, decimal price, decimal? priceDiscount, string imagePath, string portalRoute, long agreementTermTypeId)
        {
            Id = id;
            Name = name;
            Description = description;
            ProviderId = providerId;
            Price = price;
            PriceDiscount = priceDiscount;
            ImagePath = imagePath;
            PortalRoute = portalRoute;
            AgreementTermTypeId = agreementTermTypeId;
        }
    }
}
