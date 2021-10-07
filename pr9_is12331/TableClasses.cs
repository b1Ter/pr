using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr9_is12331
{
    [Table(Name = "Users")]
    public class User
    {

        [Column(IsPrimaryKey = true, IsDbGenerated = true,
            Name = "user_id", CanBeNull = false)]
        public int ID { get; set; }

        [Column(Name = "username", CanBeNull = false)]
        public string username { get; set; }

        [Column(CanBeNull = true, Name = "created")]
        public DateTime created { get; set; }

        [Column(Name = "phone", CanBeNull = true)]
        public string phone { get; set; }

        [Column(Name = "email", CanBeNull = false)]
        public string email { get; set; }

        [Column(Name = "first_name", CanBeNull = false)]
        public string firstName { get; set; }

        [Column(Name = "last_name", CanBeNull = false)]
        public string lastName { get; set; }

        //private EntitySet<OrderStatus> _Order;
        //[Association(Storage = "_Order", OtherKey = "customerIDs")]
        //public EntitySet<Order> Order
        //{
        //    get { return this.Order; }
        //    set { this.Order.Assign(value); }
        //}

    }

    [Table(Name = "Colors")]
    public class Color
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public string color_id;

        //private EntitySet<OrderStatus> _orderStatuses;
        //[Association(Storage = "_orderStatuses", OtherKey = "color_ids")]
        //public EntitySet<OrderStatus> orderStatuses
        //{
        //    get { return this.orderStatuses; }
        //    set { this.orderStatuses.Assign(value); }
        //}
    }

    [Table(Name = "OrderStatuses")]
    public class OrderStatus
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int status_id;
        [Column]
        public string status_name;
        [Column]
        public Color color_ids;

        //private EntityRef<Color> _color;
        //[Association(Storage = "_color", ThisKey = "color_id")]
        //public Color Color
        //{
        //    get { return this._color.Entity; }
        //    set { this._color.Entity = value; }
        //}

        //private EntitySet<Order> _order;
        //[Association(Storage = "_order", OtherKey = "status_ids")]
        //public EntitySet<Order> Orders
        //{
        //    get { return this._order; }
        //    set { this._order.Assign(value); }
        //}
    }

    [Table(Name = "Points")]
    public class Point
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int point_id;
        [Column]
        public string point_name;

        //private EntitySet<Order> _order;
        //[Association(Storage = "_order", OtherKey = "point_ids")]
        //public EntitySet<Order> Orders
        //{
        //    get { return this._order; }
        //    set { this._order.Assign(value); }
        //}
    }

    [Table(Name = "DeliveryServices")]
    public class DeliveryService
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int delivery_service_id;
        [Column]
        public string delivery_name;
        [Column]
        public string address;

        //private EntitySet<Order> _order;
        //[Association(Storage = "_order", OtherKey = "delivery_service_ids")]
        //public EntitySet<Order> Orders
        //{
        //    get { return this._order; }
        //    set { this._order.Assign(value); }
        //}
    }

    [Table(Name = "Orders")]
    public class Order
    {
        [Column(Name = "order_id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID;
        [Column(Name = "created")]
        public DateTime created;
        [Column(Name = "customer_ids")]
        public User customerIDs;
        [Column(Name = "point_ids")]
        public Point pointIDs;
        [Column(Name = "sum")]
        public double sum;
        [Column(Name = "status_ids")]
        public OrderStatus status_ids;
        [Column(Name = "delivery_service_ids")]
        public DeliveryService delivery_service_ids;

        private EntityRef<User> _User;
        [Association(Storage = "_User", ThisKey = "customerIDs")]
        public User User
        {
            get { return this._User.Entity; }
            set { this._User.Entity = value; }
        }

        //private EntityRef<OrderStatus> _orderStatus;
        //[Association(Storage = "_orderStatus", ThisKey = "status_id")]
        //public OrderStatus OrderStatus
        //{
        //    get { return this._orderStatus.Entity; }
        //    set { this._orderStatus.Entity = value; }
        //}

        //private EntityRef<Point> _point;
        //[Association(Storage = "_point", ThisKey = "point_id")]
        //public Point Point
        //{
        //    get { return this._point.Entity; }
        //    set { this._point.Entity = value; }
        //}

        //private EntitySet<ProductInOrder> _productInOrders;
        //[Association(Storage = "_productInOrders", OtherKey = "order_ids")]
        //public EntitySet<ProductInOrder> ProductInOrder
        //{
        //    get { return this._productInOrders; }
        //    set { this._productInOrders.Assign(value); }
        //}
    }

    [Table(Name = "Products")]
    public class Product
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int product_id;
        [Column]
        public string product_name;
        [Column]
        public double price;
        [Column]
        public string imgPath;

        //private EntitySet<ProductInOrder> _productInOrders;
        //[Association(Storage = "_productInOrders", OtherKey = "product_ids")]
        //public EntitySet<ProductInOrder> ProductInOrder
        //{
        //    get { return this._productInOrders; }
        //    set { this._productInOrders.Assign(value); }
        //}
    }

    [Table(Name = "ProductinOrders")]
    public class ProductInOrder
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id;
        [Column]
        public Order order_ids;
        [Column]
        public Product product_ids;

        //private EntityRef<Order> _order;
        //[Association(Storage = "_order", ThisKey = "order_id")]
        //public Order Order
        //{
        //    get { return this._order.Entity; }
        //    set { this._order.Entity = value; }
        //}

        //private EntityRef<Product> _product;
        //[Association(Storage = "_product", ThisKey = "product_id")]
        //public Product Product
        //{
        //    get { return this._product.Entity; }
        //    set { this._product.Entity = value; }
        //}
    }
}
