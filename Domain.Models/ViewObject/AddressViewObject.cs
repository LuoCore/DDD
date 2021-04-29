using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.ViewObject
{
    /// <summary>
    /// 地址
    /// </summary>

    public class AddressViewObject : ValueObjectBase<AddressViewObject>
    {

        protected override bool EqualsCore(AddressViewObject other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; private set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; private set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; private set; }


        public AddressViewObject() { }
        public AddressViewObject(string province, string city,
            string county, string street)
        {
            this.Province = province;
            this.City = city;
            this.County = county;
            this.Street = street;
        }

       
    }
}
